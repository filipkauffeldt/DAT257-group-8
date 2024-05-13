using API;

namespace Client.Store.Actions
{
    public class HomeCountryDetectedSuccessfullyAction 
    {
        public Country Country { get; }
        public HomeCountryDetectedSuccessfullyAction(Country country) => Country = country;
    };
    public class HomeCountryDetectedFailedAction { };
    public class CountryOfTheDayDetectedSuccessfullyAction 
    {
        public Country Country { get; }
        public CountryOfTheDayDetectedSuccessfullyAction(Country country) => Country = country;
    };
    public class CountryOfTheDayDetectedFailedAction { };
    public class SharedMetricsDetectedSuccessfullyAction 
    {
        public IList<string> Metrics { get; }
        public SharedMetricsDetectedSuccessfullyAction(IList<string> metrics) => Metrics = metrics;
    };
    public class SharedMetricsDetectedFailedAction { };
    public class ShownMetricsSelectedAction 
    { 
        public IList<string> Metrics { get; }
        public ShownMetricsSelectedAction(IList<string> metrics) => Metrics = metrics;
    };

}
