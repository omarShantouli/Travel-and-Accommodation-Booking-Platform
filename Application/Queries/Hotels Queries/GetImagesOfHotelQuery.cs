using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class GetImagesOfHotelQuery : IRequest<List<ImageDto>>
    {
        public Guid HotelId { get; set; }
    }
}
