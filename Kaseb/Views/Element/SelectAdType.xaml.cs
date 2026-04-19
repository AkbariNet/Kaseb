
using Kaseb.Services;
using Kaseb.ViewModels.Element;
using KasebCore.Models.Element;
using Microsoft.Maui.Devices.Sensors;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using UraniumUI.Material.Controls;
namespace Kaseb.Views.Element
{
    public partial class SelectAdType : ButtonView, INotifyPropertyChanged
    {
        public SelectAdType()
        {
            InitializeComponent();


            ChangeCategoryCommand = new Command<string>((param) =>
            {
                ButtonClicked?.Invoke(Category);
                /*
                MessageBox messageBox = new MessageBox();
                messageBox.ShowMessage();*/
            });
        }

        public ICommand ChangeCategoryCommand { get; set; }
        public static Action<Category?>? ButtonClicked { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        // برای راحتی، اضافه کردن سازندهٔ پارامتر
        public SelectAdType(SelectAdTypeModel _model) : this()
        {
            Model = _model;
        }

        SelectAdTypeModel Model = new SelectAdTypeModel();
        public string CategoryName
        {
            get
            {

                return Model.CategoryName;
            }

        }
        public Category? Category
        {
            get { return Model.Category; }
            set
            {
                Model.Category = value;
                OnPropertyChanged(nameof(Category));
                OnPropertyChanged(nameof(CategoryName));
            }
        }
        public string icon
        {
            get { return Model.icon; }
            set { 
                Model.icon = value;
                OnPropertyChanged(nameof(icon));
            }
        }
    }

}
