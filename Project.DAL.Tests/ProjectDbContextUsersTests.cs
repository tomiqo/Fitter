using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Project.DAL.Entity;
using Xunit;

namespace Project.DAL.Tests
{
    public class ProjectDbContextUsersTests
    {
        private IDbContextProject dbContextProject;

        public ProjectDbContextUsersTests()
        {
            dbContextProject = new InMemoryProjectDbContext();
        }

        [Fact]
        public void CreateUser()
        {
            var user = new User
            {
                Name = "Michal Packa",
                Email = "miskojesuper@gmail.com",
                Nick = "michalko12",
                Password = "asdfg"
            };

            using (var dbContext = dbContextProject.CreateDbContext())
            {
                dbContext.Users.Add(user);
                dbContext.SaveChanges();
            }

            using (var dbContext = dbContextProject.CreateDbContext())
            {
                var retrievedUser = dbContext.Users
                    .Include(x => x.UsersInTeams)
                    .ThenInclude(Admin => Admin.Team)
                    .First(x => x.Id == user.Id);
                Assert.NotNull(retrievedUser);
            }
        }

        [Fact]
        public void AddUserToTeam()
        {
            var user = new User
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
            };

            using (var dbContext = dbContextProject.CreateDbContext())
            {
                dbContext.Users.Add(user);
                dbContext.SaveChanges();
            }

            using (var dbContext = dbContextProject.CreateDbContext())
            {
                var retrievedUser = dbContext.Users
                    .Include(x => x.UsersInTeams)
                    .ThenInclude(Admin => Admin.Team)
                    .First(x => x.Id == user.Id);
                Assert.Equal(1, retrievedUser.UsersInTeams.Count);
            }
        }

        [Fact]
        public void CheckPassword()
        {
            var user = new User
            {
                Name = "Fero Mrkvica",
                Email = "ferko7@gmail.com",
                Nick = "ferinho11",
                Password = "ronaldo7",
                UsersInTeams = new List<UsersInTeam>()
                {
                    new UsersInTeam()
                    {
                        Team = new Team()
                        {
                            Admin = 1,
                            Created = new DateTime(2018, 12, 5),
                            MemberCount = 2,
                            Name = "Fitaci"
                        }
                    }
                }
            };

            using (var dbContext = dbContextProject.CreateDbContext())
            {
                dbContext.Users.Add(user);
                dbContext.SaveChanges();
            }

            using (var dbContext = dbContextProject.CreateDbContext())
            {
                var retrievedUser = dbContext.Users
                    .Include(x => x.UsersInTeams)
                    .ThenInclude(Admin => Admin.Team)
                    .First(x => x.Id == user.Id);
                Assert.NotEqual(user.Password, retrievedUser.Password);
            }
        }

        [Fact]
        public void UpdateUser()
        {
            var user = new User
            {
                Name = "Eva Adamova",
                Email = "evicka@seznam.cz",
                Nick = "evka98",
                Password = "heslo587",
                UsersInTeams = new List<UsersInTeam>()
                {
                    new UsersInTeam()
                    {
                        Team = new Team()
                        {
                            Admin = 4,
                            Created = new DateTime(2015, 1, 1),
                            MemberCount = 11,
                            Name = "United"
                        }
                    }
                }
            };
      
            using (var dbContext = dbContextProject.CreateDbContext())
            {
                dbContext.Users.Add(user);
                dbContext.SaveChanges();
            }

            user.Nick = "evka97";
            using (var dbContext = dbContextProject.CreateDbContext())
            {
                dbContext.Users.Update(user);
                dbContext.SaveChanges();
            }

            using (var dbContext = dbContextProject.CreateDbContext())
            {
                var retrievedUser = dbContext.Users
                    .Include(x => x.UsersInTeams)
                    .ThenInclude(Admin => Admin.Team)
                    .First(x => x.Id == user.Id);
                Assert.NotNull(retrievedUser);
                Assert.Equal(user.Nick, retrievedUser.Nick);
            }
        }

        [Fact]
        public void RemoveUser()
        {
            var user = new User
            {
                Name = "Jozef Kovac",
                Email = "jovi67@pokec.sk",
                Nick = "jozsi67",
                Password = "adobe12345",
                UsersInTeams = new List<UsersInTeam>()
                {
                    new UsersInTeam()
                    {
                        Team = new Team()
                        {
                            Admin = 4,
                            Created = new DateTime(2015, 1, 1),
                            MemberCount = 11,
                            Name = "United"
                        }
                    }
                }
            };

            using (var dbContext = dbContextProject.CreateDbContext())
            {
                dbContext.Users.Add(user);
                dbContext.SaveChanges();
            }

            using (var dbContext = dbContextProject.CreateDbContext())
            {
                dbContext.Users.Remove(user);
                dbContext.SaveChanges();
            }

            using (var dbContext = dbContextProject.CreateDbContext())
            {
                var retrievedUser = dbContext.Users
                    .Include(x => x.UsersInTeams)
                    .ThenInclude(Admin => Admin.Team)
                    .FirstOrDefault(x => x.Id == user.Id);
                Assert.Null(retrievedUser);
            }
        }
    }
}