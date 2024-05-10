using API;

namespace Client.Store.Actions
{
    public class OriginCountryChosenAction
    {
        public Country Country { get; }
        public OriginCountryChosenAction(Country country) => Country = country;
    };

    public class OriginCountryChoiceFailedAction { };

    public class ComparedCountryChosenAction
    {
        public Country Country { get; }
        public ComparedCountryChosenAction(Country country) => Country = country;
    };

    public class ComparedCountryChoiceFailedAction { };

    public class ComparedMetricsSelectedAction
    {
        public IList<string> Metrics { get; }
        public ComparedMetricsSelectedAction(IList<string> metrics) => Metrics = metrics;
    };
}