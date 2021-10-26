using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SonequaBot.Shared.Commands;
using Xunit;

namespace SonequaBot.Sentiment.Tests.WikiRandomCommand
{
    public class WikiRandomCommandTest
    {
        [Fact]
        public void Command_Should_Run()
        {
            var source = new CommandSource
            {
                Channel = "Test channel",
                Message = "!wikirandom",
                User = "Defkon1"
            };

            var commandProvider = new CommandWikiRandom();
            Assert.NotNull(commandProvider);

            var cardData = commandProvider.GetImageCardEvent(source);
            Assert.NotNull(cardData);

            Assert.NotEqual(string.Empty, cardData.Title);
            Assert.NotEqual(string.Empty, cardData.Description);
            //// Assert.NotEqual(string.Empty, cardData.ImageUrl); // image can be empty if no thumbnail is provided from Wikipedia
            Assert.NotEqual(string.Empty, cardData.Url);
        }
    }
}
