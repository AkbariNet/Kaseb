using KasebCore.Models.Element;
using KasebCore.Models.Services.AdService;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace KasebCore.Services.Converting
{
    public static class Convert
    {
        public static string ConvertImagePathToLink(string ImagePath)
        {
            string Link = AdServiceModel.httpClient.BaseAddress + "images/" + ImagePath;
            return Link;
        }

        public static string ConvertDateTimeToSummeryTime(DateTime dateTime)
        {
            TimeSpan timeDifference = DateTime.Now - dateTime;
            double totalMinutes = timeDifference.TotalMinutes;
            double totalHours = timeDifference.TotalHours;
            double totalDays = timeDifference.TotalDays;
            double totalWeeks = timeDifference.TotalDays / 7;

            if (totalMinutes >= 0 && totalMinutes < 59)
            {
                return "دقایقی پیش";
            }
            else if (totalHours >= 0 && totalHours < 24)
            {
                return "ساعاتی پیش";
            }
            else if (totalDays >= 0 && totalDays < 1)
            {
                return "دیروز";
            }
            else if (totalDays >= 1 && totalDays < 2)
            {
                return "پریروز";
            }
            else if (totalDays >= 2 && totalDays < 3)
            {
                return "سه روز پیش";
            }
            else if (totalDays >= 3 && totalDays < 4)
            {
                return "چهار روز پیش";
            }
            else if (totalDays >= 4 && totalDays < 5)
            {
                return "پنج روز پیش";
            }
            else if (totalDays >= 5 && totalDays < 6)
            {
                return "شش روز پیش";
            }
            else if (totalDays >= 6 && totalDays < 7)
            {
                return "یک هفته پیش";
            }
            else if (totalDays >= 7 && totalDays < 14)
            {
                return "دو هفته پیش";
            }
            else if (totalDays >= 14 && totalDays < 21)
            {
                return "سه هفته پیش";
            }
            else if (totalDays >= 21 && totalDays < 28)
            {
                return "چهار هفته پیش";
            }
            else
            {
                return "بیش از چهار هفته پیش";  // اگر زمان بیشتر از چهار هفته است
            }
        }

        public static string ConvertCategoryListToCategoryString(Category? category)
        {

            try
            {

                return category switch
                {
                    KasebCore.Models.Element.Category.Garlic => "سیر",
                    KasebCore.Models.Element.Category.Shallot => "موسیر",
                    KasebCore.Models.Element.Category.Walnut => "گردو",
                    KasebCore.Models.Element.Category.Potato => "سیب زمینی",
                    KasebCore.Models.Element.Category.Cucumber => "خیار",
                    KasebCore.Models.Element.Category.Tomato => "گوجه فرنگی",
                    KasebCore.Models.Element.Category.Mushroom => "قارچ",
                    KasebCore.Models.Element.Category.Almond => "بادام",
                    KasebCore.Models.Element.Category.IsNull => "هنوز انتخاب نشده",
                    _ => "نامشخص"
                };
            }
            catch (Exception)
            {
                return "error";
                throw;
            }
        }
        public static string ConvertCityListToCityString(Cities? city)
        {
            return city switch
            {
                KasebCore.Models.Element.Cities.IsNull => "هنوز انتخاب نشده",
                KasebCore.Models.Element.Cities.Barfejin => "برفجین",
                KasebCore.Models.Element.Cities.Toejin => "توئجین",
                KasebCore.Models.Element.Cities.Muejin => "موئجین",
                KasebCore.Models.Element.Cities.Selulan => "سلولان",
                KasebCore.Models.Element.Cities.HeydareBalaShahr => "حیدره بالای شهر",
                KasebCore.Models.Element.Cities.Maryanaj => "مریانج",
                KasebCore.Models.Element.Cities.Bahar => "بهار",
                _ => "تعریف نشده",
            };
        }
    }
}
