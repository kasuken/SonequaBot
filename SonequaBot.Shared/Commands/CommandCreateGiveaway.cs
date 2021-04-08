using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using LiteDB;
using LiteDB.Engine;
using SonequaBot.Shared.Commands;
using SonequaBot.Shared.Commands.Interfaces.Responses;
using SonequaBot.Shared.Models;

namespace SonequaBot.Shared.Commands
{
    public class CommandCreateGiveaway : CommandBase, IResponseMessage
    {
        // private LiteDatabase  _liteDb;
        
        public CommandCreateGiveaway()
        {
            // var settings = new EngineSettings { Filename = "giveaway.db" };
            // var db = new LiteEngine(settings);
            // _liteDb = new LiteDatabase(db);   
        }
        
        protected override string ActivationCommand => "#creategiveaway";

        public override bool IsActivated(CommandSource source)
        {
            if (source.User == "kasuken" && source.Message.StartsWith(ActivationCommand))
            {
                return true;
            }

            return false;
        }

        public string GetMessageEvent(CommandSource source)
        {
            var message = string.Empty;

            var name = source.Message.Split(" ");

            if (name[1] == "winner")
            {
                var winner = DrawWinner();

                message = $"Il vincitore del giveaway è: {winner}";
                return message;
            }

            var giveaway = new Giveaway();
            giveaway.Name = name[1];
            
            var settings = new EngineSettings { Filename = "giveaway.db" };
            var db = new LiteEngine(settings);
            var _liteDb = new LiteDatabase(db);   
            
            _liteDb.GetCollection<Giveaway>().Insert(giveaway);
            _liteDb.Checkpoint();
            
            _liteDb.Dispose();

            message = $"{source.User} hai creato il giveaway: {name[1]}";

            return message;
        }

        private string DrawWinner()
        {
            var settings = new EngineSettings { Filename = "giveaway.db" };
            var db = new LiteEngine(settings);
            var _liteDb = new LiteDatabase(db);   
            
            var giveaway = _liteDb.GetCollection<Giveaway>().FindAll().LastOrDefault();
            var users = _liteDb.GetCollection<GiveawayUsers>().FindAll().Where(c => c.GiveawayId == giveaway.Id).ToList();
            
            _liteDb.Dispose();
            
            var rnd = new Random();
            var r = rnd.Next(users.Count - 1);
            var winner = users[r].Username;
            
            return winner;
        }
    }
}