using Infrastructure.SignalR.Hubs;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SignalR;
public static class ConfigureSignalR
{
    public static void ConfigureMapHubs(this WebApplication app)
    {
        app.MapHub<AuthorHub>("/author-hub");
    }
}
