using KasebCore.Models.Element;
using Kaseb.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace Kaseb.ViewModels.Element
{
    class SelectAdTypeVM: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public string CategoryName { set; get; }
        public Category _Category
        {
            get;
            set
            { 
                CategoryName = value switch
                {
                    Category.Garlic => "سیر",
                    Category.Shallot => "موسیر",
                    Category.Walnut => "گردو",
                    Category.Potato => "سیب زمینی",
                    Category.Cucumber => "خیار",
                    Category.Tomato => "گوجه فرنگی",
                    Category.Mushroom => "قارچ",
                    Category.Almond => "بادام",
                    Category.IsNull => "هنوز انتخاب نشده"
                };
                Category = value;
            }
        }
        public Category Category { get; set; }
        public string icon { get; set; }

        public Action<Category> ButtonClicked { get; set; }

        public SelectAdTypeVM()
        {
            ChangeCategoryCommand = new Command<string>((param) =>
            {
                ButtonClicked?.Invoke(Category);
                /*
                MessageBox messageBox = new MessageBox();
                messageBox.ShowMessage();*/
            });

        }

        public ICommand ChangeCategoryCommand { get; }
    }
}
