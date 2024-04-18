using API.Model;
using API.Model.ObjectModels;
using ZetaLongPaths;

namespace CSVReader
{
    class Program
    {
        static void Main(string[] args)
        {
            var repository = new CountryRepository(new CountryDbContext());
            var pathToCountryFile = new ZlpDirectoryInfo(@""); // Add path to csv file with countries, eg allcountries.csv
            var basePathForData = new ZlpDirectoryInfo(@""); // Add path to directory with csv files containing data, eg /Data

            var countryFile = pathToCountryFile.GetFiles(SearchOption.TopDirectoryOnly);
            var countries = CSVReader.ReadCountries(countryFile.First().ToString());

            var dataFiles = basePathForData.GetFiles(SearchOption.AllDirectories).ToList();

            foreach (var df in dataFiles) 
            {
                Console.WriteLine($"Opening: {df.ToString()}");
                try
                { 
                    countries = CSVReader.AddData(countries, df.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            foreach (var country in countries)
            {
                Console.WriteLine($"{country.Code} {country.Name} {country.Continent}");

                repository.AddCountry(country);
            }
        }
    }
}
