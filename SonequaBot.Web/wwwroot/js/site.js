"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/sonequaBotHub").build();

connection.on("ReceiveDevastante", function() {
    document.getElementById("alertdevastante").style.display = "block";
    
    document.getElementById("sounddevastante").play();

    setTimeout(removeAlert, 5000);
});

connection.on("ReceivePhp", function () {
    document.getElementById("alertphp").style.display = "block";

    setTimeout(removeAlert, 5000);
});

connection.on("ReceiveSentiment", function (sentiment) {
    document.getElementById("sentimentImg").src = "/img/" + sentiment + ".png";
});

connection.on("ReceiveSentimentRealtime", function (sentiment) {
    document.getElementById("sentimentImg").src = "/img/" + sentiment + ".png";
});

connection.start().then(function () {
    
}).catch(function (err) {
    return console.error(err.toString());
});

function removeAlert() {
    document.getElementById("alertphp").style.display = "none";
    document.getElementById("alertdevastante").style.display = "none";
}