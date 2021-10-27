using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SonequaBot.Shared.Commands;
using Xunit;

namespace SonequaBot.Sentiment.Tests.ChuckNorrisCommand
{
    public class ChuckNorrisCommandTest
    {
        [Fact]
        public void Command_Should_Run()
        {
            var source = new CommandSource
            {
                Channel = "Test channel",
                Message = "!chucknorris",
                User = "Defkon1"
            };

            var commandProvider = new CommandChuckNorris();
            Assert.NotNull(commandProvider);

            var cardData = commandProvider.GetImageCardEvent(source);
            Assert.NotNull(cardData);

            Assert.NotEqual(string.Empty, cardData.Title);
            Assert.Equal("Hey, Defkon1!", cardData.Title);
            Assert.NotEqual(string.Empty, cardData.Description);
            Assert.NotEqual(string.Empty, cardData.ImageUrl);
            Assert.NotEqual(string.Empty, cardData.Url);
        }
    }
}
