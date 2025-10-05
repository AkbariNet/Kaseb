using CommunityToolkit.Mvvm.Input;
using Kaseb.Services;
using Kaseb.Views.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Kaseb.ViewModels.Element
{
    internal partial class HeaderVM
    {

        [RelayCommand]
        public void Search_Load()  {PageLoader.includeOverlay(null, new SearchElement());}

        [RelayCommand]
        public void Close() {PageLoader.removeOverlay(new SearchElement());}
    }
}
