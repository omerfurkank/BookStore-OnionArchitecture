using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authors.Commands.UpdateAuthor;
public class UpdateAuthorCommandRequest : IRequest<UpdateAuthorCommandResponse>
{
    public int Id { get; set; }
    public string Name { get; set; }
}
