using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Fitter.BL.Model;
using Newtonsoft.Json;
using Xunit;

namespace Fitter.IntegrationTests
{
    public class CommentsControllerTests
    {
        /*[Fact]
        public async Task CreateComment()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("/api/comments/create", new StringContent(
                    JsonConvert.SerializeObject(new CommentModel()
                    {
                        Id = new Guid("7f3eed3c-9f55-4522-add3-af797a52ac5d"),
                        Author = new UserDetailModel()
                        {
                            Id = Guid.NewGuid(),
                            FirstName = "Daniel",
                            LastName = "Velky",
                            Password = "heslo",
                            Email = "daniel@velky.com"
                        },
                        Created = DateTime.Today,
                        Post = new PostModel()
                        {
                            Id = Guid.NewGuid(),
                            Author = new UserDetailModel()
                            {
                                Id = Guid.NewGuid(),
                                FirstName = "Robert",
                                LastName = "Maly",
                                Password = "password147",
                                Email = "robert@maly.com"
                            },
                            Created = DateTime.Today,
                            Team = new TeamDetailModel()
                            {
                                Admin = new UserDetailModel()
                                {
                                    Id = Guid.NewGuid(),
                                    FirstName = "Eva",
                                    LastName = "Vianocna",
                                    Password = "hesloheslo",
                                    Email = "eva@vianoce.com"
                                },
                                Id = Guid.NewGuid(),
                                Description = "text",
                                Name = "team"
                            },
                            Title = "Title",
                            Text = "Text"
                        },
                        Text = "Komentar na post"
                    }), Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }*/

        [Fact]
        public async Task CreateComment2()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("/api/comments/create", new StringContent(
                    JsonConvert.SerializeObject(new CommentModel()
                    {
                        Id = new Guid("27d4b53b-5fe5-41c2-ab23-b978819f1b2c"),
                        Author = new UserDetailModel()
                        {
                            Id = Guid.NewGuid(),
                            FirstName = "Alex",
                            LastName = "Zamcan",
                            Password = "123456",
                            Email = "alexis@gmail.com"
                        },
                        Created = DateTime.Now.ToString("MM/dd/yyyy HH:mm"),
                        Post = new PostModel()
                        {
                            Id = Guid.NewGuid(),
                            Author = new UserDetailModel()
                            {
                                Id = Guid.NewGuid(),
                                FirstName = "Norbert",
                                LastName = "Mokras",
                                Password = "ediediedi",
                                Email = "norbert@pokec.com"
                            },
                            Created = DateTime.Now.ToString("MM/dd/yyyy HH:mm"),
                            Team = new TeamDetailModel()
                            {
                                Admin = new UserDetailModel()
                                {
                                    Id = Guid.NewGuid(),
                                    FirstName = "Boris",
                                    LastName = "Dnesny",
                                    Password = "159951",
                                    Email = "shadow@azet.sk"
                                },
                                Id = Guid.NewGuid(),
                                Description = "Description of team",
                                Name = "Name of team"
                            },
                            Title = "Title of post",
                            Text = "Text in post"
                        },
                        Text = "Komentar na post v teame"
                    }), Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task getCommentsForPost()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/api/comments/getCommentsForPost?id=f15083ac-082b-4f45-999f-2106663069fd");

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task DeleteComment()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.DeleteAsync("/api/comments/delete?id=27d4b53b-5fe5-41c2-ab23-b978819f1b2c");

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }
    }
}