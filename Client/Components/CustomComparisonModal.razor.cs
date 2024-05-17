using API;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace Client.Components
{

    public partial class CustomComparisonModal
    {
        [Parameter]
        public required ComparisonComponent ComparisonComponent { get; set; }

        private Country _countryOrigin;
        private IList<Country> _countryComparison;
        private string _resourceDescription;
        private string _unit;
        private DateOnly _date;

        protected override async Task OnInitializedAsync()
        {
            _countryOrigin = ComparisonComponent.OriginCountry;
            _countryComparison = ComparisonComponent.ComparedCountries;
            _resourceDescription = ComparisonComponent.ResourceType;
           _date = ComparisonComponent.Date;
            await base.OnInitializedAsync();

        }
    }
}
