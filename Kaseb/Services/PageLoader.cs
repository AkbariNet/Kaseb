using Kaseb.Models;
using Kaseb.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaseb.Services 
{
    internal static class PageLoader
    {
        public static Container TheParent = new Container();
        public static void getCountainer(Container container)
        {
            TheParent = container;
        }
        public static Lobby Lobby = new Lobby();
        public static Ads Ads = new Ads();
        public static AddAds AddAds = new AddAds();

        public static void includePage()
        {

            switch (PageLoadProcessing.MenuStatement)
            {
                case PageLoadProcessing.PageState.location:
                    TheParent.UpdateGrid(Lobby);

                    break;

                case PageLoadProcessing.PageState.ad:
                    TheParent.UpdateGrid(Ads);
                    break;

                case PageLoadProcessing.PageState.addAd:
                    TheParent.UpdateGrid(AddAds);
                    break;
            }
        }
        public static void includePage(View view) => TheParent.UpdateGrid(view);
        public static void includeOverlay(object sender, IView Content)
        {
            TheParent.AddOverlay(Content);
        }
        public static void removeOverlay(IView Content)
        {

            TheParent.RemoveOverlay(Content);
        }
        public static void removeOverlay()
        {

            TheParent.RemoveOverlay();
        }
    }
}
