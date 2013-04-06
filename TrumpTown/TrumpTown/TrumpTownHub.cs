using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using MongoDB.Bson;
using TrumpTown.DataAccess;
using TrumpTown.Models;
using MongoDB.Bson.IO;

namespace TrumpTown
{
    public class TrumpTownHub : Hub
    {
        private static List<string> _clients = new List<string>();
        private static List<string> _readyUsers = new List<string>();
        private static Dictionary<BsonValue, string> _userCardsInPlay = new Dictionary<BsonValue, string>();
        private static List<BsonDocument> _cardsInPlay = new List<BsonDocument>();
        private static MongoData _mongo = new MongoData();

        public void JoinGame()
        {
            var username = GetUserName();
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
            if (_clients.Count == 0)
            {
                Clients.Caller.WonLast = true;
            }
            _clients.Add(Context.ConnectionId);
            return base.OnConnected();
        }

        public void Deal()
        {
            var card = _mongo.GetRecord();
            var id = card["_id"];
            _userCardsInPlay.Add(id, Context.ConnectionId);
            _cardsInPlay.Add(card);

            var json = card.ToJson(new JsonWriterSettings { OutputMode = JsonOutputMode.Strict });

            Clients.Caller.OnCard(json);
        }

        public void Play(string dataField, bool compareLower)
        {
            if (Clients.Caller.WonLast)
            {
                // compare data, get winner
                var winningCard = Compare(dataField, compareLower);

                var winner = Clients.Client(_userCardsInPlay[winningCard]);

                winner.WonLast = true;
                Clients.All.OnEndRound(winningCard.ToJson(), winner.Username);
            }
        }

        private BsonValue Compare(string dataField, bool compareLower)
        {
            // get the field values from the cards
            var comparer = new DocComparer(dataField, compareLower);
            _cardsInPlay.Sort(comparer);
            return _cardsInPlay.FirstOrDefault()["_id"];
        }

        public void PlayerReady()
        {
            _readyUsers.Add(Context.ConnectionId);
            Clients.Others.OnPlayerReady(Clients.Caller.Username);

            if (_readyUsers.Count.Equals(_clients.Count))
            {
                Clients.All.OnDeal();

                _readyUsers.Clear();
            }
        }

        public string GetUserName()
        {
            return Context.User != null ? Context.User.Identity.Name : "";
        }
    }

    public class DocComparer : IComparer<BsonDocument>
    {
        private readonly string _field;
        private readonly bool _compareLower;

        public DocComparer(string field, bool compareLower)
        {
            _field = field;
            _compareLower = compareLower;
        }

        public int Compare(BsonDocument xDoc, BsonDocument yDoc)
        {
            if (xDoc[_field] > yDoc[_field])
                return _compareLower ? -1 : 1;

            if (xDoc[_field] < yDoc[_field])
                return _compareLower ? 1 : -1;

            return 0;
        }
    }
}
