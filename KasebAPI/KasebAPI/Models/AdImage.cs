namespace KasebAPI.Models
{
    public class AdImage
    {
        public int Id { get; set; }

        public string ImagePath { get; set; }

        public int AdId { get; set; }

        public Ad Ad { get; set; }
    }
}