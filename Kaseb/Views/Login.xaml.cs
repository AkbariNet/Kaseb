using KasebCore.Models.Services.AdService;
using Microsoft.Maui.Controls.Compatibility;
using System.Text;

namespace Kaseb.Views;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        var response = AdServiceModel.httpClient
            .GetAsync($"Profile/SendOTP?PhoneNumber{PhoneNumberText.Text}"
            );

        Debug.Text = response.Result.ToString();
    }
}