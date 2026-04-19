using KasebCore.Models.Element;
using KasebCore.Services.Converting;
using KasebCore.Models.Services.AdService;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Convert = KasebCore.Services.Converting.Convert;

namespace KasebCore.Services.Combining
{
    public static class Combine
    {
        public static string CombineDateAndCity(DateTime Date,string City)
        {
            string Value = Convert.ConvertDateTimeToSummeryTime(Date) + " در " + City;


            return Value;
        }

    }
}
