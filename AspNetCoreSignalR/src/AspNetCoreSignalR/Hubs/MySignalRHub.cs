using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Hubs;

namespace AspNetCoreSignalR.Hubs
{
    [HubName("updater")]
    public class MySignalRHub : Hub
    {
        public void NewUpdate(string command, double state)
        {
            Clients.Others.newUpdate(command, state);
        }
    }
}
