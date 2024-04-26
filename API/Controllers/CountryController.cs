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
        public async Task<IEnumerable<CountryContract>> GetAllCountries()
        {
            var countries = _countryRepository.GetAllCountries();

            var countryContracts = Mapper.MapCountryList(countries);

            return countryContracts;
        }

        [HttpGet()]
        public async Task<IEnumerable<CountryContract>> GetAllCountryIdentifiers()
        {
            var countries = await _countryRepository.GetAllCountryIdentifiers();

            var countryContracts = Mapper.MapCountryList(countries);

            return countryContracts;
        }

        [HttpGet()]
        public async Task<ActionResult<CountryContract>> GetCountryOfTheDay()
        {
            var country = await _countryRepository.GetCountryOfTheDayAsync();

            return country != null ? Ok(Mapper.MapCountry(country)) : NotFound("No country of the day found.");
        }

        [HttpGet("{code}")]
        public async Task<ActionResult<CountryContract>> GetCountry(string code)
        {
            var country = await _countryRepository.GetCountryByCodeAsync(code);

            return country != null ? Ok(Mapper.MapCountry(country)) : NotFound("The specified country could not be found.");
        }

        [HttpGet("{code}/{date}")]
        public async Task<ActionResult<CountryContract>> GetCountryDataForYear(string code, DateOnly date)
        {
            if (string.IsNullOrEmpty(code))
            {
                return BadRequest("Code is required");
            }

            if (date.Year < 1 || date.Year > 2022)
            {
                return BadRequest("Year must be a valid year");
            }

            var country = await _countryRepository.GetCountryWithYearAsync(code, date);
            if (country == null) return NotFound($"No country with code = '{code}' found.");

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
