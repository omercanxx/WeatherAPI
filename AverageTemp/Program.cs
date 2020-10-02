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

namespace TaylorSwift
{
    public class Model
    {
        public Coord Coord { get; set; }
        public List<Weather> Weather { get; set; }
        public string Base { get; set; }
        public Main Main { get; set; }
        public double Visibility { get; set; }
        public Wind Wind { get; set; }
        public Rain Rain { get; set; }
        public Cloud Cloud { get; set; }
        public string Dt { get; set; }
        public Sys Sys { get; set; }
        public int Timezone { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Cod { get; set; }
    }

    public class Sys
    {
        public string Country { get; set; }
        public string Sunrise { get; set; }
        public string Sunset { get; set; }
    }

    public class Cloud
    {
        public string All { get; set; }
    }

    public class Coord
    {
        public double Lon { get; set; }
        public double Lat { get; set; }
    }
    public class Weather
    {
        public int Id { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }
    public class Main
    {
        public double Temp { get; set; }
        public double Feels_like { get; set; }
        public double Temp_min { get; set; }
        public double Temp_max { get; set; }
        public double Pressure { get; set; }
        public double Humidity { get; set; }
    }

    public class Wind
    {
        public double Speed { get; set; }
        public double Deg { get; set; }
    }
    public class Rain
    {
        public string Perhour { get; set; }
    }
    public class Program
    {
        const string APPID = "e9b1b3f8add39dd1f36224533e46c124";
        static string cityName = "";
        static string countryName = "";
        public static void Main()
        {
            getWeather();
            Console.ReadLine();
        }
        static void getWeather()
        {
            using (WebClient web = new WebClient())
            {
                string apiUrl = string.Format("https://api.openweathermap.org/data/2.5/weather?q=Aluthgama&appid={0}",APPID);

                Uri url = new Uri(apiUrl);
                WebClient client = new WebClient();
                client.Encoding = System.Text.Encoding.UTF8;


                string json = web.DownloadString(url);

                var result = JsonConvert.DeserializeObject<Model>(json);

                Model output = result;
                cityName = string.Format("{0}", output.Name);
                countryName = string.Format("{0}", output.Sys.Country);

                Console.WriteLine("country name -> {0} \ncity name -> {1}", cityName, countryName);

            }
        }
    }
}
