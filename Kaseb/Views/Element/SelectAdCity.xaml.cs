
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
    public partial class SelectAdCity : ButtonView, INotifyPropertyChanged
    {
        public SelectAdCity()
        {
            InitializeComponent();


            ChangeCityCommand = new Command<string>((param) =>
            {
                ButtonClicked?.Invoke(Cities);
                /*
                MessageBox messageBox = new MessageBox();
                messageBox.ShowMessage();*/
            });
        }

        public ICommand ChangeCityCommand { get; set; }
        public static Action<Cities?>? ButtonClicked { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        // برای راحتی، اضافه کردن سازندهٔ پارامتر
        public SelectAdCity(SelectAdCityModel _model) : this()
        {
            Model = _model;
        }

        SelectAdCityModel Model = new SelectAdCityModel();
        public string CityName
        {
            get
            {

                return Model.CityName;
            }

        }
        public Cities? Cities
        {
            get { return Model.Cities; }
            set
            {
                Model.Cities = value;
                OnPropertyChanged(nameof(Cities));
                OnPropertyChanged(nameof(CityName));
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
