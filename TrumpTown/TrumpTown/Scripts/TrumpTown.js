$(function () {
    // Declare a proxy to reference the hub. 
    trumpTown = $.connection.trumpTownHub;

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
        var userName = $('#username').val();
        if (!!userName) {
            //trumpTown.server.joinGame(userName);
        }
    });
});