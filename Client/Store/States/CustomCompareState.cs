using API;
using Fluxor;

namespace Client.Store.States
{
    [FeatureState]
    public record class CustomCompareState
    {
        public required Country OriginCountry { get; init; }
        public required Country ComparedCountry { get; init; }
        public required IList<string> SharedMetrics { get; init; } = new List<string>();
        public required IList<string> ShownMetrics { get; init; } = new List<string>();

        public required IList<Country> CountryIdentifiers { get; init; }
    }
}
