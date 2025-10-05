using Kaseb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Kaseb.ViewModels.Lobby_Childrens
{
    internal class MenuVM
    {

        public ICommand isAdButtonClicked { get; set; }
        public ICommand isAddAdButtonClicked { get; set; }
        public ICommand isLocationButtonClicked { get; set; }

        public MenuVM()
        {
            isAdButtonClicked = new Command(() =>
            {
                PageLoadProcessing.MenuStatement = PageLoadProcessing.PageState.ad;
                PageLoader.includePage();
            });
            isAddAdButtonClicked = new Command(() =>
            {
                PageLoadProcessing.MenuStatement = PageLoadProcessing.PageState.addAd;
                PageLoader.includePage();
            });
            isLocationButtonClicked = new Command(() =>
            {
                PageLoadProcessing.MenuStatement = PageLoadProcessing.PageState.location;
                PageLoader.includePage();
            });

        }
    }
}
