"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/sonequaBotHub").build();

// generate random number  
const randomNumber = (min, max) => {  
    min = Math.ceil(min); 
    max = Math.floor(max); 
    return Math.floor(Math.random() * (max - min + 1)) + min; 
}  

connection.on("ReceiveDevastante", function() {
    document.getElementById("alertdevastante").style.display = "block";
    
    document.getElementById("sounddevastante").play();

    setTimeout(removeAlert, 5000);
});

connection.on("ReceiveDio", function() {
    document.getElementById("alertdio").style.display = "block";
    
    const random = randomNumber(1, 3);
    document.getElementById("sounddio").src = `/spfx/dio_${random}.mp3`;
    document.getElementById("sounddio").play();

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

connection.on("ReceiveKasu", function () {
    document.getElementById("alertkasu").style.display = "block";

    setTimeout(removeAlert, 5000);
});

connection.on("ReceiveDebug", function () {
    document.getElementById("alertdebug").style.display = "block";

    setTimeout(removeAlert, 5000);
});

connection.on("ReceiveExcel", function () {
    document.getElementById("alertexcel").style.display = "block";

    setTimeout(removeAlert, 5000);
});

connection.on("ReceiveSentiment", function (sentiment) {
    document.getElementById("sentimentImg").src = "/img/" + sentiment + ".png";
});

connection.on("ReceiveUserAppear", function (username) {

    debugger;
    var element = document.getElementById("userAppear");

    element.querySelector(".username").innerHTML = username;

    element.style.display = "block";

});

connection.on("ReceiveGren", function (sentiment) {
    document.getElementById("alertgren").style.display = "block";

    setTimeout(removeAlert, 5000);
});

connection.on("ReceivePaura", function() {
    document.getElementById("alertpaura").style.display = "block";
    document.getElementById("soundpaura").play();
  
    setTimeout(removeAlert, 5000);
});

connection.start().then(function () {
    
}).catch(function (err) {
    return console.error(err.toString());
});

function removeAlert() {
    var alerts = document.querySelectorAll(".alertgif");

    alerts.forEach((alert) => { alert.style.display = "none"; });
}