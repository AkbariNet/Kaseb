using KasebCore;
using KasebCore.Models.Element;
using Kaseb.Services.Calculating;
using KasebCore.Models.Services.AdService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;

namespace KasebAdServices.Connection
{
    public class Ad_Services
    {
        int lastID = 0;
         static Ad_Services()
        {
            AdServiceModel.SetHTTP();
        }
        public async Task<List<AdElementModel>> ReadAdFromDatabase()
        {
            try
            { 
                var response = await AdServiceModel.httpClient.GetStringAsync("ads?MaxValue=10");
                Console.WriteLine(response);

                //List<AdElementModel> adElement = JsonSerializer.Deserialize<List<AdElementModel>>(response);
                List<AdElementModel> Respound = JsonSerializer.Deserialize<List<AdElementModel>>(response);
                lastID = Respound[Respound.Count - 1].Id;
                //  return adElements;

                Console.WriteLine("لیست دریافت شد ");
                return Respound;
            }
            catch (Exception ex)
            {
                Console.WriteLine("خطا در دریافت داده: " + ex.Message);
                return new List<AdElementModel>();  // حداقل لیست خالی برگرده
            }
        }


        public async Task<List<AdElementModel>> ReadAdFromDatabase(bool isUpdate)
        {
            if (isUpdate)
            {
                try
                {
                    var response = await AdServiceModel.httpClient.GetStringAsync("ads?per_page=10&after_id=" + lastID);
                    Console.WriteLine(response);

                    //List<AdElementModel> adElement = JsonSerializer.Deserialize<List<AdElementModel>>(response);
                    List<AdElementModel> Respound = JsonSerializer.Deserialize<List<AdElementModel>>(response);
                    lastID = Respound[Respound.Count - 1].Id;
                    //  return adElements;*/
                    return Respound;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("2خطا در دریافت داده: " + ex.Message);
                    return new List<AdElementModel>();  // حداقل لیست خالی برگرده
                }
            }
            else return new List<AdElementModel>();
        }

        public List<AdElementModel> AdsModel { get; set; } = new();

        public Action AdsChanged;
        public Action isLazyLoadCompleted;
        public async Task<List<AdElementModel>> LoadAdsAsync(bool isUpdate=false)
        {
            AdsModel.Clear();
            if (isUpdate)
            {
                foreach (var ad in await ReadAdFromDatabase(true))
                {
                    if (ad.ValueOfWeighKG is not null)
                    {
                        ad.ValueOfWeighKG = uint.Parse(ad.ValueOfWeighKG) >= 1000
                        ? KasebProcessor.KiloToTon(double.Parse(ad.ValueOfWeighKG)).ToString() + " تن"
                        : ad.ValueOfWeighKG + " کیلوگرم";

                    }
                    AdsModel.Add(ad);
                }

            }
            else
            {
                AdsModel.Clear();
                foreach (var ad in await ReadAdFromDatabase())
                {
                    if (ad.ValueOfWeighKG is not null)
                    {
                        ad.ValueOfWeighKG = uint.Parse(ad.ValueOfWeighKG) >= 1000
                        ? KasebProcessor.KiloToTon(double.Parse(ad.ValueOfWeighKG)).ToString() + " تن"
                        : ad.ValueOfWeighKG + " کیلوگرم";

                    }
                    AdsModel.Add(ad);
                }

            }

            isLazyLoadCompleted.Invoke();
            AdsChanged.Invoke();
            return AdsModel;
        }
    }

}
