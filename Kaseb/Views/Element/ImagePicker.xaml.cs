
using Kaseb.Services;
using Kaseb.Views.Lobby_Childrens;
using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.Layouts;
using System.Linq;
using UraniumUI.Material.Controls;
namespace Kaseb.Views.Element
{
    public partial class ImagePicker : ButtonView
    {
        public List<ImagePicker> ListOfImages;

        public readonly BindableProperty ImageChangedProperty =
                                BindableProperty.Create(
                                nameof(Images),
                                typeof(Image),
                                typeof(ImagePicker),
                                defaultValue: new Image()
                                 );

        public Image Images
        {
            get => (Image)GetValue(ImageChangedProperty);
            set
            {
                SetValue(ImageChangedProperty, value);
            }
        }

        // مسیر فایل انتخاب‌شده (برای آپلود به سرور)
        public string FilePath { get; set; } = string.Empty;
        public ImagePicker()
        {
            InitializeComponent();
            ImagePicker_FadeIn();

        }

        public bool ImagePickerTapped;
        private void ImagePickerElement_Tapped(object sender, EventArgs e)
        {
            if (!ImagePickerTapped)
            {
                DeleteImageBorder_FadeIn();
                DeleteImageBorder_FadeOut();
            }
            else
            {
                try
                {
                    ImagePicker_FadeOut();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        private async void ImagePicker_FadeIn()
        {
            ImagePickerElement.Opacity = 0;
            ImagePickerElement.IsVisible = true;
            await ImagePickerElement.FadeTo(1, 600);
        }
        private async void ImagePicker_FadeOut()
        {
            await ImagePickerElement.FadeTo(0, 600);
            ImagePickerElement.IsVisible = false;
            ListOfImages.Remove(this);
        }
        private async void DeleteImageBorder_FadeIn()
        {
            ImagePickerTapped = true;
            DeleteImageBorder.Opacity = 0;
            DeleteImageBorder.IsVisible = true;
            await DeleteImageBorder.FadeTo(0.9, 300);
        }
        private async void DeleteImageBorder_FadeOut()
        {
            await Task.Delay(5000);
            await DeleteImageBorder.FadeTo(0, 300);
            DeleteImageBorder.IsVisible = false;
            ImagePickerTapped = false;
        }
    }

}
