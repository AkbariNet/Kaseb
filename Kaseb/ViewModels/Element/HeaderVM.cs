using CommunityToolkit.Mvvm.Input;
using Kaseb.Services;
using Kaseb.Views.Element;
using KasebCore.Models.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Kaseb.ViewModels.Element
{
    internal partial class HeaderVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [RelayCommand]
        public void Search_Load()
        {
            Shell.Current.GoToAsync(nameof(SearchElement));
        }

        [RelayCommand]
        public void Close()
        {
            Shell.Current.GoToAsync("..");
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
                OnPropertyChanged(nameof(SearchText));
                OnPropertyChanged(nameof(SearchData));
            }
        }
        public string SearchData
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(SearchModel.MainSearchModel.Title))
                {
                    return SearchModel.MainSearchModel.Title ?? "";
                }
                else
                {
                    return "جستجو کنید...";
                }

            }
        }
    }
}
