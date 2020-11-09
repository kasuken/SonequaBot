using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SonequaBot.Web.Hubs
{
    public class SonequaBotHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public async Task SendTask(string message, string detail = "default")
        {
            string receivedTask = message.Replace("Send", "Receive");

            if (detail == "default")
            {
                await Clients.All.SendAsync(receivedTask);
            }
            else
                await Clients.All.SendAsync(receivedTask, detail);
        }
    }
}
