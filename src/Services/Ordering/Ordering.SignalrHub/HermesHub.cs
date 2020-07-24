using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Ordering.SignalrHub
{
    [Authorize]
    public class HermesHub : Hub
    {
        
    }
}