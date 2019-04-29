using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Fitter.BL.Model;
using Fitter.DAL.Enums;
using Newtonsoft.Json;
using Xunit;

namespace Fitter.IntegrationTests
{
    public class PostsControllerTests
    {
        /*[Fact]
        public async Task CreatePost()
        {
            using (var client = new TestClientProvider().Client)
            {
                var user = new UserDetailModel()
                {
                    Id = Guid.NewGuid(),
                    Email = "eva@nova.cz",
                    FirstName = "Eva",
                    LastName = "Nova",
                    Password = "heslo"
                };
                var team = new TeamDetailModel()
                {
                    Id = Guid.NewGuid(),
                    Admin = user,
                    Description = "Evkin Team",
                    Name = "Name"
                };
                var response = await client.PostAsync("/api/posts/create", new StringContent(
                    JsonConvert.SerializeObject(new PostModel()
                    {
                        Author = user,
                        Created = DateTime.Today,
                        Team = team,
                        Title = "Post in team",
                        Text = "Nejaky nahodne vygenerovany text",
                        Id = new Guid("ee65198c-a044-4dca-8b14-fc36e5047af8"),
                    }), Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }*/

        [Fact]
        public async Task CreatePost2()
        {
            using (var client = new TestClientProvider().Client)
            {
                var user = new UserDetailModel()
                {
                    Id = Guid.NewGuid(),
                    Email = "optimus@pokec.cz",
                    FirstName = "Eduard",
                    LastName = "Krivanek",
                    Password = "password123"
                };
                var team = new TeamDetailModel()
                {
                    Id = Guid.NewGuid(),
                    Admin = user,
                    Description = "Edov Team",
                    Name = "Meno"
                };
                var response = await client.PostAsync("/api/posts/create", new StringContent(
                    JsonConvert.SerializeObject(new PostModel()
                    {
                        Author = user,
                        Created = DateTime.Now.ToString("MM/dd/yyyy HH:mm"),
                        Team = team,
                        Title = "Post v time",
                        Text = "Nejaky text",
                        Id = new Guid("1162afa0-65b0-49f3-b792-80ad4e958c53"),
                    }), Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task getPostsForTeam()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/api/posts/getPostsForTeam?id=ee65198c-a044-4dca-8b14-fc36e5047af8");

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task getAttachmentsForPost()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/api/posts/getAttachmentsForPost?id=ee65198c-a044-4dca-8b14-fc36e5047af8");

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task getTagsForPost()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/api/posts/getTagsForPost?id=ee65198c-a044-4dca-8b14-fc36e5047af8");

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task PostDelete()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.DeleteAsync("/api/posts/delete?id=1162afa0-65b0-49f3-b792-80ad4e958c53");

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }
    }
}