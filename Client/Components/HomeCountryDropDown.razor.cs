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

        private string? HomeCountry = "Choose your home country";


        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        private async Task HomeCountryChange()
        {
            StateHasChanged();
            var HomeCountryCode = CountryCodeDict[HomeCountry];
            await CountryChanged.InvokeAsync(HomeCountryCode);
            StateHasChanged();
        }
    }
}
