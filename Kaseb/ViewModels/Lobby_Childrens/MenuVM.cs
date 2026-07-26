using Kaseb.Services;
using Kaseb.Views;
using Kaseb.Views.Element;
using KasebCore.Models;
using KasebCore.Models.Profile;
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
            //Rigesteration Routes
            Routing.RegisterRoute(nameof(Ads), typeof(Ads));
            Routing.RegisterRoute(nameof(AddAds), typeof(AddAds));
            Routing.RegisterRoute(nameof(Lobby), typeof(Lobby));
            Routing.RegisterRoute(nameof(Login), typeof(Login));
            Routing.RegisterRoute(nameof(Kaseb.Views.Profile), typeof(Kaseb.Views.Profile));
            //



            isAdButtonClicked = new Command(() =>
            {
                if (PageLoadProcessing.MenuStatement != PageLoadProcessing.PageState.ad)
                {
                    PageLoadProcessing.MenuStatement = PageLoadProcessing.PageState.ad;

                    Shell.Current.GoToAsync(nameof(Ads));

                }

            });
            isAddAdButtonClicked = new Command(() =>
            {
                if (PageLoadProcessing.MenuStatement != PageLoadProcessing.PageState.addAd)
                {
                    PageLoadProcessing.MenuStatement = PageLoadProcessing.PageState.addAd;
                    Shell.Current.GoToAsync(nameof(AddAds));
                }

            });
            isLocationButtonClicked = new Command(() =>
            {
                if (PageLoadProcessing.MenuStatement != PageLoadProcessing.PageState.location)
                {
                    PageLoadProcessing.MenuStatement = PageLoadProcessing.PageState.location;
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
                    if (KasebCore.Models.Profile.Profile.MainProfile != null)
                    {
                        Shell.Current.GoToAsync(nameof(Kaseb.Views.Profile));

                    }
                    else
                        Shell.Current.GoToAsync(nameof(Login));
                }
            });

        }
    }
}
