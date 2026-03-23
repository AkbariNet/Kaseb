using KasebCore;
using KasebCore.Models.Element;
using KasebCore.Models.Services.AdService;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace KasebAdServices.Connection
{
    public static class UploadAd
    {
        /// <summary>
        /// The Process of Uploading ad to database with static methods
        /// </summary>
        /// <param name="model"></param>
        /// <returns> </returns>
    
        public static async Task<GetResoultInfo> UploadAdAsync(AdElementModel model)
        {
            try
            {

                var form = new MultipartFormDataContent();

                var properties = model.GetType().GetProperties();

                foreach (var prop in properties)
                {
                    var value = prop.GetValue(model);

                    if (value == null)
                        continue;

                    // اگر لیست string بود → عکس‌ها
                    if (value is IEnumerable<string> paths && prop.Name == "ImagePaths")
                    {
                        foreach (var path in paths)
                        {
                            var stream = File.OpenRead(path);

                            var content = new StreamContent(stream);

                            content.Headers.ContentType =
                                new MediaTypeHeaderValue("image/jpeg");

                            form.Add(content, "Images", Path.GetFileName(path));
                        }
                    }
                    else
                    {
                        form.Add(
                            new StringContent(value.ToString()),
                            prop.Name
                        );
                    }
                }

                try
                {

                   var response = await AdServiceModel.httpClient.PostAsync("ads/create-ad", form);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return new GetResoultInfo()
                        {
                            IsSuccess = true,
                            Message = "Ad uploaded successfully",
                            Data = result,
                            StatusCode = (int)response.StatusCode
                        };
                    }
                    else
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        // For Logging: Console.WriteLine($"Error uploading ad: {error}");
                        return new GetResoultInfo()
                        {
                            IsSuccess = false,
                            Message = "Failed to upload ad",
                            Data = error,
                            StatusCode = (int)response.StatusCode
                        };

                    }
                }
                catch (Exception a)
                {
                    return new GetResoultInfo()
                    {
                        IsSuccess = false,
                        Message = "Failed to connect to the database",
                        StatusCode = (int)a.HResult,
                    };
                    throw;
                }

            }
            catch (Exception a)
            {
                return new GetResoultInfo()
                {
                    IsSuccess = false,
                    Message = "Failed to add information",
                    StatusCode = (int)a.HResult,
                };
                throw;
            }
        }

        public static async Task<GetResoultInfo> UploadAdAsync(AdElementModel ad,bool useOldFunction)
        {
            if (useOldFunction)
            {

                // Serialize خود مدل به JSON
                var json = JsonSerializer.Serialize(ad, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                });

                var contentData = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await AdServiceModel.httpClient.PostAsync("ads/create-ad", contentData);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    // می‌تونید result رو Deserialize کنید و ID یا داده برگشتی را استفاده کنید
                    return new GetResoultInfo()
                    {
                        IsSuccess = true,
                        Message = "Ad uploaded successfully",
                        Data = result,
                        StatusCode = (int)response.StatusCode
                    };
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    // اینجا می‌تونید لاگ کنید
                    return new GetResoultInfo()
                    {
                        IsSuccess = false,
                        Message = "Failed to upload ad",
                        Data = error,
                        StatusCode = (int)response.StatusCode
                    };
                }
            }
            else return await UploadAdAsync(ad);
        }
    }
}
