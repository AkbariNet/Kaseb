using Kaseb.Models;
using Kaseb.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaseb.Services 
{
    internal class PageLoader
    {
        public static Container MyContiner = new Container();
        public static void getCountainer(Container container)
        {
            MyContiner = container;
        }
        public static Lobby Lobby = new Lobby();
        public static Ads Ads = new Ads();
        public static AddAds AddAds = new AddAds();

        public static void includePage()
        {

            switch (PageLoadProcessing.MenuStatement)
            {
                case PageLoadProcessing.PageState.location:
                    MyContiner.UpdateGrid(Lobby);

                    break;

                case PageLoadProcessing.PageState.ad:
                    MyContiner.UpdateGrid(Ads);
                    break;

                case PageLoadProcessing.PageState.addAd:
                    MyContiner.UpdateGrid(AddAds);
                    break;
            }
        }
        public static void includePage(View view) => MyContiner.UpdateGrid(view);
        public static void includeOverlay(object sender, IView Content)
        {
            MyContiner.AddOverlay(Content);
        }
        public static void removeOverlay(IView Content)
        {

            MyContiner.RemoveOverlay(Content);
        }
        public static void removeOverlay()
        {

            MyContiner.RemoveOverlay();
        }
    }
}
