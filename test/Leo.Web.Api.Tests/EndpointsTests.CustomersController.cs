// MIT License

using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using Leo.Data.Domain.Dtos;
using Microsoft.AspNetCore.Http;

namespace Leo.Web.Api.Tests
{
    public partial class EndpointsTests : IClassFixture<LeoWebApplicationFactory<Program>>
    {
        const string _customersPathSegment = "/customers";

        [Fact]
        public async Task Get_Customers_ReturnOK()
        {
            HttpResponseMessage resp = await _client.GetAsync(_customersPathSegment);

            IEnumerable<CustomerDto>? customers = await resp.Content.ReadFromJsonAsync<IEnumerable<CustomerDto>>();

            Assert.NotNull(customers);
        }

        [Fact]
        public async Task Post_Customers_ReturnCreated()
        {
            var customer = new CustomerDto
            {
                Name = "Test",
                Gender = Leo.Data.Domain.Entities.Gender.Male,
                CardNo = "test",
                Phone = "test",
            };
            HttpResponseMessage res = await _client.PostAsJsonAsync(_customersPathSegment, customer);

            Assert.Equal(HttpStatusCode.Created, res.StatusCode);

            JsonObject? result = await res.Content.ReadFromJsonAsync<JsonObject>();
            string id = result!["id"]!.ToString();

            Assert.NotEmpty(id);
        }

        [Fact]
        public async Task Get_CustomerById_ReturnOK()
        {
            var customer = new CustomerDto
            {
                Name = "Test",
                Gender = Leo.Data.Domain.Entities.Gender.Male,
                CardNo = "test",
                Phone = "test",
            };
            HttpResponseMessage res = await _client.PostAsJsonAsync(_customersPathSegment, customer);
            JsonObject? result = await res.Content.ReadFromJsonAsync<JsonObject>();
            string id = result!["id"]!.ToString();

            res = await _client.GetAsync($"{_customersPathSegment}/{id}");

            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        }
    }
}
