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

        [Parameter]
        public IEnumerable<string> DefaultSelectedValues { get; set; }

        [Parameter]
        public EventCallback<IEnumerable<string>> OnChange { get; set; }

        private IEnumerable<string> _data;

        private bool _initialized = false;

        private IList<string> _values;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            if (!_initialized)
            {
                _values = DefaultSelectedValues?.ToList() ?? Data.ToList();
                _data = Data;
                _initialized = true;
            }
        }

        private async Task OnValueChanged(IEnumerable<string> selectedValues)
        {
            await OnChange.InvokeAsync(selectedValues);
        }

    }
}
