using Microsoft.AspNetCore.Components;
using API;
using System.Linq;
using Radzen.Blazor;
using Client.API;
using System.Net.Http;

namespace Client.Components
{
    public partial class DataFilterer
    {

        IEnumerable<Data> data;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            var countryData = await apiHandler.FetchCountryOfTheDay(httpClient);
            data = countryData.Data;
        }

    }
}
