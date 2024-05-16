using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API;
using Client.Store.States;
using Client.Store.Reducers;
using Xunit;
using Client.Store.Actions;

namespace TestClient.Store.Reducers
{
    public class CustomCompareReducersTest
    {
        [Fact]
        public void OriginCountryChosenReducer_ShouldUpdateOriginCountry()
        {
            // Arrange
            var state = new CustomCompareState
            {
                OriginCountry = null,
                ComparedCountries = null,
                SharedMetrics = null,
                ShownMetrics = null,
                CountryIdentifiers = null
            };
            var country = new Country
            {
                Code = "CountryCode",
                Name = "CountryName"
            };
            var action = new OriginCountryChosenAction(country);

            // Act
            var newState = CustomCompareReducers.OriginCountryChosenReducer(state, action);

            // Assert
            Assert.Equal(country, newState.OriginCountry);
        }

        [Fact]
        public void ComparedCountriesChosenReducer_ShouldUpdateComparedCountries()
        {
            // Arrange
            var state = new CustomCompareState
            {
                OriginCountry = null,
                ComparedCountries = null,
                SharedMetrics = null,
                ShownMetrics = null,
                CountryIdentifiers = null
            };

            var country = new Country
            {
                Code = "CountryCode",
                Name = "CountryName"
            };

            var country2 = new Country
            {
                Code = "CountryCode2",
                Name = "CountryName2"
            };

            var countries = new List<Country> { country, country2 };

            var action = new ComparedCountriesChosenAction(countries);

            // Act
            var newState = CustomCompareReducers.ComparedCountriesChosenReducer(state, action);

            // Assert
            Assert.Equal(countries, newState.ComparedCountries);
        }

        [Fact]
        public void SharedMetricsDetectedSuccessfullyReducer_ShouldUpdateSharedMetrics()
        {
            // Arrange
            var state = new CustomCompareState
            {
                OriginCountry = null,
                ComparedCountries = null,
                SharedMetrics = null,
                ShownMetrics = null,
                CountryIdentifiers = null
            };

            var metrics = new List<string> { "Metric1", "Metric2" };

            var action = new ComparedSharedMetricsChangedAction(metrics);

            // Act
            var newState = CustomCompareReducers.SharedMetricsDetectedSuccessfullyReducer(state, action);

            // Assert
            Assert.Equal(metrics, newState.SharedMetrics);
        }

        [Fact]
        public void ComparedMetricsSelectedReducer_ShouldUpdateShownMetrics()
        {
            // Arrange
            var state = new CustomCompareState
            {
                OriginCountry = null,
                ComparedCountries = null,
                SharedMetrics = null,
                ShownMetrics = null,
                CountryIdentifiers = null
            };

            var metrics = new List<string> { "Metric1", "Metric2" };

            var action = new ComparedMetricsSelectedAction(metrics);

            // Act
            var newState = CustomCompareReducers.ComparedMetricsSelectedReducer(state, action);

            // Assert
            Assert.Equal(metrics, newState.ShownMetrics);
        }

        [Fact]
        public void CountryIdentifiersDetectedSuccessfullyReducer_ShouldUpdateCountryIdentifiers()
        {
            // Arrange
            var state = new CustomCompareState
            {
                OriginCountry = null,
                ComparedCountries = null,
                SharedMetrics = null,
                ShownMetrics = null,
                CountryIdentifiers = null
            };

            var countries = new List<Country>
            {
                new Country { Code = "Country1", Name = "Country1" },
                new Country { Code = "Country2", Name = "Country2" }
            };

            var action = new CountryIdentifiersFetchedAction(countries);

            // Act
            var newState = CustomCompareReducers.CountryIdentifiersDetectedSuccessfullyReducer(state, action);

            // Assert
            Assert.Equal(countries, newState.CountryIdentifiers);
        }
    }
}
