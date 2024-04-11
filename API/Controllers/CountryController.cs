using Microsoft.AspNetCore.Mvc;
using API.Contracts;
namespace API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CountryController : ControllerBase
    {
        private readonly ILogger<CountryController> _logger;
        
        public CountryController(ILogger<CountryController> logger)
        {
            _logger = logger;
        }
        // Returns bogus object
        [HttpGet()]
        public IEnumerable<Country> GetAllCountries()
        {
            return new List<Country>();
        }
        // Returns bogus object
        [HttpGet()]
        public Country GetCountryOfTheDay()
        {

            return new Country { Code = "0", Name = "Land1" };
        }
        // Returns bogus object
        [HttpGet("{iso}")]
        public Country GetCountry(string iso)
        {
            return new Country { Code = "0", Name = "Land1" };
        }
    }
}
