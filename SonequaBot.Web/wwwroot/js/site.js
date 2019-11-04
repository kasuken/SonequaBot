"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/sonequaBotHub").build();

connection.on("ReceiveMessage", function (message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = " says " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.on("ReceiveDevastante", function() {
    document.getElementById("alertdevastante").style.display = "block";
    
    document.getElementById("sounddevastante").play();

    setTimeout(removeAlert, 5000);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

function removeAlert() {
    document.getElementById("alertdevastante").style.display = "none";
}