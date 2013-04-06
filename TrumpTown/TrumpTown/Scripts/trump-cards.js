$().ready(function () {
    $("tr:odd").css("background-color", "#bbbbff");

    $("#high").click(function () {
        playTrumpCard("high");
    });

    $("#low").click(function () {
        playTrumpCard("low");
    });
});



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