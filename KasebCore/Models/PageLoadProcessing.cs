namespace KasebCore.Models
{
    public static class PageLoadProcessing
    {
        public static PageState MenuStatement {  get; set; } = PageState.ad;
        public static bool isAdButtonClicked {  get; set; }
        public static bool isAddAdButtonClicked { get; set; }
        public static bool isLocationButtonClicked { get; set; }

        public enum PageState
        {
            ad,
            location,
            addAd
        }
    }
}
