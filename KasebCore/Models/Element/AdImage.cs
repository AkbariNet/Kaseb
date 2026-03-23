namespace KasebCore.Models.Element
{
    public class AdImage
    {
        public int Id { get; set; }

        public string ImagePath { get; set; }

        public int AdId { get; set; }

        public AdElementModel Ad { get; set; }
    }
}