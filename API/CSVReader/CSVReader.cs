using System.Globalization;
using System.Text.RegularExpressions;
using API.Model.ObjectModels;

namespace CSVReader
{
    public class CSVReader
    {
        public static List<Country> ReadCountries(string path)
        {
            List<Country> countries = new List<Country>();

            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
 
                        var values = Regex.Split(line, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");

                        values[1] = values[1].Replace("\"", string.Empty);

                        var country = new Country()
                        {
                            Code = values[0],
                            Name = values[1],
                            Continent = values[2],
                            Data = new List<Data>()
                        };
                        countries.Add(country);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading CSV file: " + ex.Message);
            }

            return countries;
        }
        
        public static List<Country> AddData(List<Country> countries, string path)
        {
            var dataMap = new Dictionary<string, Data>();

            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    while (!reader.EndOfStream)
                    {
                        try
                        {
                            string line = reader.ReadLine();

                            var values = Regex.Split(line, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");

                            values[0] = values[0].Replace("\"", string.Empty);
                            values[1] = values[1].Replace("\"", string.Empty);
                            values[2] = values[2].Replace("\"", string.Empty);
                            values[3] = values[3].Replace("\"", string.Empty);
                            values[4] = values[4].Replace("\"", string.Empty);

                            var dataName = values[0];
                        
                            var countryName = values[1];
                            var unit = values[2];
                            var year = values[3];
                            var value = double.Parse(values[4], CultureInfo.InvariantCulture);

                            Console.WriteLine($"Add data for {countryName}");

                            if (!dataMap.ContainsKey(countryName))
                            {
                                var newData = new Data()
                                { 
                                    Name = dataName,
                                    Unit = unit,
                                    Points = new List<DataPoint>()
                                };

                                dataMap.Add(countryName, newData);
                            }

                            var dp = new DataPoint()
                            {
                                Date = new DateOnly(int.Parse(year), 1, 1),
                                Value = value
                            };

                            var countryData = dataMap[countryName];
                            var points = countryData.Points;
                            points.Add(dp);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Error adding data: {e.Message}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding data: {ex.Message}");
            }

            foreach ((var n, var d) in dataMap)
            {
                Console.WriteLine($"Country: {n}, Data: {d.Name}");
                countries.Where(c => c.Name == n)
                    .ToList()
                    .ForEach(c => c.Data.Add(d));
            }

            return countries;
        }
    }
}