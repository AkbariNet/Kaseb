using Kaseb.Models.Element;
using Kaseb.Models.Services.AdService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Kaseb.Services.AdService
{
    class UploadAd
    {

        public UploadAd()
        {
            // اگر نیاز به JWT یا کوکی دارید:
            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "YOUR_TOKEN");
        }

        public async Task<bool> UploadAdAsync(AdElementModel ad)
        {
            
            AdServiceModel.SetToken();
            // Serialize خود مدل به JSON
            var json = JsonSerializer.Serialize(ad, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            });

            var contentData = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await AdServiceModel.httpClient.PostAsync("create-ad", contentData);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                // می‌تونید result رو Deserialize کنید و ID یا داده برگشتی را استفاده کنید
                return true;
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                // اینجا می‌تونید لاگ کنید
                return false;
            }
        }
    }
}
