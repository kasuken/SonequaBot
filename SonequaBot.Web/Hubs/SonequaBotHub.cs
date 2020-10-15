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

        public async Task SendDio()
        {
            await Clients.All.SendAsync("ReceiveDio");
        }

        public async Task SendPhp()
        {
            await Clients.All.SendAsync("ReceivePhp");
        }

        public async Task SendFriday()
        {
            await Clients.All.SendAsync("ReceiveFriday");
        }

        public async Task SendDisagio()
        {
            await Clients.All.SendAsync("ReceiveDisagio");
        }

        public async Task SendAnsia()
        {
            await Clients.All.SendAsync("ReceiveAnsia");
        }

        public async Task SendAccompagnare()
        {
            await Clients.All.SendAsync("ReceiveAccompagnare");
        }

        public async Task SendMerda()
        {
            await Clients.All.SendAsync("ReceiveMerda");
        }

        public async Task SendKasu()
        {
            await Clients.All.SendAsync("ReceiveKasu");
        }

        public async Task SendExcel()
        {
            await Clients.All.SendAsync("ReceiveExcel");
        }
        
        public async Task SendGren()
        {
            await Clients.All.SendAsync("ReceiveGren");
        }
        
        public async Task SendPaura()
        {
            await Clients.All.SendAsync("ReceivePaura");
        }

        public async Task SendDebug()
        {
            await Clients.All.SendAsync("ReceiveDebug");
        }

        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

        public async Task Sentiment (string sentiment)
        {
            await Clients.All.SendAsync("ReceiveSentiment", sentiment);
        }

        public async Task GaugeSentiment(double sentiment)
        {
            await Clients.All.SendAsync("ReceiveGaugeSentiment", sentiment);
        }
    }
}
