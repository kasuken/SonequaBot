using System;
using System.Threading.Tasks;
using TwitchLib.Api;
using TwitchLib.Client.Events;

namespace SonequaBot.Commands
{
    public class CommandUptime : AbstractCommand
    {
        protected new string Command = "!uptime";
        private TwitchAPI twitchAPI;

        public override string GetResponseMessage(TwitchAPI twitchApi, object sender, OnMessageReceivedArgs e)
        {
            twitchAPI = twitchApi;

            var upTime = GetUpTime().Result;
            return upTime?.ToString() ?? "Offline";
        }

        private async Task<string> GetUserId(string username)
        {
            var userList = await twitchAPI.V5.Users.GetUserByNameAsync(username);

            return userList.Matches[0].Id;
        }

        private async Task<TimeSpan?> GetUpTime()
        {
            var userId = await GetUserId(TwitchInfo.ChannelName);

            return await twitchAPI.V5.Streams.GetUptimeAsync(userId);
        }
    }
}