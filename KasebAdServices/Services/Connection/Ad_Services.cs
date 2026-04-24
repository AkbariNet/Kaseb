using KasebCore.Models.Element;
using KasebCore.Models.Search;
using KasebCore.Models.Services.AdService;
using KasebCore.Services.Converting;
using System.Text.Json;
using System.Web;
using ConvertCore = KasebCore.Services.Converting.Convert;
using Convert = System.Convert;

namespace KasebAdServices.Services.Connection
{
    public class Ad_Services
    {
         static Ad_Services()
        {
            AdServiceModel.SetHTTP();
        }
        public async Task<string> PostStringAdsAsync()
        {
            try
            {

                var form = new MultipartFormDataContent();

                var properties = SearchModel.MainSearchModel.GetType().GetProperties();

                foreach (var prop in properties)
                {
                    var value = prop.GetValue(SearchModel.MainSearchModel);

                    if (value == null)
                        continue;

                    form.Add(
                        new StringContent(value.ToString()),
                        prop.Name
                    );
                }

                try
                {

                    var response = await AdServiceModel.httpClient.PostAsync("api/ads/search", form);
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();

                    }
                    else
                    {
                        Console.WriteLine(response.Content.ToString());
                        return "";
                    }
                }
                catch (Exception ex)
                {
                    return ""; 
                }
                

            }
            catch { return ""; }
        }
        public async Task<List<AdElementModel>> ReadAdFromDatabase(bool isUpdate)
        {
            if (isUpdate)
            {
                try
                {
                    //var response = await AdServiceModel.httpClient.GetStringAsync($"api/ads/MaximumAds=5&LastID={lastID}");

                    var response = await PostStringAdsAsync();
                    Console.WriteLine(response);

                    //List<AdElementModel> adElement = JsonSerializer.Deserialize<List<AdElementModel>>(response);
                    List<AdElementModel> Respound = JsonSerializer.Deserialize<List<AdElementModel>>(response);
                    if (Respound != null)
                    {

                        foreach (AdElementModel model in Respound)
                        {
                            if (model?.Images?.Count > 0)
                            {
                                foreach (AdImage image in model.Images)
                                {
                                    model.ImageLinks.Add(ConvertCore.ConvertImagePathToLink(image.ImagePath));
                                
                                }

                            }
                            else
                            {
                                model?.Images?.Add(new AdImage());
                            }
                            if (model?.ImageLinks.Count > 0)
                            {
                                model?.MainImageLink = model.ImageLinks[0];

                            }
                        }

                        SearchModel.MainSearchModel.LastAdID = (int)Respound[Respound.Count - 1].Id;
                        Console.WriteLine("لیست دریافت شد ");
                        return Respound;
                    }
                    else
                    {
                        Console.WriteLine("بدون محتوا ");
                        return new List<AdElementModel>();  // حداقل لیست خالی برگرده

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("2خطا در دریافت داده: " + ex.Message);
                    return new List<AdElementModel>();  // حداقل لیست خالی برگرده
                }
            }
            else 
            try
            {
                    //var response = await AdServiceModel.httpClient.GetStringAsync("api/ads/MaxAds=10");
                    SearchModel.MainSearchModel.LastAdID = -1;
                    var response = await PostStringAdsAsync();
                    Console.WriteLine(response);
                //List<AdElementModel> adElement = JsonSerializer.Deserialize<List<AdElementModel>>(response);
                List<AdElementModel> Respound = JsonSerializer.Deserialize<List<AdElementModel>>(response);
                if (Respound != null && Respound.Count>0)
                {

                    foreach (AdElementModel model in Respound)
                    {
                        if (model?.Images?.Count>0)
                        {
                            foreach (AdImage image in model.Images)
                            {
                                model.ImageLinks.Add(ConvertCore.ConvertImagePathToLink(image.ImagePath));
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

                        SearchModel.MainSearchModel.LastAdID = (int)Respound[Respound.Count - 1].Id;
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



        public List<AdElementModel> AdsModel { get; set; } = new();

        public Action AdsChanged;
        public Action isLazyLoadCompleted;
        public async Task<List<AdElementModel>> LoadAdsAsync(bool isUpdate=false)
        {
            AdsModel.Clear();
            if (isUpdate)
            {
                foreach (var ad in await ReadAdFromDatabase(true))
                {/*
                    if (ad.ValueOfWeighKG !=0)
                    {
                        ad.ValueOfWeighKG = ad.ValueOfWeighKG >= 1000
                        ? KasebProcessor.KiloToTon(ad.ValueOfWeighKG).ToString() + " تن"
                        : ad.ValueOfWeighKG + " کیلوگرم";

                    }*/
                    AdsModel.Add(ad);
                }

            }
            else
            {
                AdsModel.Clear();
                foreach (var ad in await ReadAdFromDatabase(false))
                {/*
                    if (ad.ValueOfWeighKG !=0)
                    {
                        ad.ValueOfWeighKG = ad.ValueOfWeighKG >= 1000
                        ? KasebProcessor.KiloToTon(double.Parse(ad.ValueOfWeighKG)).ToString() + " تن"
                        : ad.ValueOfWeighKG + " کیلوگرم";

                    }*/
                    AdsModel.Add(ad);
                }

            }

            isLazyLoadCompleted.Invoke();
            AdsChanged.Invoke();
            return AdsModel;
        }
    }

}
