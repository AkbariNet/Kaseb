using System;
using System.Collections.Generic;
using System.Text;

namespace Kaseb.Models.Services.AdService
{
    class AdServiceModel
    {

         public static HttpClient httpClient;
        private static string Token;
        public static void SetHTTP()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://moradstudio.com/wp-json/kaseb/v1/");

        }
        public static void UnSetToken() => httpClient.DefaultRequestHeaders.Authorization = null;
        
        public static void SetToken()
        {
            Token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJodHRwczovL21vcmFkc3R1ZGlvLmNvbSIsImlhdCI6MTc1OTUzMDc1NiwibmJmIjoxNzU5NTMwNzU2LCJleHAiOjE3NjAxMzU1NTYsImRhdGEiOnsidXNlciI6eyJpZCI6IjE3In19fQ.09YzMVg8vNdS6c_xLeKcYK1vnzKo0sgC2kcs6opLL28";


            if (Token is not null)
                httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);
        }
        public static void SetHTTP(Uri uri , string TokenTemp)
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = uri;

            if (Token is not null)
                httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", TokenTemp);
        }
    }
}
