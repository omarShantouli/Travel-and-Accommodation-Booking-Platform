using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Travel_and_Accommodation_Booking_Platform.Tests.InfrastructureTests.RepositoriesTests.TestData;
using Xunit.Abstractions;

namespace Travel_and_Accommodation_Booking_Platform.Tests.InfrastructureTests.RepositoriesTests
{
    /// <summary>
    /// Tests for the <see cref="CityRepository"/> class.
    /// </summary>
    public class CityRepositoryTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CityRepositoryTests"/> class.
        /// </summary>
        /// <param name="testOutputHelper">The test output helper.</param>
        public CityRepositoryTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            // We must comment optionsBuilder.UseSqlServer(...) in ApplicationDbContext.cs,
            // because Only a single database provider can be registered in a service provider
            var options = new DbContextOptionsBuilder()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);
        }

        /// <summary>
        /// Tests the GetAll method of the <see cref="CityRepository"/> class.
        /// </summary>
        /// <param name="expectedCities">The expected cities to be returned.</param>
        [Theory]
        [MemberData(nameof(CityRepositoryTestData.CitiesTestData),
            MemberType = typeof(CityRepositoryTestData))]
        [Trait("Category", "Queries")]
        public async Task GetAll_ShouldReturnAllCities(List<City> expectedCities)
        {
            // Arrange

            // Act
            _context.Cities.AddRange(expectedCities);

            await _context.SaveChangesAsync();

            var sut = new CityRepository(_context);

            var fetchedCities = sut.GetAll();

            // Assert 
            Assert.Equal(expectedCities.Count, fetchedCities.Count());
        }

        /// <summary>
        /// Tests the GetByIdAsync method of the <see cref="CityRepository"/> class.
        /// </summary>
        /// <param name="cityToFind">The city to find by ID.</param>
        [Theory]
        [MemberData(nameof(CityRepositoryTestData.CityRepositoryValidTestData),
            MemberType = typeof(CityRepositoryTestData))]
        [Trait("Category", "Queries")]
        public async Task GetByIdAsync_ShouldReturnCityWithMatchingId(City cityToFind)
        {
            // Arrange
            _context.Cities.Add(cityToFind);
            await _context.SaveChangesAsync();

            var sut = new CityRepository(_context);

            // Act
            var result = await sut.GetByIdAsync(cityToFind.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(cityToFind.Id, result?.Id);
            Assert.Equal(cityToFind.Name, result?.Name);
        }

        /// <summary>
        /// Tests the CreateAsync method of the <see cref="CityRepository"/> class.
        /// </summary>
        /// <param name="cityToAdd">The city to add.</param>
        [Theory]
        [MemberData(nameof(CityRepositoryTestData.CityRepositoryValidTestData),
            MemberType = typeof(CityRepositoryTestData))]
        [Trait("Category", "Commands")]
        public async Task CreateAsync_ShouldCreateCity(City cityToAdd)
        {
            // Arrange
            var sut = new CityRepository(_context);

            // Act
            var result = await sut.CreateAsync(cityToAdd);

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual(Guid.Empty, result?.Id);
            Assert.Equal(cityToAdd.Name, result?.Name);

            // Check if the city is actually in the database
            var savedCity = await _context.Cities.FindAsync(result?.Id);
            Assert.NotNull(savedCity);
            Assert.Equal(cityToAdd.Name, savedCity?.Name);
        }

        /// <summary>
        /// Tests the UpdateAsync method of the <see cref="CityRepository"/> class.
        /// </summary>
        /// <param name="existingCity">The existing city to update.</param>
        [Theory]
        [MemberData(nameof(CityRepositoryTestData.CityRepositoryValidTestData),
            MemberType = typeof(CityRepositoryTestData))]
        [Trait("Category", "Commands")]
        public async Task UpdateAsync_ShouldUpdateCity(City existingCity)
        {
            // Arrange
            _context.Cities.Add(existingCity);
            await _context.SaveChangesAsync();
            _context.Entry(existingCity).State = EntityState.Detached;

            var updatedCity = new City
            {
                Id = existingCity.Id,
                Name = "UpdatedCity",
                CountryName = "ct1",
                CountryCode = "xyz",
                PostOffice = "PostOffice"
            };
            var sut = new CityRepository(_context);

            // Act
            await sut.UpdateAsync(updatedCity);

            // Assert
            var result = await _context.Cities.FindAsync(existingCity.Id);
            Assert.NotNull(result);
            Assert.Equal(updatedCity.Name, result?.Name);
        }

        /// <summary>
        /// Tests the DeleteAsync method of the <see cref="CityRepository"/> class.
        /// </summary>
        /// <param name="cityToDelete">The city to delete.</param>
        [Theory]
        [MemberData(nameof(CityRepositoryTestData.CityRepositoryValidTestData),
            MemberType = typeof(CityRepositoryTestData))]
        [Trait("Category", "Commands")]
        public async Task DeleteAsync_ShouldDeleteCity(City cityToDelete)
        {
            // Arrange
            await _context.Cities.AddAsync(cityToDelete);
            await _context.SaveChangesAsync();
            _context.Entry(cityToDelete).State = EntityState.Detached;

            var sut = new CityRepository(_context);

            // Act
            var cityExistsBeforeDeletion = await _context.Cities.AnyAsync(city => city.Id == cityToDelete.Id);

            if (cityExistsBeforeDeletion)
            {
                await sut.DeleteAsync(cityToDelete.Id);
            }
            else
            {
                _testOutputHelper.WriteLine($"City with ID {cityToDelete.Id} does not exist. Skipping deletion.");
            }

            // Assert
            _testOutputHelper.WriteLine(cityToDelete.Id.ToString());
            var result = await _context.Cities.AsNoTracking().SingleOrDefaultAsync(city => cityToDelete.Id.Equals(city.Id));
            Assert.Null(result);
        }
    }
}
