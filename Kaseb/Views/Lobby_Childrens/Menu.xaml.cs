using Kaseb.Models;
using Kaseb.Services;
using Microsoft.Maui.Devices.Sensors;
using System.Linq;
using System.Windows.Input;
using UraniumUI.Material.Controls;
using UraniumUI.Pages;
namespace Kaseb.Views.Lobby_Childrens
{
    public partial class MenuAPP : Grid 
    {

        /// <summary>
        /// for Click Item of menu  
        /// </summary>
        /// <param name="isAdClicked"></param>
        /// <returns></returns>

        public static readonly BindableProperty isAdClickedProperty =
        BindableProperty.Create(
        nameof(isAdClicked),
        typeof(bool),
        typeof(MenuAPP),
        defaultValue: false
         );

        public bool isAdClicked
        {
            get => (bool)GetValue(isAdClickedProperty);
            set {
                SetValue(isAdClickedProperty, value);
                isMapClicked = false; isAddAdClicked = false;
            }
        }

        /// <summary>
        /// 2
        /// </summary>
        /// 
        public bool isMapClicked
        {
            get => (bool)GetValue(isMapClickedProperty);
            set
            {
                SetValue(isMapClickedProperty, value);
                isAdClicked = false; isAddAdClicked = false;
            }
        }

        public static readonly BindableProperty isMapClickedProperty =
        BindableProperty.Create(
        nameof(isMapClicked),
        typeof(bool),
        typeof(MenuAPP),
        defaultValue: false
         );

        /// <summary>
        /// 3
        /// </summary>

        public bool isAddAdClicked
        {
            get => (bool)GetValue(isAddAdClickedProperty);
            set
            {
                SetValue(isAddAdClickedProperty, value);
                isAdClicked = false; isMapClicked = false;
            }
        }


        public static readonly BindableProperty isAddAdClickedProperty =
        BindableProperty.Create(
        nameof(isAddAdClicked),
        typeof(bool),
        typeof(MenuAPP),
        defaultValue: false
         );


        public MenuAPP()
        {

            InitializeComponent();
        }
        
    }

}
