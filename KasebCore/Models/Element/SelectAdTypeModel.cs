using System;
using System.Collections.Generic;
using System.Text;
using ConvertCore = KasebCore.Services.Converting.Convert;

namespace KasebCore.Models.Element
{
    public class SelectAdTypeModel
    {
        public string CategoryName
        {
            get
            {
             return ConvertCore.ConvertCategoryListToCategoryString(Category);
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

                return ConvertCore.ConvertCityListToCityString(Cities);
            }

        }
    }
}
