using Android.Media.TV.Ads;
using Android.Views;
using AndroidX.AppCompat.View.Menu;
using AndroidX.Lifecycle;
using CommunityToolkit.Mvvm.Input;
using Kaseb.Models.Element;
using Kaseb.Services;
using Kaseb.Services.AdService;
using Kaseb.ViewModels.Element;
using Kaseb.Views.AddAds_Childrens;
using Kaseb.Views.Element;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using static Android.Graphics.ColorSpace;
using static Android.Icu.Text.CaseMap;

namespace Kaseb.ViewModels
{
    internal partial class AddAdsVM : INotifyPropertyChanged
    {


        private AdElementModel _model = new AdElementModel();
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Action<List<ImagePicker>> ItsTimeToUploadAd;

        public static string ImageToBase64(string imagePath)
        {
            if (!File.Exists(imagePath))
                return null;

            // پسوند فایل رو پیدا کن (jpg, png, webp, ...)
            var extension = Path.GetExtension(imagePath).ToLower().Replace(".", "");

            // محتوای فایل رو به بایت بخون
            byte[] bytes = File.ReadAllBytes(imagePath);

            // بایت‌ها رو Base64 کن
            string base64 = Convert.ToBase64String(bytes);

            // خروجی استاندارد برای JSON
            return $"data:image/{extension};base64,{base64}";
        }
        public async void UploadAdFunc(List<ImagePicker> imgs)
        {
            Images = new List<string>();
            foreach (ImagePicker img in imgs)
            {
                // تبدیل به Base64 و اضافه کردن به مدل
                string base64Image = ImageToBase64(img.FilePath);
                if (!string.IsNullOrEmpty(base64Image))
                {
                    Images.Add(base64Image);
                }
            }
            UploadAd UploadAd = new UploadAd();
            bool success = await UploadAd.UploadAdAsync(_model);

            if (success)
            {
                PageLoader.includePage(new ProcessingOfUpladAd());
            }
            else
            {
                MessageBox.ShowMessage();
            }

        }

        public AdElementModel Model
        {
            get => _model;
            set
            {
                _model = value;
                OnPropertyChanged(nameof(Model));
                OnPropertyChanged(nameof(Title));
                OnPropertyChanged(nameof(Content));
                OnPropertyChanged(nameof(IsUrgent));
                OnPropertyChanged(nameof(ValueOfTag1));
                OnPropertyChanged(nameof(ValueOfTag2));
                OnPropertyChanged(nameof(Price));
                OnPropertyChanged(nameof(ValueOfWeighKG));
                OnPropertyChanged(nameof(Category));


                ///

                ///Add <,> to Price like 1,000,000 
                Price = string.Format("{0:N0}", long.Parse(Price));

            }
        }

        //Propery For Title
        public Category Category
        {
            get => Model?.Category ?? Category.IsNull;
            set
            {
                if (Model?.Category != value)
                {
                    Model?.Category = value;
                    OnPropertyChanged(nameof(Category));
                    OnPropertyChanged(nameof(CategoryName));
                    CategoryName = Category switch
                    {
                        Category.Garlic => "سیر",
                        Category.Shallot => "موسیر",
                        Category.Walnut => "گردو",
                        Category.Potato => "سیب زمینی",
                        Category.Cucumber => "خیار",
                        Category.Tomato => "گوجه فرنگی",
                        Category.Mushroom => "قارچ",
                        Category.Almond => "بادام",
                        Category.IsNull => "هنوز انتخاب نشده",
                        _ => "نامشخص"
                    };
                }
            }
        }

        
        public StackLayout StackOfElements { get; set; } 

        //For Visiblity of collectionsection
        public bool IsColletionSelectionVisible { get; set; }

        [RelayCommand]
        public void CloseCollectionSelection(Category category)
        {

            Category = category;

            IsColletionSelectionVisible = false;
            OnPropertyChanged(nameof(IsColletionSelectionVisible));

        }

        [RelayCommand]
        public void OpenCollectionSelection()
        {
            StackOfElements = new StackLayout();
            foreach (Category category in Enum.GetValues(typeof(Category)))
            {
                // Skip IsNull اگه لازم باشه
                if (category == Category.IsNull)
                    continue;

                SelectAdTypeVM r = new SelectAdTypeVM() { _Category = category };
                r.ButtonClicked += CloseCollectionSelection;
                SelectAdType selectAdType = new SelectAdType() { BindingContext = r };
                StackOfElements.Children.Add(selectAdType);
            }
            ItsTimeToUploadAd += UploadAdFunc;
            IsColletionSelectionVisible = true;
            OnPropertyChanged(nameof(IsColletionSelectionVisible));
        }




        //Propery For Category Name
        public string CategoryName
        {
            get
            {
                return Category switch
                {
                    Category.Garlic => "سیر",
                    Category.Shallot => "موسیر",
                    Category.Walnut => "گردو",
                    Category.Potato => "سیب زمینی",
                    Category.Cucumber => "خیار",
                    Category.Tomato => "گوجه فرنگی",
                    Category.Mushroom => "قارچ",
                    Category.Almond => "بادام",
                    Category.IsNull => "هنوز انتخاب نشده",
                    _ => "نامشخص"
                };
            }
            set { }
        }

        //Propery For Title
        public string Title
        {
            get => Model?.Title ?? "";
            set
            {
                if (Model != null && Model.Title != value)
                {
                    Model.Title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }


        //Propery For Content
        public string Content
        {
            get => Model?.Content ?? "";
            set
            {
                if (Model != null && Model.Content != value)
                {
                    Model.Content = value;
                    OnPropertyChanged(nameof(Content));
                }
            }
        }

        //Propery For Date
        public string Date
        {
            get => Model?.Date ?? "";
            set
            {
                if (Model != null && Model.Date != value)
                {
                    Model.Date = value;
                    OnPropertyChanged(nameof(Date));
                }
            }
        }
        //Propery For City
        public string City
        {
            get => Model?.City ?? "";
            set
            {
                if (Model != null && Model.City != value)
                {
                    Model.City = value;
                    OnPropertyChanged(nameof(City));
                }
            }
        }


        //Propery For Price
        public string Price
        {
            get => Model?.Price ?? "";
            set
            {
                if (Model != null && Model.Price != value)
                {
                    Model.Price = value;
                    OnPropertyChanged(nameof(Price));
                }
            }
        }

        //Propery For Images
        public List<string> Images
        {
            get => Model?.Images ?? null;
            set
            {
                if (Model != null && Model.Images != value)
                {
                    Model.Images = value;
                    OnPropertyChanged(nameof(Images));
                }
            }
        }

        //Propery For Image
        public string Image
        {
            get => Model?.Image ?? "";
            set
            {
                if (Model != null && Model.Image != value)
                {
                    Model.Image = value;
                    OnPropertyChanged(nameof(Image));
                }
            }
        }

        //Propery For IsUrgent
        public bool IsUrgent
        {
            get => Model?.IsUrgent ?? false;
            set
            {
                if (Model != null && Model.IsUrgent != value)
                {
                    Model.IsUrgent = value;
                    OnPropertyChanged(nameof(IsUrgent));
                }
            }
        }

        //Value For WeighKG
        public string ValueOfWeighKG
        {

            get => Model?.ValueOfWeighKG ?? "";

            set
            {
                if (Model != null && Model.ValueOfWeighKG != value)
                {
                    Model.ValueOfWeighKG = value;
                    OnPropertyChanged(nameof(ValueOfWeighKG));
                }
            }
        }


        //Value For Tag1
        public string ValueOfTag1
        {
            get => Model?.ValueOfTag1 ?? "";
            set
            {
                if (Model != null && Model.ValueOfTag1 != value)
                {
                    Model.ValueOfTag1 = value;
                    OnPropertyChanged(nameof(ValueOfTag1));
                }
            }
        }


        //Value For Tag2
        public string ValueOfTag2
        {
            get => Model?.ValueOfTag2 ?? "";
            set
            {
                if (Model != null && Model.ValueOfTag2 != value)
                {
                    Model.ValueOfTag2 = value;
                    OnPropertyChanged(nameof(ValueOfTag2));
                }
            }
        }

        // Propery For Phone
        public string Phone
        {
            get => Model?.Phone ?? "0";
            set
            {
                if (Model != null && Model.Phone != value)
                {
                    Model.Phone = value;
                    OnPropertyChanged(nameof(Phone));
                }
            }
        }
        //Update API 1.6


        // Propery For NonCash
        public bool NonCash
        {
            get => Model?.NonCash ?? false;
            set
            {
                if (Model != null && Model.NonCash != value)
                {
                    Model.NonCash = value;
                    OnPropertyChanged(nameof(NonCash));
                }
            }
        }

        // Propery For SomeOfCashMostPayed
        public bool SomeOfCashMostPayed
        {
            get => Model?.SomeOfCashMostPayed ?? false;
            set
            {
                if (Model != null && Model.SomeOfCashMostPayed != value)
                {
                    Model.SomeOfCashMostPayed = value;
                    OnPropertyChanged(nameof(SomeOfCashMostPayed));
                }
            }
        }

        // Propery For IsUrgentRequest
        public bool IsUrgentRequest
        {
            get => Model?.IsUrgentRequest ?? false;
            set
            {
                if (Model != null && Model.IsUrgentRequest != value)
                {
                    Model.IsUrgentRequest = value;
                    OnPropertyChanged(nameof(IsUrgentRequest));
                }
            }
        }

        // Propery For MonthForNonCash
        public int MonthForNonCash
        {
            get => Model?.MonthForNonCash ?? 0;
            set
            {
                if (Model != null && Model.MonthForNonCash != value)
                {
                    Model.MonthForNonCash = value;
                    OnPropertyChanged(nameof(MonthForNonCash));
                }
            }
        }

        // Propery For Latitude
        public double Latitude
        {
            get => Model?.Latitude ?? 0.0;
            set
            {
                if (Model != null && Model.Latitude != value)
                {
                    Model.Latitude = value;
                    OnPropertyChanged(nameof(Latitude));
                }
            }
        }

        // Propery For Longitude
        public double Longitude
        {
            get => Model?.Longitude ?? 0.0;
            set
            {
                if (Model != null && Model.Longitude != value)
                {
                    Model.Longitude = value;
                    OnPropertyChanged(nameof(Longitude));
                }
            }
        }

        // Propery For InventoryGuarantee
        public int InventoryGuarantee
        {
            get => Model?.InventoryGuarantee ?? 0;
            set
            {
                if (Model != null && Model.InventoryGuarantee != value)
                {
                    Model.InventoryGuarantee = value;
                    OnPropertyChanged(nameof(InventoryGuarantee));
                }
            }
        }

    }

}
