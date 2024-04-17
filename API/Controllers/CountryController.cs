using Microsoft.AspNetCore.Mvc;
using API.Contracts;
using API.Model;
using API.Model.ObjectModels;
using Microsoft.AspNetCore.Http.HttpResults;
using API.Utils;
namespace API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CountryController : ControllerBase
    {
        private readonly ILogger<CountryController> _logger;
        private readonly CountryRepository _countryRepository;

        public CountryController(ILogger<CountryController> logger, CountryRepository countryRepository)
        {
            _logger = logger;
            _countryRepository = countryRepository;
        }

        [HttpGet()]
        public IEnumerable<CountryContract> GetAllCountries()
        {
            var countries = _countryRepository.GetAllCountries();

            var countryContracts = Mapper.MapCountryList(countries);

            return countryContracts;
        }

        [HttpGet()]
        public IActionResult GetCountryOfTheDay()
        {
            var country = _countryRepository.GetCountryOfTheDay();

            return country != null ? Ok(Mapper.MapCountry(country)) : NotFound("No country of the day found.");
        }

        [HttpGet("{code}")]
        public IActionResult GetCountry(string code)
        {
            var country = _countryRepository.GetCountryByCode(code);

            return country != null ? Ok(Mapper.MapCountry(country)) : NotFound("The specified country could not be found.");
        }

        [HttpGet]
        public IActionResult GetCountryDataForYear(string code, DateOnly year)
        {
            // TODO: Get actual data from database
            var country = _countryRepository.GetCountryWithYear(code, year);
            if (country == null) return NotFound($"No country with code = '{code}' found.");

            // Fake country, should use country from database instead
            //var country = new Country
            //{
            //    Code = code,
            //    Name = "Fake Country",
            //    Continent = "Fake Continent",
            //    Description = "Fake Description",
            //    Data = new List<Data>
            //    {
            //        new Data
            //        {
            //            Name = "Fake Data",
            //            Description = "Fake Description",
            //            Unit = "Fake Unit",
            //            Points = new List<DataPoint>
            //            {
            //                new DataPoint
            //                {
            //                    Value = 80,
            //                    Date = new DateOnly(2000, 1, 1)
            //                },

            //                new DataPoint
            //                {
            //                    Value = 30,
            //                    Date = new DateOnly(2023, 1, 1)
            //                }
            //            }
            //        },
            //        new Data
            //        {
            //            Name = "More Fake Data",
            //            Description = "More Fake Data",
            //            Unit = "More Fake Data",
            //            Points = new List<DataPoint>
            //            {
            //                new DataPoint
            //                {
            //                    Value = 65,
            //                    Date = new DateOnly(2000, 1, 1)
            //                },

            //                new DataPoint
            //                {
            //                    Value = 25,
            //                    Date = new DateOnly(2023, 1, 1)
            //                }
            //            }
            //        }
            //    }
            //};


            // Remove all DataPoints that is outside the given timespan
            country.Data = country.Data.Select(data =>
                new Data
                {
                    Name = data.Name,
                    Description = data.Description,
                    Unit = data.Unit,
                    Points = data.Points?.Where(point =>
                        point.Date.Year == year.Year
                    ).ToList() 
                }
            ).ToList();
            

            var countryContract = Mapper.MapCountry(country);

            return Ok(countryContract);
        }

        [HttpPost()]
        public IActionResult CreateRandomCountry()
        {
            var country = new Country
            {
                Code = Guid.NewGuid().ToString(),
                Name = "Random Country",
                Continent = "Random Continent",
                Description = "Random Description",
                Data = new List<Data>
                {
                    new Data
                    {
                        Name = "Random Data",
                        Description = "Random Description",
                        Unit = "Random Unit",
                        Points = new List<DataPoint>
                        {
                            new DataPoint
                            {
                                Value = Random.Shared.Next(0, 100),
                                Date = DateOnly.FromDateTime(DateTime.Now)
                            }
                        }
                    }
                }
            };

            _countryRepository.AddCountry(country);

            var countryContract = Mapper.MapCountry(country);

            return CreatedAtAction(nameof(CreateRandomCountry), new { code = countryContract.Code }, countryContract);
        }
    }
}
