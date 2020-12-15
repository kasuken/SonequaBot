"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/sonequaBotHub").build();

var rootElement = document.getElementById('outputContainer');

connection.on("ReceiveCreateImage", function(url) {
    var el = document.createElement('img');
        el.setAttribute('class', 'alertgif');
        el.setAttribute('src', url);
        el.style.display = "block";

    rootElement.append(el);
    
    setTimeout(function() {
        el.remove();
    },5000)
});

connection.on("ReceiveCreateVideo", function(url) {

    var cont = document.createElement('video');
    cont.setAttribute('class', 'alertgif');
    cont.setAttribute('src', url);
    cont.style.display = "block";

    rootElement.append(cont);

    var el = document.createElement('source');
        el.setAttribute('src', url);
        
    cont.append(el);
    cont.play();
    
    setTimeout(function() {
        cont.remove();
    },5000)
});

connection.on("ReceiveCreateAudio", function(url) {
    var el = document.createElement('audio');
        el.setAttribute('src', url);
    
        rootElement.append(el);
    
    el.play();

    setTimeout(function() {
        el.remove();
    },5000)
});

connection.on("ReceiveSentiment", function (sentiment) {
    document.getElementById("sentimentImg").src = "/img/" + sentiment + ".png";
});

connection.on("ReceiveUserAppear", function (username) {

    var element = document.getElementById("userAppear");
    element.querySelector(".username").innerHTML = username;
    element.style.display = "block";

    setTimeout(function() {
        element.style.display = "none";
    }, 10000);
});

connection.start().then(function () {
    
}).catch(function (err) {
    return console.error(err.toString());
});