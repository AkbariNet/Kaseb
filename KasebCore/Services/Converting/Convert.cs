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
    }
}
