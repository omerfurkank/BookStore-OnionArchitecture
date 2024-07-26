using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authors.Commands.CreateRangeAuthor;
public class CreateRangeAuthorCommandRequest : IRequest<CreateRangeAuthorCommandResponse>
{
    public List<AuthorDto> Authors { get; set; }

    public string[] Roles => new[] { "admin" };
    public class AuthorDto
    {
        public string Name { get; set; }
    }
}



