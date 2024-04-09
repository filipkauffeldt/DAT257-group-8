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

        [HttpGet(Name = "GetAllCountries")]
        public IEnumerable<Country> Get()
        {
            return null;
        }

        [HttpGet(Name = "GetCountryOfTheDay")]
        public Country GetCountryOfTheDay()
        {
            return null;
        }

        [HttpGet(Name = "GetCountryFromID")]
        public Country GetCountry(int id)
        {
            return null;
        }        
    }
}
