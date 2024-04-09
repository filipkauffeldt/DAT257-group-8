using Microsoft.AspNetCore.Mvc;
using API.Contracts;
namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly ILogger<CountryController> _logger;
        
        public CountryController(ILogger<CountryController> logger)
        {
            _logger = logger;
        }
        // Returns bogus object
        [HttpGet(Name = "GetAllCountries")]
        public IEnumerable<Country> Get()
        {
            return new List<Country>();
        }
        // Returns bogus object
        [HttpGet(Name = "GetCountryOfTheDay")]
        public Country GetCountryOfTheDay()
        {
            return new Country { Code = "0", Name = "Land1"};
        }
        // Returns bogus object
        [HttpGet(Name = "GetCountryFromID")]
        public Country GetCountry(int id)
        {
            return new Country { Code = "0", Name = "Land1" };
        }        
    }
}
