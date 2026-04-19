/*using KasebCore.Models.Element;
using Kaseb.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace Kaseb.ViewModels.Element
{
    public class SelectAdTypeVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        SelectAdTypeModel Model = new SelectAdTypeModel();
        public string CategoryName
        {
            get { return Model.CategoryName; }
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
            set
            {
                Model.icon = value;
                OnPropertyChanged(nameof(icon));
            }
        }

        public Action<Category?> ButtonClicked { get; set; }

        public SelectAdTypeVM()
        {

            ChangeCategoryCommand = new Command<string>((param) =>
            {
                ButtonClicked?.Invoke(Category);
                *//*
                MessageBox messageBox = new MessageBox();
                messageBox.ShowMessage();*//*
            });

        }

        public ICommand ChangeCategoryCommand { get; set; }
    }
}
*/