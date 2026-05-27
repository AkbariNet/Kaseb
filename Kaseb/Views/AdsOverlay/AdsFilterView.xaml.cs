using Kaseb.Services;
using Kaseb.ViewModels.AdsOverlay;
using Kaseb.Views.Element;
using KasebCore.Models.Search;

namespace Kaseb.Views.AdsOverlay;

public partial class AdsFilterView : ContentPage
{
	public AdsFilterView()
	{
		InitializeComponent();
        this.BindingContext = VM;

    }

    public AdsFilterViewVM VM = new AdsFilterViewVM(); 
    private void Cancel_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("..");
    }

    private void Submit_Clicked(object sender, EventArgs e)
    {
        VM.ApplyFilter.Invoke();


        Shell.Current.GoToAsync("..");
    }
}