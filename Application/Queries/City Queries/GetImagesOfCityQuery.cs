using Application.DTOs;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class GetImagesOfCityQuery : IRequest<List<ImageDto>>
    {
        public Guid CityId { get; set; }
    }
}
