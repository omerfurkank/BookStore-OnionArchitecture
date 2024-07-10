using Application.Pipelines.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries.GetListUser;
public class GetListUserQueryRequest : IRequest<IList<GetListUserQueryResponse>>, ISecuredRequest
{
    public int Index { get; set; } = 0;
    public int Size { get; set; } = 100;
    public string[] Roles => new[] { "admin" };
}
