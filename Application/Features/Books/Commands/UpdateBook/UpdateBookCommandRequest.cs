using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Books.Commands.UpdateBook;
public class UpdateBookCommandRequest : IRequest<UpdateBookCommandResponse>
{
    public int Id { get; set; }
    public int AuthorId { get; set; }
    public string Name { get; set; }
}
