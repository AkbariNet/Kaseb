using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Android.Icu.Text.CaseMap;

namespace Kaseb.Services
{
    internal class MessageBox
    {
        public static async void ShowMessage(string Title="",string Subtitle="", string Button="ok")
        {
            await Application.Current.MainPage.DisplayAlert(
                Title,
                Subtitle,
                Button
            );
        }
    }
}
