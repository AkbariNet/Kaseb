using Kaseb.Services;
using KasebCore.Models.Element;
using KasebCore.Models.Search;
using KasebCore.Services.Converting;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using ConvertCore = KasebCore.Services.Converting.Convert;

namespace Kaseb.ViewModels.AdsOverlay
{
    public class AdsFilterViewVM : BaseViewModel, INotifyPropertyChanged
    {

        public AdsFilterViewVM()
        {
            ApplyFilter += AdsFilterViewVM_ApplyFilter;
        }

        private void AdsFilterViewVM_ApplyFilter()
        {

            SearchModel.MainSearchModel = Temp;

            Console.WriteLine("");
        }

        public Action ApplyFilter;
        public AgriculturalProductsSearchModel Temp = new AgriculturalProductsSearchModel();

        //Propery For City
        public Cities City
        {
            get => Temp?.City ?? KasebCore.Models.Element.Cities.IsNull;

            set
            {
                if (Temp != null && Temp.City != value)
                {
                    Temp.City = value;
                    OnPropertyChanged(nameof(City));
                }
            }
        }

        //Propery For Category Name
        public string CityName
        {
            get
            {
                return ConvertCore.ConvertCityListToCityString(City);
            }
        }

        //Propery For Category Name
        public string CategoryName
        {
            get
            {
                return ConvertCore.ConvertCategoryListToCategoryString(Category);
            }
        }
        //Propery For MinPrice
        public decimal MinPrice
        {
            get => Temp?.MinPrice ?? 0;

            set
            {
                if (Temp != null && Temp.MinPrice != value)
                {
                    Temp.MinPrice = value;
                    OnPropertyChanged(nameof(MinPrice));
                }
            }
        }
        //Propery For MaxPrice
        public decimal MaxPrice
        {
            get => Temp?.MaxPrice ?? 0;

            set
            {
                if (Temp != null && Temp.MaxPrice != value)
                {
                    Temp.MaxPrice = value;
                    OnPropertyChanged(nameof(MaxPrice));
                }
            }
        }
        //Propery For MinValueOfWeighKG
        public decimal MinValueOfWeighKG
        {
            get => Temp?.MinValueOfWeighKG ?? 0;

            set
            {
                if (Temp != null && Temp.MinValueOfWeighKG != value)
                {
                    Temp.MinValueOfWeighKG = value;
                    OnPropertyChanged(nameof(MinValueOfWeighKG));
                }
            }
        }

        //Propery For MaxValueOfWeighKG
        public decimal MaxValueOfWeighKG
        {
            get => Temp?.MaxValueOfWeighKG ?? 0;

            set
            {
                if (Temp != null && Temp.MaxValueOfWeighKG != value)
                {
                    Temp.MaxValueOfWeighKG = value;
                    OnPropertyChanged(nameof(MaxValueOfWeighKG));
                }
            }
        }

        //Propery For Category
        public Category? Category
        {
            get => Temp?.Category ?? KasebCore.Models.Element.Category.IsNull;
            set
            {
                if (Temp.Category != value && value != KasebCore.Models.Element.Category.IsNull)
                {
                    Temp.Category = value;

                    OnPropertyChanged(nameof(Category));
                    OnPropertyChanged(nameof(CategoryName));
                }
            }
        }

        //Propery For IsUrgent
        public bool IsUrgent
        {
            get => Temp?.IsUrgent ?? false;
            set
            {
                if (Temp != null && Temp.IsUrgent != value)
                {
                    Temp.IsUrgent = value;
                    OnPropertyChanged(nameof(IsUrgent));
                }
            }
        }
        //Propery For IsUrgent
        public bool IsNonCash
        {
            get => Temp?.IsNonCash ?? false;
            set
            {
                if (Temp != null && Temp.IsNonCash != value)
                {
                    Temp.IsNonCash = value;
                    OnPropertyChanged(nameof(IsNonCash));
                }
            }
        }

    }
}
