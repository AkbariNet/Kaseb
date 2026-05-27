
using Kaseb.Services;
using Kaseb.Views.AdsOverlay;
namespace Kaseb.Views.Element
{
    public partial class Header : Border
    {


        public Header()
        {
            InitializeComponent();

        }

        private void AdsFilterButton_Clicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync(nameof(AdsFilterView));
        }
    }

}
