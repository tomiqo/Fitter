using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Fitter.BL.Model;
using Fitter.DAL.Entity;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace Fitter.IntegrationTests
{
    public class UsersControllerTests
    {
        [Fact]
        public async Task GetUserByEmail()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/api/users/getByEmail?email=adrianboros@centrum.sk");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        }

        [Fact]
        public async Task CreateUser()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("/api/users/create", new StringContent(
                   JsonConvert.SerializeObject(new UserDetailModel(){ Email = "adrianboros2@gmail.com", LastName = "Abraham",
                    FirstName = "Adam", Password = "123", Id = Guid.NewGuid()}), Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        }
    }
}