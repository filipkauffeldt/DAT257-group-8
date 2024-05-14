using API;

namespace Client.Store.Actions
{
    public class OriginCountryChosenAction
    {
        public Country Country { get; }
        public OriginCountryChosenAction(Country country) => Country = country;
    };

    public class ComparedCountryChosenAction
    {
        public Country Country { get; }
        public ComparedCountryChosenAction(Country country) => Country = country;

    };


    public class ComparedCountriesChosenAction
    {
        public IList<Country> Countries { get; }
        public ComparedCountriesChosenAction(IList<Country> countries) => Countries = countries;
    }

    public class InitComparedCountriesAction
    {
        public IList<Country> Countries { get; }
        public InitComparedCountriesAction(Country country) => Countries = [country];
    }

    public class ComparedSharedMetricsChangedAction
    {
        public IList<string> Metrics { get; }
        public ComparedSharedMetricsChangedAction(IList<string> metrics) => Metrics = metrics;
    };

    public class ComparedMetricsSelectedAction
    {
        public IList<string> Metrics { get; }
        public ComparedMetricsSelectedAction(IList<string> metrics) => Metrics = metrics;
    };

    public class UpdateOriginCountryAction
    {
        public Country Country { get; }
        public UpdateOriginCountryAction(Country country) => Country = country;
    };

    public class UpdateComparedCountryAction
    {
        public Country Country { get; }
        public UpdateComparedCountryAction(Country country) => Country = country;
    };

    public class UpdateComparedCountriesAction
    {
        public IList<Country> Countries { get; }

        public UpdateComparedCountriesAction(Country country) => Countries.Append(country);
    }
}