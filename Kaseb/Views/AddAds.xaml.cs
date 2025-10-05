using Kaseb.Services;
using Kaseb.Services.AdService;
using Kaseb.ViewModels;
using Kaseb.Views.Element;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Devices.Sensors;
using System.ComponentModel;
using System.Linq;
namespace Kaseb.Views
{
    public partial class AddAds : Grid
    {

        internal AddAdsVM ViewModel => this.BindingContext as AddAdsVM;
        public  AddAds()
        {
            InitializeComponent();
            ViewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(ViewModel.IsColletionSelectionVisible))
                {
                    if (ViewModel.IsColletionSelectionVisible) ShowWithFade(CollectionSelection);

                    else HideWithFade(CollectionSelection);
                }
            };
        }

        bool AnimationReserved=false;

        private async Task ShowWithFade(View view)
        {
            // اول بذار پایین صفحه (مثلاً 200 پیکسل پایین‌تر از جاش)
            view.TranslationY = 200;
            view.Opacity = 0;
            view.IsVisible = true;

            // انیمیشن ظاهر شدن: بیاد بالا + fade in
            await Task.WhenAll(
                view.TranslateTo(0, 0, 400, Easing.CubicOut),
                view.FadeTo(1, 400, Easing.CubicInOut)
            );
        }

        private async Task HideWithFade(View view)
        {
            // انیمیشن محو شدن و رفتن به پایین
            await Task.WhenAll(
                view.TranslateTo(0, 200, 400, Easing.CubicIn),
                view.FadeTo(0, 400, Easing.CubicInOut)
            );

            view.IsVisible = false;                 // بعد پنهانش کن
        }


        private async void NextButton_Clicked(object sender, EventArgs e)
        {
            if (Section1.IsVisible && !AnimationReserved)
            {
                AnimationReserved = true;
                await SlideSections(Section1, Section2, true);
                SectionBackButton.IsVisible = true;
            }
            else if (Section2.IsVisible && !AnimationReserved)
            {
                AnimationReserved = true;
                await SlideSections(Section2, Section3, true);
                SectionBackButton.IsVisible = true;
            }
            else if (Section3.IsVisible && !AnimationReserved)
            {
                AnimationReserved = true;
                await SlideSections(Section3, Section4, true);
                SectionBackButton.IsVisible = true;
            }
            else if (Section4.IsVisible && !AnimationReserved)
            {
                ViewModel.ItsTimeToUploadAd.Invoke(images);
            }
        }

        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            if (Section4.IsVisible && !AnimationReserved)
            {
                AnimationReserved = true;
                await SlideSections(Section4, Section3, false);
                SectionBackButton.IsVisible = true;
            }
            else if (Section3.IsVisible && !AnimationReserved)
            {
                AnimationReserved = true;
                await SlideSections(Section3, Section2, false);
                SectionBackButton.IsVisible = true;
            }
            else if (Section2.IsVisible && !AnimationReserved)
            {
                AnimationReserved = true;
                await SlideSections(Section2, Section1, false);
                SectionBackButton.IsVisible = false;
            }
        }

        private async Task SlideSections(View from, View to, bool forward)
        {
            double width = this.Width; // عرض صفحه

            to.TranslationX = forward ? -width : width; // آماده‌سازی سکشن بعدی
            to.IsVisible = true;

            // انیمیشن همزمان
            await Task.WhenAll(
               from.TranslateTo(forward ? width : -width, 0, 300, Easing.SinInOut),
               to.TranslateTo(0, 0, 300, Easing.SinInOut)
             );

            AnimationReserved = false;
            from.IsVisible = false; // بعد از انیمیشن مخفی بشه

        }

        List<ImagePicker> images = new List<ImagePicker>();

        private async void PickPhoto(object sender, EventArgs e)
        {
            if (images.Count < 5)
            {


                var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "انتخاب عکس"
                });

                if (result != null)
                {
                    var stream = await result.OpenReadAsync();
                    images.Add(new ImagePicker());
                    images[images.Count - 1].ListOfImages= images;
                    images[images.Count - 1].Images.Source = ImageSource.FromStream(() => stream);
                    images[images.Count - 1].FilePath = result.FullPath;
                    ImagesFlex.Children.Add(images[images.Count - 1]);

                }
            }
            else
            {
                MessageBox.ShowMessage("امکان افزایش عکس وجود ندارد", "شما فقط میتوانید 5 عکس انتخاب کنید","دریافت شد");

            }
        }

        private void Map_MapClicked(object sender, Microsoft.Maui.Controls.Maps.MapClickedEventArgs e)
        {
            var map = (Microsoft.Maui.Controls.Maps.Map)sender;
            map.Pins.Clear();
            var pin = new Pin
            {
                Label = "مکان آگهی",
                Address = $"Lat: {e.Location.Latitude}, Lon: {e.Location.Longitude}",
                Type = PinType.Place,
                Location = e.Location,
                
            };
            ViewModel.Latitude = e.Location.Latitude;
            ViewModel.Longitude = e.Location.Longitude;
            map.Pins.Add(pin);
        }
    }

}
