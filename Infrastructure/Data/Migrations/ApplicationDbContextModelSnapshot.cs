﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.Entities.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("AppUsers");
                });

            modelBuilder.Entity("Domain.Entities.Bookings", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("BookingDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CheckInDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CheckOutDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("GuestId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoomId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("GuestId");

                    b.HasIndex("RoomId");

                    b.ToTable("Bookings");

                    b.HasData(
                        new
                        {
                            Id = new Guid("7d3155a2-95f8-4d9b-bc24-662ae053f1c9"),
                            BookingDate = new DateTime(2023, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CheckInDate = new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CheckOutDate = new DateTime(2023, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            GuestId = new Guid("c6c45f7c-2dfe-4c1e-9a9b-8b173c71b32c"),
                            RoomId = new Guid("a98b8a9d-4c5a-4a90-a2d2-5f1441b93db6")
                        },
                        new
                        {
                            Id = new Guid("efeb3d13-3dab-46c9-aa9a-9f22dd58e06e"),
                            BookingDate = new DateTime(2023, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CheckInDate = new DateTime(2023, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CheckOutDate = new DateTime(2023, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            GuestId = new Guid("aaf21a7d-8fc3-4c9f-8a8e-1eeec8dcd462"),
                            RoomId = new Guid("4e1cb3d9-bc3b-4997-a3d5-0c56cf17fe7a")
                        },
                        new
                        {
                            Id = new Guid("0bf4a177-98b8-4f67-8a56-95669c320890"),
                            BookingDate = new DateTime(2023, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CheckInDate = new DateTime(2023, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CheckOutDate = new DateTime(2023, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            GuestId = new Guid("f44c3eb4-2c8a-4a77-a31b-04c4619aa15a"),
                            RoomId = new Guid("c6898b7e-ee09-4b36-8b20-22e8c2a63e29")
                        });
                });

            modelBuilder.Entity("Domain.Entities.City", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CountryCode")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("CountryName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PostOffice")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            Id = new Guid("f9e85d04-548c-4f98-afe9-2a8831c62a90"),
                            CountryCode = "US",
                            CountryName = "United States",
                            Name = "New York",
                            PostOffice = "NYC"
                        },
                        new
                        {
                            Id = new Guid("8d2aeb7a-7c67-4911-aa2c-d6a3b4dc7e9e"),
                            CountryCode = "UK",
                            CountryName = "United Kingdom",
                            Name = "London",
                            PostOffice = "LDN"
                        },
                        new
                        {
                            Id = new Guid("3c7e66f5-46a9-4b8d-8e90-85b5a9e2c2fd"),
                            CountryCode = "JP",
                            CountryName = "Japan",
                            Name = "Tokyo",
                            PostOffice = "TKY"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Guest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("EmailAddress");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("Id");

                    b.ToTable("Guests");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c6c45f7c-2dfe-4c1e-9a9b-8b173c71b32c"),
                            Email = "john.doe@example.com",
                            FirstName = "John",
                            LastName = "Doe",
                            Phone = "1234567890"
                        },
                        new
                        {
                            Id = new Guid("aaf21a7d-8fc3-4c9f-8a8e-1eeec8dcd462"),
                            Email = "jane.smith@example.com",
                            FirstName = "Jane",
                            LastName = "Smith",
                            Phone = "2012345678"
                        },
                        new
                        {
                            Id = new Guid("f44c3eb4-2c8a-4a77-a31b-04c4619aa15a"),
                            Email = "hiroshi.tanaka@example.co.jp",
                            FirstName = "Hiroshi",
                            LastName = "Tanaka",
                            Phone = "312345678"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Hotels", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("FloorsNumber")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<float>("Rating")
                        .HasColumnType("real");

                    b.Property<string>("StreetAddress")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Hotels");

                    b.HasData(
                        new
                        {
                            Id = new Guid("98c2c9fe-1a1c-4eaa-a7f5-b9d19b246c27"),
                            CityId = new Guid("f9e85d04-548c-4f98-afe9-2a8831c62a90"),
                            Description = "A luxurious hotel with top-notch amenities.",
                            FloorsNumber = 10,
                            Name = "Luxury Inn",
                            OwnerId = new Guid("a1d1aa11-12e7-4e0f-8425-67c1c1e62c2d"),
                            Phone = "1234567890",
                            Rating = 4.5f,
                            StreetAddress = "123 Main Street"
                        },
                        new
                        {
                            Id = new Guid("bfa4173d-7893-48b9-a497-5f4c7fb2492b"),
                            CityId = new Guid("8d2aeb7a-7c67-4911-aa2c-d6a3b4dc7e9e"),
                            Description = "A cozy lodge nestled in the heart of nature.",
                            FloorsNumber = 3,
                            Name = "Cozy Lodge",
                            OwnerId = new Guid("a1d1aa11-12e7-4e0f-8425-67c1c1e62c2d"),
                            Phone = "2012345678",
                            Rating = 3.8f,
                            StreetAddress = "456 Oak Avenue"
                        },
                        new
                        {
                            Id = new Guid("9461e08b-92d3-45da-b6b3-efc0cfcc4a3a"),
                            CityId = new Guid("3c7e66f5-46a9-4b8d-8e90-85b5a9e2c2fd"),
                            Description = "A resort with breathtaking sunset views over the ocean.",
                            FloorsNumber = 5,
                            Name = "Sunset Resort",
                            OwnerId = new Guid("a1d1aa11-12e7-4e0f-8425-67c1c1e62c2d"),
                            Phone = "312345678",
                            Rating = 4.2f,
                            StreetAddress = "789 Beachfront Road"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Images", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EntityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EntityType")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("URL")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.HasKey("Id");

                    b.ToTable("Images");

                    b.HasData(
                        new
                        {
                            Id = new Guid("3a4b5c6d-7e8f-9a0b-1c2d-3a4b5c6d7e8f"),
                            EntityId = new Guid("bfa4173d-7893-48b9-a497-5f4c7fb2492b"),
                            EntityType = "Hotel",
                            Type = "Thumbnail",
                            URL = "https://images.bubbleup.com/width1920/quality35/mville2017/1-brand/1-margaritaville.com/gallery-media/220803-compasshotel-medford-pool-73868-1677873697.jpg"
                        },
                        new
                        {
                            Id = new Guid("1a2b3c4d-5e6f-7a8b-9c0d-1a2b3c4d5e6f"),
                            EntityId = new Guid("98c2c9fe-1a1c-4eaa-a7f5-b9d19b246c27"),
                            EntityType = "Hotel",
                            Type = "Exterior",
                            URL = "https://cf.bstatic.com/xdata/images/hotel/max1024x768/373326414.jpg?k=2ac575ebc4df5a8bb620431112286ece53fa83effc3bd20dbbecf869214d7057&o=&hp=1"
                        },
                        new
                        {
                            Id = new Guid("4a5b6c7d-8e9f-0a1b-2c3d-4a5b6c7d8e9f"),
                            EntityId = new Guid("9461e08b-92d3-45da-b6b3-efc0cfcc4a3a"),
                            EntityType = "Hotel",
                            Type = "Exterior",
                            URL = "https://d2rewpp8r4d4h2.cloudfront.net/compasshotel.com-1728563160/cms/cache/v2/6329c8f511d47.jpeg/406x242/fit/80/fb8dc39799ecad4199c47f4355fa0dc3.jpg"
                        },
                        new
                        {
                            Id = new Guid("2a3b4c5d-6e7f-8a9b-0c1d-2a3b4c5d6e7f"),
                            EntityId = new Guid("c6898b7e-ee09-4b36-8b20-22e8c2a63e29"),
                            EntityType = "Room",
                            Type = "Interior",
                            URL = "https://media.cnn.com/api/v1/images/stellar/prod/140127103345-peninsula-shanghai-deluxe-mock-up.jpg?q=w_2226,h_1449,x_0,y_0,c_fill"
                        },
                        new
                        {
                            Id = new Guid("0a1b2c3d-4e5f-6a7b-8c9d-0a1b2c3d4e5f"),
                            EntityId = new Guid("4e1cb3d9-bc3b-4997-a3d5-0c56cf17fe7a"),
                            EntityType = "Room",
                            Type = "Interior",
                            URL = "https://thumbs.dreamstime.com/b/hotel-room-beautiful-orange-sofa-included-43642330.jpg"
                        },
                        new
                        {
                            Id = new Guid("9a0b1c2d-3e4f-5a6b-7c8d-9a0b1c2d3e4f"),
                            EntityId = new Guid("a98b8a9d-4c5a-4a90-a2d2-5f1441b93db6"),
                            EntityType = "Room",
                            Type = "Interior",
                            URL = "https://www.shutterstock.com/image-photo/hotel-room-interior-modern-seaside-260nw-1387008533.jpg"
                        },
                        new
                        {
                            Id = new Guid("7a8b9c0d-1e2f-3a4b-5c6d-7a8b9c0d1e2f"),
                            EntityId = new Guid("f9e85d04-548c-4f98-afe9-2a8831c62a90"),
                            EntityType = "City",
                            Type = "Exterior",
                            URL = "https://i.natgeofe.com/k/5b396b5e-59e7-43a6-9448-708125549aa1/new-york-statue-of-liberty.jpg"
                        },
                        new
                        {
                            Id = new Guid("6a7b8c9d-0e1f-2a3b-4c5d-6a7b8c9d0e1f"),
                            EntityId = new Guid("8d2aeb7a-7c67-4911-aa2c-d6a3b4dc7e9e"),
                            EntityType = "City",
                            Type = "Exterior",
                            URL = "https://dynamic-media-cdn.tripadvisor.com/media/photo-o/15/33/f5/de/london.jpg?w=700&h=500&s=1"
                        },
                        new
                        {
                            Id = new Guid("5a6b7c8d-9e0f-1a2b-3c4d-5a6b7c8d9e0f"),
                            EntityId = new Guid("3c7e66f5-46a9-4b8d-8e90-85b5a9e2c2fd"),
                            EntityType = "City",
                            Type = "Exterior",
                            URL = "https://media.cntraveler.com/photos/63482b255e7943ad4006df0b/16:9/w_2560%2Cc_limit/tokyoGettyImages-1031467664.jpeg"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Owner", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("EmailAddress");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("Id");

                    b.ToTable("Owners");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a1d1aa11-12e7-4e0f-8425-67c1c1e62c2d"),
                            Email = "hantoli797@gmail.com",
                            FirstName = "Omar",
                            LastName = "Hantouli",
                            Phone = "0598191973"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Payments", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<Guid>("BookingId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Method")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Payments");

                    b.HasData(
                        new
                        {
                            Id = new Guid("7f5cc9f0-796f-498d-9f3f-9e5249a4f6ae"),
                            Amount = 1500.0,
                            BookingId = new Guid("0bf4a177-98b8-4f67-8a56-95669c320890"),
                            Method = "Credit Card",
                            Status = "Completed"
                        },
                        new
                        {
                            Id = new Guid("1c8d70bd-2534-4991-bddf-84c7edee1a79"),
                            Amount = 1200.0,
                            BookingId = new Guid("efeb3d13-3dab-46c9-aa9a-9f22dd58e06e"),
                            Method = "PayPal",
                            Status = "Pending"
                        },
                        new
                        {
                            Id = new Guid("8f974636-4f53-48d9-af99-2f7f1d3e0474"),
                            Amount = 2000.0,
                            BookingId = new Guid("7d3155a2-95f8-4d9b-bc24-662ae053f1c9"),
                            Method = "Bank Transfer",
                            Status = "Completed"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Reviews", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BookingId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<float>("Rating")
                        .HasColumnType("real");

                    b.Property<DateTime>("ReviewDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Reviews");

                    b.HasData(
                        new
                        {
                            Id = new Guid("63e4bb25-28b1-4fc4-9b93-9254d94dab23"),
                            BookingId = new Guid("0bf4a177-98b8-4f67-8a56-95669c320890"),
                            Comment = "Excellent service and comfortable stay!",
                            Rating = 4.8f,
                            ReviewDate = new DateTime(2023, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("85a5a0b4-0e04-4c46-b7ac-6cf609e4f2aa"),
                            BookingId = new Guid("efeb3d13-3dab-46c9-aa9a-9f22dd58e06e"),
                            Comment = "Friendly staff and great location.",
                            Rating = 4.5f,
                            ReviewDate = new DateTime(2023, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("192045db-c6db-49c9-aa6b-2e3d6c7f3b79"),
                            BookingId = new Guid("7d3155a2-95f8-4d9b-bc24-662ae053f1c9"),
                            Comment = "Clean rooms and beautiful views.",
                            Rating = 4.2f,
                            ReviewDate = new DateTime(2023, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Domain.Entities.Rooms", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AdultsCapacity")
                        .HasColumnType("int");

                    b.Property<int>("ChildrenCapacity")
                        .HasColumnType("int");

                    b.Property<Guid>("HotelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Rating")
                        .HasColumnType("real");

                    b.Property<Guid>("RoomTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("View")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.ToTable("Rooms");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a98b8a9d-4c5a-4a90-a2d2-5f1441b93db6"),
                            AdultsCapacity = 2,
                            ChildrenCapacity = 1,
                            HotelId = new Guid("98c2c9fe-1a1c-4eaa-a7f5-b9d19b246c27"),
                            Rating = 4.5f,
                            RoomTypeId = new Guid("5a5de3b8-3ed8-4f0a-bda9-cf73225a64a1"),
                            View = "City View"
                        },
                        new
                        {
                            Id = new Guid("4e1cb3d9-bc3b-4997-a3d5-0c56cf17fe7a"),
                            AdultsCapacity = 3,
                            ChildrenCapacity = 2,
                            HotelId = new Guid("bfa4173d-7893-48b9-a497-5f4c7fb2492b"),
                            Rating = 4.2f,
                            RoomTypeId = new Guid("d67ddbe4-1f1a-4d85-bcc1-ec3a475ecb68"),
                            View = "Ocean View"
                        },
                        new
                        {
                            Id = new Guid("c6898b7e-ee09-4b36-8b20-22e8c2a63e29"),
                            AdultsCapacity = 4,
                            ChildrenCapacity = 0,
                            HotelId = new Guid("9461e08b-92d3-45da-b6b3-efc0cfcc4a3a"),
                            Rating = 4.8f,
                            RoomTypeId = new Guid("4b4c0ea5-0b9a-4a20-8ad9-77441fb912d2"),
                            View = "Mountain View"
                        });
                });

            modelBuilder.Entity("Domain.Entities.RoomTypes", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("HotelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("PricePerNight")
                        .HasColumnType("real");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("RoomTypes");

                    b.HasData(
                        new
                        {
                            Id = new Guid("5a5de3b8-3ed8-4f0a-bda9-cf73225a64a1"),
                            HotelId = new Guid("98c2c9fe-1a1c-4eaa-a7f5-b9d19b246c27"),
                            PricePerNight = 100f,
                            Type = "Single"
                        },
                        new
                        {
                            Id = new Guid("d67ddbe4-1f1a-4d85-bcc1-ec3a475ecb68"),
                            HotelId = new Guid("bfa4173d-7893-48b9-a497-5f4c7fb2492b"),
                            PricePerNight = 150f,
                            Type = "Double"
                        },
                        new
                        {
                            Id = new Guid("4b4c0ea5-0b9a-4a20-8ad9-77441fb912d2"),
                            HotelId = new Guid("9461e08b-92d3-45da-b6b3-efc0cfcc4a3a"),
                            PricePerNight = 200f,
                            Type = "Suite"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Bookings", b =>
                {
                    b.HasOne("Domain.Entities.Guest", "Guest")
                        .WithMany("Bookings")
                        .HasForeignKey("GuestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Rooms", "Room")
                        .WithMany("Bookings")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Guest");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("Domain.Entities.Hotels", b =>
                {
                    b.HasOne("Domain.Entities.City", "City")
                        .WithMany("Hotels")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Owner", "Owner")
                        .WithMany("Hotels")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Domain.Entities.Rooms", b =>
                {
                    b.HasOne("Domain.Entities.Hotels", "Hotel")
                        .WithMany("Rooms")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("Domain.Entities.City", b =>
                {
                    b.Navigation("Hotels");
                });

            modelBuilder.Entity("Domain.Entities.Guest", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("Domain.Entities.Hotels", b =>
                {
                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("Domain.Entities.Owner", b =>
                {
                    b.Navigation("Hotels");
                });

            modelBuilder.Entity("Domain.Entities.Rooms", b =>
                {
                    b.Navigation("Bookings");
                });
#pragma warning restore 612, 618
        }
    }
}
