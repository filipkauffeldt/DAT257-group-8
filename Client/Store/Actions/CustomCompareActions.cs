using API;

namespace Client.Store.Actions
{
    public class OriginCountryChosenAction
    {
        public Country Country { get; }
        public OriginCountryChosenAction(Country country) => Country = country;
    };


    public class ComparedCountriesChosenAction
    {
        public IList<Country> Countries { get; }
        public ComparedCountriesChosenAction(IList<Country> countries) => Countries = countries;
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

    public class UpdateComparedCountriesAction
    {
        public IList<Country> Countries { get; }

        public UpdateComparedCountriesAction(IList<Country> countries) => Countries = countries;
    };

    public class UpdateYearAction
    {
        public DateOnly Year { get; }

        public UpdateYearAction(DateOnly year)
        {
            this.Year = year;
        }
    }
}