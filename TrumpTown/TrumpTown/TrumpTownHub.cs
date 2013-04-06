using System;
using System.Collections.Generic;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace TrumpTown
{
    public class TrumpTownHub : Hub
    {
        private List<string> _clients = new List<string>();
        private List<HubConnectionContext> _readyUsers;
        private Dictionary<string, string> _cardsInPlay = new Dictionary<string, string>(); 
        private DataAccess.MongoData _mongo = new DataAccess.MongoData();
 
        public void JoinGame(string username)
        {
            Clients.Caller.username = username;
            Clients.Others.OnJoined(username);
        }

        public override System.Threading.Tasks.Task OnDisconnected()
        {
            Clients.Others.OnLeave(Clients.Caller.Username);
            _clients.Remove(Context.ConnectionId);
            return base.OnDisconnected();
        }
        
        public override System.Threading.Tasks.Task OnConnected()
        {
            _clients.Add(Context.ConnectionId);
 	         return base.OnConnected();
        }

        public string Deal()
        {
            var card = new Random().Next().ToString();
            
            var a = _mongo.GetRecord("515ff7a54f8165619b36efcb");
            _cardsInPlay.Add(a.Id.ToString(), Context.ConnectionId);

            return card;
        }

        public void Play(string dataField, string dataValue, bool compareLower)
        {
            if (Clients.Caller.WonLast)
            {
                // compare data, get winner
                var winningCard = Compare(dataField, dataValue, compareLower);

                var winner = Clients.Client(_cardsInPlay[winningCard]);

                winner.WonLast = true;
                Clients.All.OnEndRound(winningCard, winner.Username);
            }
        }

        private string Compare(string dataField, string dataValue, bool compareLower)
        {
            return null;
        }
        
        public void PlayerReady()
        {
            _readyUsers.Add(Clients.Caller);
            Clients.Others.OnPlayerReady(Clients.Caller.Username);

            if (_readyUsers.Count.Equals(_clients.Count))
                Clients.All.OnDeal();

            _readyUsers.Clear();
        }
    }
}