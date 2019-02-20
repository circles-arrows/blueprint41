using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace Blueprint41.Modeller
{
    public static class UriConfig
    {
        public static string LicensingUrl => ConfigurationManager.AppSettings.Get("LicensingUrl");
        public static Uri ConnectorUri => new Uri(new Uri(LicensingUrl), @"api/license/connector");
        public static Uri PingUri => new Uri(new Uri(LicensingUrl), @"api/license/ping");

        internal static async Task<bool> CheckServerIsOnline()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(PingUri);
                    return response.IsSuccessStatusCode;
                }
            }
            catch (HttpRequestException e)
            {
                return false;
            }
        }
    }
}
