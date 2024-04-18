using Microsoft.AspNetCore.Components;
using API;
using System.Linq;
using Radzen.Blazor;

namespace Client.Components
{
    public partial class DataFilterer
    {

        IEnumerable<Data> data;
        private Country countryCompTwo = new Country()
        {
            Name = "Bogusland",
            Code = "BO",
            Data = [
                new Data() { Name = "Water", Unit = "L", Points = [new DataPoint() { Date = new DateOnly(2023, 1, 1), Value = 200 }] },
                new Data() { Name = "Oil", Unit = "L", Points = [new DataPoint() { Date = new DateOnly(2023, 1, 1), Value = 200 }] },
                new Data() { Name = "Energy", Unit = "L", Points = [new DataPoint() { Date = new DateOnly(2023, 1, 1), Value = 200 }] }
                ]
        };

        protected override async Task OnInitializedAsync()
        {

            await base.OnInitializedAsync();

            //Country exampleCountry = await apiHandler.FetchCountry("SWE", httpClient);

            //var countryData = exampleCountry.Data;
            var countryData = countryCompTwo.Data;
            data = countryData;


        }

        private IList<string> PopulateData(Country country)
        {
            var countryData = countryCompTwo.Data;
            var dataNames = new List<string>();
            foreach (Data cd in countryData)
            {
                dataNames.Add(cd.Name);
            }
            return dataNames;
        }


    }
}
