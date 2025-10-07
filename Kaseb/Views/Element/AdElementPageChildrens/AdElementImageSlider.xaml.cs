namespace Kaseb.Views.Element.AdElementPageChildrens;

public partial class AdElementImageSlider : Grid
{
	public AdElementImageSlider()
	{
		InitializeComponent();
        ImageSlider.ItemsSource = images;
    }
    public List<string> images { get; set; }


}