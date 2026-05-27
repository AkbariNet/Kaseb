using KasebCore.Models;

namespace Kaseb.Views.Lobby_Childrens
{
    public partial class MenuAPP : Grid 
    {

        /// <summary>
        /// for Click Item of menu  
        /// </summary>
        /// <param name="isAdClicked"></param>
        /// <returns></returns>


        public bool isAdClicked
        {
            get
            {
                if (PageLoadProcessing.MenuStatement == PageLoadProcessing.PageState.ad)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 2
        /// </summary>
        /// 
        public bool isMapClicked
        {
            get
            {
                if (PageLoadProcessing.MenuStatement == PageLoadProcessing.PageState.location)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        /// <summary>
        /// 3
        /// </summary>

        public bool isAddAdClicked
        {
            get
            {
                if (PageLoadProcessing.MenuStatement == PageLoadProcessing.PageState.addAd)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        /// <summary>
        /// 4
        /// </summary>

        public bool isProfileClicked
        {
            get
            {
                if (PageLoadProcessing.MenuStatement == PageLoadProcessing.PageState.profile)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }



        public MenuAPP()
        {

            InitializeComponent();
        }
        
    }

}
