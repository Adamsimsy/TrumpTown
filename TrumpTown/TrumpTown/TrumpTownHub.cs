using Microsoft.AspNet.SignalR;

namespace TrumpTown
{
    public class TrumpTownHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}