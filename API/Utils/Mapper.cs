namespace API.Utils
{
    using API.Contracts;
    using API.Model.ObjectModels;
    using System.Collections.ObjectModel;

    public static class Mapper
    {
        public static CountryContract MapCountry(Country country)
        {
            return new CountryContract
            {
                Code = country.Code,
                Name = country.Name,
                Continent = country.Continent,
                Description = country.Description,
                Data = MapDataCollection(country.Data)
            };
        }

        public static Collection<CountryContract> MapCountryCollection(Collection<Country> countries)
        {
            var countryContracts = new Collection<CountryContract>();

            foreach (Country c in countries)
            {
                countryContracts.Add(MapCountry(c));
            }

            return countryContracts;
        }

        public static Collection<CountryContract> MapCountryCollection(IEnumerable<Country> countries)
        {
            var countryContracts = new Collection<CountryContract>();

            foreach (Country c in countries)
            {
                countryContracts.Add(MapCountry(c));
            }

            return countryContracts;
        }

        public static List<CountryContract> MapCountryList(IEnumerable<Country> countries)
        {
            var countryContracts = new List<CountryContract>();

            foreach (Country c in countries)
            {
                countryContracts.Add(MapCountry(c));
            }

            return countryContracts;
        }

        public static Collection<DataContract> MapDataCollection(Collection<Data> data)
        {
            var dataContracts = new Collection<DataContract>();

            foreach (Data d in data)
            {
                dataContracts.Add(MapData(d));
            }

            return dataContracts;
        }

        public static Collection<DataContract> MapDataCollection(IEnumerable<Data> data)
        {
            var dataContracts = new Collection<DataContract>();

            foreach (Data d in data)
            {
                dataContracts.Add(MapData(d));
            }

            return dataContracts;
        }

        public static List<DataContract> MapDataList(IEnumerable<Data> data)
        {
            var dataContracts = new List<DataContract>();

            foreach (Data d in data)
            {
                dataContracts.Add(MapData(d));
            }

            return dataContracts;
        }

        public static DataContract MapData(Data data)
        {
            return new DataContract
            {
                Name = data.Name,
                Description = data.Description,
                Unit = data.Unit,
                Points = MapDataPointCollection(data.Points)
            };
        }

        public static Collection<DataPointContract> MapDataPointCollection(Collection<DataPoint> dataPoints)
        {
            var dataPointContracts = new Collection<DataPointContract>();

            foreach (DataPoint dp in dataPoints)
            {
                dataPointContracts.Add(MapDataPoint(dp));
            }

            return dataPointContracts;
        }

        public static Collection<DataPointContract> MapDataPointCollection(IEnumerable<DataPoint> dataPoints)
        {
            var dataPointContracts = new Collection<DataPointContract>();

            foreach (DataPoint dp in dataPoints)
            {
                dataPointContracts.Add(MapDataPoint(dp));
            }

            return dataPointContracts;
        }

        public static DataPointContract MapDataPoint(DataPoint dataPoint)
        {
            return new DataPointContract
            {
                Date = dataPoint.Date,
                Value = dataPoint.Value
            };
        }
    }
}