using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries.GetByIdUser;
public class GetByIdUserQueryRequest : IRequest<GetByIdUserQueryResponse>
{
    public int Id { get; set; }
}
