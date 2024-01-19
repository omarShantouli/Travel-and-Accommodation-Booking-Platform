using Domain.Entities;

namespace Travel_and_Accommodation_Booking_Platform.Tests.InfrastructureTests.RepositoriesTests.TestData
{
    public class HotelRepositoryTestData
    {
        public static IEnumerable<object[]> HotelRepositoryValidTestData
        {
            get
            {
                yield return new object[] { new Hotels()
                {
                    Id = Guid.NewGuid(),
                    Name = "Hotel1",
                    Rating = 4.5f,
                    StreetAddress = "StreetAddress1",
                    Description = "Description1",
                    Phone = "123-456-7890",
                    FloorsNumber = 5,
                }};

                yield return new object[] { new Hotels()
                {
                    Id = Guid.NewGuid(),
                    Name = "Hotel2",
                    Rating = 4.2f,
                    StreetAddress = "StreetAddress2",
                    Description = "Description2",
                    Phone = "987-654-3210",
                    FloorsNumber = 8,
                }};

                yield return new object[] { new Hotels()
                {
                    Id = Guid.NewGuid(),
                    Name = "Hotel3",
                    Rating = 4.8f,
                    StreetAddress = "StreetAddress3",
                    Description = "Description3",
                    Phone = "555-123-4567",
                    FloorsNumber = 12,
                }};

                yield return new object[] { new Hotels()
                {
                    Id = Guid.NewGuid(),
                    Name = "Hotel4",
                    Rating = 4.0f,
                    StreetAddress = "StreetAddress4",
                    Description = "Description4",
                    Phone = "777-888-9999",
                    FloorsNumber = 6,
                }};

            }
        }

        public static IEnumerable<object[]> HotelsTestData()
        {
            yield return new object[]
            {
                new List<Hotels>
                {
                    new Hotels
                    {
                        Id = Guid.NewGuid(),
                        Name = "Hotel1",
                        Rating = 4.0f,
                        StreetAddress = "StreetAddress1",
                        Description = "Description1",
                        Phone = "123-456-7890",
                        FloorsNumber = 3,
                    },
                    new Hotels
                    {
                        Id = Guid.NewGuid(),
                        Name = "Hotel2",
                        Rating = 4.2f,
                        StreetAddress = "StreetAddress2",
                        Description = "Description2",
                        Phone = "987-654-3210",
                        FloorsNumber = 8,
                    },
                    new Hotels
                    {
                        Id = Guid.NewGuid(),
                        Name = "Hotel3",
                        Rating = 4.8f,
                        StreetAddress = "StreetAddress3",
                        Description = "Description3",
                        Phone = "555-123-4567",
                        FloorsNumber = 12,
                    },
                    new Hotels
                    {
                        Id = Guid.NewGuid(),
                        Name = "Hotel4",
                        Rating = 4.0f,
                        StreetAddress = "StreetAddress4",
                        Description = "Description4",
                        Phone = "777-888-9999",
                        FloorsNumber = 6,
                    },
                }
            };
        }
    }
}
