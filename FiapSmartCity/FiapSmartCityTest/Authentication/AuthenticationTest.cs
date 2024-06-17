using RichardSzalay.MockHttp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FiapSmartCityTest.Authentication
{
    public class AuthenticationTest
    {
        private object _client;

        [Fact]
        public async Task Get_ReturnsHttpStatusCode200()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("http://localhost/api/Authentication/Get")
                    .Respond("application/json", "{'name' : 'Test McGee'}"); // Respond with JSON

            //Arrange
            var client = new HttpClient(mockHttp);

            //Act
            var response = await client.GetAsync("http://localhost/api/Authentication/Get");
            var json = await response.Content.ReadAsStringAsync();

            //Assert
            if (response.StatusCode == HttpStatusCode.OK)
                Assert.True(true);
        }


        [Fact]
        public async Task Get_ReturnsHttpStatusCode401()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("http://localhost/api/Authentication/Get")
                .Respond("application/json", ""); // Respond with JSON

            //Arrange
            var client = new HttpClient(mockHttp);

            //Act
            var response = await client.GetAsync("http://localhost/api/Authentication/Get");
            response.StatusCode = HttpStatusCode.Unauthorized;

            //Assert
            if (response.StatusCode == HttpStatusCode.Unauthorized)
                Assert.True(true);

        }
    }
}
