using Kaseb.Services;
using Kaseb.ViewModels;
using Kaseb.Views.Element.AdElementPageChildrens;
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

    internal AddAdsVM ViewModel => this.BindingContext as AddAdsVM;
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
        var location = new Location(ViewModel.Latitude, ViewModel.Longitude);
        var span = MapSpan.FromCenterAndRadius(location, Distance.FromKilometers(2));
        MapAd.MoveToRegion(span);
    }

    public bool isDetailVisible
    {
        get;
        set
        {

        }
    }
    private void DetailButton_Clicked(object sender, EventArgs e)
    {
        if (isDetailVisible) bottomSheet.IsVisible = false;
        else bottomSheet.IsVisible = true;

        isDetailVisible=!isDetailVisible;
    }

    private void ImageView_Tapped(object sender, EventArgs e)
    {
        AdElementImageSlider views = new AdElementImageSlider();
        views.images = ViewModel.Images;
        GridOfImages.Children.Add(views);
        GridOfImages.IsVisible = true;

    }
}