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
        public async Task GetUserById()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/api/users/getById?id=d628cab8-bd94-4346-32c3-08d6b8f33c69");

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode); 
            }
        }

        [Fact]
        public async Task GetUserByEmail()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/api/users/getByEmail?email=adrian@boros.sk");

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task GetUsersInTeamAsync()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/api/users/getUsersInTeam?id=d628cab8-bd94-4346-32c3-08d6b8f33c69");

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task GetUsersNotInTeamAsync()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/api/users/getUsersNotInTeam?id=d628cab8-bd94-4346-32c3-08d6b8f33c69");

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        /*[Fact]
        public async Task CreateUser()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("/api/users/create", new StringContent(
                   JsonConvert.SerializeObject(new UserDetailModel(){ Email = "adrianboros2@gmail.com", LastName = "Abraham",
                    FirstName = "Adam", Password = "123", Id = Guid.NewGuid()}), Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }*/
    }
}