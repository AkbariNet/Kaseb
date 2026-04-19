
using Kaseb.Services;
using KasebCore.Services;
using Microsoft.Maui.Controls;
using System.Threading.Tasks;
using Animation = KasebCore.Services.Animation;
namespace Kaseb
{
    public partial class Container : ContentPage
    {
        public Container()
        {
            InitializeComponent();
            PageLoader.getCountainer(this);
            PageLoader.includePage();

        }
        public void UpdateGrid(IView newContent)
        {
            Contain.Children.Clear();
            Contain.Children.Add(newContent);
        }
        public async Task AddOverlay(IView newContent)
        {
            Overlay.IsVisible = false;
            Overlay.Children.Clear();
            Overlay.Children.Add(newContent);

            await Animation.ShowWithFade(Overlay, Contain);

        }
        public async Task RemoveOverlay(IView OldContent)
        {
            await Animation.HideWithFade(Overlay, Contain);
            Overlay.Children.Clear();
            Contain.Effects.Clear();


        }
        public void RemoveOverlay()
        {
            Overlay.Children.Clear();
            Contain.Effects.Clear();
            Contain.Opacity = 1;
            Contain.IsEnabled = true;

        }
    }

}
