
using Kaseb.Services;
using KasebAdServices.Services.Connection;
using Kaseb.ViewModels;
using Kaseb.Views.Element;
using Microsoft.Maui.Devices.Sensors;
using System.ComponentModel;
using System.Linq;
namespace Kaseb.Views
{
    public partial class Ads : ContentPage
    {
        public AdsVM ViewModel=new AdsVM();
        public  Ads()
        {
            
            InitializeComponent();
            this.BindingContext = ViewModel;
        }

        
    }

}
