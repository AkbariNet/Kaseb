using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Dispatching;
using MvvmHelpers;
using KasebCore.Models.Element;
using KasebAdServices.Services.Connection;
using KasebCore.Models.Search;

namespace Kaseb.ViewModels
{
    public partial class AdsVM : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        
        private  Ad_Services ad_Services;
        // ObservableCollection برای نگه‌داری AdElementView ها
        public ObservableRangeCollection<AdElementModel> Items { get; set; } = new();
        public List<AdElementModel> ItemsLazyLoad { get; set; } = new();

        bool LazyLoadCompleted = true;

        public Action RefreshAdsAction;

        // Pull to Refresh
        [ObservableProperty]
        private bool isRefreshing;

        bool RefreshReserved;
        // Command برای RefreshView
        [RelayCommand]
        private async Task RefreshAds()
        {
            Items.Clear();
            ad_Services.AdsModel.Clear();
            IsRefreshing = true;
            if (!RefreshReserved)
            {
                try
                {
                    RefreshReserved = true;
                    await LoadAdsAsync();
                    RefreshReserved = false;

                }
                catch (Exception ex)
                {
                    RefreshReserved = false;

                    throw;
                }

            }

            IsRefreshing = false;
        }

        public AdsVM()
        {
            ad_Services = new Ad_Services();

            // اضافه کردن Event برای تغییرات Ads
            ad_Services.AdsChanged += () => ProcessOfAddItems();
            ad_Services.isLazyLoadCompleted += () => LazyLoadCompleted=true;
            // بارگذاری اولیه
            _ = LoadAdsAsync();
            RefreshAdsAction += () => RefreshAds();
        }


        private void ProcessOfAddItems()
        {  
            // تغییرات UI باید روی Thread اصلی انجام شود
            Microsoft.Maui.ApplicationModel.MainThread.BeginInvokeOnMainThread(() =>
            {
                    Items.AddRange(ad_Services.AdsModel);
                
            });
        }

       
        [RelayCommand]
        private void LazyLoader()
        {
            if (LazyLoadCompleted)
            {
                LazyLoadCompleted = false;
                ItemsLazyLoad.Clear();
                // بارگذاری آپدیت
                _ = LoadAdsAsync(true);
            }


        }
        
        private async System.Threading.Tasks.Task LoadAdsAsync(bool isUpdate=false)
        {
            // فرض: LoadAdsAsync async است و AdsView را پر می‌کند
            await ad_Services.LoadAdsAsync(isUpdate);
        }

      
    }

}
