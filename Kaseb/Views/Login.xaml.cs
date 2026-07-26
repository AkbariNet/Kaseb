using Kaseb.ViewModels;
using KasebCore.Models.Services.AdService;
using Microsoft.Maui.Controls.Compatibility;
using System.Text;

namespace Kaseb.Views;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
        OTPSent += Login_OTPSent;
	}

    private void Login_OTPSent(LoginPageStatement Statement)
    {
        if (Statement == LoginPageStatement.GetCodePage)
        {
            MainPage.IsVisible = false;
            GetCodePage.IsVisible = true;

        }
        else if (Statement == LoginPageStatement.MainPage)
        {

            MainPage.IsVisible = true;
            GetCodePage.IsVisible = false;
        }
    }

    public static Action<LoginPageStatement> ?OTPSent;

    public enum LoginPageStatement
    {
        MainPage,
        GetCodePage,
    }
}