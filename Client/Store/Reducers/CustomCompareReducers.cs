using Client.Store.Actions;
using Client.Store.States;
using Fluxor;

namespace Client.Store.Reducers
{
    public class CustomCompareReducers
    {
        [ReducerMethod]
        public static CustomCompareState OriginCountryChosenReducer(CustomCompareState state, OriginCountryChosenAction action)
        {
            return state with
            {
                OriginCountry = action.Country
            };
        }

        [ReducerMethod]
        public static CustomCompareState ComparedCountryChosenReducer(CustomCompareState state, ComparedCountryChosenAction action)
        {
            return state with
            {
                ComparedCountry = action.Country
            };
        }

        [ReducerMethod]
        public static CustomCompareState SharedMetricsDetectedSuccessfullyReducer(CustomCompareState state, ComparedSharedMetricsChangedAction action)
        {
            return state with
            {
                SharedMetrics = action.Metrics
            };
        }

        [ReducerMethod]
        public static CustomCompareState ComparedMetricsSelectedReducer(CustomCompareState state, ComparedMetricsSelectedAction action)
        {
            return state with
            {
                ShownMetrics = action.Metrics
            };
        }

        [ReducerMethod]
        public static CustomCompareState CountryIdentifiersDetectedSuccessfullyReducer(CustomCompareState state, CountryIdentifiersFetchedAction action)
        {
            return state with
            {
                CountryIdentifiers = action.CountryIdentifiers
            };
        }

        [ReducerMethod]
        public static CustomCompareState UpdateOriginCountryReducer(CustomCompareState state, UpdateOriginCountryAction action)
        {
            return state with
            {
                OriginCountry = action.Country
            };
        }

        [ReducerMethod]
        public static CustomCompareState UpdateComparedCountryReducer(CustomCompareState state, UpdateComparedCountryAction action)
        {
            return state with
            {
                ComparedCountry = action.Country
            };
        }
    }
}