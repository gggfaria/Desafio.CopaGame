using Desafio.CopaGame.Domain.Consts;
using Desafio.CopaGame.Domain.Entities.Games;
using Desafio.CopaGame.Domain.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Desafio.CopaGame.Data.Jogos
{
    public class JogoRepository : IJogoRepository
    {
        private readonly IHttpClientFactory _clientFactory;
        public JogoRepository(IHttpClientFactory httpClientFactory)
        {
            _clientFactory = httpClientFactory;
        }


        public async Task<IEnumerable<Jogo>> GetAll()
        {
            var client = _clientFactory.CreateClient(HttpClientConst.JOGO);

            var response = await client.GetAsync("api/Competidores?copa=games");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<Jogo>>(json);
            }
            else
            {
                throw new System.Exception("API Indisponível");
            }
        }


    }
}
