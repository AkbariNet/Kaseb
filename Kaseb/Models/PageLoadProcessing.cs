using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaseb.Services
{
    internal class PageLoadProcessing
    {
        public static PageState MenuStatement {  get; set; } = PageState.ad;
        public static bool isAdButtonClicked {  get; set; }
        public static bool isAddAdButtonClicked { get; set; }
        public static bool isLocationButtonClicked { get; set; }

        public enum PageState
        {
            ad,
            location,
            addAd
        }
    }
}
