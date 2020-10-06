using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SonequaBot.Web.Hubs
{
    public class SonequaBotHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public async Task SendDevastante()
        {
            await Clients.All.SendAsync("ReceiveDevastante");
        }

        public async Task SendPhp()
        {
            await Clients.All.SendAsync("ReceivePhp");
        }

        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

        public async Task Sentiment (string sentiment)
        {
            await Clients.All.SendAsync("ReceiveSentiment", sentiment);
        }

        public async Task SentimentRealtime(string sentiment)
        {
            await Clients.All.SendAsync("ReceiveSentimentRealTime", sentiment);
        }

        public async Task GaugeSentiment(double sentiment)
        {
            await Clients.All.SendAsync("ReceiveGaugeSentiment", sentiment);
        }
    }
}
