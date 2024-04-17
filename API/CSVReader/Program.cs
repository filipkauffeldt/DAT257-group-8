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
            var path = new ZlpDirectoryInfo(@""); // Add path to file with countries
            var basePath = new ZlpDirectoryInfo(@""); // Add path to directory with files containing data

            var countryFile = path.GetFiles(SearchOption.TopDirectoryOnly);
            var countries = CSVReader.ReadCountries(countryFile.First().ToString());

            var dataFiles = basePath.GetFiles(SearchOption.AllDirectories).ToList();

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
