using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Image_Commands
{
    public class CreateImageCommand : IRequest
    {
        public ImageDto Image { get; set; }
    }
}
