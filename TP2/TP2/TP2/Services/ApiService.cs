using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TP2.Models;

namespace TP2.Services
{
    public class ApiService
    {
        private HttpResponseMessage response;

        public ApiService()
        {

        }

        public List<Breed> GetBreeds()  //include subbreeds
        {
            WebClient web = new WebClient();
            var body = web.DownloadString("https://dogfinder-api.herokuapp.com/breeds");
            return JsonConvert.DeserializeObject<List<Breed>>(body);
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