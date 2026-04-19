using Kaseb.Services;
using Kaseb.ViewModels.AdsOverlay;

namespace Kaseb.Views.AdsOverlay;

public partial class AdsFilterView : Grid
{
	public AdsFilterView()
	{
		InitializeComponent();
        this.BindingContext = VM;

    }

    public AdsFilterViewVM VM = new AdsFilterViewVM(); 
    private void Cancel_Clicked(object sender, EventArgs e)
    {
        VM.ApplyFilter.Invoke();
        PageLoader.removeOverlay(this);
    }

    private void Submit_Clicked(object sender, EventArgs e)
    {

        PageLoader.removeOverlay(this);
    }
}