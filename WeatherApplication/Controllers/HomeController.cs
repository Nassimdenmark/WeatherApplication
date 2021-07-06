using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var _weatherModelFromApi = JsonConvert.DeserializeObject<WeatherModel>(response);
            var a = ConvertToLocal(_weatherModelFromApi);
            return View(a);
        }

        public ListCollectionDate ConvertToLocal(WeatherModel _weatherModelFromApi)
        {
            ListCollectionDate lc = new ListCollectionDate() 
            { 
                ListOfCollections = new List<ListCollection>()
            };
            var current = String.Empty;
            foreach (var w in _weatherModelFromApi.List)
            {
                current = w.DtTxt.ToString().Split(' ')[0]; // 05-07-2021
                //var isCurrent = lc.ListOfCollections.GroupBy(x => x.DateName.Equals(current)).FirstOrDefault();
                var isCurrent = lc.ListOfCollections.Where(e => e.DateName.Equals(current)).Select(e => e).FirstOrDefault();
                if (isCurrent == null)
                {
                    lc.ListOfCollections.Add(
                        new ListCollection
                        {
                            DateName = current,
                            ListOfDateTimes = new List<List>() { w }
                        });
                } else
                {
                    isCurrent.ListOfDateTimes.Add(w);
                }
            }
            return lc;
        }
    }
}