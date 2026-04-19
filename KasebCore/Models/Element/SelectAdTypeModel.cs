using System;
using System.Collections.Generic;
using System.Text;

namespace KasebCore.Models.Element
{
    public class SelectAdTypeModel
    {
        public string CategoryName
        {
            get
            {

                return Category switch
                {
                    Element.Category.Garlic => "سیر",
                    Element.Category.Shallot => "موسیر",
                    Element.Category.Walnut => "گردو",
                    Element.Category.Potato => "سیب زمینی",
                    Element.Category.Cucumber => "خیار",
                    Element.Category.Tomato => "گوجه فرنگی",
                    Element.Category.Mushroom => "قارچ",
                    Element.Category.Almond => "بادام",
                    Element.Category.IsNull => "هنوز انتخاب نشده"
                };
            }

        }
        
        public Category? Category { get; set; }
        public string icon { get; set; } = String.Empty;

    }
    public class SelectAdCityModel()
    {

        public string icon { get; set; } = String.Empty;
        public Cities? Cities { get; set; }
        public string CityName
        {
            get
            {

                return Cities switch
                {

                    Element.Cities.IsNull => "نامشخص",
                    Element.Cities.Barfejin => "برفجین",
                    Element.Cities.Toejin => "توئجین",
                    Element.Cities.Muejin => "موئجین",
                    Element.Cities.Selulan => "سلولان",
                    Element.Cities.HeydareBalaShahr => "حیدره بالای شهر",
                    Element.Cities.Maryanaj => "مریانج",
                    Element.Cities.Bahar => "بهار",
                };
            }

        }
    }
}
