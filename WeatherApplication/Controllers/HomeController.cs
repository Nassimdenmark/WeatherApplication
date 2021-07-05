using Newtonsoft.Json;
using System;
using System.Web.Mvc;
using WeatherApplication.Models;
using WeatherApplication.Utils;

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
            var url = String.Format($"https://community-open-weather-map.p.rapidapi.com/forecast?q={param}?units={units}");
            var request = HelperClass.WbRequest(url, "GET");
            HelperClass.InsertHead(request, key, host);
            var response = HelperClass.GetHttpResponse(request);
            var _weatherModel = JsonConvert.DeserializeObject<WeatherModel>(response); // test
            return View(_weatherModel);
        }
    }
}