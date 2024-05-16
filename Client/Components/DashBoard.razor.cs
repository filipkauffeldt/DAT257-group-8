using API;
using Client.Store.Actions;
using Client.Store.States;
using Fluxor;
using Microsoft.AspNetCore.Components;

namespace Client.Components
{
    public partial class DashBoard
    {
        private HomeCountryDropDown dropDown;
        private Dictionary<string, ComparisonComponent> compComp = new();
        private Dictionary<string, string> countryCodeDict = new();
        private List<string> countryNames;

        [Inject]
        private HttpClient _httpClient { get; set; }

        [Inject]
        private IApiHandler _apiHandler { get; set; }

        [Inject]
        private IDispatcher Dispatcher { get; set; }

        [Inject]
        private IState<CountryOfTheDayState> State { get; set; } 
        
        private DateOnly _date = new DateOnly(2022, 1, 1);

        private IList<Country> Countries = new List<Country>();

        private bool _initialized = false;
        private bool _homeCountryError = false;

        protected override async Task OnInitializedAsync()
        {
            await InitCountryIdentifiersAsync();
            await InitHomeCountryAsync();
            await InitCountryOfTheDayAsync();
            InitSharedMetrics();
            Countries.Add(State.Value.HomeCountry);
            _initialized = true;
        }

        private async void HomeCountryChange(string CountryCode)
        {
            var country = await _apiHandler.FetchCountryByYearAsync(_httpClient, CountryCode, _date);
            Dispatcher.Dispatch(new HomeCountryDetectedSuccessfullyAction(country));
            StateHasChanged();
            foreach (var cC in compComp.Values)
            {
               cC.LoadValues();
            }
            StateHasChanged();
        }

        private void InitSharedMetrics()
        {
            if (State.Value.SharedMetrics.Count > 0) return;

            if (!State.Value.HomeCountryFound || !State.Value.CountryOfTheDayFound || State.Value.HomeCountry.Data == null || State.Value.CountryOfTheDay.Data == null)
            {
                Dispatcher.Dispatch(new SharedMetricsDetectedFailedAction());
                Dispatcher.Dispatch(new ShownMetricsSelectedAction(new List<string>()));
            }

            var sharedMetrics = new List<string>();
            foreach (var metric in State.Value.HomeCountry.Data!.Select(d => d.Name).ToList())
            {
                var countryCompDataExists = State.Value.HomeCountry.Data?.Any(d => d.Name == metric && d.Points.Any(e => e.Date.Year == _date.Year)) ?? false;
                var countryCompTwoDataExists = State.Value.CountryOfTheDay.Data?.Any(d => d.Name == metric && d.Points.Any(e => e.Date.Year == _date.Year)) ?? false;

                if (countryCompDataExists && countryCompTwoDataExists)
                {
                    sharedMetrics.Add(metric);
                }
            }

            Dispatcher.Dispatch(new SharedMetricsDetectedSuccessfullyAction(sharedMetrics));
            Dispatcher.Dispatch(new ShownMetricsSelectedAction(sharedMetrics));
        }

        private static string FormatLabel(string label)
        {
            return $"{label.Replace(" ", "-")}-statistic";
        }

        private void HandleFilterChange(IEnumerable<string> selectedValues)
        {
            var selectedMetrics = selectedValues.ToList();

            selectedMetrics = selectedMetrics.OrderBy(d => State.Value.SharedMetrics.IndexOf(d)).ToList();

            Dispatcher.Dispatch(new ShownMetricsSelectedAction(selectedMetrics));
        }

        private async Task InitHomeCountryAsync()
        {
            if (State.Value.HomeCountry != null) return;

            try
            {
                var country = await _apiHandler.FetchHomeCountryAsync(_httpClient);
                   
                if (country != null)
                {
                    Dispatcher.Dispatch(new HomeCountryDetectedSuccessfullyAction(country));
                }
                else
                {
                    _homeCountryError = true;
                    Dispatcher.Dispatch(new HomeCountryDetectedFailedAction());
                }
            } catch (Exception ex)
            {
                _homeCountryError = true;
                Console.Out.WriteLineAsync(ex.Message);
                Dispatcher.Dispatch(new HomeCountryDetectedFailedAction());
            }
        }

        private async Task InitCountryOfTheDayAsync()
        {
            if (State.Value.CountryOfTheDay != null) return;
            try
            {
                var countryOfTheDay = await _apiHandler.FetchCountryOfTheDayAsync(_httpClient);

                if (countryOfTheDay != null)
                {
                    Dispatcher.Dispatch(new CountryOfTheDayDetectedSuccessfullyAction(countryOfTheDay));
                } else
                {
                    Dispatcher.Dispatch(new CountryOfTheDayDetectedFailedAction());
                }
            } catch(Exception ex)
            {
                Console.Out.WriteLineAsync(ex.Message);
                Dispatcher.Dispatch(new CountryOfTheDayDetectedFailedAction());
            }
        }

        private async Task InitCountryIdentifiersAsync()
        {
            if (State.Value.CountryIdentifiers == null) {
                var countryIdentifiers = await _apiHandler.FetchAllCountryIdentifiersAsync(_httpClient);
                Dispatcher.Dispatch(new CountryIdentifiersFetchedAction(countryIdentifiers.ToList()));
            }
            
            foreach(var country in State.Value.CountryIdentifiers)
            {
                countryCodeDict.Add(country.Name, country.Code);
            }
            countryNames = countryCodeDict.Keys.ToList();
        }
    }
}