namespace Kaseb.Services.ShowingContext
{
    public static class MessageBox
    {
        public static async void ShowMessage(string Title="",string Subtitle="", string ButtonText="ok")
        {
            await Application.Current.MainPage.DisplayAlert(
                Title,
                Subtitle,
                ButtonText
            );
        }
    }
}
