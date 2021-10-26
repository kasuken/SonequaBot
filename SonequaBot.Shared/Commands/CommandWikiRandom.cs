using Newtonsoft.Json.Linq;
using SonequaBot.Shared.Commands.DTO;
using SonequaBot.Shared.Commands.Interfaces.Responses;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace SonequaBot.Shared.Commands
{
    public class CommandWikiRandom: CommandBase, IResponseImageCard
    {
        private const string ENDPOINT = "https://it.wikipedia.org/api/rest_v1/page/random/summary";

        protected override string ActivationCommand => "!wikirandom";
        
        public ImageCardData GetImageCardEvent(CommandSource source)
        {
            ImageCardData result = new ImageCardData()
            {
                Title = "Rick Astley",
                Description = "Richard Paul Astley (Newton-le-Willows, 6 febbraio 1966) è un cantautore, musicista e conduttore radiofonico britannico.",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/6d/Rick_Astley_-_Pepsifest_2009.jpg/200px-Rick_Astley_-_Pepsifest_2009.jpg"
            };

            try
            {
                string json = this.GetPage().Result;

                if (!string.IsNullOrEmpty(json))
                {
                    JObject wikiPage = JObject.Parse(json);
                    result.Title = wikiPage["displaytitle"]?.ToString() ?? string.Empty;
                    result.Description = wikiPage["extract"]?.ToString() ?? string.Empty;
                    result.ImageUrl = wikiPage["thumbnail"]?["source"]?.ToString() ?? string.Empty;
                    result.Url = wikiPage["content_urls"]?["desktop"]?["page"]?.ToString() ?? string.Empty;
                }
            }
            catch (Exception ex)
            {
                // external CommandHandler is catching exceptions
                throw;
            }

            return result;
        }
        
        private async Task<string> GetPage()
        {
            string json = string.Empty;

            try
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(ENDPOINT);
                    HttpContent responseContent = response.Content;
                    using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
                    {
                        json = await reader.ReadToEndAsync();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return json;
        }
    }
}