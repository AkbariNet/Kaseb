using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KasebCore.Models; // AdElementModel
using KasebCore.Models.Element;
using Kaseb.Services;
using KasebAdServices.Services.Connection;
using Microsoft.Maui.ApplicationModel;
using System.Collections.ObjectModel;

namespace Kaseb.ViewModels.Element
{
    internal partial class SelectorVM : ObservableObject
    {
        private readonly Ad_Services ad_Services;

        public ObservableCollection<AdElementModel> Items { get; set; } = new();
        public List<AdElementModel> ItemsLazyLoad { get; set; } = new();

        bool LazyLoadCompleted = true;

        [ObservableProperty]
        private bool isRefreshing;

        [RelayCommand]
        private async void RefreshAds()
        {
            IsRefreshing = true;
            Items.Clear();
            ad_Services.AdsModel.Clear();
            try
            {
                await LoadAdsAsync();

            }
            catch (Exception)
            {

                throw;
            }
            IsRefreshing = false;
        }

        [RelayCommand]
        private async void LoadMore()
        {
           // await LoadAdsAsync();
        }
        private void ProcessOfAddItems()
        {
            // تغییرات UI باید روی Thread اصلی انجام شود
            Microsoft.Maui.ApplicationModel.MainThread.BeginInvokeOnMainThread(() =>
            {
                foreach (var ad in ad_Services.AdsModel)
                {/*
                    ad.ValueOfWeighKG = uint.Parse(ad.ValueOfWeighKG) >= 1000
                    ? KasebProcessor.KiloToTon(double.Parse(ad.ValueOfWeighKG)).ToString() + " تن"
                    : ad.ValueOfWeighKG + " کیلوگرم";*/
                    ItemsLazyLoad.Add(ad);
                }
            });
            ushort a = 0;
            for (int i = 0; i < ItemsLazyLoad.Count && a < 10; i++)
            {
                var item = ItemsLazyLoad[i];
                Items.Add(item);
                ItemsLazyLoad.RemoveAt(i);
                a++;
                i--; // چون آیتم حذف شد، ایندکس‌ها تغییر کردند
            }
        }

        public SelectorVM()
        {
            ad_Services = new Ad_Services();
            ad_Services.isLazyLoadCompleted += () => LazyLoadCompleted = true;

            // اضافه کردن Event برای تغییرات Ads
            ad_Services.AdsChanged += () => ProcessOfAddItems();

            // بارگذاری اولیه
            _ = LoadAdsAsync();
        }

        [RelayCommand]
        private void LazyLoader()
        {
            if (LazyLoadCompleted)
            {
                ItemsLazyLoad.Clear();
                // بارگذاری آپدیت
                _ = LoadAdsAsync(true);
            }
        }
        private async System.Threading.Tasks.Task LoadAdsAsync(bool isUpdate=false)
        {
            await ad_Services.LoadAdsAsync(isUpdate);
        }
    }
}
