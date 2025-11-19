using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace HostWPF.Services
{
    internal class CatFactsService : ICatFactsService
    {
        private readonly IWebClient _webClient;

        public CatFactsService(IWebClient webClient)
        {
            _webClient = webClient;
        }

        public async Task<IEnumerable<string>> GetCatFactsAsync(int count = 1)
        {

            var facts = await _webClient.GetStringAsync($"https://catfact.ninja/facts?limit={count}");

            var data = JsonNode.Parse(facts);
     
            return data["data"].AsArray().Select(fact => fact["fact"].ToString()).ToList();

        }
    }
    }

