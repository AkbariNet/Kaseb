
using Kaseb.Services;
using Microsoft.Maui.Devices.Sensors;
using System.Linq;
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
        public void AddOverlay(IView newContent)
        {
            Overlay.Children.Clear();
     
            Contain.IsEnabled = false;
            Overlay.Children.Add(newContent);

        }
        public void RemoveOverlay(IView OldContent)
        {
            Overlay.Children.Clear();
            Contain.Effects.Clear();
            Contain.IsEnabled = true;

        }
        public void RemoveOverlay()
        {
            Overlay.Children.Clear();
            Contain.Effects.Clear();
            Contain.IsEnabled = true;

        }
    }

}
