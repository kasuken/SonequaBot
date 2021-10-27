namespace SonequaBot.Shared.Commands
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Newtonsoft.Json.Linq;
    using SonequaBot.Shared.Commands.DTO;
    using SonequaBot.Shared.Commands.Interfaces;
    using SonequaBot.Shared.Commands.Interfaces.Responses;
    
    public class CommandChuckNorris : CommandBase, IResponseImageCard
    {
        private const string ENDPOINT = "https://api.chucknorris.io/jokes/random";

        protected override CommandActivationComparison ActivationComparison => CommandActivationComparison.Contains;

        protected override string ActivationCommand => "!chucknorris";
        
        public ImageCardData GetImageCardEvent(CommandSource source)
        {
            ImageCardData result = new ImageCardData()
            {
                Title = $"Hey, {source.User}!",
                Description = "If any one of Chuck Norris' sextapes was ever released publicly, it would win the Best Picture Oscar",
                ImageUrl = "https://assets.chucknorris.host/img/avatar/chuck-norris.png",
                Url = "https://api.chucknorris.io/jokes/AONc06lISwSZVjqMW2gO3Q"
            };

            try
            {
                string json = this.GetPage().Result;

                if (!string.IsNullOrEmpty(json))
                {
                    JObject fact = JObject.Parse(json);
                    result.Title = string.IsNullOrEmpty(source?.User) ? "Hey, you!" : $"Hey, {source.User}!";
                    result.Description = fact["value"]?.ToString() ?? string.Empty;
                    result.ImageUrl = fact["icon_url"]?.ToString() ?? string.Empty;
                    result.Url = fact["url"]?.ToString() ?? string.Empty;
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