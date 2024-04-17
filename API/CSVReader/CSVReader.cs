using System;
using System.Collections.Generic;
using System.IO;
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
                        string[] values = line.Split(',');

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
    }
}