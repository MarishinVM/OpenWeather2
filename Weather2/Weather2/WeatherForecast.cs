using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather2
{
    public class WeatherForecast
    {
        public city city { get; set; }
        public List<list> list { get; set; } // прогноз

    }

    public class main
    {
        public double temp { get; set; }
        public double speed { get; set; }
    }

    public class temp
    {
        public double day { get; set; }
    }

    public class weather
    {
        public string main { get; set; } // состояние погоды
        public string description { get; set; } // описание
        public string icon { get; set; } // изображение
    }

    public class wind
    {
        public double speed { get; set; }
    }

    public class city
    {
        public string name { get; set; }
    }

    public class list
    {
        public double dt { get; set; }
        public double pressure { get; set; }
        public double humidity { get; set; }
        public wind wind { get; set; }
        public temp temp { get; set; }
        public List<weather> weather { get; set; }
        public string dt_txt { get; set; }
        public main main { get; set; }


    }
}
