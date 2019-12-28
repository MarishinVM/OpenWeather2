using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Weather2
{
    public partial class Form1 : Form
    {
        const string APPID = "339ec1932650a0effff53b12ca9f8fb4";
        string cityName = "Moscow";
        public Form1()
        {
            InitializeComponent();
            getWeather(cityName);
            getForecast(cityName);
        }

        void getWeather(string city)
        {
            using (WebClient web = new WebClient())
            {
                string url = string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&APPID={1}&units=metric&cnt=6" , city , APPID);

                var json = web.DownloadString(url);

                var result = JsonConvert.DeserializeObject<WeatherInfo.root>(json);

                WeatherInfo.root outPut = result;

                label_city.Text = string.Format("{0}" , outPut.name);

                label_Region.Text = string.Format("{0}" , outPut.sys.country);

                label_CurTemp.Text = string.Format("{0} \u00B0"+"C" , outPut.main.temp);


                picture_1.Image = setIcon(outPut.weather[0].icon);
            }

        }

        void getForecast(string city)
        {
            int day = 5;

            string url = string.Format("http://api.openweathermap.org/data/2.5/forecast?q={0},ru&units=metric&cnt={1}&APPID={2}", city , day, APPID);

            using (WebClient web = new WebClient())
            {
                var json = web.DownloadString(url);

                var Object = JsonConvert.DeserializeObject<WeatherForecast>(json);

                WeatherForecast forecast = Object;

                //3 часа

                label_day.Text = string.Format("{0}", getDate(forecast.list[0].dt).DayOfWeek);

                label_date.Text = string.Format("{0}", getDate(forecast.list[0].dt));

                label_con.Text = string.Format("{0}", forecast.list[0].weather[0].main); // состояние погоды

                label_des.Text = string.Format("{0}", forecast.list[0].weather[0].description); // описание

                label_temp.Text = string.Format("{0}\u00B0" + "C", forecast.list[0].main.temp);

                label_speed.Text = string.Format("{0} m/s", forecast.list[0].wind.speed);

                //6 часов

                label_day2.Text = string.Format("{0}", getDate(forecast.list[1].dt).DayOfWeek);

                label_date2.Text = string.Format("{0}", getDate(forecast.list[1].dt));

                label_con2.Text = string.Format("{0}", forecast.list[1].weather[0].main); // состояние погоды

                label_des2.Text = string.Format("{0}", forecast.list[1].weather[0].description); // описание

                label_temp2.Text = string.Format("{0}\u00B0" + "C", forecast.list[1].main.temp);

                label_speed2.Text = string.Format("{0} m/s", forecast.list[1].wind.speed);

                // 9 часов

                label_day3.Text = string.Format("{0}", getDate(forecast.list[2].dt).DayOfWeek);

                label_date3.Text = string.Format("{0}", getDate(forecast.list[2].dt));

                label_con3.Text = string.Format("{0}", forecast.list[2].weather[0].main); // состояние погоды

                label_des3.Text = string.Format("{0}", forecast.list[2].weather[0].description); // описание

                label_temp3.Text = string.Format("{0}\u00B0" + "C", forecast.list[2].main.temp);

                label_speed3.Text = string.Format("{0} m/s", forecast.list[2].wind.speed);

                //12 часов

                label_day4.Text = string.Format("{0}", getDate(forecast.list[3].dt).DayOfWeek);

                label_date4.Text = string.Format("{0}", getDate(forecast.list[3].dt));

                label_con4.Text = string.Format("{0}", forecast.list[3].weather[0].main); // состояние погоды

                label_des4.Text = string.Format("{0}", forecast.list[3].weather[0].description); // описание

                label_temp4.Text = string.Format("{0}\u00B0" + "C", forecast.list[3].main.temp);

                label_speed4.Text = string.Format("{0} m/s", forecast.list[3].wind.speed);

                //15 часов

                label_day5.Text = string.Format("{0}", getDate(forecast.list[4].dt).DayOfWeek);

                label_date5.Text = string.Format("{0}", getDate(forecast.list[4].dt));

                label_con5.Text = string.Format("{0}", forecast.list[4].weather[0].main); // состояние погоды

                label_des5.Text = string.Format("{0}", forecast.list[4].weather[0].description); // описание

                label_temp5.Text = string.Format("{0}\u00B0" + "C", forecast.list[4].main.temp);

                label_speed5.Text = string.Format("{0} m/s", forecast.list[4].wind.speed);

                // изображения
                
                picture_2.Image = setIcon(forecast.list[0].weather[0].icon); 

                picture_3.Image = setIcon(forecast.list[1].weather[0].icon);

                picture_4.Image = setIcon(forecast.list[2].weather[0].icon);

                picture_5.Image = setIcon(forecast.list[3].weather[0].icon);

                picture_6.Image = setIcon(forecast.list[4].weather[0].icon);
               

            }

        }

        DateTime getDate(double millisecound)
        {

            DateTime day = new System.DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).ToLocalTime();
            day = day.AddSeconds(millisecound).ToLocalTime();

            return day;
        }

        Image setIcon(string iconID)
        {

            string url = string.Format("http://openweathermap.org/img/wn/{0}.png" , iconID); //изображение
            
            var request = WebRequest.Create(url);

            using (var responce = request.GetResponse())
            
            using (var weatherIcon = responce.GetResponseStream())
            {

                Image weatherImg = Bitmap.FromStream(weatherIcon);

                return weatherImg;

            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

     
        private void button_search_Click(object sender, EventArgs e)
        {
            if(text_city.Text != "")
            {
                getWeather(text_city.Text);

                getForecast(text_city.Text);
            }
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            if (text_city.Text != "")
            {
                using(StreamWriter str = new StreamWriter("Save_weather.txt"))
                {
                    str.WriteLine("Город: " + label_city.Text);

                    str.WriteLine("Регион: " + label_Region.Text);

                    str.WriteLine("Погода на текущий момент времени: " + label_CurTemp.Text);

                    str.WriteLine("Прогноз погоды на " + label_date.Text + ":");

                    str.WriteLine("Температура: " + label_temp.Text);

                    str.WriteLine("Состояние погоды: " + label_con.Text);

                    str.WriteLine("Описание: " + label_des.Text);

                    str.WriteLine("Прогноз погоды на " + label_date2.Text + ":");

                    str.WriteLine("Температура: " + label_temp2.Text);

                    str.WriteLine("Состояние погоды: " + label_con2.Text);

                    str.WriteLine("Описание: " + label_des2.Text);

                    str.WriteLine("Прогноз погоды на " + label_date3.Text + ":");

                    str.WriteLine("Температура: " + label_temp3.Text);

                    str.WriteLine("Состояние погоды: " + label_con3.Text);

                    str.WriteLine("Описание: " + label_des3.Text);

                    str.WriteLine("Прогноз погоды на " + label_date4.Text + ":");

                    str.WriteLine("Температура: " + label_temp4.Text);

                    str.WriteLine("Состояние погоды: " + label_con4.Text);

                    str.WriteLine("Описание: " + label_des4.Text);

                    str.WriteLine("Прогноз погоды на " + label_date5.Text + ":");

                    str.WriteLine("Температура: " + label_temp5.Text);

                    str.WriteLine("Состояние погоды: " + label_con5.Text);

                    str.WriteLine("Описание: " + label_des5.Text);

                }
            }
        }
    }
}
