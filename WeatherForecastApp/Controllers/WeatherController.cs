using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherForecastApp.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Linq; // if you use JObject

namespace WeatherForecastApp.Controllers
{
    public class WeatherController : Controller
    {
        // GET: Weather
        private readonly string apiKey = "083e7aa454ce805ac3cfdee0de0cdacb";

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(string city)
        {
            var weather = await GetWeather(city);
            return View(weather);
        }

        private async Task<WeatherViewModel> GetWeather(string city)
        {
            WeatherViewModel weather = new WeatherViewModel();
            List<ForecastViewModel> forecastList = new List<ForecastViewModel>();

            using (HttpClient client = new HttpClient())
            {
                // --- Get Current Weather ---
                string currentUrl = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";
                HttpResponseMessage response = await client.GetAsync(currentUrl);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    JObject data = JObject.Parse(json);

                    weather.City = data["name"].ToString();
                    weather.Description = data["weather"][0]["description"].ToString();
                    weather.Temperature = double.Parse(data["main"]["temp"].ToString());
                    // Optional: weather.Icon = data["weather"][0]["icon"].ToString();
                }

                // --- Get 5-Day Forecast ---
                string forecastUrl = $"https://api.openweathermap.org/data/2.5/forecast?q={city}&appid={apiKey}&units=metric";
                HttpResponseMessage forecastResponse = await client.GetAsync(forecastUrl);

                if (forecastResponse.IsSuccessStatusCode)
                {
                    var forecastData = JObject.Parse(await forecastResponse.Content.ReadAsStringAsync());

                    foreach (var item in forecastData["list"].Where((_, index) => index % 8 == 0))
                    {
                        forecastList.Add(new ForecastViewModel
                        {
                            Date = item["dt_txt"].ToString(),
                            Temperature = item["main"]["temp"].ToString(),
                            Description = item["weather"][0]["description"].ToString(),
                            /*Icon = item["weather"][0]["icon"].ToString()*/
                        });
                    }
                }
            }

            ViewBag.Forecast = forecastList;
            return weather;
        }

    }
}