using Kaseb.ViewModels;
using KasebCore.Models.Services.AdService;
using Microsoft.Maui.Controls.Compatibility;
using System.Text;

namespace Kaseb.Views.LoginChildren;

public partial class LoginMainPage : Border
{
	public LoginMainPage()
	{
		InitializeComponent();
	}
    private void Button_Clicked(object sender, EventArgs e)
    {
        LoginVM.PhoneNumber = PhoneNumberText.Text;
        LoginVM.SendOTP();

    }
}