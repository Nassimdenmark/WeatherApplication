using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WeatherApplication.Models;

namespace WeatherApplication.Controllers
{
    public class HomeController : Controller
    {
        readonly string key = "x-rapidapi-key:,e6fdd2cc99mshaa41acf362a7e09p172484jsn4c75f592c564";
        readonly string host = "x-rapidapi-host:,community-open-weather-map.p.rapidapi.com";

        public ActionResult Index()
        {
            var param = "aarhus,dkk";
            var units = "metric";
            var url = String.Format($"https://community-open-weather-map.p.rapidapi.com/weather?q={param}?units={units}");
            var request = WbRequest(url, "GET");
            InsertHead(request, key, host);
            var response = GetHttpResponse(request);
            var _weatherModel = JsonConvert.DeserializeObject<WeatherModel>(response);
            return View(_weatherModel);
        }

        public static void InsertHead(HttpWebRequest request, string _key, string _host)
        {
            var key = _key.Split(',');
            var host = _host.Split(',');
            request.Headers.Add(key[0] + key[1]);
            request.Headers.Add(host[0] + host[1]);
        }

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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}