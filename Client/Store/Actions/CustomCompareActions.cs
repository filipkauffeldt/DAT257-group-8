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

    public class CustomSharedMetricsDetectedSuccessfullyAction
    {
        public IList<string> Metrics { get; }
        public CustomSharedMetricsDetectedSuccessfullyAction(IList<string> metrics) => Metrics = metrics;
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
}