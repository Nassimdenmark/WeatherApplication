using System;
using System.IO;
using System.Net;

namespace WeatherApplication.Utils
{
    public class HelperClass
    {
        public static HttpWebRequest WbRequest(string shopAddress, string methodType)
        {
            var request = (HttpWebRequest)WebRequest.Create(shopAddress);
            request.Method = methodType;
            return request;
        }

        public static string GetHttpResponse(HttpWebRequest request)
        {
            string responseString = "";
            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (var stream = response.GetResponseStream())
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            responseString = reader.ReadToEnd();
                        }
                    }
                }
                return responseString;
            }
            catch (Exception ex) { return ex.Message; }
        }

        public static void InsertHead(HttpWebRequest request, string _key, string _host)
        {
            var key = _key.Split(',');
            var host = _host.Split(',');
            request.Headers.Add(key[0] + key[1]);
            request.Headers.Add(host[0] + host[1]);
        }
    }
}