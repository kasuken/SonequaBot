using System;

namespace SonequaBot.Shared.Models
{
    public class Giveaway
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public DateTime DueDate { get; set; }
    }
}