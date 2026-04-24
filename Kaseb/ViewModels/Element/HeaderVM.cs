using CommunityToolkit.Mvvm.Input;
using Kaseb.Services;
using Kaseb.Views.Element;
using KasebCore.Models.Search;
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
        public void Search_Load() { PageLoader.includeOverlay(null, new SearchElement()); }

        [RelayCommand]
        public void Close()
        {
            PageLoader.removeOverlay(new SearchElement());

            PageLoader.Ads.ViewModel.RefreshAdsAction?.Invoke();
        }

        public string SearchText
        {
            get
            {
                return SearchModel.MainSearchModel.Title ?? "";
            }
            set
            {

                SearchModel.MainSearchModel.Title = value;
            }
        }
    }
}
