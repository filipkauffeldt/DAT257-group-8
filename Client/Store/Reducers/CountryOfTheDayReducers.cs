using API;
using Client.Store.Actions;
using Client.Store.States;
using Fluxor;

namespace Client.Store.Reducers
{
    public class CountryOfTheDayReducers
    {
        [ReducerMethod]
        public static CountryOfTheDayState HomeCountryDetectedSuccessfullyReducer(CountryOfTheDayState state, HomeCountryDetectedSuccessfullyAction action)
        {
            return state with
            {
                HomeCountry = action.Country,
                HomeCountryFound = true
            };
        }

        [ReducerMethod]
        public static CountryOfTheDayState CountryOfTheDayDetectedSuccessfullyReducer(CountryOfTheDayState state, CountryOfTheDayDetectedSuccessfullyAction action)
        {
            return state with
            {
                CountryOfTheDay = action.Country,
                CountryOfTheDayFound = true
            };
        }

        [ReducerMethod]
        public static CountryOfTheDayState SharedMetricsDetectedSuccessfullyReducer(CountryOfTheDayState state, SharedMetricsDetectedSuccessfullyAction action)
        {
            return state with
            {
                SharedMetrics = action.Metrics
            };
        }

        [ReducerMethod]
        public static CountryOfTheDayState ShownMetricsSelectedReducer(CountryOfTheDayState state, ShownMetricsSelectedAction action)
        {
            return state with
            {
                ShownMetrics = action.Metrics
            };
        }

        [ReducerMethod]
        public static CountryOfTheDayState CountryIdentifiersFetchedReducer(CountryOfTheDayState state, CountryIdentifiersFetchedAction action)
        {
            return state with
            {
                CountryIdentifiers = action.CountryIdentifiers
            };
        }
    }
}
