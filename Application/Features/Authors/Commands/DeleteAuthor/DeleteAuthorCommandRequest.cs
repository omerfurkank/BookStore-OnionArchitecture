using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authors.Commands.DeleteAuthor;
public class DeleteAuthorCommandRequest : IRequest<DeleteAuthorCommandResponse>
{
    public int Id { get; set; }
}
