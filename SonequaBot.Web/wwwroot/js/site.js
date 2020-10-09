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

connection.on("ReceiveFriday", function () {
    document.getElementById("alertfriday").style.display = "block";

    setTimeout(removeAlert, 5000);
});

connection.on("ReceiveDisagio", function () {
    document.getElementById("alertdisagio").style.display = "block";

    setTimeout(removeAlert, 5000);
});

connection.on("ReceiveExcel", function () {
    document.getElementById("alertexcel").style.display = "block";

    setTimeout(removeAlert, 5000);
});

connection.on("ReceiveSentiment", function (sentiment) {
    document.getElementById("sentimentImg").src = "/img/" + sentiment + ".png";
});

connection.on("ReceiveGren", function (sentiment) {
    document.getElementById("alertgren").style.display = "block";

    setTimeout(removeAlert, 5000);
});

connection.start().then(function () {
    
}).catch(function (err) {
    return console.error(err.toString());
});

function removeAlert() {
    document.querySelectorAll(".alertgif").forEach(function(item) {
        item.style.display = "none";
    });
}