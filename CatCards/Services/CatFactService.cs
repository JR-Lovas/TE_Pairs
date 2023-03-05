using CatCards.Models;
using RestSharp;
using System.Runtime.CompilerServices;

namespace CatCards.Services
{
    public class CatFactService : ICatFactService
    {

        private readonly string API_URL = "https://cat-data.netlify.app/api/";
        private readonly string API_KEY = "44346";
        
        private readonly RestClient client;

        public CatFactService()
        {
            client = new RestClient(API_KEY);
        }

        public CatFact GetFact()
        {
            RestRequest request = new RestRequest("facts?apikey=" + API_KEY);

            IRestResponse<CatFact> response = client.Get<CatFact>(request);

            if (response.IsSuccessful)
            {
                return response.Data;
            }

            throw new System.NotImplementedException();
        }
    }
}
