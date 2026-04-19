using KasebCore.Models.Element;
using KasebCore.Services.Converting;
using Convert = KasebCore.Services.Converting.Convert;
using KasebCore.Models.Services.AdService;
using KasebAdServices.Services.Calculating;
using System.Text.Json;

namespace KasebAdServices.Services.Connection
{
    public class Ad_Services
    {
        int? lastID = 0;
         static Ad_Services()
        {
            AdServiceModel.SetHTTP();
        }
        public async Task<List<AdElementModel>> ReadAdFromDatabase()
        {
            try
            { 
                var response = await AdServiceModel.httpClient.GetStringAsync("api/ads/MaxAds=10");
                Console.WriteLine(response);
                //List<AdElementModel> adElement = JsonSerializer.Deserialize<List<AdElementModel>>(response);
                List<AdElementModel> Respound = JsonSerializer.Deserialize<List<AdElementModel>>(response);
                if (Respound != null)
                {

                    foreach (AdElementModel model in Respound)
                    {
                        if (model?.Images?.Count>0)
                        {
                            foreach (AdImage image in model.Images)
                            {
                                model.ImageLinks.Add(Convert.ConvertImagePathToLink(image.ImagePath));
                            }

                        }
                        else
                        {
                            model?.Images?.Add(new AdImage());
                        }
                        if (model?.ImageLinks.Count>0)
                        {
                            model?.MainImageLink = model.ImageLinks[0];

                        }
                    }

                    lastID = Respound[Respound.Count - 1].Id;
                }
                //  return adElements;
                if (Respound != null)
                {
                    Console.WriteLine("لیست دریافت شد ");
                    return Respound;

                }
                else
                {
                    Console.WriteLine("بدون محتوا " );
                    return new List<AdElementModel>();  // حداقل لیست خالی برگرده

                }

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
                    var response = await AdServiceModel.httpClient.GetStringAsync($"api/ads/MaximumAds=5&LastID={lastID}");
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
