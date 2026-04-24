using KasebCore.Models.Element;
using Kaseb.Services;
using KasebAdServices.Services.Connection;
using Kaseb.ViewModels.Element;
using Kaseb.Views.AddAds_Childrens;
using Kaseb.Views.Element;
using System.ComponentModel;
using Enum = System.Enum;
using Kaseb.Services.ShowingContext;
using ConvertCore = KasebCore.Services.Converting.Convert;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using Kaseb.Views;
using KasebCore.Models.Search;
using System.ComponentModel.DataAnnotations;

namespace Kaseb.ViewModels
{
    public partial class AddAdsVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public AddAdsVM()
        {
            ItsTimeToUploadAd += UploadAdFunc;
            SelectAdType.ButtonClicked += CloseCollectionSelection;
            SelectAdCity.ButtonClicked += CloseMapCollectionSelection;
        }

        private AdElementModel _model = new AdElementModel();


        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Action<List<ImagePicker>> ItsTimeToUploadAd ;

        public static string ImageToBase64(string imagePath)
        {
            if (!File.Exists(imagePath))
                return "null";

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
            Date = DateTime.Now;
            Images = new List<AdImage?>();
            foreach (ImagePicker img in imgs)
            {
                // تبدیل به Base64 و اضافه کردن به مدل
                string base64Image = ImageToBase64(img.FilePath);
                if (!string.IsNullOrEmpty(base64Image))
                {
                    AdImage adImage = new AdImage { ImagePath = base64Image };
                    Images.Add(adImage);
                }
            }
            foreach (ImagePicker img in imgs)
                ImagePaths.Add(img.FilePath);
            ProcessingOverlay OverlayOfProcessing = new ProcessingOverlay();
            
            OverlayOfProcessing.Show("درحال افزودن آگهی...");
            PageLoader.includeOverlay(null, OverlayOfProcessing);

            GetResoultInfo InfoOfValidation = _model.IsValidToUpload();
            if (InfoOfValidation.IsSuccess )
            {
                GetResoultInfo UploadAdProcessingInfo = await UploadAd.UploadAdAsync(_model);

                if (UploadAdProcessingInfo.IsSuccess)
                {
                    OverlayOfProcessing.Show("موفقیت آمیز!", UploadAdProcessingInfo.Message, false);
                    await Task.Delay(2000);
                    OverlayOfProcessing.Remove();

                    PageLoader.includePage(PageLoader.Ads);
                    PageLoader.Ads.ViewModel.RefreshAdsAction?.Invoke();

                }
                else
                {
                    OverlayOfProcessing.Show("شکست!", UploadAdProcessingInfo.Message, false);
               
                }
            }
            else
            {

                OverlayOfProcessing.Show("شکست!", InfoOfValidation.Message, false);
            }


        }

        public AdElementModel Model
        {
            get => _model;
            set
            {
                _model = value;/*
                OnPropertyChanged(nameof(Model));
                OnPropertyChanged(nameof(Title));
                OnPropertyChanged(nameof(Content));
                OnPropertyChanged(nameof(IsUrgent));
                OnPropertyChanged(nameof(ValueOfTag1));
                OnPropertyChanged(nameof(ValueOfTag2));
                OnPropertyChanged(nameof(Price));
                OnPropertyChanged(nameof(ValueOfWeighKG));
                OnPropertyChanged(nameof(Category));*/


                ///


            }
        }

        //Propery For Category
        public Category? Category
        {
            get => Model?.Category ?? KasebCore.Models.Element.Category.IsNull;
            set
            {
                if (Model.Category != value && value != KasebCore.Models.Element.Category.IsNull)
                {
                    Model.Category = value;
                 
                    OnPropertyChanged(nameof(Category));
                    OnPropertyChanged(nameof(CategoryName));
                }
            }
        }



        //For Visiblity of collectionsection
        public bool IsColletionSelectionVisible { get; set; }

        //For Visiblity of MapCollectionsection
        public bool IsMapColletionSelectionVisible { get; set; }

        [RelayCommand]
        public void CloseCollectionSelection(Category? category)
        {

            Category = category;

            IsColletionSelectionVisible = false;
            OnPropertyChanged(nameof(IsColletionSelectionVisible));

        }

        [RelayCommand]
        public void CloseMapCollectionSelection(Cities? cities)
        {

            City = cities;

            IsMapColletionSelectionVisible = false;
            OnPropertyChanged(nameof(IsMapColletionSelectionVisible));

        }
        public ObservableRangeCollection<Kaseb.Views.Element.SelectAdType> ElementsOfCategory { get; set; } = new();
        [RelayCommand]
        public void OpenCollectionSelection()
        {
            var theList = new List<SelectAdType>();

            foreach (Category category in Enum.GetValues(typeof(Category)))
            {
                if (category == KasebCore.Models.Element.Category.IsNull) continue;

                var model = new SelectAdTypeModel { Category = category };

                var view = new SelectAdType(model);      // یا new SelectAdType() { BindingContext = vm };

                theList.Add(view);
            }
            ElementsOfCategory.Clear();
            ElementsOfCategory.AddRange(theList);
            IsColletionSelectionVisible = true;
            OnPropertyChanged(nameof(IsColletionSelectionVisible));
        }
        public ObservableRangeCollection<Kaseb.Views.Element.SelectAdCity> ElementsOfCities { get; set; } = new();
        
        [RelayCommand]
        public void OpenMapCollectionSelection()
        {
            var theList = new List<SelectAdCity>();

            foreach (Cities Cities in Enum.GetValues(typeof(Cities)))
            {
                if (Cities == KasebCore.Models.Element.Cities.IsNull) continue;

                var model = new SelectAdCityModel { Cities = Cities };

                var view = new SelectAdCity(model);      // یا new SelectAdType() { BindingContext = vm };

                theList.Add(view);
            }
            ElementsOfCities.Clear();
            ElementsOfCities.AddRange(theList);
            IsMapColletionSelectionVisible = true;
            OnPropertyChanged(nameof(IsMapColletionSelectionVisible));
        }




        //Propery For Category Name
        public string CategoryName
        {
            get
            {
                try
                {

                    Model.CategoryString = ConvertCore.ConvertCategoryListToCategoryString(Category);
                    return Model.CategoryString;
                }
                catch (Exception)
                {
                    return "error";
                    throw;
                }
            }
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
        public DateTime Date
        {
            get => Model?.Date ?? new DateTime();
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
        public Cities? City
        {

            get => Model?.Cities ?? KasebCore.Models.Element.Cities.IsNull;
            set
            {
                if (Model.Cities != value && value != KasebCore.Models.Element.Cities.IsNull)
                {
                    Model.Cities = value;

                    OnPropertyChanged(nameof(City));
                    OnPropertyChanged(nameof(CityName));
                }
            }
        }

        //Propery For Category Name
        public string CityName
        {
            get
            {
                try
                {

                    return Model?.City ?? "نامشخص";
                }
                catch (Exception)
                {
                    return "error";
                    throw;
                }
            }
        }

        //Propery For Price
        public decimal Price
        {
            get => Model?.Price ?? 0;
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
        public List<AdImage?> Images
        {
            get
            {
                if (Model?.Images is not null)
                {
                    return Model.Images;
                }
                else
                {
                    List<AdImage?> AList = new List<AdImage?>();
                    return AList;
                }
            } 
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
            get => Model?.Images?[0].ImagePath ?? "";
            set
            {
                if (Model != null && Model?.Images?[0].ImagePath != value)
                {
                    Model?.Images?[0].ImagePath = value;
                    OnPropertyChanged(nameof(Image));
                }
            }
        }

        //Value For WeighKG
        public List<string> ImagePaths
        {

            get => Model?.ImagePaths ?? new List<string>();

            set
            {
                if (Model != null && Model.ImagePaths != value )
                {
                    Model.ImagePaths = value;
                    OnPropertyChanged(nameof(ImagePaths));
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
        public decimal ValueOfWeighKG
        {

            get => Model?.ValueOfWeighKG ?? 0;

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
                    if (NonCash)
                    {
                        ValueOfTag1 = "غیرنقدی";
                    }
                    else
                    {

                        ValueOfTag1 = "نقدی";
                    }
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
