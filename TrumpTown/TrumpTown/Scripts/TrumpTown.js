$(function () {
    // Declare a proxy to reference the hub. 
    trumpTown = $.connection.trumpTownHub;
    trumpTownScore = $.connection.scoreHub;

    trumpTownScore.client.GetUpdatedScoreBoard = function () {
        trumpTownScore.server.getScores();
    };

    trumpTownScore.client.OnScores = function (scores) {
        var scoresJson= JSON.parse(scores);
        var text = "<table><thead style=\"font-weight:bold;\"><td style=\"padding-right:15px;font-weight:bold;\">Position</td><td style=\"padding-right:15px;font-weight:bold;\">Username</td><td style=\"font-weight:bold;\">Score</td></thead>";

        var counter = 1;
        $.each(scoresJson.Scores, function (i, item) {
            text = text + "<tr><td>" + counter++ + "</td><td>" + item.Username + "</td><td>" + item.Score + "</td></tr>";
        });

        text = text + "</table>";

        $("#scores").html(text);
    };

    trumpTown.client.OnJoined = function (name) {
        console.log(name + " has joined the game");
    };

    trumpTown.client.OnLeave = function (name) {
        console.log(name + " has left the game");
    }
    
    trumpTown.client.OnPlayerReady = function (name) {
        console.log(name + " is ready to play");
    };

    trumpTown.client.OnDeal = function () {
        // render the details of the card
        trumpTown.server.deal();
    };



    trumpTown.client.OnCard = function(card) {
        var cardData = JSON.parse(card);
        console.log(cardData);
        writeCardDetails(cardData);
    };

    trumpTown.client.OnEndRound = function (cardId, user) {
        // if card and user match highlight as winner
        
        // enable ready button
    };


    // Set initial focus to message input box.  
    //$('#message').focus();
    // Start the connection.
    $.connection.hub.start().done(function () {

        trumpTown.server.joinGame();

        //Need to initially get the scores
        trumpTownScore.server.getScores();
    });
});