using API;
using Client.Store.States;
using Client.Store.Reducers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Store.Actions;

namespace TestClient.Store.Reducers
{
    public class CountryOfTheDayReducersTest
    {
        [Fact]
        public static void HomeCountryDetectedSuccessfullyReducer_ReturnsNewState()
        {
            var state = new CountryOfTheDayState
            {
                CountryOfTheDay = new Country
                {
                    Code = "a",
                    Name = "a"
                },
                HomeCountry = null,
                CountryOfTheDayFound = true,
                HomeCountryFound = false,
                SharedMetrics = null,
                ShownMetrics = null
            };

            var homeCountry = new Country
            {
                Code = "b",
                Name = "b"
            };

            var action = new HomeCountryDetectedSuccessfullyAction(homeCountry);

            var newState = CountryOfTheDayReducers.HomeCountryDetectedSuccessfullyReducer(state, action);

            Assert.NotEqual(state, newState);
            Assert.Equal(homeCountry, newState.HomeCountry);
            Assert.True(newState.HomeCountryFound);
            Assert.Equal(state.CountryOfTheDay, newState.CountryOfTheDay);
            Assert.Equal(state.CountryOfTheDayFound, newState.CountryOfTheDayFound);
            Assert.Equal(state.SharedMetrics, newState.SharedMetrics);
            Assert.Equal(state.ShownMetrics, newState.ShownMetrics);
        }

        [Fact]
        public static void SharedMetricsDetectedSuccessfullyReducer_ReturnsNewState()
        {
            var state = new CountryOfTheDayState
            {
                CountryOfTheDay = new Country
                {
                    Code = "a",
                    Name = "a"
                },
                HomeCountry = new Country
                {
                    Code = "b",
                    Name = "b"
                },
                CountryOfTheDayFound = true,
                HomeCountryFound = true,
                SharedMetrics = null,
                ShownMetrics = null
            };

            var sharedMetrics = new List<string> { "a", "b" };

            var action = new SharedMetricsDetectedSuccessfullyAction(sharedMetrics);

            var newState = CountryOfTheDayReducers.SharedMetricsDetectedSuccessfullyReducer(state, action);

            Assert.NotEqual(state, newState);
            Assert.Equal(sharedMetrics, newState.SharedMetrics);
            Assert.Equal(state.CountryOfTheDay, newState.CountryOfTheDay);
            Assert.Equal(state.CountryOfTheDayFound, newState.CountryOfTheDayFound);
            Assert.Equal(state.HomeCountry, newState.HomeCountry);
            Assert.Equal(state.HomeCountryFound, newState.HomeCountryFound);    
            Assert.Equal(state.ShownMetrics, newState.ShownMetrics);
        }

        [Fact]
        public static void ShownMetricsSelectedReducer_ReturnsNewState()
        {
            var state = new CountryOfTheDayState
            {
                CountryOfTheDay = new Country
                {
                    Code = "a",
                    Name = "a"
                },
                HomeCountry = new Country
                {
                    Code = "b",
                    Name = "b"
                },
                CountryOfTheDayFound = true,
                HomeCountryFound = true,
                SharedMetrics = new List<string> { "a", "b" },
                ShownMetrics = null
            };

            var shownMetrics = new List<string> { "a" };

            var action = new ShownMetricsSelectedAction(shownMetrics);

            var newState = CountryOfTheDayReducers.ShownMetricsSelectedReducer(state, action);

            Assert.NotEqual(state, newState);
            Assert.Equal(shownMetrics, newState.ShownMetrics);
            Assert.Equal(state.CountryOfTheDay, newState.CountryOfTheDay);
            Assert.Equal(state.CountryOfTheDayFound, newState.CountryOfTheDayFound);
            Assert.Equal(state.HomeCountry, newState.HomeCountry);
            Assert.Equal(state.HomeCountryFound, newState.HomeCountryFound);
            Assert.Equal(state.SharedMetrics, newState.SharedMetrics);
        }
    }
}
