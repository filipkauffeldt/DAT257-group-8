using API.Contracts;
using API.Model;
using API.Model.ObjectModels;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

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
            // TODO: Implement logic to get country of the day
            return _dbContext.Countries
                .Include (c => c.Data)
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
            // Put temp country in db with points for the year and the year before, only for testing
            //FIXME: Remove this when actual data is in the database
            //var tempCountry = new Country {
            //    Code = code,
            //    Name = "Test Country",
            //    Description = $"Input date: {date.ToString()}",
            //    Data = new List<Data> {
            //        new Data {
            //            Name = "Test Data",
            //            Unit = "Test Unit",
            //            Points = new List<DataPoint> {
            //                new DataPoint {
            //                    Date = new DateOnly(2022, 1, 1),
            //                    Value = 1
            //                },
            //                new DataPoint {
            //                    Date = new DateOnly(2023, 1, 1),
            //                    Value = 2
            //                }
            //            }
            //        }
            //    }
            //};

            //_dbContext.Countries.Add(tempCountry);

            return _dbContext.Countries
                .Where(c => c.Code == code)
                .Include(c => c.Data)
                .ThenInclude(d => d.Points
                    .Where(p => p.Date.Year == date.Year))
                .FirstOrDefault();

            //fetchedCountry?.Data

            //if (fetchedCountry != null)
            //{
            //    foreach (var data in fetchedCountry.Data)
            //    {
            //        data.Points = data.Points?
            //            .Where(p => p.Date.Year == date.Year)
            //            .ToList();
            //    }
            //}

        }
    }
}