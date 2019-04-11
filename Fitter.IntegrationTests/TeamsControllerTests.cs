using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Fitter.BL.Model;
using Fitter.DAL.Entity;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Newtonsoft.Json;
using Xunit;

namespace Fitter.IntegrationTests
{
    public class TeamsControllerTests
    {
        /*[Fact]
        public async Task CreateTeam()
        {
            using (var client = new TestClientProvider().Client)
            {
                var user1 = new UserDetailModel()
                {
                    Id = Guid.NewGuid(),
                    LastName = "Zigo",
                    FirstName = "Tomas",
                    Password = "123456789",
                    Email = "tomas@zigo.sk"
                };

                var response = await client.PostAsync("/api/teams/create", new StringContent(
                    JsonConvert.SerializeObject(new TeamDetailModel()
                    {
                        Name = "IW5",
                        Description = "Team for IW5",
                        Admin = user1,
                        Id = new Guid("41013b86-a8fa-4daa-85c8-f4ef39ff5f90")
                    }), Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }*/

        [Fact]
        public async Task CreateTeam2()
        {
            using (var client = new TestClientProvider().Client)
            {
                var user2 = new UserDetailModel()
                {
                    Id = Guid.NewGuid(),
                    LastName = "Abraham",
                    FirstName = "Michal",
                    Password = "password",
                    Email = "michal@abraham.sk"
                };

                var response = await client.PostAsync("/api/teams/create", new StringContent(
                    JsonConvert.SerializeObject(new TeamDetailModel()
                    {
                        Name = "ICS",
                        Description = "Team for ICS",
                        Admin = user2,
                        Id = new Guid("4e6534b3-a53f-4f1d-9d04-09445e0e7221")
                    }), Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task TeamById()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/api/teams/getById?id=41013b86-a8fa-4daa-85c8-f4ef39ff5f90");

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task GetTeamsForUser()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/api/teams/getTeamsForUser?id=41013b86-a8fa-4daa-85c8-f4ef39ff5f90");

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task CheckIfTeamExists()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/api/teams/exists?name=IW5Team");

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task DeleteTeam()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.DeleteAsync("/api/teams/delete?id=4e6534b3-a53f-4f1d-9d04-09445e0e7221");

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }
    }
}