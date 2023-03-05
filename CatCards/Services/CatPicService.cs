using CatCards.Models;
using RestSharp;

namespace CatCards.Services
{
    public class CatPicService : ICatPicService
    {

        private readonly string API_URL = "https://cat-data.netlify.app/api/";
        private readonly string API_KEY = "44346";

        private readonly RestClient client;

        public CatPicService()
        {
            client = new RestClient(API_URL);
        }

        public CatPic GetPic()
        {
            RestRequest request = new RestRequest("pictures?apiKey=" + API_KEY);

            IRestResponse<CatPic> response = client.Get<CatPic>(request);

            if (response.IsSuccessful)
            {
                return response.Data;
            }

            throw new System.Net.Http.HttpRequestException();
        }
    }
}
