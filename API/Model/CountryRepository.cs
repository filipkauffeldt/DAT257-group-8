using API.Contracts;
using API.Model;
using API.Model.ObjectModels;
using Microsoft.EntityFrameworkCore;

namespace API.Model
{
    public class CountryRepository
    {
        private readonly CountryDbContext _dbContext;

        public CountryRepository(CountryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Country> GetAllCountries()
        {
            return _dbContext.Countries
                    .Include(c => c.Data)
                    .ThenInclude(d => d.Points)
                    .ToList();
        }

        public Country? GetCountryByCode(string code)
        {
            var country = _dbContext.Countries
                    .Where(c => c.Code == code)
                    .Include(c => c.Data)
                    .ThenInclude(d => d.Points)
                    .FirstOrDefault();

            if (country == null) return country;
            else return country.Copy();
        }

        public Country? GetCountryOfTheDay()
        {
            
            DateTime today = DateTime.Today;
            string stringToBeHashed = today.Month.ToString() + today.Day.ToString();
            int hashed = stringToBeHashed.GetHashCode();


            List<Country> countries = _dbContext.Countries.ToList();
            int AmountOfCountries = countries.Count();
            string CountryCode = countries[hashed % AmountOfCountries].Code;
         
            return _dbContext.Countries
                .Where(c => c.Code.Equals(CountryCode))
                .Include(c => c.Data)
                .ThenInclude(d => d.Points)
                .FirstOrDefault();
        }

        public void AddCountry(Country country)
        {
            _dbContext.Countries.Add(country);
            _dbContext.SaveChanges();
        }

        public void UpdateCountry(Country country)
        {
            _dbContext.Countries.Update(country);
            _dbContext.SaveChanges();
        }

        public void DeleteCountry(string code)
        {
            var country = _dbContext.Countries.FirstOrDefault(c => c.Code == code);
            if (country != null)
            {
                _dbContext.Countries.Remove(country);
                _dbContext.SaveChanges();
            }
        }

        public Country? GetCountryWithYear(string code, DateOnly date)
        {
            return _dbContext.Countries
                .Where(c => c.Code == code)
                .Include(c => c.Data)
                .ThenInclude(d => d.Points
                    .Where(p => p.Date.Year == date.Year))
                .FirstOrDefault();
        }
    }
}