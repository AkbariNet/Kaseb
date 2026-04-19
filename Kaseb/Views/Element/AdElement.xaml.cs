using CommunityToolkit.Mvvm.Input;
using KasebCore.Models.Element;
using Kaseb.Services;
using Kaseb.ViewModels;
using Kaseb.ViewModels.Element;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices.Sensors;
using System.Globalization;
using System.Linq;
using UraniumUI.Material.Controls;
using Kaseb.Services.ShowingContext;

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
                        BindingContext = new AdElementVM()
                        {
                            Model = this.Model

                        }
                    });
                }
                catch (Exception e)
                {
                    MessageBox.ShowMessage("مشکلی پیش آمده", "در اجرای این آگهی به مشکل برخوردیم، لطفا دوباره امتحان کنید..." +e.Message, "باشه");
                   
                }
            }    
        }

        private void AdElementView_Pressed(object sender, EventArgs e)
        {
            OpenAd();
        }
    }

}
