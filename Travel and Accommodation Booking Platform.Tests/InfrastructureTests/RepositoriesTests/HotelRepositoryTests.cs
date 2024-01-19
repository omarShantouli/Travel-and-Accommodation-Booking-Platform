using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Travel_and_Accommodation_Booking_Platform.Tests.InfrastructureTests.RepositoriesTests.TestData;
using Xunit.Abstractions;

namespace Travel_and_Accommodation_Booking_Platform.Tests.InfrastructureTests.RepositoriesTests
{
    /// <summary>
    /// Tests for the <see cref="HotelRepository"/> class.
    /// </summary>
    public class HotelRepositoryTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="HotelRepositoryTests"/> class.
        /// </summary>
        /// <param name="testOutputHelper">The test output helper.</param>
        public HotelRepositoryTests(ITestOutputHelper testOutputHelper)
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
        /// Tests the GetAll method of the <see cref="HotelRepository"/> class.
        /// </summary>
        /// <param name="expectedHotels">The expected hotels to be returned.</param>
        [Theory]
        [MemberData(nameof(HotelRepositoryTestData.HotelsTestData),
            MemberType = typeof(HotelRepositoryTestData))]
        [Trait("Category", "Queries")]
        public async Task GetAll_ShouldReturnAllHotels(List<Hotels> expectedHotels)
        {
            // Arrange

            // Act
            _context.Hotels.AddRange(expectedHotels);

            await _context.SaveChangesAsync();

            var sut = new HotelsRepository(_context);

            var fetchedHotels = sut.GetAll();

            // Assert 
            Assert.Equal(expectedHotels.Count, fetchedHotels.Count());
        }

        /// <summary>
        /// Tests the GetByIdAsync method of the <see cref="HotelRepository"/> class.
        /// </summary>
        /// <param name="hotelToFind">The hotel to find by ID.</param>
        [Theory]
        [MemberData(nameof(HotelRepositoryTestData.HotelRepositoryValidTestData),
            MemberType = typeof(HotelRepositoryTestData))]
        [Trait("Category", "Queries")]
        public async Task GetByIdAsync_ShouldReturnHotelWithMatchingId(Hotels hotelToFind)
        {
            // Arrange
            _context.Hotels.Add(hotelToFind);
            await _context.SaveChangesAsync();

            var sut = new HotelsRepository(_context);

            // Act
            var result = await sut.GetByIdAsync(hotelToFind.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(hotelToFind.Id, result?.Id);
            Assert.Equal(hotelToFind.Name, result?.Name);
        }

        /// <summary>
        /// Tests the CreateAsync method of the <see cref="HotelRepository"/> class.
        /// </summary>
        /// <param name="hotelToAdd">The hotel to add.</param>
        [Theory]
        [MemberData(nameof(HotelRepositoryTestData.HotelRepositoryValidTestData),
            MemberType = typeof(HotelRepositoryTestData))]
        [Trait("Category", "Commands")]
        public async Task CreateAsync_ShouldCreateHotel(Hotels hotelToAdd)
        {
            // Arrange
            var sut = new HotelsRepository(_context);

            // Act
            var result = await sut.CreateAsync(hotelToAdd);

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual(Guid.Empty, result?.Id);
            Assert.Equal(hotelToAdd.Name, result?.Name);

            // Check if the hotel is actually in the database
            var savedHotel = await _context.Hotels.FindAsync(result?.Id);
            Assert.NotNull(savedHotel);
            Assert.Equal(hotelToAdd.Name, savedHotel?.Name);
        }

        /// <summary>
        /// Tests the UpdateAsync method of the <see cref="HotelRepository"/> class.
        /// </summary>
        /// <param name="existingHotel">The existing hotel to update.</param>
        [Theory]
        [MemberData(nameof(HotelRepositoryTestData.HotelRepositoryValidTestData),
            MemberType = typeof(HotelRepositoryTestData))]
        [Trait("Category", "Commands")]
        public async Task UpdateAsync_ShouldUpdateHotel(Hotels existingHotel)
        {
            // Arrange
            _context.Hotels.Add(existingHotel);
            await _context.SaveChangesAsync();
            _context.Entry(existingHotel).State = EntityState.Detached;

            var updatedHotel = new Hotels
            {
                Id = existingHotel.Id,
                Name = "UpdatedHotel",
                // Additional properties for hotel update
            };
            var sut = new HotelsRepository(_context);

            // Act
            await sut.UpdateAsync(updatedHotel);

            // Assert
            var result = await _context.Hotels.FindAsync(existingHotel.Id);
            Assert.NotNull(result);
            Assert.Equal(updatedHotel.Name, result?.Name);
        }

        /// <summary>
        /// Tests the DeleteAsync method of the <see cref="HotelRepository"/> class.
        /// </summary>
        /// <param name="hotelToDelete">The hotel to delete.</param>
        [Theory]
        [MemberData(nameof(HotelRepositoryTestData.HotelRepositoryValidTestData),
            MemberType = typeof(HotelRepositoryTestData))]
        [Trait("Category", "Commands")]
        public async Task DeleteAsync_ShouldDeleteHotel(Hotels hotelToDelete)
        {
            // Arrange
            await _context.Hotels.AddAsync(hotelToDelete);
            await _context.SaveChangesAsync();
            _context.Entry(hotelToDelete).State = EntityState.Detached;

            var sut = new HotelsRepository(_context);

            // Act
            var hotelExistsBeforeDeletion = await _context.Hotels.AnyAsync(hotel => hotel.Id == hotelToDelete.Id);

            if (hotelExistsBeforeDeletion)
            {
                await sut.DeleteAsync(hotelToDelete.Id);
            }
            else
            {
                _testOutputHelper.WriteLine($"Hotel with ID {hotelToDelete.Id} does not exist. Skipping deletion.");
            }

            // Assert
            _testOutputHelper.WriteLine(hotelToDelete.Id.ToString());
            var result = await _context.Hotels.AsNoTracking().SingleOrDefaultAsync(hotel => hotelToDelete.Id.Equals(hotel.Id));
            Assert.Null(result);
        }
    }
}
