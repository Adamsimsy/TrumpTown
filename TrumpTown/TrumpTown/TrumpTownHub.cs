using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using MongoDB.Bson;
using TrumpTown.DataAccess;

namespace TrumpTown
{
    public class TrumpTownHub : Hub
    {
        private List<string> _clients = new List<string>();
        private List<HubConnectionContext> _readyUsers;
        private Dictionary<BsonValue, string> _cardsInPlay = new Dictionary<BsonValue, string>(); 
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
            _cardsInPlay.Add(a.Id, Context.ConnectionId);

            return card;
        }

        public void Play(string dataField, bool compareLower)
        {
            if (Clients.Caller.WonLast)
            {
                // compare data, get winner
                var winningCard = Compare(dataField, compareLower);

                var winner = Clients.Client(_cardsInPlay[winningCard]);

                winner.WonLast = true;
                Clients.All.OnEndRound(winningCard, winner.Username);
            }
        }

        private BsonValue Compare(string dataField, bool compareLower)
        {
            // get the field values from the cards
            var cards = _mongo.GetByIds(_cardsInPlay.Keys).ToList();
            var comparer = new DocComparer(dataField, compareLower);
            cards.Sort(comparer);
            return cards.FirstOrDefault().Id;
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

    public class DocComparer: IComparer<MongoData.TrumpCard>
    {
        private string field;
        private bool compareLower;

        public DocComparer(string field, bool compareLower)
        {
            this.field = field;
            this.compareLower = compareLower;
        }

        public int Compare(MongoData.TrumpCard x, MongoData.TrumpCard y)
        {
            var xDoc = x.ToBsonDocument();
            var yDoc = y.ToBsonDocument();

            if (xDoc[field] > yDoc[field])
                return compareLower ? -1 : 1;

            if (xDoc[field] < yDoc[field])
                return compareLower ? 1 : -1;

            return 0;
        }
    }
}