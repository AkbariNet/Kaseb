using CommunityToolkit.Mvvm.Input;
using KasebCore.Models.Element;
using Kaseb.Services;
using KasebAdServices.Services.Connection;
using Kaseb.Views.Element;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Windows.Input;

namespace Kaseb.ViewModels.Element
{
    internal partial class AdElementVM : BaseViewModel, INotifyPropertyChanged
    {

        private AdElementModel _model = new AdElementModel();
        public event PropertyChangedEventHandler PropertyChanged;
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
                OnPropertyChanged(nameof(IsShowWeighKG));
                OnPropertyChanged(nameof(ValueOfTag1));
                OnPropertyChanged(nameof(ValueOfTag2));
                OnPropertyChanged(nameof(ValueOfWeighKG));
                OnPropertyChanged(nameof(Image));


            }
        }



        //Propery For Title
        public new string Title
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

        //Propery For Content
        public string CategoryString
        {
            get => Model?.CategoryString ?? "";
            set
            {
                if (Model != null && Model.CategoryString != value)
                {
                    Model.CategoryString = value;
                    OnPropertyChanged(nameof(CategoryString));
                }
            }
        }


        //Propery For Phone
        public string Phone
        {
            get => Model?.Phone ?? "";
            set
            {
                if (Model != null && Model.Phone != value)
                {
                    Model.Phone = value;
                    OnPropertyChanged(nameof(Phone));
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

        //Propery For Image
        public string Image
        {
            get
            {
                if (Model?.Images?[0] is not null)
                {
                    return Model?.Images[0].ImagePath ?? "";

                }
                else
                {
                    return "";
                }
            }
            set
            {
            }
        }

        //Propery For Image
        public string MainImageLink
        {
            get
            {
                if (Model?.MainImageLink is not null)
                {
                    return Model?.MainImageLink ?? "";

                }
                else
                {
                    return "";
                }
            }
            set
            {
            }
        }

        public List<string> ImageLinks
        {
            get
            {
                if (Model?.ImageLinks is not null)
                {
                    return Model?.ImageLinks ?? new();

                }
                else
                {
                    return new();
                }
            }
            set
            {
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
        //for tags
        public bool IsShowWeighKG
        {
            get => Model?.IsShowWeighKG ?? false;
        }
        public bool IsShowTag1
        {
            get => Model?.IsShowTag1 ?? false;
        }
        public bool IsShowTag2
        {
            get => Model?.IsShowTag2 ?? false;
        }
   
        public string TheSummeryOfDateAndCity
        {
            get
            {
                return Model?.ValueSummery ?? "خطا!";
            }
        }
        //Value For WeighKG
        public decimal ValueOfWeighKG
        {

            get
            {

                return Model?.ValueOfWeighKG ?? 0;
            }


            set
            {
                if (Model != null && Model.ValueOfWeighKG != value)
                {
                    Model.ValueOfWeighKG = value;
                    OnPropertyChanged(nameof(ValueOfWeighKG));
                }
            }
        }
        //String For WeighKG
        public string ValueOfWeighKGString
        {

            get
            {

                return Model?.ValueOfWeighKGString ?? "";
            }

        }

        //String For Price
        public string PriceString
        {

            get
            {

                return Model?.PriceString ?? "";
            }

        }
        //Value For Tag1
        public string ValueOfTag1
        {
            get
            {
                return Model?.ValueOfTag1 ?? "";

            }
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
            get
            {
                return Model?.ValueOfTag2 ?? "";

            }
            set
            {
                if (Model != null && Model.ValueOfTag2 != value)
                {

                    Model.ValueOfTag2 = value;
                    OnPropertyChanged(nameof(ValueOfTag2));
                }
            }
        }




    }
}
