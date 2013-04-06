using Microsoft.AspNet.SignalR;

namespace TrumpTown
{
    public class TrumpTownHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        public void Send(string name, string message)
        {
            TrumpTown.DataAccess.MongoData mongo = new TrumpTown.DataAccess.MongoData();

            var a = mongo.GetRecord("515ff7a54f8165619b36efcb");

            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(name, a.GorName);
        }
    }
}