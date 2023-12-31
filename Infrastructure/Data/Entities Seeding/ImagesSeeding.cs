using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Entities_Seeding
{
    public class ImagesSeeding
    {
        public static IEnumerable<Images> SeedData()
        {
            return new List<Images>
            {
                new Images
                {
                    Id = new Guid("3a4b5c6d-7e8f-9a0b-1c2d-3a4b5c6d7e8f"),
                    EntityId = new Guid("bfa4173d-7893-48b9-a497-5f4c7fb2492b"),
                    EntityType = EntityType.Hotel.ToString(),
                    URL = "https://images.bubbleup.com/width1920/quality35/mville2017/1-brand/1-margaritaville.com/gallery-media/220803-compasshotel-medford-pool-73868-1677873697.jpg",
                    Type = "Thumbnail"
                },
                new Images
                {
                    Id = new Guid("1a2b3c4d-5e6f-7a8b-9c0d-1a2b3c4d5e6f"),
                    EntityId = new Guid("98c2c9fe-1a1c-4eaa-a7f5-b9d19b246c27"),
                    EntityType = EntityType.Hotel.ToString(),
                    URL = "https://cf.bstatic.com/xdata/images/hotel/max1024x768/373326414.jpg?k=2ac575ebc4df5a8bb620431112286ece53fa83effc3bd20dbbecf869214d7057&o=&hp=1",
                    Type = "Exterior"
                },
                new Images
                {
                    Id = new Guid("4a5b6c7d-8e9f-0a1b-2c3d-4a5b6c7d8e9f"),
                    EntityId = new Guid("9461e08b-92d3-45da-b6b3-efc0cfcc4a3a"),
                    EntityType = EntityType.Hotel.ToString(),
                    URL = "https://d2rewpp8r4d4h2.cloudfront.net/compasshotel.com-1728563160/cms/cache/v2/6329c8f511d47.jpeg/406x242/fit/80/fb8dc39799ecad4199c47f4355fa0dc3.jpg",
                    Type = "Exterior"
                },
                new Images
                {
                    Id = new Guid("2a3b4c5d-6e7f-8a9b-0c1d-2a3b4c5d6e7f"),
                    EntityId = new Guid("c6898b7e-ee09-4b36-8b20-22e8c2a63e29"),
                    EntityType = EntityType.Room.ToString(),
                    URL = "https://media.cnn.com/api/v1/images/stellar/prod/140127103345-peninsula-shanghai-deluxe-mock-up.jpg?q=w_2226,h_1449,x_0,y_0,c_fill",
                    Type = "Interior"
                },
                new Images
                {
                    Id = new Guid("0a1b2c3d-4e5f-6a7b-8c9d-0a1b2c3d4e5f"),
                    EntityId = new Guid("4e1cb3d9-bc3b-4997-a3d5-0c56cf17fe7a"),
                    EntityType = EntityType.Room.ToString(),
                    URL = "https://thumbs.dreamstime.com/b/hotel-room-beautiful-orange-sofa-included-43642330.jpg",
                    Type = "Interior"
                },
                new Images
                {
                    Id = new Guid("9a0b1c2d-3e4f-5a6b-7c8d-9a0b1c2d3e4f"),
                    EntityId = new Guid("a98b8a9d-4c5a-4a90-a2d2-5f1441b93db6"),
                    EntityType = EntityType.Room.ToString(),
                    URL = "https://www.shutterstock.com/image-photo/hotel-room-interior-modern-seaside-260nw-1387008533.jpg",
                    Type = "Interior"
                },
                new Images
                {
                    Id = new Guid("7a8b9c0d-1e2f-3a4b-5c6d-7a8b9c0d1e2f"),
                    EntityId = new Guid("f9e85d04-548c-4f98-afe9-2a8831c62a90"),
                    EntityType = EntityType.City.ToString(),
                    URL = "https://i.natgeofe.com/k/5b396b5e-59e7-43a6-9448-708125549aa1/new-york-statue-of-liberty.jpg",
                    Type = "Exterior"
                },
                new Images
                {
                    Id = new Guid("6a7b8c9d-0e1f-2a3b-4c5d-6a7b8c9d0e1f"),
                    EntityId = new Guid("8d2aeb7a-7c67-4911-aa2c-d6a3b4dc7e9e"),
                    EntityType = EntityType.City.ToString(),
                    URL = "https://dynamic-media-cdn.tripadvisor.com/media/photo-o/15/33/f5/de/london.jpg?w=700&h=500&s=1",
                    Type = "Exterior"
                },
                new Images
                {
                    Id = new Guid("5a6b7c8d-9e0f-1a2b-3c4d-5a6b7c8d9e0f"),
                    EntityId = new Guid("3c7e66f5-46a9-4b8d-8e90-85b5a9e2c2fd"),
                    EntityType = EntityType.City.ToString(),
                    URL = "https://media.cntraveler.com/photos/63482b255e7943ad4006df0b/16:9/w_2560%2Cc_limit/tokyoGettyImages-1031467664.jpeg",
                    Type = "Exterior"
                }
            };
        }

    }
}
