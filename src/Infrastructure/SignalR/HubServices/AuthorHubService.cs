using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.SignalR.HubServices;
using Infrastructure.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
namespace Infrastructure.SignalR.HubServices;
public class AuthorHubService : IAuthorHubService
{
    private readonly IHubContext<AuthorHub> _hubContext;

    public AuthorHubService(IHubContext<AuthorHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendMessage(string message)
    {
        await _hubContext.Clients.All.SendAsync("ReceiveMessage", message);

    }
}
