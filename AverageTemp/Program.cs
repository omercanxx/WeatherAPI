using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace WeatherAPI
{
    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class Current
    {
        public int dt { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
        public double temp { get; set; }
        public double feels_like { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public double dew_point { get; set; }
        public double uvi { get; set; }
        public int clouds { get; set; }
        public int visibility { get; set; }
        public double wind_speed { get; set; }
        public int wind_deg { get; set; }
        public List<Weather> weather { get; set; }
    }

    public class Minutely
    {
        public int dt { get; set; }
        public int precipitation { get; set; }
    }

    public class Weather2
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class Hourly
    {
        public int dt { get; set; }
        public double temp { get; set; }
        public double feels_like { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public double dew_point { get; set; }
        public int clouds { get; set; }
        public int visibility { get; set; }
        public double wind_speed { get; set; }
        public int wind_deg { get; set; }
        public List<Weather2> weather { get; set; }
        public int pop { get; set; }
    }

    public class Root
    {
        public double lat { get; set; }
        public double lon { get; set; }
        public string timezone { get; set; }
        public int timezone_offset { get; set; }
        public Current current { get; set; }
        public List<Minutely> minutely { get; set; }
        public List<Hourly> hourly { get; set; }
    }
    
    public class Program
    {
        const string APPID = "e9b1b3f8add39dd1f36224533e46c124";
        const string latitude = "41.015137";
        const string longtitude = "28.979530";
        const string exclude = "daily";
        const string units = "metric";
        /*
         * Temperature is available in Fahrenheit, Celsius and Kelvin units.
         * Wind speed is available in miles/hour and meter/sec.
         * For temperature in Fahrenheit and wind speed in miles/hour, use units=imperial
         * For temperature in Celsius and wind speed in meter/sec, use units=metric
         * Temperature in Kelvin and wind speed in meter/sec is used by default, so there is no need to use the units parameter in the API call if you want this
         */
        static void Main()
        {
            GetWeather();
            Console.ReadLine();
        }
        static void GetWeather()
        {
            double avg = 0, count = 0, sum = 0;
            using (WebClient web = new WebClient())
            {
                string apiUrl = string.Format("https://api.openweathermap.org/data/2.5/onecall?lat={0}&lon={1}&exclude={2}&appid={3}&units={4}", latitude, longtitude, exclude, APPID, units);

                Uri url = new Uri(apiUrl);
                WebClient client = new WebClient();
                client.Encoding = System.Text.Encoding.UTF8;


                string json = web.DownloadString(url);

                Root result = JsonConvert.DeserializeObject<Root>(json);
                Console.WriteLine("**********{0}**********\nFeels like=>{1}",result.timezone,result.current.feels_like);
                foreach(var item in result.hourly)
                {
                    sum += item.temp;
                    count++;
                    avg = sum/count;
                    Console.WriteLine("**********\nHour {0}\nTemp=>{1}F\tsum=>{2}F\tavg=>{3}F", count, item.temp, sum, avg);
                }
            }
        }
        static double FahrenheitToCelcius(double fahrenheit)
        {
            return (fahrenheit-32)*0.5556;
        }
    }
}
