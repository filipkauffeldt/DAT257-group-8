using Microsoft.AspNetCore.Components;
using API;
using System.Linq;
using Radzen.Blazor;
using System.Net.Http;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace Client.Components
{
    public partial class CountryFilterer
    {
        [Parameter]
        public required IEnumerable<Country> AllCountries { get; set; }

        [Parameter]
        public IEnumerable<Country> DefaultSelectedCountries { get; set; }

        [Parameter]
        public EventCallback<IEnumerable<Country>> OnChange { get; set; }

        [Parameter]
        public DateOnly Date { get; set; }

        private IEnumerable<string>? _countryNames;

        private bool _initialized = false;

        private IList<string>? _selectedCountries;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            if (!_initialized)
            {
                _selectedCountries = DefaultSelectedCountries?.Select(d => d.Name).ToList() ?? [];
                _countryNames = AllCountries.Select(d => d.Name).ToList();
                _initialized = true;
            }
        }

        private async Task OnValuesChanged(IEnumerable<string> selectedValues)
        {
            var selectedCountries = new List<Country>();
            foreach(var value in selectedValues)
            {
                selectedCountries.Add(AllCountries.FirstOrDefault(d => d.Name == value));
            }
            
            await OnChange.InvokeAsync(selectedCountries);
        }
    }
}
