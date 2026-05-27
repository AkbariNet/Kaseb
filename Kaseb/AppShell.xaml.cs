
using Kaseb.Services;
using Kaseb.Views;
using Kaseb.Views.AddAds_Childrens;
using Kaseb.Views.AdsOverlay;
using Kaseb.Views.Element;
using Kaseb.Views.Element.AdElementPageChildrens;
using KasebCore.Services;
using Microsoft.Maui.Controls;
using System.Threading.Tasks;
using Animation = KasebCore.Services.Animation;
namespace Kaseb
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AdElementPage), typeof(AdElementPage));
            Routing.RegisterRoute(nameof(AdElementImageSlider), typeof(AdElementImageSlider));
            Routing.RegisterRoute(nameof(SearchElement), typeof(SearchElement));
            Routing.RegisterRoute(nameof(AdsFilterView), typeof(AdsFilterView));
            Routing.RegisterRoute(nameof(ProcessingOverlay), typeof(ProcessingOverlay));


        }

       
    }

}
