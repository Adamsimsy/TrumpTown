﻿$(function () {
    

    $("#high").click(function () {
        playTrumpCard("high");
    });

    $("#low").click(function () {
        playTrumpCard("low");
    });

    $('#ready').click(function() {
        trumpTown.server.playerReady();
    });
});

function writeCardDetails(card) {

    var text = "<table style=\"padding-right:15px;\"><thead><tr><td style=\"padding-right:15px;font-weight:bold;\">Category</td><td style=\"padding-right:15px;font-weight:bold;\">Stat</td></tr></thead>";
    
    var counter = 0;
    for (var entry in card) {

        text = text + "<tr><td style=\"padding-right:15px;\"><input name=\"trump-radio\" value=\"" + entry + "\" type=\"radio\" id=\"radio" + counter++ +
            "\" /><label class=\"radio\" for=\"0\">" + entry + "</label></td><td>" + card[entry] + "</td></tr>";
    }
    text = text + "</table>";
    $("#open-data").html(text);
}


function playTrumpCard(selection) {

    var selected = $('input[name=trump-radio]:checked').val();


    if (selected == undefined) {
        $("#category-error").html("<p><strong>Please select a category!</strong></p>");
    }
    else {
        var selectedCategory = $('label[for=' + selected + ']').text();

        $("#category-played").html("<p>You played " + selectedCategory + " as " + selection + "!");
        $("#category-highlow").css("display", "none");
        $('input[name=trump-radio]').attr('disabled', true);
    }
}
