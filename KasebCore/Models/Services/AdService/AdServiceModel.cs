using System;
using System.Collections.Generic;
using System.Text;

namespace KasebCore.Models.Services.AdService
{
    public static class AdServiceModel
    {

         public static HttpClient httpClient=new();
        public static void SetHTTP()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://192.168.0.103:5008/api/");

        }
      
        public static void SetHTTP(Uri uri , string TokenTemp)
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = uri;
/*
            if (Token is not null)
                httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", TokenTemp);*/
        }
    }
}
