$(function () {
    // Declare a proxy to reference the hub. 
    var trumpTown = $.connection.trumpTownHub;

    trumpTown.client.OnJoined = function (name) {
        //Add username to list of people
    };

    trumpTown.client.OnPlayerReady = function (name) {
        //highlght user as ready to play
    };

    trumpTown.client.OnDeal = function () {
        // render the details of the card
        var mycard = trumpTown.server.Deal();
    };

    trumpTown.client.OnEndRound = function (cardId, user) {
        // if card and user match highlight as winner

        // enable ready button
    };

    // Start the connection.
    $.connection.hub.start().done(function () {
        // do registration of events here
        //$('#sendmessage').click(function () {
        //    // Call the Send method on the hub. 
        //    chat.server.send($('#displayname').val(), $('#message').val());
        //    // Clear text box and reset focus for next comment. 
        //    $('#message').val('').focus();
        //});

        //server calls to use in the right place
        trumpTown.server.JoinGame("username");

        trumpTown.server.Play("dataField", "dataValue", "higher/lower");

        trumpTown.server.OnPlayerReady();
    });
});