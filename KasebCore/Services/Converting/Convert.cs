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

        public static double MeterToKilometer(double Meter)
        {
            return Meter / 1000.0;
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
                return "A few minutes ago";
            }
            else if (totalHours >= 0 && totalHours < 24)
            {
                return "A few hours ago";
            }
            else if (totalDays >= 0 && totalDays < 1)
            {
                return "Yesterday";
            }
            else if (totalDays >= 1 && totalDays < 2)
            {
                return "Two days ago";
            }
            else if (totalDays >= 2 && totalDays < 3)
            {
                return "Three days ago";
            }
            else if (totalDays >= 3 && totalDays < 4)
            {
                return "Four days ago";
            }
            else if (totalDays >= 4 && totalDays < 5)
            {
                return "Five days ago";
            }
            else if (totalDays >= 5 && totalDays < 6)
            {
                return "Six days ago";
            }
            else if (totalDays >= 6 && totalDays < 7)
            {
                return "One week ago";
            }
            else if (totalDays >= 7 && totalDays < 14)
            {
                return "Two weeks ago";
            }
            else if (totalDays >= 14 && totalDays < 21)
            {
                return "Three weeks ago";
            }
            else if (totalDays >= 21 && totalDays < 28)
            {
                return "Four weeks ago";
            }
            else
            {
                return "More than one month ago"; // If the time is greater than four weeks
            }

        }

        public static string ConvertCategoryListToCategoryString(Category? category)
        {

            try
            {

                return category switch
                {
                    KasebCore.Models.Element.Category.Worn_texture => "Worn texture",
                    KasebCore.Models.Element.Category.apartment => "Apartment",
                    KasebCore.Models.Element.Category.Land => "Land",
                    KasebCore.Models.Element.Category.Rent => "Rent",
                    KasebCore.Models.Element.Category.house => "house",
                    KasebCore.Models.Element.Category.IsNull => "Empty",
                    _ => "Unknown"
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
                KasebCore.Models.Element.Cities.IsNull => "Empty",
                KasebCore.Models.Element.Cities.Yerevan => "Yerevan",
                KasebCore.Models.Element.Cities.Gyumri => "Gyumri",
                KasebCore.Models.Element.Cities.Vanadzor => "Vanadzor",
                KasebCore.Models.Element.Cities.Abovyan => "Abovyan",
                KasebCore.Models.Element.Cities.Vagharshapat => "Vagharshapat",
                KasebCore.Models.Element.Cities.Hrazdan => "Hrazdan",
                KasebCore.Models.Element.Cities.Kapan => "Kapan",
                KasebCore.Models.Element.Cities.Armavir => "Armavir",
                KasebCore.Models.Element.Cities.Artashat => "Artashat",
                KasebCore.Models.Element.Cities.Ijevan => "Ijevan",
                KasebCore.Models.Element.Cities.Gavar => "Gavar",
                KasebCore.Models.Element.Cities.Goris => "Goris",
                KasebCore.Models.Element.Cities.Charentsavan => "Charentsavan",
                KasebCore.Models.Element.Cities.Masis => "Masis",
                KasebCore.Models.Element.Cities.Ashtarak => "Ashtarak",
                KasebCore.Models.Element.Cities.Sevan => "Sevan",
                KasebCore.Models.Element.Cities.Dilijan => "Dilijan",
                KasebCore.Models.Element.Cities.Spitak => "Spitak",
                KasebCore.Models.Element.Cities.Sisian => "Sisian",
                KasebCore.Models.Element.Cities.Stepanavan => "Stepanavan",
                KasebCore.Models.Element.Cities.Martuni => "Martuni",
                KasebCore.Models.Element.Cities.Vardenis => "Vardenis",
                KasebCore.Models.Element.Cities.Yeghvard => "Yeghvard",
                KasebCore.Models.Element.Cities.Byureghavan => "Byureghavan",
                KasebCore.Models.Element.Cities.NorHachn => "Nor Hachn",
                KasebCore.Models.Element.Cities.Aparan => "Aparan",
                KasebCore.Models.Element.Cities.Berd => "Berd",
                KasebCore.Models.Element.Cities.Tashir => "Tashir",
                KasebCore.Models.Element.Cities.Alaverdi => "Alaverdi",
                KasebCore.Models.Element.Cities.Noyemberyan => "Noyemberyan",
                KasebCore.Models.Element.Cities.Jermuk => "Jermuk",
                KasebCore.Models.Element.Cities.Chambarak => "Chambarak",
                KasebCore.Models.Element.Cities.Metsamor => "Metsamor",
                KasebCore.Models.Element.Cities.Vedi => "Vedi",
                KasebCore.Models.Element.Cities.Maralik => "Maralik",
                KasebCore.Models.Element.Cities.Talin => "Talin",
                KasebCore.Models.Element.Cities.Tumanyan => "Tumanyan",
                KasebCore.Models.Element.Cities.Meghri => "Meghri",
                KasebCore.Models.Element.Cities.Agarak => "Agarak",
                KasebCore.Models.Element.Cities.Kajaran => "Kajaran",
                KasebCore.Models.Element.Cities.Dastakert => "Dastakert",
                KasebCore.Models.Element.Cities.Shamlugh => "Shamlugh",
                KasebCore.Models.Element.Cities.Ayrum => "Ayrum",
                KasebCore.Models.Element.Cities.Tsaghkadzor => "Tsaghkadzor",
                KasebCore.Models.Element.Cities.Ararat => "Ararat",
                _ => "Unknown"
            };
        }
        public static string ConvertMeterDecimalToMeterString (decimal MeterDecimal)
        {
                        return MeterDecimal >= 1000
                        ? MeterToKilometer((double)MeterDecimal).ToString() + " Kilometer"
                        : ((double)MeterDecimal).ToString() + " Meter";


        }
        public static string ConvertPriceDecimalToPriceStringWithSeprator(decimal Price)
        {
            return Price.ToString("N0", CultureInfo.CreateSpecificCulture("fa-IR"));
        }
    }
}
