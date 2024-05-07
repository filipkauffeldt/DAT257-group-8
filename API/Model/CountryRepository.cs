using API.Contracts;
using API.Model;
using API.Model.ObjectModels;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace API.Model
{
    public class CountryRepository
    {
        private readonly CountryDbContext _dbContext;

        public CountryRepository(CountryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Country>> GetAllCountriesAsync()
        {
            return await _dbContext.Countries
                    .Include(c => c.Data)
                    .ThenInclude(d => d.Points)
                    .ToListAsync();
        }

        public async Task<IEnumerable<Country>> GetAllCountryIdentifiersAsync()
        {
            return await _dbContext.Countries
                    .ToListAsync();
        }

        public async Task<Country?> GetCountryByCodeAsync(string code)
        {
            return await _dbContext.Countries
                    .Where(c => c.Code == code)
                    .Include(c => c.Data)
                    .ThenInclude(d => d.Points)
                    .FirstOrDefaultAsync();
        }

        public async Task<Country?> GetCountryOfTheDayAsync()
        {
            
            DateTime today = DateTime.Today;
            var stringToBeHashed = today.Month.ToString() + today.Day.ToString();
            int hashed = CreateIntHashFromString(stringToBeHashed);


            var countries = await _dbContext.Countries.ToListAsync();
            var amountOfCountries = countries.Count();
            string countryCode = countries[hashed % amountOfCountries].Code;
         
            return await _dbContext.Countries
                .Where(c => c.Code.Equals(countryCode))
                .Include(c => c.Data)
                .ThenInclude(d => d.Points)
                .FirstOrDefaultAsync();
        }
        private int CreateIntHashFromString(string inputString)
        {
            MD5 md5Hasher = MD5.Create();
            var hashed = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(inputString));
            var ivalue = BitConverter.ToInt32(hashed, 0);
            return Math.Abs(ivalue);
        }

        public async Task<Country> AddCountry(Country country)
        {
            _dbContext.Countries.Add(country);
            await _dbContext.SaveChangesAsync();
            return country;
        }

        public void UpdateCountry(Country country)
        {
            _dbContext.Countries.Update(country);
            _dbContext.SaveChangesAsync();
        }

        public void DeleteCountry(string code)
        {
            var country = _dbContext.Countries.FirstOrDefault(c => c.Code == code);
            if (country != null)
            {
                _dbContext.Countries.Remove(country);
                _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Country?> GetCountryWithYearAsync(string code, DateOnly date)
        {
            return await _dbContext.Countries
                .Where(c => c.Code == code)
                .Include(c => c.Data)
                .ThenInclude(d => d.Points
                    .Where(p => p.Date.Year == date.Year))
                .FirstOrDefaultAsync();
        }
    }
}