using API;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace Client.Components
{

    public partial class CustomComparisonModal
    {
        [Parameter]
        public required IList<Country> ComparedCountries { get; set; }
        [Parameter]
        public required Country OriginCountry { get; set; }
        [Parameter]
        public required string ResourceType { get; set; }
        [Parameter]
        public required DateOnly Date { get; set; }

        private Country _countryOrigin;
        private IList<Country> _countryComparison;
        private string _resourceDescription;
        private DateOnly _date;

        protected override async Task OnInitializedAsync()
        {
            _countryOrigin = OriginCountry;
            _countryComparison = ComparedCountries;
            _resourceDescription = ResourceType;
           _date = Date;
            await base.OnInitializedAsync();

        }
    }
}
