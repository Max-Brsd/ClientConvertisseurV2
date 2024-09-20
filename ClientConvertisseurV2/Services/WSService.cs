using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ClientConvertisseurV2.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ClientConvertisseurV2.Services
{
    internal class WSService : IService
    {
        private static WSService _instance;
        private static readonly object _lock = new object();
        private HttpClient HttpClient { get; set; }

        private WSService(String uri)
        {
            HttpClient = new HttpClient();
            //http://localhost:7223/api/
            HttpClient.BaseAddress = new Uri(uri);
            HttpClient.DefaultRequestHeaders.Accept.Clear();
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static WSService GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new WSService("https://localhost:7192/api/");
                    }
                }
            }
            return _instance;
        }

        public async Task<List<Devise>> GetDevisesAsync(string nomControleur)
        {
            try
            {
                return await HttpClient.GetFromJsonAsync<List<Devise>>(nomControleur);
            }
            catch (Exception)
            {
                return null;

            }
        }
    }
}
