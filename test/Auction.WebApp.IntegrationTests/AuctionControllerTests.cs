using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Auction.WebApp.Dtos;
using Auction.WebApp.ReadModels;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Auction.WebApp.IntegrationTests
{
    public class AuctionControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public AuctionControllerTests(
            WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }


        [Fact]
        public async Task CreateAuctionReturns200IfSuccessful()
        {
            var auction = new CreateAuctionDto
            {
                StartingPrice = 15.0m,
                Description = "Guitar"
            };

            var postResponse = await ApiCalls.PostCreateAuction(_client, auction);

            Assert.Equal(HttpStatusCode.OK, postResponse.StatusCode);
        }

        [Fact]
        public async Task GetLiveAuctionsShouldReturnAllLiveAuctions()
        {
            await ApiCalls.PostCreateAuction(_client, new CreateAuctionDto
            {
                StartingPrice = 15.0m,
                Description = "Guitar"
            });
            await ApiCalls.PostCreateAuction(_client, new CreateAuctionDto
            {
                StartingPrice = 20.0m,
                Description = "Piano"
            });

            var getResponse = await ApiCalls.GetLiveAuctions(_client);


            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

            var content = await getResponse.Content.ReadAsStringAsync();
            var auctions = JsonConvert.DeserializeObject<List<AuctionReadModel>>(content);
            Assert.Equal(2, auctions.Count);
        }
    }
}
