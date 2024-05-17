using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AppLibrosMobile.ApiConsumer
{
    public class ApiConsumer<T>
    {
        public static T Create(string url, T data)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(data);
                var request = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };

                var response = client.SendAsync(request).Result;
                json = response.Content.ReadAsStringAsync().Result;

                var result = JsonConvert.DeserializeObject<T>(json);


                return result;
            }
        }

        public static T[] Read(string url)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(url).Result;
                var json = response.Content.ReadAsStringAsync().Result;

                var result = JsonConvert.DeserializeObject<T[]>(json);

                return result;
            }
        }

        public static T Read(string url, int id)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(url + "/" + id).Result;
                var json = response.Content.ReadAsStringAsync().Result;

                var result = JsonConvert.DeserializeObject<T>(json);

                return result;
            }
        }

        public static T Update(string url, int id, T data)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url + "/" + id);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(data);
                var request = new HttpRequestMessage(HttpMethod.Put, client.BaseAddress)
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };

                var response = client.SendAsync(request).Result;
                json = response.Content.ReadAsStringAsync().Result;

                var result = JsonConvert.DeserializeObject<T>(json);

                return result;
            }
        }

        public static void Delete(string url, int id)
        {
            using (var client = new HttpClient())
            {
                var response = client.DeleteAsync(url + "/" + id).Result;
            }
        }
    }
}
