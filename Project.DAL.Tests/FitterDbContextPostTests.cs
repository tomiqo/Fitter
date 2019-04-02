using System;
using System.Collections.Generic;
using Xunit;

namespace Fitter.DAL.Tests
{
    public class FitterDbContextPostTests
    {
        private IFitterDbContext FitterDbContext;

        public FitterDbContextPostTests()
        {
            FitterDbContext = new InMemoryFitterDbContext();
        }

        [Fact]
        public void CreatePost()
        {
            var post = new Post()
            {
                Author = 1,
                Text = "This is first post",
                Created = new DateTime(2018, 10, 11),
                Title = "First post",
                Team = new Team()
                {
                    Admin = 1,
                    Created = new DateTime(2017, 10, 6),
                    MemberCount = 5,
                    Name = "IW5",
                    UsersInTeams = new List<UsersInTeam>()
                    {
                        new UsersInTeam()
                        {
                            User = new User()
                            {
                                Name = "Jan Novak",
                                Email = "jannovak@gmail.com",
                                Nick = "janko123",
                                Password = "jankojesuper",
                            }
                        }
                    }
                },
                Attachments = new List<Attachment>()
                {
                    new Attachment()
                    {
                        Name = "Picture",
                        File = new byte[512],
                        Comment = new Comment()
                        {
                            Author = 5,
                            Created = new DateTime(2017,10,6),
                            Text = "Text for picture"
                        }
                    }
                },
                Comments = new List<Comment>()
                {
                    new Comment()
                    {
                        Author = 5,
                        Created = new DateTime(2018,2,12),
                        Text = "Next commentar"
                    }
                },
                Tags = new List<User>()
                {
                    new User()
                    {
                        Name = "Jan Novak",
                        Email = "jannovak@gmail.com",
                        Nick = "janko123",
                        Password = "jankojesuper",
                        UsersInTeams = new List<UsersInTeam>()
                        {
                            new UsersInTeam()
                            {
                                Team = new Team()
                                {
                                    Admin = 3,
                                    Created = new DateTime(2017, 12, 12),
                                    MemberCount = 4,
                                    Name = "IW5"
                                }
                            }
                        }
                    }
                },
                Parent = new Comment()
                {
                    Author = 2,
                    Created = new DateTime(2015,1,2),
                    Text = "Original comment",
                }
            };

            using (var dbContext = FitterDbContext.CreateDbContext())
            {
                dbContext.Posts.Add(post);
                dbContext.SaveChanges();
            }

            using (var dbContext = FitterDbContext.CreateDbContext())
            {
                var retrievedPost = dbContext.Posts
                    .Include(x => x.Team)
                    .Include(u => u.Team.UsersInTeams)
                    .Include(t => t.Attachments)
                    .Include(c => c.Comments)
                    .Include(t => t.Tags)
                    .First(x => x.Id == post.Id);
                Assert.NotNull(retrievedPost);
                Assert.Equal("IW5", retrievedPost.Team.Name);
                Assert.Equal(1, retrievedPost.Team.UsersInTeams.Count);
                Assert.Equal(1, retrievedPost.Tags.Count);
            }
        }

    }
}