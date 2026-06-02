using Kaseb.Services;
using Kaseb.Views;
using Kaseb.Views.Element;
using KasebCore.Models;
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
        public ICommand isProfileButtonClicked { get; set; }

        public MenuVM()
        {
            isAdButtonClicked = new Command(() =>
            {
                if (PageLoadProcessing.MenuStatement != PageLoadProcessing.PageState.ad)
                {
                    PageLoadProcessing.MenuStatement = PageLoadProcessing.PageState.ad;
                    Routing.RegisterRoute(nameof(Ads), typeof(Ads));

                    Shell.Current.GoToAsync(nameof(Ads));

                }

            });
            isAddAdButtonClicked = new Command(() =>
            {
                if (PageLoadProcessing.MenuStatement != PageLoadProcessing.PageState.addAd)
                {
                    PageLoadProcessing.MenuStatement = PageLoadProcessing.PageState.addAd;
                    Routing.RegisterRoute(nameof(AddAds), typeof(AddAds));
                    Shell.Current.GoToAsync(nameof(AddAds));
                }

            });
            isLocationButtonClicked = new Command(() =>
            {
                if (PageLoadProcessing.MenuStatement != PageLoadProcessing.PageState.location)
                {
                    PageLoadProcessing.MenuStatement = PageLoadProcessing.PageState.location;
                    Routing.RegisterRoute(nameof(Lobby), typeof(Lobby));
                    Shell.Current.GoToAsync(nameof(Lobby));
                }
            });
     /*       isProfileButtonClicked = new Command(() =>
            {
                if (PageLoadProcessing.MenuStatement != PageLoadProcessing.PageState.profile)
                {
                    PageLoadProcessing.MenuStatement = PageLoadProcessing.PageState.profile;
                    Routing.RegisterRoute(nameof(Profile), typeof(Profile));
                    Shell.Current.GoToAsync(nameof(Profile));
                }
            });*/

            isProfileButtonClicked = new Command(() =>
            {
                if (PageLoadProcessing.MenuStatement != PageLoadProcessing.PageState.profile)
                {
                    PageLoadProcessing.MenuStatement = PageLoadProcessing.PageState.profile;
                    Routing.RegisterRoute(nameof(Login), typeof(Login));
                    Shell.Current.GoToAsync(nameof(Login));
                }
            });

        }
    }
}
