using API;
using Fluxor;

namespace Client.Store.States
{
    [FeatureState]
    public record class CountryOfTheDayState
    {
        public required Country CountryOfTheDay { get; init; }
        public required Country HomeCountry { get; init; }
        public required bool CountryOfTheDayFound { get; init; } = false;
        public required bool HomeCountryFound { get; init; } = false;

        public required IList<Country> CountryIdentifiers { get; init; }

        public required IList<string> SharedMetrics { get; init; } = new List<string>();
        public required IList<string> ShownMetrics { get; init; } = new List<string>();
    }
}
