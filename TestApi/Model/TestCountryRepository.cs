using Xunit;
using Moq;
using API.Model;
using API.Model.ObjectModels;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq.EntityFrameworkCore;
using System.Threading.Tasks;

namespace TestApi.Model {
    public class TestCountryRepository
    {
        private readonly Mock<DbSet<Country>> _mockSet;
        private readonly Mock<CountryDbContext> _mockContext;
        private readonly CountryRepository _mockRepo;

        public TestCountryRepository()
        {
            var data = new List<Country>
            {
                new Country { Code = "US", Name = "United States", Data = new List<Data> {} },
                new Country { Code = "CA", Name = "Canada", Data = new List<Data> {} },
                new Country { Code = "MX", Name = "Mexico", Data = new List<Data> {} }
            }.AsQueryable();

            _mockSet = new Mock<DbSet<Country>>();
            _mockSet.As<IQueryable<Country>>().Setup(m => m.Provider).Returns(data.Provider);
            _mockSet.As<IQueryable<Country>>().Setup(m => m.Expression).Returns(data.Expression);
            _mockSet.As<IQueryable<Country>>().Setup(m => m.ElementType).Returns(data.ElementType);
            _mockSet.As<IQueryable<Country>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _mockContext = new Mock<CountryDbContext>();
            _mockContext.Setup(x => x.Countries).ReturnsDbSet(_mockSet.Object);

            _mockRepo = new CountryRepository(_mockContext.Object);
        }

        [Fact]
        public async Task GetAllCountriesAsync_ShouldReturnAllCountries()
        {
            var countries = await _mockRepo.GetAllCountriesAsync();
            Assert.Equal(3, countries.Count());
        }

        [Fact]
        public async Task GetAllCountryIdentifiersAsync_ShouldReturnAllCountryIdentifiers()
        {
            var countries = await _mockRepo.GetAllCountryIdentifiersAsync();
            Assert.Equal(3, countries.Count());
        }

        [Fact]
        public async Task GetCountryByCodeAsync_ShouldReturnCountry()
        {
            var country = await _mockRepo.GetCountryByCodeAsync("US");
            Assert.NotNull(country);
            Assert.Equal("US", country.Code);
        }

        [Fact]
        public async Task AddCountryAsync_ShouldAddCountry()
        {
            var country = new Country { Code = "BR", Name = "Brazil", Data = new List<Data> {} };
            var addedCountry = await _mockRepo.AddCountryAsync(country);
            // _mockSet.Verify(m => m.Add(It.IsAny<Country>()), Times.Once());
            // _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
            Assert.Equal(country, addedCountry);
        }

        [Fact]
        public async Task UpdateCountryAsync_ShouldUpdateCountry()
        {
            var country = new Country { Code = "BR", Name = "Brazil", Data = new List<Data> {} };
            var updatedCountry = await _mockRepo.UpdateCountryAsync(country);
            // _mockSet.Verify(m => m.Update(It.IsAny<Country>()), Times.Once());
            // _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
            Assert.Equal(country, updatedCountry);
        }

        [Fact]
        public async Task DeleteCountryAsync_ShouldDeleteCountry()
        {
            var code = "US";
            var deletedCode = await _mockRepo.DeleteCountryAsync(code);
            // _mockSet.Verify(m => m.Remove(It.IsAny<Country>()), Times.Once());
            // _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
            Assert.Equal(code, deletedCode);
        }

        [Fact]
        public async Task GetCountryWithYearAsync_ShouldReturnCountryWithYear()
        {
            var code = "US";
            var date = DateOnly.FromDateTime(DateTime.Now);
            var country = await _mockRepo.GetCountryWithYearAsync(code, date);
            Assert.NotNull(country);
            Assert.Equal(code, country.Code);
        }
    }
}