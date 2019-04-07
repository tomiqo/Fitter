using System;
using System.Diagnostics.SymbolStore;
using System.Runtime.InteropServices.ComTypes;
using Fitter.BL.Factories;
using Fitter.BL.Model;
using Fitter.BL.Repositories;
using Fitter.BL.Repositories.Interfaces;
using Fitter.DAL;
using Fitter.DAL.Entity;
using Fitter.DAL.Tests;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Fitter.BL.Tests
{
    public class TeamsRepositoryTests
    {
        [Fact]
        public void CreateTeam()
        {
            var sut = CreateSUT();
            var model = new TeamDetailModel()
            {
                Admin = new UserDetailModel()
                {
                    LastName = "Eva",
                    FirstName = "Novakova",
                    Password = "asassad",
                    Email = "novakovaEva@gmail.com"
                },
                Name = "Team",
                Description = "Team pre IW5/ICS",
            };
            var createdTeam = sut.Create(model);
            Assert.NotNull(createdTeam);
        }

        [Fact]
        public void GetTeamById()
        {
            var sut = CreateSUT();
            var model = new TeamDetailModel()
            {
                Admin = new UserDetailModel()
                {
                    LastName = "Alfonz",
                    FirstName = "Mokry",
                    Password = "147258369",
                    Email = "dark@gmail.com"
                },
                Name = "The bests",
                Description = "Alfonzov team",
            };
            var createdTeam = sut.Create(model);
            var foundedTeam = sut.GetById(createdTeam.Id);
            Assert.NotNull(foundedTeam);
        }

        [Fact]
        public void DeleteTeam()
        {
            var sut = CreateSUT();
            var admin = new UserDetailModel()
            {
                Id = Guid.NewGuid(),
                LastName = "Adam",
                FirstName = "Vysoky",
                Password = "adamko13213",
                Email = "andershall@gmail.com"
            };

            var model = new TeamDetailModel()
            {
                Id = Guid.NewGuid(),
                Admin = admin,
                Name = "United",
                Description = "Najlepsi futbalovy team",
            };
            var createdTeam = sut.Create(model);
            var teamInDb = sut.GetById(createdTeam.Id);
            sut.Delete(createdTeam.Id);
            teamInDb = sut.GetById(createdTeam.Id);
            Assert.Null(createdTeam.Name);
        }

        [Fact]
        public void AddUserToTeam()
        {
            var sut = CreateSUT();
            var model = new TeamDetailModel()
            {
                Admin = new UserDetailModel()
                {
                    LastName = "Michaela",
                    FirstName = "Sucha",
                    Password = "miskasucha",
                    Email = "michaella159@seznam.cz"
                },
                Name = "Miskin team",
                Description = "Dievcensky team pre michaelu",
            };
            var user = new UserDetailModel()
            {
                LastName = "Eva",
                FirstName = "Novakova",
                Password = "asassad",
                Email = "novakovaEva@gmail.com"
            };
            var createdTeam = sut.Create(model);
            sut.AddUserToTeam(user,createdTeam.Id);
            Assert.NotNull(user);
        }

        [Fact]
        public void RemoveUserFromTeam()
        {
            var sut = CreateSUT();
            var users = CreateUser();
            var model = new TeamDetailModel()
            {
                Admin = new UserDetailModel()
                {
                    LastName = "Vaclav",
                    FirstName = "Siroky",
                    Password = "dobreheslo",
                    Email = "sirokyvaclav@gmail.com"
                },
                Name = "Ministri",
                Description = "Team pre ministrov",
            };
            var user = new UserDetailModel()
            {
                LastName = "Michal",
                FirstName = "Kruty",
                Password = "najlepsieheslo",
                Email = "kruty@gmail.com"
            };

            var createdUser = users.Create(user);
            var createdTeam = sut.Create(model);
            sut.AddUserToTeam(user, createdTeam.Id);
            sut.RemoveUserFromTeam(user, createdTeam.Id);
            var usersInTeam = users.GetUsersInTeam(createdUser.Id);
            Assert.Null(usersInTeam);
        }

        [Fact]
        public void CheckTeamExisting()
        {
            var sut = CreateSUT();
            var model = new TeamDetailModel()
            {
                Admin = new UserDetailModel()
                {
                    LastName = "Vaclav",
                    FirstName = "Siroky",
                    Password = "dobreheslo",
                    Email = "sirokyvaclav@gmail.com"
                },
                Name = "Ministri",
                Description = "Team pre ministrov",
            };

            var createdTeam = sut.Create(model);
            var teamExisting = sut.Exists(createdTeam.Name);
            Assert.True(teamExisting);
        }

        [Fact]
        public void CheckTeamExistingAfterDelete()
        {
            var sut = CreateSUT();
            var user = new UserDetailModel()
            {
                LastName = "Michal",
                FirstName = "Kruty",
                Password = "najlepsieheslo",
                Email = "kruty@gmail.com"
            };
            var model = new TeamDetailModel()
            {
                Admin = user,
                Name = "Michalov team",
                Description = "Najlepsi z najlepsich",
            };

            var createdTeam = sut.Create(model);
            sut.Delete(createdTeam.Id);
            var exist = sut.Exists(createdTeam.Name);
            Assert.False(exist);
        }

        private TeamsRepository CreateSUT()
        {

            return new TeamsRepository(new InMemoryDbContext(), new Mapper.Mapper());
        }


        private UsersRepository CreateUser()
        {
            return new UsersRepository(new InMemoryDbContext(), new Mapper.Mapper());
        }
    }
}