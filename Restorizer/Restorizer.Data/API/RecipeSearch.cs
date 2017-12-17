using Newtonsoft.Json;
using Restorizer.Data.Model;
using Restorizer.Data.API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Restorizer.Data.API
{
    public class RecipeSearch
    {

        private string uri = "https://webknox-recipes.p.mashape.com/recipes/search";
        private string key = "aeuSw4hHJrmshd3sHnSi71hhiB7Wp1ZRvuQjsn1gzYMJvDrkUx";

        public async Task<List<DTO.RecipeSearchResult>> GetResult(string query)
        {

            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Add("X-Mashape-Key", key);
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                string url = uri + $"?query={query}&number=25";

                var response = await client.GetStringAsync(url);

                var data = JsonConvert.DeserializeObject<Result>(response);

                return data.results.Select(item => new RecipeSearchResult()
                {

                    Title = item.title

                }).ToList();

            }

        }
    }
}
