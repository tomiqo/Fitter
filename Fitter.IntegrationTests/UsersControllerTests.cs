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
                var response = await client.GetAsync("/api/users/getById?id=7d9b38d0-4fb9-4b49-b052-08d6ce33d4e9");

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode); 
            }
        }

        [Fact]
        public async Task GetUserByEmail()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/api/users/getByEmail?email=a");

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task GetUsersInTeamAsync()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/api/users/getUsersInTeam?id=7d9b38d0-4fb9-4b49-b052-08d6ce33d4e9");

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task GetUsersNotInTeamAsync()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/api/users/getUsersNotInTeam?id=7d9b38d0-4fb9-4b49-b052-08d6ce33d4e9");

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }
    }
}