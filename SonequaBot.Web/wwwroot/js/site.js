"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/sonequaBotHub").build();

//connection.on("ReceiveMessage", function (message) {
//    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
//    var encodedMsg = " says " + msg;
//    var li = document.createElement("li");
//    li.textContent = encodedMsg;
//    document.getElementById("messagesList").appendChild(li);
//});

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

connection.start().then(function () {
    
}).catch(function (err) {
    return console.error(err.toString());
});

function removeAlert() {
    document.getElementById("alertphp").style.display = "none";
    document.getElementById("alertdevastante").style.display = "none";
}