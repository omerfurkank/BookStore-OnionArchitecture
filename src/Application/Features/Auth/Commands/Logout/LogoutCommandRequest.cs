using Application.Pipelines.Caching;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Logout;
public class LogoutCommandRequest : IRequest<LogoutCommandResponse>, ICacheRemovableRequest
{
    public string? Email { get; set; }
    public string CacheKey => string.Empty;
    public bool RemoveAllCache => true;
}
