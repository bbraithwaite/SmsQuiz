using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;

namespace BB.SmsQuiz.Web.App_Start
{
    public class ApiClient
    {
        public static HttpClient GetClient()
        {
            var client = new HttpClient()
                {
                    BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseApiUri"]),
                };

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }
}