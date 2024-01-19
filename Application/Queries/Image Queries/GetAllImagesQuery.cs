using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Image_Queries
{
    public class GetAllImagesQuery : IRequest<List<ImageDto>>
    {
    }
}
