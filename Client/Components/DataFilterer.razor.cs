using Microsoft.AspNetCore.Components;
using API;
using System.Linq;
using Radzen.Blazor;
using Client.API;
using System.Net.Http;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace Client.Components
{
    public partial class DataFilterer
    {
        [Parameter]
        public required IEnumerable<string> Data { get; set; }

        private IList<string> values;


        //[Parameter]
        //public required Country Country { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            values = Data.ToList();
        }
    }
}
