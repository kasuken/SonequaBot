using System;

namespace SonequaBot.Shared.Commands.DTO
{
    public class ImageCardData
    {
        public ImageCardData()
        {
            this.Title = string.Empty;
            this.Description = string.Empty;
            this.ImageUrl = string.Empty;
            this.Url = string.Empty;
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string Url { get; set; }
    }
}
