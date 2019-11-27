using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TP2.Models;
using TP2.Models.Entities;

namespace TP2.Services
{
    public class DogApiService : IDogApiService
    {
        public RootObject GetDogBreeds()  //include subbreeds
        {
            WebClient web = new WebClient();
            var json = web.DownloadString("https://dog.ceo/api/breeds/list");
            var breeds = JsonConvert.DeserializeObject<RootObject>(json);
            return breeds;
        }

        public RandomImage GetRandomImageURL(string Url)
        {
            WebClient web = new WebClient();
            var json = web.DownloadString(Url);
            var image = JsonConvert.DeserializeObject<RandomImage>(json);
            return image;
        }
    }
}