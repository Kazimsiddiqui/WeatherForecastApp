using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherForecastApp.Models
{
    public class ForecastViewModel
    {
        public string Date { get; set; }
        public string Temperature { get; set; }
        public string Description { get; set; }
        /*public string Icon { get; set; } = string.Empty;*/
    }
}