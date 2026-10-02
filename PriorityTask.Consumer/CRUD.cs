using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PriorityTask.Consumer
{
    public static class CRUD<T>
    {

        public static List<T> GetAll(string Endpoint)
        {
            using (var usuario = new HttpClient())
            {
                var response = usuario.GetAsync(Endpoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<List<T>>(json);
                }
                else
                {
                    // Captura el texto/excepción que devuelve la Web API
                    var errorContent = response.Content.ReadAsStringAsync().Result;
                    throw new Exception($"Error {response.StatusCode}: {errorContent}");
                }
            }
        }
        public static T GetById(string Endpoint,int id)
        {
            using (var usuario = new HttpClient())
            {
                var response = usuario.GetAsync($"{Endpoint}/{id}").Result;
                if(response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<T>(json);
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode}");
                }
            }
        }
        public static T Create(string Endpoint,T item)
        {
            using (var usuario = new HttpClient())
            {
                var response = usuario.PostAsync(Endpoint,
                    new StringContent(JsonConvert.SerializeObject(item),
                    Encoding.UTF8, "application/json")).Result;
                if(response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<T>(json);
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode}");
                }
            }
        }
        public static bool Update(string Endpoint,int id, T item)
        {
            using (var usuario = new HttpClient())
            {
                var response = usuario.PutAsync(
                    $"{Endpoint}/{id}",
                    new StringContent(JsonConvert.SerializeObject(item),
                    Encoding.UTF8, "application/json")).Result;
                if(response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode}");
                }
            }
        }
        public static bool Delete(string Endpoint,int id)
        {
            using(var usuario = new HttpClient())
            {
                var response = usuario.DeleteAsync($"{Endpoint}/{id}").Result;
                if(response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode}");
                }
            }
        }

    }
}