using Application.Features.Books.Queries.GetByIdBook;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Books.Queries.GetListBook;
public class GetListBookQueryRequest : IRequest<IList<GetListBookQueryResponse>>
{
    public int Index { get; set; }
    public int Size { get; set; }
}
