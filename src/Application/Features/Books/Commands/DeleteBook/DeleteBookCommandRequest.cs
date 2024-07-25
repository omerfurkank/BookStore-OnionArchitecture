using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Books.Commands.DeleteBook;
public class DeleteBookCommandRequest : IRequest<DeleteBookCommandResponse>
{
    public int Id { get; set; }
}
