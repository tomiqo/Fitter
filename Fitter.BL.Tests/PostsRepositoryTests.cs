using System;
using System.Collections.Generic;
using System.Linq;
using Fitter.BL.Model;
using Fitter.BL.Repositories;
using Fitter.DAL.Enums;
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
                Created = DateTime.Today,
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
                Created = DateTime.Today,
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
                Created = DateTime.Today,
                Team = team,
                Title = "Prvy post",
                Text = "Toto je prvy post v skupine",
            };

            sut.Create(model);
            var retrievedPost = sut.GetPostsForTeam(team.Id);
            Assert.NotNull(retrievedPost);
        }

        [Fact]
        public void CheckAttachmentInPost()
        {
            var sut = CreateSUT();
            var team = new TeamDetailModel()
            {
                Id = Guid.NewGuid(),
                Admin = new UserDetailModel()
                {
                    Id = Guid.NewGuid(),
                    LastName = "Vaclav",
                    FirstName = "Siroky",
                    Password = "dobreheslo",
                    Email = "sirokyvaclav@gmail.com"
                },
                Name = "Ministri",
                Description = "Team pre ministrov"
            };
            var model = new PostModel
            {
                Id = Guid.NewGuid(),
                Author = new UserDetailModel()
                {
                    Id = Guid.NewGuid(),
                    LastName = "Michal",
                    FirstName = "Kruty",
                    Password = "najlepsieheslo",
                    Email = "kruty@gmail.com"
                },
                Created = DateTime.Today,
                Team = team,
                Title = "Najepsi post",
                Text = "Post s prilohou",
            };
            sut.Create(model);
            var attachment = new AttachmentModel()
            {
                Id = Guid.NewGuid(),
                File = new byte[2],
                FileType = FileType.File,
                Name = "Priloha",
                Post = model
            };
            sut.AddAttachments(new List<AttachmentModel>(){attachment}, model.Id);
            var retrievedAttachment = sut.GetAttachmentsForPost(model.Id);
            Assert.NotNull(retrievedAttachment);
        }

        [Fact]
        public void TagUserInPost()
        {
            var sut = CreateSUT();
            var user = new UserDetailModel()
            {
                Id = Guid.NewGuid(),
                FirstName = "Bradley",
                LastName = "Smith",
                Email = "bradley@outlook.com",
                Password = "qwertz"
            };
            var team = new TeamDetailModel()
            {
                Id = Guid.NewGuid(),
                Admin = new UserDetailModel()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Anna",
                    LastName = "White",
                    Email = "AnnaWhite@outlook.com",
                    Password = "anna987"
                },
                Description = "Team number1",
                Name = "Team1"
            };
            var model = new PostModel()
            {
                Id = Guid.NewGuid(),
                Author = new UserDetailModel()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Anna",
                    LastName = "White",
                    Email = "AnnaWhite@outlook.com",
                    Password = "anna987"
                },
                Created = DateTime.Today,
                Team = team,
                Text = "Post in team number1",
                Title = "Post1"
            };
            sut.Create(model);
            sut.TagUsers(new List<UserDetailModel>(){user}, model.Id);
            Assert.NotNull(sut.GetTagsForPost(model.Id));
        }

        private PostsRepository CreateSUT()
        {
            return new PostsRepository(new InMemoryDbContext(), new Mapper.Mapper());
        }
    }
}