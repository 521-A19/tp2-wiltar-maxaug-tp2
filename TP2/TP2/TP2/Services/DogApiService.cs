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


        /*
        public async Task<Collection<Pizza>> AsyncGetPizzas()
        {
            var _httpClient = new HttpClient();

            response = await _httpClient.GetAsync("https://gist.githubusercontent.com/ymazieres/2fb497a791bfa3173c02830dca9ec238/raw/9791c5e685874f0d691ba6fc7acbb45f31e905e7/pizzas.xml");

            var xmlSerializer = new XmlSerializer(typeof(PizzaXmlBody));
            var streamContents = await response.Content.ReadAsStreamAsync();
            var pizzaXmlBody = xmlSerializer.Deserialize(streamContents) as PizzaXmlBody;
            var pizzaBody = pizzaXmlBody as PizzaXmlBody; // as = CAST
            return pizzaBody.Pizzas;


            try
            {
                response = await _httpClient.GetAsync("https://gist.githubusercontent.com/ymazieres/2fb497a791bfa3173c02830dca9ec238/raw/9791c5e685874f0d691ba6fc7acbb45f31e905e7/pizzas.xml");
            }
            catch (Exception ex)
            {
                throw new Exception("La connextion n'a pas été établie", ex);
            }

            try
            {
                var xmlSerializer = new XmlSerializer(typeof(PizzaXmlBody));
                var streamContents = await response.Content.ReadAsStreamAsync();
                var pizzaXmlBody = xmlSerializer.Deserialize(streamContents) as PizzaXmlBody;
                var pizzaBody = pizzaXmlBody as PizzaXmlBody; // as = CAST
                return pizzaBody.Pizzas;
            }
            catch (Exception ex)
            {
                throw new Exception("Les données ne sont pas exploitables", ex);
            }

            // code async pour aller chercher les données sur le web
            //throw new NotImplementedException();
        }*/
    }
}