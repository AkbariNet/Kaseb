
using Kaseb.ViewModels;
using KasebCore.Models.Services.AdService;
using Microsoft.Maui.Controls.Compatibility;
using System.Text;

namespace Kaseb.Views.LoginChildren;

public partial class LoginGetCode : Border
{
	public LoginGetCode()
	{
		InitializeComponent();
	}
    private void Button_Clicked(object sender, EventArgs e)
    {
		string CodeValidation=string.Empty;
		CodeValidation=CodeChar1.Text+CodeChar2.Text + CodeChar3.Text + CodeChar4.Text + CodeChar5.Text;
        LoginVM.Validation=CodeValidation;

        LoginVM.SendValidation();

    }

    private void BackButton_Clicked(object sender, EventArgs e)
    {
        Login.OTPSent.Invoke(Login.LoginPageStatement.MainPage);
    }
}