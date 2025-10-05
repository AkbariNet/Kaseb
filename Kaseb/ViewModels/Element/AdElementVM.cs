using CommunityToolkit.Mvvm.Input;
using Kaseb.Models.Element;
using Kaseb.Services;
using Kaseb.Services.AdService;
using Kaseb.Views.Element;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Windows.Input;
using static Android.Provider.MediaStore;

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


                if (_model.ValueOfWeighKG != null)
                    IsShowWeighKG = true;

                if (_model.ValueOfTag1 != null)
                    IsShowTag1 = true;

                if (_model.ValueOfTag2 != null)
                    IsShowTag2 = true;
                ///

                ///Add <,> to Price like 1,000,000 

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
        //Propery For Price
        public string Price
        {
            get =>  string.Format("{0:N0}", long.Parse(Model?.Price ?? ""));
                
            set
            {
                if (Model != null && Model.Price != value)
                {
                    Model.Price = value;
                    OnPropertyChanged(nameof(Price));
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

        //Propery For KG
        
        public bool IsShowWeighKG { get; set; }

        public static readonly BindableProperty IsShowWeighKGProperty =
        BindableProperty.Create(
        nameof(IsShowWeighKG),
        typeof(bool),
        typeof(AdElement),
        defaultValue: false
         );


        //Propery For Tag1
        public bool IsShowTag1 { get; set; }

        //Propery For Tag2
        public bool IsShowTag2 { get; set; }



        //Value For WeighKG
        public string ValueOfWeighKG
        {

            get => Model?.ValueOfWeighKG ?? "" ;

            set
            {
                if (Model != null && Model.ValueOfWeighKG != value)
                {
                    Model.ValueOfWeighKG= value;
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




    }
}
