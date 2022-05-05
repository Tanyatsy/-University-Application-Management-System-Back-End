using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Unipply.Services
{
    public class RecommendationsIteractor : IRecommendationsIteractor
    {
        public RecommendationsIteractor()
        {
        }

        public async Task<HttpResponseMessage> GetRecomendationsFacultiesAsync(List<string> hobbies)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:6000/");

            var content = new StringContent(JsonConvert.SerializeObject(hobbies).ToString(), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync($"Recommender/faculties", content);

            return response;
        }

        public async Task<HttpResponseMessage> GetRecomendationsHobbiesAsync()
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:6000/");

            HttpResponseMessage response = await client.GetAsync($"Recommender/hobbies");

            return response;
        }
    }

    public interface IRecommendationsIteractor
    {
        Task<HttpResponseMessage> GetRecomendationsFacultiesAsync(List<string> hobbies);
        Task<HttpResponseMessage> GetRecomendationsHobbiesAsync();
    }
}
