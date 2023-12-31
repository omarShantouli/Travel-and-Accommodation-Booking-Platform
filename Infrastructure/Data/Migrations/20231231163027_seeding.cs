using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class seeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryCode", "CountryName", "Name", "PostOffice" },
                values: new object[,]
                {
                    { new Guid("3c7e66f5-46a9-4b8d-8e90-85b5a9e2c2fd"), "JP", "Japan", "Tokyo", "TKY" },
                    { new Guid("8d2aeb7a-7c67-4911-aa2c-d6a3b4dc7e9e"), "UK", "United Kingdom", "London", "LDN" },
                    { new Guid("f9e85d04-548c-4f98-afe9-2a8831c62a90"), "US", "United States", "New York", "NYC" }
                });

            migrationBuilder.InsertData(
                table: "Guests",
                columns: new[] { "Id", "EmailAddress", "FirstName", "LastName", "Phone" },
                values: new object[,]
                {
                    { new Guid("aaf21a7d-8fc3-4c9f-8a8e-1eeec8dcd462"), "jane.smith@example.com", "Jane", "Smith", "2012345678" },
                    { new Guid("c6c45f7c-2dfe-4c1e-9a9b-8b173c71b32c"), "john.doe@example.com", "John", "Doe", "1234567890" },
                    { new Guid("f44c3eb4-2c8a-4a77-a31b-04c4619aa15a"), "hiroshi.tanaka@example.co.jp", "Hiroshi", "Tanaka", "312345678" }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "EntityId", "EntityType", "Type", "URL" },
                values: new object[,]
                {
                    { new Guid("0a1b2c3d-4e5f-6a7b-8c9d-0a1b2c3d4e5f"), new Guid("4e1cb3d9-bc3b-4997-a3d5-0c56cf17fe7a"), "Room", "Interior", "https://thumbs.dreamstime.com/b/hotel-room-beautiful-orange-sofa-included-43642330.jpg" },
                    { new Guid("1a2b3c4d-5e6f-7a8b-9c0d-1a2b3c4d5e6f"), new Guid("98c2c9fe-1a1c-4eaa-a7f5-b9d19b246c27"), "Hotel", "Exterior", "https://cf.bstatic.com/xdata/images/hotel/max1024x768/373326414.jpg?k=2ac575ebc4df5a8bb620431112286ece53fa83effc3bd20dbbecf869214d7057&o=&hp=1" },
                    { new Guid("2a3b4c5d-6e7f-8a9b-0c1d-2a3b4c5d6e7f"), new Guid("c6898b7e-ee09-4b36-8b20-22e8c2a63e29"), "Room", "Interior", "https://media.cnn.com/api/v1/images/stellar/prod/140127103345-peninsula-shanghai-deluxe-mock-up.jpg?q=w_2226,h_1449,x_0,y_0,c_fill" },
                    { new Guid("3a4b5c6d-7e8f-9a0b-1c2d-3a4b5c6d7e8f"), new Guid("bfa4173d-7893-48b9-a497-5f4c7fb2492b"), "Hotel", "Thumbnail", "https://images.bubbleup.com/width1920/quality35/mville2017/1-brand/1-margaritaville.com/gallery-media/220803-compasshotel-medford-pool-73868-1677873697.jpg" },
                    { new Guid("4a5b6c7d-8e9f-0a1b-2c3d-4a5b6c7d8e9f"), new Guid("9461e08b-92d3-45da-b6b3-efc0cfcc4a3a"), "Hotel", "Exterior", "https://d2rewpp8r4d4h2.cloudfront.net/compasshotel.com-1728563160/cms/cache/v2/6329c8f511d47.jpeg/406x242/fit/80/fb8dc39799ecad4199c47f4355fa0dc3.jpg" },
                    { new Guid("5a6b7c8d-9e0f-1a2b-3c4d-5a6b7c8d9e0f"), new Guid("3c7e66f5-46a9-4b8d-8e90-85b5a9e2c2fd"), "City", "Exterior", "https://media.cntraveler.com/photos/63482b255e7943ad4006df0b/16:9/w_2560%2Cc_limit/tokyoGettyImages-1031467664.jpeg" },
                    { new Guid("6a7b8c9d-0e1f-2a3b-4c5d-6a7b8c9d0e1f"), new Guid("8d2aeb7a-7c67-4911-aa2c-d6a3b4dc7e9e"), "City", "Exterior", "https://dynamic-media-cdn.tripadvisor.com/media/photo-o/15/33/f5/de/london.jpg?w=700&h=500&s=1" },
                    { new Guid("7a8b9c0d-1e2f-3a4b-5c6d-7a8b9c0d1e2f"), new Guid("f9e85d04-548c-4f98-afe9-2a8831c62a90"), "City", "Exterior", "https://i.natgeofe.com/k/5b396b5e-59e7-43a6-9448-708125549aa1/new-york-statue-of-liberty.jpg" },
                    { new Guid("9a0b1c2d-3e4f-5a6b-7c8d-9a0b1c2d3e4f"), new Guid("a98b8a9d-4c5a-4a90-a2d2-5f1441b93db6"), "Room", "Interior", "https://www.shutterstock.com/image-photo/hotel-room-interior-modern-seaside-260nw-1387008533.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "Id", "EmailAddress", "FirstName", "LastName", "Phone" },
                values: new object[] { new Guid("a1d1aa11-12e7-4e0f-8425-67c1c1e62c2d"), "hantoli797@gmail.com", "Omar", "Hantouli", "0598191973" });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "Amount", "BookingId", "Method", "Status" },
                values: new object[,]
                {
                    { new Guid("1c8d70bd-2534-4991-bddf-84c7edee1a79"), 1200.0, new Guid("efeb3d13-3dab-46c9-aa9a-9f22dd58e06e"), "PayPal", "Pending" },
                    { new Guid("7f5cc9f0-796f-498d-9f3f-9e5249a4f6ae"), 1500.0, new Guid("0bf4a177-98b8-4f67-8a56-95669c320890"), "Credit Card", "Completed" },
                    { new Guid("8f974636-4f53-48d9-af99-2f7f1d3e0474"), 2000.0, new Guid("7d3155a2-95f8-4d9b-bc24-662ae053f1c9"), "Bank Transfer", "Completed" }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "BookingId", "Comment", "Rating", "ReviewDate" },
                values: new object[,]
                {
                    { new Guid("192045db-c6db-49c9-aa6b-2e3d6c7f3b79"), new Guid("7d3155a2-95f8-4d9b-bc24-662ae053f1c9"), "Clean rooms and beautiful views.", 4.2f, new DateTime(2023, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("63e4bb25-28b1-4fc4-9b93-9254d94dab23"), new Guid("0bf4a177-98b8-4f67-8a56-95669c320890"), "Excellent service and comfortable stay!", 4.8f, new DateTime(2023, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("85a5a0b4-0e04-4c46-b7ac-6cf609e4f2aa"), new Guid("efeb3d13-3dab-46c9-aa9a-9f22dd58e06e"), "Friendly staff and great location.", 4.5f, new DateTime(2023, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "RoomTypes",
                columns: new[] { "Id", "HotelId", "PricePerNight", "Type" },
                values: new object[,]
                {
                    { new Guid("4b4c0ea5-0b9a-4a20-8ad9-77441fb912d2"), new Guid("9461e08b-92d3-45da-b6b3-efc0cfcc4a3a"), 200f, "Suite" },
                    { new Guid("5a5de3b8-3ed8-4f0a-bda9-cf73225a64a1"), new Guid("98c2c9fe-1a1c-4eaa-a7f5-b9d19b246c27"), 100f, "Single" },
                    { new Guid("d67ddbe4-1f1a-4d85-bcc1-ec3a475ecb68"), new Guid("bfa4173d-7893-48b9-a497-5f4c7fb2492b"), 150f, "Double" }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "AdultsCapacity", "ChildrenCapacity", "HotelId", "Rating", "RoomTypeId", "View" },
                values: new object[,]
                {
                    { new Guid("4e1cb3d9-bc3b-4997-a3d5-0c56cf17fe7a"), 3, 2, new Guid("bfa4173d-7893-48b9-a497-5f4c7fb2492b"), 4.2f, new Guid("d67ddbe4-1f1a-4d85-bcc1-ec3a475ecb68"), "Ocean View" },
                    { new Guid("a98b8a9d-4c5a-4a90-a2d2-5f1441b93db6"), 2, 1, new Guid("98c2c9fe-1a1c-4eaa-a7f5-b9d19b246c27"), 4.5f, new Guid("5a5de3b8-3ed8-4f0a-bda9-cf73225a64a1"), "City View" },
                    { new Guid("c6898b7e-ee09-4b36-8b20-22e8c2a63e29"), 4, 0, new Guid("9461e08b-92d3-45da-b6b3-efc0cfcc4a3a"), 4.8f, new Guid("4b4c0ea5-0b9a-4a20-8ad9-77441fb912d2"), "Mountain View" }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "BookingDate", "CheckInDate", "CheckOutDate", "GuestId", "RoomId" },
                values: new object[,]
                {
                    { new Guid("0bf4a177-98b8-4f67-8a56-95669c320890"), new DateTime(2023, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f44c3eb4-2c8a-4a77-a31b-04c4619aa15a"), new Guid("c6898b7e-ee09-4b36-8b20-22e8c2a63e29") },
                    { new Guid("7d3155a2-95f8-4d9b-bc24-662ae053f1c9"), new DateTime(2023, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c6c45f7c-2dfe-4c1e-9a9b-8b173c71b32c"), new Guid("a98b8a9d-4c5a-4a90-a2d2-5f1441b93db6") },
                    { new Guid("efeb3d13-3dab-46c9-aa9a-9f22dd58e06e"), new DateTime(2023, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("aaf21a7d-8fc3-4c9f-8a8e-1eeec8dcd462"), new Guid("4e1cb3d9-bc3b-4997-a3d5-0c56cf17fe7a") }
                });

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "CityId", "Description", "FloorsNumber", "Name", "OwnerId", "Phone", "Rating", "StreetAddress" },
                values: new object[,]
                {
                    { new Guid("9461e08b-92d3-45da-b6b3-efc0cfcc4a3a"), new Guid("3c7e66f5-46a9-4b8d-8e90-85b5a9e2c2fd"), "A resort with breathtaking sunset views over the ocean.", 5, "Sunset Resort", new Guid("a1d1aa11-12e7-4e0f-8425-67c1c1e62c2d"), "312345678", 4.2f, "789 Beachfront Road" },
                    { new Guid("98c2c9fe-1a1c-4eaa-a7f5-b9d19b246c27"), new Guid("f9e85d04-548c-4f98-afe9-2a8831c62a90"), "A luxurious hotel with top-notch amenities.", 10, "Luxury Inn", new Guid("a1d1aa11-12e7-4e0f-8425-67c1c1e62c2d"), "1234567890", 4.5f, "123 Main Street" },
                    { new Guid("bfa4173d-7893-48b9-a497-5f4c7fb2492b"), new Guid("8d2aeb7a-7c67-4911-aa2c-d6a3b4dc7e9e"), "A cozy lodge nestled in the heart of nature.", 3, "Cozy Lodge", new Guid("a1d1aa11-12e7-4e0f-8425-67c1c1e62c2d"), "2012345678", 3.8f, "456 Oak Avenue" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: new Guid("0bf4a177-98b8-4f67-8a56-95669c320890"));

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: new Guid("7d3155a2-95f8-4d9b-bc24-662ae053f1c9"));

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: new Guid("efeb3d13-3dab-46c9-aa9a-9f22dd58e06e"));

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: new Guid("9461e08b-92d3-45da-b6b3-efc0cfcc4a3a"));

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: new Guid("98c2c9fe-1a1c-4eaa-a7f5-b9d19b246c27"));

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: new Guid("bfa4173d-7893-48b9-a497-5f4c7fb2492b"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("0a1b2c3d-4e5f-6a7b-8c9d-0a1b2c3d4e5f"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("1a2b3c4d-5e6f-7a8b-9c0d-1a2b3c4d5e6f"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("2a3b4c5d-6e7f-8a9b-0c1d-2a3b4c5d6e7f"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("3a4b5c6d-7e8f-9a0b-1c2d-3a4b5c6d7e8f"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("4a5b6c7d-8e9f-0a1b-2c3d-4a5b6c7d8e9f"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("5a6b7c8d-9e0f-1a2b-3c4d-5a6b7c8d9e0f"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("6a7b8c9d-0e1f-2a3b-4c5d-6a7b8c9d0e1f"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("7a8b9c0d-1e2f-3a4b-5c6d-7a8b9c0d1e2f"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("9a0b1c2d-3e4f-5a6b-7c8d-9a0b1c2d3e4f"));

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: new Guid("1c8d70bd-2534-4991-bddf-84c7edee1a79"));

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: new Guid("7f5cc9f0-796f-498d-9f3f-9e5249a4f6ae"));

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: new Guid("8f974636-4f53-48d9-af99-2f7f1d3e0474"));

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: new Guid("192045db-c6db-49c9-aa6b-2e3d6c7f3b79"));

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: new Guid("63e4bb25-28b1-4fc4-9b93-9254d94dab23"));

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: new Guid("85a5a0b4-0e04-4c46-b7ac-6cf609e4f2aa"));

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: new Guid("4b4c0ea5-0b9a-4a20-8ad9-77441fb912d2"));

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: new Guid("5a5de3b8-3ed8-4f0a-bda9-cf73225a64a1"));

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: new Guid("d67ddbe4-1f1a-4d85-bcc1-ec3a475ecb68"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("3c7e66f5-46a9-4b8d-8e90-85b5a9e2c2fd"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("8d2aeb7a-7c67-4911-aa2c-d6a3b4dc7e9e"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("f9e85d04-548c-4f98-afe9-2a8831c62a90"));

            migrationBuilder.DeleteData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: new Guid("aaf21a7d-8fc3-4c9f-8a8e-1eeec8dcd462"));

            migrationBuilder.DeleteData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: new Guid("c6c45f7c-2dfe-4c1e-9a9b-8b173c71b32c"));

            migrationBuilder.DeleteData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: new Guid("f44c3eb4-2c8a-4a77-a31b-04c4619aa15a"));

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: new Guid("a1d1aa11-12e7-4e0f-8425-67c1c1e62c2d"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("4e1cb3d9-bc3b-4997-a3d5-0c56cf17fe7a"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("a98b8a9d-4c5a-4a90-a2d2-5f1441b93db6"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("c6898b7e-ee09-4b36-8b20-22e8c2a63e29"));
        }
    }
}
