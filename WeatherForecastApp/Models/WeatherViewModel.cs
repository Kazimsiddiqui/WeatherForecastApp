using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherForecastApp.Models
{
    public class WeatherViewModel
    {
        public string City { get; set; }
        public string Description { get; set; }
        public double Temperature { get; set; }
        /*public string Icon { get; set; }*/
    }
}