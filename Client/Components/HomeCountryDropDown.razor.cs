using Microsoft.AspNetCore.Components;

namespace Client.Components
{
    public partial class HomeCountryDropDown
    {
        [Parameter]
        public List<String> CountryNames { get; set; }

        [Parameter]
        public Dictionary<string, string> CountryCodeDict { get; set; }

        [Parameter]
        public EventCallback<String> CountryChanged { get; set; }

        private string? _HomeCountry = "Choose your home country";


        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        private async Task HomeCountryChange()
        {
            StateHasChanged();
            var homeCountryCode = CountryCodeDict[_HomeCountry];
            await CountryChanged.InvokeAsync(homeCountryCode);
            StateHasChanged();
        }
    }
}
