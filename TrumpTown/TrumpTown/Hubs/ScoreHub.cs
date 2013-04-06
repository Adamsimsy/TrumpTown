using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace TrumpTown.Hubs
{
    public class ScoreHub : Hub
    {
        private static ScoreTable _table = new ScoreTable();

        public ScoreHub()
        {
            //Simple intialiser for getting the signals working. Need to get stuff from Mongo
            if (_table.Scores.Count < 1)
            {
                _table.Scores.Add(new PlayerScore() { Username = "Username 1", Score = 0 });
                _table.Scores.Add(new PlayerScore() { Username = "Username 2", Score = 0 });
                _table.Scores.Add(new PlayerScore() { Username = "Username 3", Score = 0 });
            }
        }

        //probably not need. Would be invoked by logic in card hub
        public void RecordPlayerWin(string username)
        {
            _table.RecordPlayerWin(username);
            Clients.All.GetUpdatedScoreBoard();
        }

        public void GetScores()
        {
            var json = new JavaScriptSerializer().Serialize(_table);
            Clients.All.OnScores(json);
        }
    }

    public class ScoreTable
    {
        public ScoreTable()
        {
            Scores = new List<PlayerScore>();
        }

        public List<PlayerScore> Scores { get; set; }

        public void RecordPlayerWin(string username)
        {
            var player = Scores.Where(x => x.Username == username).First();

            if (player != null)
            {
                player.Score += 10;
            }
            Scores = Scores.OrderByDescending(x => x.Score).ToList();
        }
    }

    public class PlayerScore
    {
        public string Username { get; set; }
        public int Score { get; set; }
    }
}