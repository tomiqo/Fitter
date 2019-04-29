using System;
using System.Collections.Generic;
using System.Linq;
using Fitter.BL.Model;
using Fitter.BL.Repositories;
using Xunit;

namespace Fitter.BL.Tests
{
    public class PostsRepositoryTests
    {
        [Fact]
        public void CreatePost()
        {
            var sut = CreateSUT();
            var team = new TeamDetailModel()
            {
                Id = Guid.NewGuid(),
                Admin = new UserDetailModel()
                {
                    FirstName = "John",
                    LastName = "Smith",
                    Email = "johnsmith@gmail.com",
                    Password = "asdf12345",
                },
                Description = "Team of John",
                Name = "Johny"
            };

            var model = new PostModel()
            {
                Id = Guid.NewGuid(),
                Author = new UserDetailModel()
                {
                    Id = Guid.NewGuid(),
                    LastName = "Doe",
                    FirstName = "John",
                    Password = "123",
                    Email = "john@doe.com"
                },
                Created = DateTime.Now.ToString("MM/dd/yyyy HH:mm"),
                Team = team,
                Text = "Randon text in post",
                Title = "Random"
            };
            sut.Create(model);
            Assert.NotNull(sut.GetPostsForTeam(model.Id));
        }

        [Fact]
        public void DeletePost()
        {
            var sut = CreateSUT();
            var team = new TeamDetailModel()
            {
                Id = Guid.NewGuid(),
                Admin = new UserDetailModel()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Lisa",
                    LastName = "Black",
                    Email = "LisaBlack@gmail.com",
                    Password = "lisalisalisa"
                },
                Description = "Post in Lisa´s team",
                Name = "Lisa Post"
            };
            var model = new PostModel()
            {
                Id = Guid.NewGuid(),
                Author = new UserDetailModel()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Lisa",
                    LastName = "Black",
                    Email = "LisaBlack@gmail.com",
                    Password = "lisalisalisa"
                },
                Created = DateTime.Now.ToString("MM/dd/yyyy HH:mm"),
                Team = team
            };

            sut.Create(model);
            sut.Delete(model.Id);
            var posts = sut.GetPostsForTeam(team.Id).ToList();
            Assert.Empty(posts);
        }

        [Fact]
        public void GetPostFromTeam()
        {
            var sut = CreateSUT();
            var team = new TeamDetailModel()
            {
                Id = Guid.NewGuid(),
                Admin = new UserDetailModel()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Jozef",
                    LastName = "Velky",
                    Email = "jozefko@gmail.com",
                    Password = "jozef67@gmail.com"
                }
            };
            var model = new PostModel
            {
                Id = Guid.NewGuid(),
                Author = new UserDetailModel()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Martin",
                    LastName = "Brnensky",
                    Password = "14758963",
                    Email = "martin12313@gmail.com"
                },
                Created = DateTime.Now.ToString("MM/dd/yyyy HH:mm"),
                Team = team,
                Title = "Prvy post",
                Text = "Toto je prvy post v skupine",
            };

            sut.Create(model);
            var retrievedPost = sut.GetPostsForTeam(team.Id);
            Assert.NotNull(retrievedPost);
        }


        private PostsRepository CreateSUT()
        {
            return new PostsRepository(new InMemoryDbContext(), new Mapper.Mapper());
        }
    }
}