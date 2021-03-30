using System;
using System.Linq;
using System.Text.RegularExpressions;
using LiteDB;
using LiteDB.Engine;
using SonequaBot.Shared.Commands;
using SonequaBot.Shared.Commands.Interfaces.Responses;
using SonequaBot.Shared.Models;

namespace SonequaBot.Shared.Commands
{
    public class CommandGiveaway : CommandBase, IResponseMessage
    {
        // private LiteDatabase  _liteDb;
        //
        // public CommandGiveaway()
        // {
        //     var settings = new EngineSettings { Filename = "giveaway.db" };
        //     var db = new LiteEngine(settings);
        //     _liteDb = new LiteDatabase(db);   
        // }
        
        protected override string ActivationCommand => "!giveaway";

        public string GetMessageEvent(CommandSource source)
        {
            var settings = new EngineSettings { Filename = "giveaway.db" };
            var db = new LiteEngine(settings);
            var _liteDb = new LiteDatabase(db);   
            
            var all = _liteDb.GetCollection<Giveaway>().FindAll();
            var giveaway = all.LastOrDefault();

            var giveawayuser = new GiveawayUsers();
            giveawayuser.Username = source.User;
            giveawayuser.GiveawayId = giveaway.Id;

            _liteDb.GetCollection<GiveawayUsers>().Insert(giveawayuser);
            _liteDb.Checkpoint();
            
            _liteDb.Dispose();
            
            var message = $"{source.User} ti sei iscritto al prossimo giveaway.";
                 
            return message;
        }
    }
}