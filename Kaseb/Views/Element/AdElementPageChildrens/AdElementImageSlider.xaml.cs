using KasebCore.Models.Element;
using System.Collections.ObjectModel;

namespace Kaseb.Views.Element.AdElementPageChildrens;

public partial class AdElementImageSlider : ContentPage
{
    List<string> _imagePaths;
    public List<string> ImagePaths
    {
        get
        {
            return _imagePaths;
        }
        set
        {
            _imagePaths = value;
            ImageSlider.ItemsSource = _imagePaths;
        }
    }

    public AdElementImageSlider()
	{
		InitializeComponent();
    }


}