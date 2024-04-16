using API.Model.ObjectModels;
using API.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestApi.Utils
{
    public class MapperTest
    {
        [Fact]
        public static void MapCountry()
        {
            var country = new Country
            {
                Code = "US",
                Name = "United States",
                Continent = "North America",
                Description = "United States of America",
                Data = new List<Data>
                {
                    new Data
                    {
                        Name = "Population",
                        Unit = "people",
                        Points = new List<DataPoint>
                        {
                            new DataPoint
                            {
                                Date = DateOnly.FromDateTime(DateTime.Today),
                                Value = 308745538
                            },
                        }
                    },
                }
            };
            
            var countryContract = Mapper.MapCountry(country);

            Assert.Equal(country.Code, countryContract.Code);
            Assert.Equal(country.Name, countryContract.Name);
            Assert.Equal(country.Continent, countryContract.Continent);
            Assert.Equal(country.Description, countryContract.Description);
            Assert.Equal(country.Data.First().Name, countryContract.Data.First().Name);
            Assert.Equal(country.Data.First().Unit, countryContract.Data.First().Unit);
            Assert.Equal(country.Data.First().Points.First().Date, countryContract.Data.First().Points.First().Date);
            Assert.Equal(country.Data.First().Points.First().Value, countryContract.Data.First().Points.First().Value);
        }

        [Fact]
        public static void MapCountryCollection()
        {
            var countries = new List<Country>
            {
                new Country
                {
                    Code = "US",
                    Name = "United States",
                    Continent = "North America",
                    Description = "United States of America",
                    Data = new List<Data>
                    {
                        new Data
                        {
                            Name = "Population",
                            Unit = "people",
                            Points = new List<DataPoint>
                            {
                                new DataPoint
                                {
                                    Date = DateOnly.FromDateTime(DateTime.Today),
                                    Value = 308745538
                                },
                            }
                        },
                    }
                },
                new Country
                {
                    Code = "CA",
                    Name = "Canada",
                    Continent = "North America",
                    Description = "Canada",
                    Data = new List<Data>
                    {
                        new Data
                        {
                            Name = "Population",
                            Unit = "people",
                            Points = new List<DataPoint>
                            {
                                new DataPoint
                                {
                                    Date = DateOnly.FromDateTime(DateTime.Today),
                                    Value = 37742154
                                },
                            }
                        },
                    }
                }
            };

            var countryContracts = Mapper.MapCountryCollection(countries);

            Assert.Equal(countries.Count, countryContracts.Count);

            for (int i = 0; i < countries.Count; i++)
            {
                Assert.Equal(countries[i].Code, countryContracts[i].Code);
                Assert.Equal(countries[i].Name, countryContracts[i].Name);
                Assert.Equal(countries[i].Continent, countryContracts[i].Continent);
                Assert.Equal(countries[i].Description, countryContracts[i].Description);
                Assert.Equal(countries[i].Data.First().Name, countryContracts[i].Data.First().Name);
                Assert.Equal(countries[i].Data.First().Unit, countryContracts[i].Data.First().Unit);
                Assert.Equal(countries[i].Data.First().Points.First().Date, countryContracts[i].Data.First().Points.First().Date);
                Assert.Equal(countries[i].Data.First().Points.First().Value, countryContracts[i].Data.First().Points.First().Value);
            }
        }

        [Fact]
        public static void MapCountryCollection_Null_ReturnsEmptyCollection()
        {
            Collection<Country> countries = null;

            var actual = Mapper.MapCountryCollection(countries);

            Assert.Empty(actual);
        }

        [Fact]
        public static void MapCountryList()
        {
            var countries = new List<Country>
            {
                new Country
                {
                    Code = "US",
                    Name = "United States",
                    Continent = "North America",
                    Description = "United States of America",
                    Data = new List<Data>
                    {
                        new Data
                        {
                            Name = "Population",
                            Unit = "people",
                            Points = new List<DataPoint>
                            {
                                new DataPoint
                                {
                                    Date = DateOnly.FromDateTime(DateTime.Today),
                                    Value = 308745538
                                },
                            }
                        },
                    }
                },
                new Country
                {
                    Code = "CA",
                    Name = "Canada",
                    Continent = "North America",
                    Description = "Canada",
                    Data = new List<Data>
                    {
                        new Data
                        {
                            Name = "Population",
                            Unit = "people",
                            Points = new List<DataPoint>
                            {
                                new DataPoint
                                {
                                    Date = DateOnly.FromDateTime(DateTime.Today),
                                    Value = 37742154
                                },
                            }
                        },
                    }
                }
            };

            var countryContracts = Mapper.MapCountryList(countries);

            Assert.Equal(countries.Count, countryContracts.Count);

            for (int i = 0; i < countries.Count; i++)
            {
                Assert.Equal(countries[i].Code, countryContracts[i].Code);
                Assert.Equal(countries[i].Name, countryContracts[i].Name);
                Assert.Equal(countries[i].Continent, countryContracts[i].Continent);
                Assert.Equal(countries[i].Description, countryContracts[i].Description);
                Assert.Equal(countries[i].Data.First().Name, countryContracts[i].Data.First().Name);
                Assert.Equal(countries[i].Data.First().Unit, countryContracts[i].Data.First().Unit);
                Assert.Equal(countries[i].Data.First().Points.First().Date, countryContracts[i].Data.First().Points.First().Date);
                Assert.Equal(countries[i].Data.First().Points.First().Value, countryContracts[i].Data.First().Points.First().Value);
            }
        }

        [Fact]
        public static void MapCountryList_Null_ReturnsEmptyList()
        {
            List<Country> list = null;

            var actual = Mapper.MapCountryList(list);

            Assert.Empty(actual);
        }

        [Fact]
        public static void MapDataCollection()
        {
            var data = new List<Data>
            {
                new Data
                {
                    Name = "Population",
                    Unit = "people",
                    Points = new List<DataPoint>
                    {
                        new DataPoint
                        {
                            Date = DateOnly.FromDateTime(DateTime.Today),
                            Value = 308745538
                        },
                    }
                },
                new Data
                {
                    Name = "GDP",
                    Unit = "USD",
                    Points = new List<DataPoint>
                    {
                        new DataPoint
                        {
                            Date = DateOnly.FromDateTime(DateTime.Today),
                            Value = 21_433_226_000_000
                        },
                    }
                }
            };

            var dataContracts = Mapper.MapDataCollection(data);

            Assert.Equal(data.Count, dataContracts.Count);

            for (int i = 0; i < data.Count; i++)
            {
                Assert.Equal(data[i].Name, dataContracts[i].Name);
                Assert.Equal(data[i].Unit, dataContracts[i].Unit);
                Assert.Equal(data[i].Points.First().Date, dataContracts[i].Points.First().Date);
                Assert.Equal(data[i].Points.First().Value, dataContracts[i].Points.First().Value);
            }
        }

        [Fact]
        public static void MapDataCollection_Null_ReturnsEmtyCollection()
        {
            Collection<Data> data = null;

            var actual = Mapper.MapDataCollection(data);

            Assert.Empty(actual);
        }

        [Fact]
        public void MapDataList()
        {
            var data = new List<Data>
            {
                new Data
                {
                    Name = "Population",
                    Unit = "people",
                    Points = new List<DataPoint>
                    {
                        new DataPoint
                        {
                            Date = DateOnly.FromDateTime(DateTime.Today),
                            Value = 308745538
                        },
                    }
                },
                new Data
                {
                    Name = "GDP",
                    Unit = "USD",
                    Points = new List<DataPoint>
                    {
                        new DataPoint
                        {
                            Date = DateOnly.FromDateTime(DateTime.Today),
                            Value = 21_433_226_000_000
                        },
                    }
                }
            };

            var dataContracts = Mapper.MapDataList(data);

            Assert.Equal(data.Count, dataContracts.Count);

            for (int i = 0; i < data.Count; i++)
            {
                Assert.Equal(data[i].Name, dataContracts[i].Name);
                Assert.Equal(data[i].Unit, dataContracts[i].Unit);
                Assert.Equal(data[i].Points.First().Date, dataContracts[i].Points.First().Date);
                Assert.Equal(data[i].Points.First().Value, dataContracts[i].Points.First().Value);
            }
        }

        [Fact]
        public static void MapDataList_Null_ReturnsEmptyList()
        {
            List<Data> data = null;

            var actual = Mapper.MapDataList(data);

            Assert.Empty(actual);
        }

        [Fact]
        public static void MapData()
        {
            var data = new Data
            {
                Name = "Population",
                Unit = "people",
                Points = new List<DataPoint>
                {
                    new DataPoint
                    {
                        Date = DateOnly.FromDateTime(DateTime.Today),
                        Value = 308745538
                    },
                }
            };

            var dataContract = Mapper.MapData(data);

            Assert.Equal(data.Name, dataContract.Name);
            Assert.Equal(data.Unit, dataContract.Unit);
            Assert.Equal(data.Points.First().Date, dataContract.Points.First().Date);
            Assert.Equal(data.Points.First().Value, dataContract.Points.First().Value);
        }

        [Fact]
        public static void MapDataPointCollection()
        {
            var dataPoints = new List<DataPoint>
            {
                new DataPoint
                {
                    Date = DateOnly.FromDateTime(DateTime.Today),
                    Value = 308745538
                },
                new DataPoint
                {
                    Date = DateOnly.FromDateTime(DateTime.Today),
                    Value = 21_433_226_000_000
                }
            };

            var dataPointContracts = Mapper.MapDataPointCollection(dataPoints);

            Assert.Equal(dataPoints.Count, dataPointContracts.Count);

            for (int i = 0; i < dataPoints.Count; i++)
            {
                Assert.Equal(dataPoints[i].Date, dataPointContracts[i].Date);
                Assert.Equal(dataPoints[i].Value, dataPointContracts[i].Value);
            }
        }

        [Fact]
        public static void MapDataPointCollection_Null_ReturnsEmptyCollection()
        {
            Collection<DataPoint> dataPoints = null;

            var actual = Mapper.MapDataPointCollection(dataPoints);

            Assert.Empty(actual);
        }

        [Fact]
        public static void MapDataPoint()
        {
            var dataPoint = new DataPoint
            {
                Date = DateOnly.FromDateTime(DateTime.Today),
                Value = 308745538
            };

            var dataPointContract = Mapper.MapDataPoint(dataPoint);

            Assert.Equal(dataPoint.Date, dataPointContract.Date);
            Assert.Equal(dataPoint.Value, dataPointContract.Value);
        }
    }
}
