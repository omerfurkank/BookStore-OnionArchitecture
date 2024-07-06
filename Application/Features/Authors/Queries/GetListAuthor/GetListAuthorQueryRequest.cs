using Application.Pipelines.Caching;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authors.Queries.GetListAuthor;
public class GetListAuthorQueryRequest : IRequest<IList<GetListAuthorQueryResponse>>/*, ICacheableRequest*/
{
    public int Index { get; set; } = 0;
    public int Size { get; set; } = 10;

//    public string CacheKey => "GetListAuthors";

//    public double CacheTime => 60;
}

