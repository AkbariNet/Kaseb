using Kaseb.Services;
using Kaseb.ViewModels;
using Kaseb.ViewModels.Element;
using Kaseb.Views.Element.AdElementPageChildrens;
using KasebCore.Models.Element;
using Microsoft.Maui.Maps;
using UraniumUI.Pages;

namespace Kaseb.Views.Element;

public partial class AdElementPage : Grid
{
	public AdElementPage()
	{
		InitializeComponent(); InitMap();

    }

    private void Button_Clicked(object sender, EventArgs e)
    {
		PageLoader.removeOverlay();
    }

    private void CallButton_Clicked(object sender, EventArgs e)
    {
            CallNumberAsync(ViewModel.Phone);
        
    }

    internal AdElementVM ViewModel => this.BindingContext as AdElementVM;
    public async Task CallNumberAsync(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return;

        try
        {
            var uri = new Uri($"tel:{phoneNumber}");
            await Launcher.OpenAsync(uri);
        }
        catch (Exception ex)
        {
            // می‌تونی اینجا مثلاً پیام خطا نشون بدی
            await Application.Current.MainPage.DisplayAlert("خطا", "شماره تماس معتبر نیست یا دستگاه از تماس پشتیبانی نمی‌کند.", "باشه");
        }
    }

    public void InitMap()
    {
        try
        {
            if (ViewModel?.Model.Latitude != null && ViewModel?.Model.Longitude != null)
            {
                var location = new Location(ViewModel.Model.Latitude, ViewModel.Model.Longitude);
                var span = MapSpan.FromCenterAndRadius(location, Distance.FromKilometers(2));
                MapAd.MoveToRegion(span);

            }
        }
        catch (Exception)
        {

        }
    }
    
    public bool isDetailVisible = false;
    private void DetailButton_Clicked(object sender, EventArgs e)
    {
        if (!isDetailVisible)
        {
            bottomSheet.IsVisible = true;
            isDetailVisible = true;
        }

        else
        {
            bottomSheet.IsVisible = false;
            isDetailVisible = false;
        }
    }

    private void ImageView_Tapped(object sender, EventArgs e)
    {
        AdElementImageSlider views = new AdElementImageSlider();
        views.ImagePaths = ViewModel.ImageLinks;
        PageLoader.includeOverlay(null,views);

    }
}