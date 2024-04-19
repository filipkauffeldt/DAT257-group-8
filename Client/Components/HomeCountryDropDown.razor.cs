using Microsoft.AspNetCore.Components;

namespace Client.Components
{
    public partial class HomeCountryDropDown
    {
        private List<String>? CountryNames { get; set; }

        private Dictionary<string, string> CountryCodeDict =
              new Dictionary<string, string>(){
                                  {"Sweden", "swe"},
                                  {"Norway", "no"},
                                  {"Denmark", "den"} };

        [Parameter]
        public EventCallback<String> CountryChanged { get; set; }

        private string? HomeCountry;


        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            CountryNames = new List<string> { "Sweden", "Norway", "Denmark" };
        }

        private async Task HomeCountryChange()
        {
            var HomeCountryCode = CountryCodeDict[HomeCountry];
            await CountryChanged.InvokeAsync(HomeCountryCode);
            StateHasChanged();
        }
    }
}
