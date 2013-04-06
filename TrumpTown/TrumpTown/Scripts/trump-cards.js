$().ready(function () {
    

    $("#high").click(function () {
        playTrumpCard("high");
    });

    $("#low").click(function () {
        playTrumpCard("low");
    });

    writeCardDetails();
});

function writeCardDetails() {

    var text = "<table style=\"padding-right:15px;\"><thead><tr><td style=\"padding-right:15px;font-weight:bold;\">Category</td><td style=\"padding-right:15px;font-weight:bold;\">Stat</td></tr></thead>";

    text = text + "<tr><td style=\"padding-right:15px;\"><input name=\"trump-radio\" value=\"0\" type=\"radio\" id=\"radio2\" /><label class=\"radio\" for=\"0\">Density</label></td><td>100,000</td></tr>";

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