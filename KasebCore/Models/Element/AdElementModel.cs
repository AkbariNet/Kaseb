using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KasebCore.Models.Element 
{
    public class AdElementModel : INotifyPropertyChanged
    {
        [JsonPropertyName("author")]
        public string? Author { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("category")]
        public Category? Category { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("content")]
        public string? Content { get; set; }

        [JsonPropertyName("city")]
        public string? City { get; set; }

        [JsonPropertyName("images")]
        public List<AdImage>? Images { get; set; }

    
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        [JsonPropertyName("date")]
        public string? Date { get; set; }

        [JsonPropertyName("phone")]
        public string? Phone { get; set; }

        [JsonPropertyName("price")]
        public string? Price { get; set; }

        [JsonPropertyName("IsUrgent")]
        public bool IsUrgent { get; set; }

        [JsonPropertyName("IsUrgentRequest")]
        public bool IsUrgentRequest { get; set; }

        [JsonPropertyName("ValueOfWeighKG")]
        public string? ValueOfWeighKG { get; set; }

        [JsonPropertyName("ValueOfTag1")]
        public string? ValueOfTag1 { get; set; }

        [JsonPropertyName("ValueOfTag2")]
        public string? ValueOfTag2 { get; set; }

        [JsonPropertyName("NonCash")]
        public bool NonCash { get; set; }

        [JsonPropertyName("SomeOfCashMostPayed")]
        public bool SomeOfCashMostPayed { get; set; }

        [JsonPropertyName("MonthForNonCash")]
        public int MonthForNonCash { get; set; }  // حداکثر 120

        [JsonPropertyName("Latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("Longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("InventoryGuarantee")]
        public int InventoryGuarantee { get; set; }  // 1 تا 30 }


        [JsonPropertyName("ImagePaths")]
        public List<string> ImagePaths { get; set; } = new();

        public AdElementModel Clone()
        {
            return new AdElementModel
            {
                Author = this.Author,
                Id = this.Id,
                Category = this.Category, // اگر Category خودش یک object هست، بهتره اونم Clone بشه
                Title = this.Title,
                Content = this.Content,
                City = this.City,
                Images = this.Images != null ? new List<AdImage>(this.Images) : null, // کپی لیست
                ImagePaths= this.ImagePaths != null ? new List<string>(this.ImagePaths) : new List<string>(), // کپی لیست
                Date = this.Date,
                Phone = this.Phone,
                Price = this.Price,
                IsUrgent = this.IsUrgent,
                IsUrgentRequest = this.IsUrgentRequest,
                ValueOfWeighKG = this.ValueOfWeighKG,
                ValueOfTag1 = this.ValueOfTag1,
                ValueOfTag2 = this.ValueOfTag2,
                NonCash = this.NonCash,
                SomeOfCashMostPayed = this.SomeOfCashMostPayed,
                MonthForNonCash = this.MonthForNonCash,
                Latitude = this.Latitude,
                Longitude = this.Longitude,
                InventoryGuarantee = this.InventoryGuarantee
            };
        }

    }
    public enum Category
    {
        IsNull,
        Garlic,
        Shallot,
        Walnut,
        Potato,
        Cucumber,
        Tomato,
        Mushroom,
        Almond,

    }
}
