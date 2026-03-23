using CommunityToolkit.Mvvm.Input;
using Kaseb.Models.Element;
using Kaseb.Services;
using Kaseb.ViewModels;
using Kaseb.ViewModels.Element;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices.Sensors;
using System.Globalization;
using System.Linq;
using UraniumUI.Material.Controls;

namespace Kaseb.Views.Element
{
    public partial class AdElement : ButtonView
    {


        public AdElement()
        {
            InitializeComponent();

        }
        public static readonly BindableProperty ModelProperty =
        BindableProperty.Create(nameof(Model), typeof(AdElementModel), typeof(AdElement));

        public AdElementModel Model
        {
            get => (AdElementModel)GetValue(ModelProperty);
            set => SetValue(ModelProperty, value);
        }


        public void OpenAd()
        {
            if (this.Model != null)
            {
                try
                {
                    PageLoader.includeOverlay(null, new AdElementPage
                    {
                        BindingContext = new AddAdsVM()
                        {
                            Model = this.Model.Clone()
                          
                        }
                    });
                }
                catch (Exception)
                {
                    MessageBox.ShowMessage("مشکلی پیش آمده", "در اجرای این آگلی به مشکل برخوردیم، لطفا دوباره امتحان کنید...", "باشه");
                    throw;
                }
            }    
        }

        private void AdElementView_Pressed(object sender, EventArgs e)
        {
            OpenAd();
        }
    }

}
