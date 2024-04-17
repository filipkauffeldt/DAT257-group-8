using API.Model;
using API.Model.ObjectModels;

namespace CSVReader
{
    class Program
    {
        static void Main(string[] args)
        {
            var repository = new CountryRepository(new CountryDbContext());
            var path = "";
            List<Country> countries = CSVReader.ReadCountries(path);

            foreach (var country in countries)
            {
                Console.WriteLine(country.Code + " " + country.Name + " " + country.Continent);

                repository.AddCountry(country);
            }
        }
    }
}
