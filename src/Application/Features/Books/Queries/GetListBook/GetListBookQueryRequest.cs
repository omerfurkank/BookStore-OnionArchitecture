using Application.Features.Books.Queries.GetByIdBook;
using Application.Pipelines.Auth;
using Application.Pipelines.Caching;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Books.Queries.GetListBook;
public class GetListBookQueryRequest : IRequest<IList<GetListBookQueryResponse>>, ISecuredRequest/*, ICacheableRequest*/
{
    public int Index { get; set; } = 0;
    public int Size { get; set; } = 100;

    public string[] Roles => new[] { "user" };

    //public string CacheKey => "GetListBooks";

    //public double CacheTime => 60;
}
