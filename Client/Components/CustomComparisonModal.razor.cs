using API;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace Client.Components
{

    public partial class CustomComparisonModal
    {
        [Parameter]
        public required ComparisonComponent ComparisonComponent { get; set; }

        [Parameter]
        public required string ResourceDescription { get; set; }

        private Country _countryOrigin;
        private Country _countryComparison;
        private string _resourceDescription;
        private string _unit;
        private DateOnly _date;

        protected override async Task OnInitializedAsync()
        {
            _countryOrigin = ComparisonComponent.CountryOrigin;
            _countryComparison = ComparisonComponent.CountryComparison;
            _resourceDescription = ResourceDescription;
            _date = ComparisonComponent.date;
            _unit = ComparisonComponent.Unit;
            await base.OnInitializedAsync();

        }
    }
}
