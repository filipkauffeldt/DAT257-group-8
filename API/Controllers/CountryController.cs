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
