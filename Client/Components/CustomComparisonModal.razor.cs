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
        private Country _countryComparison;
        private string _resourceType;
        private DateOnly _date;

        protected override async Task OnInitializedAsync()
        {
            _countryOrigin = ComparisonComponent.CountryOrigin;
            _countryComparison = ComparisonComponent.CountryComparison;
            _resourceType = ComparisonComponent.ResourceType;
            _date = ComparisonComponent.date;
            await base.OnInitializedAsync();

        }
    }
}
