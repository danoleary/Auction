using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Auction.WebApp.Dtos;
using Auction.WebApp.ReadModels;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Auction.WebApp.IntegrationTests
{
    public class ApiCalls
    {
        public static async Task<HttpResponseMessage> PostCreateAuction(HttpClient client, CreateAuctionDto dto)
        {
            var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");

            return await client.PostAsync("/api/auction", content);
        }

        public static async Task<HttpResponseMessage> GetLiveAuctions(HttpClient client)
        {
            return await client.GetAsync("/api/auction/live");
        }

        public static async Task<HttpResponseMessage> PostBid(HttpClient client, PlaceBidDto dto)
        {
            var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");

            return await client.PostAsync("/api/bid", content);
        }
    }
}
