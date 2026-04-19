using System.Text.Json.Serialization;

namespace KasebCore.Models.Element
{
    public class AdImage
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("imagePath")]
        public string ImagePath { get; set; } = string.Empty;

        [JsonPropertyName("adId")]
        public int AdId { get; set; }
/*
        [JsonPropertyName("ad")]
        public AdElementModel Ad { get; set; }*/
    }
}