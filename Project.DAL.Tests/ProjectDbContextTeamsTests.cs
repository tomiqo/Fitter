using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Project.DAL.Entity;
using Xunit;

namespace Project.DAL.Tests
{
    public class ProjectDbContextTeamsTests
    {
        private IDbContextProject dbContextProject;
        public ProjectDbContextTeamsTests()
        {
            dbContextProject = new InMemoryProjectDbContext();
        }

        [Fact]
        public void CreateTeam()
        {
            var team = new Team
            {
                Admin = 1,
                Created = new DateTime(2019, 10, 6),
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
                            Password = "jankojesuper"
                        }
                    }
                },
                Posts = new List<Post>()
                {
                    new Post()
                    {
                        Text = "Comment in group",
                        Title = "Comment title",
                        Tags = new List<User>()
                        {
                            new User()
                            {
                                Name = "Michal Packa",
                                Email = "miskojesuper@gmail.com",
                                Nick = "michalko12",
                                Password = "asdfg"
                            }
                        }
                    }
                }
            };

            using (var dbContext = dbContextProject.CreateDbContext())
            {
                dbContext.Teams.Add(team);
                dbContext.SaveChanges();
            }

            using (var dbContext = dbContextProject.CreateDbContext())
            {
                var retrievedTeam = dbContext.Teams
                    .Include(x => x.UsersInTeams)
                    .Include(p => p.Posts)
                    .First(x => x.Id == team.Id);
                Assert.NotNull(retrievedTeam);
                Assert.Equal(1, retrievedTeam.Posts.Count);
            }
        }

        [Fact]
        public void UpdateTeam()
        {
            var team = new Team
            {
                Admin = 1,
                Created = new DateTime(2018, 7, 7),
                MemberCount = 10,
                Name = "ICS",
                UsersInTeams = new List<UsersInTeam>()
                {
                    new UsersInTeam()
                    {
                        User = new User
                        {
                            Name = "Michal Packa",
                            Email = "miskojesuper@gmail.com",
                            Nick = "michalko12",
                            Password = "asdfg"
                        }
                    }
                }
            };

            using (var dbContext = dbContextProject.CreateDbContext())
            {
                dbContext.Teams.Add(team);
                dbContext.SaveChanges();
            }

            team.Name = "Fitaci";
            using (var dbContext = dbContextProject.CreateDbContext())
            {
                dbContext.Teams.Update(team);
                dbContext.SaveChanges();
            }

            using (var dbContext = dbContextProject.CreateDbContext())
            {
                var retrievedTeam = dbContext.Teams
                    .Include(x => x.UsersInTeams)
                    .ThenInclude(Name => Name.User)
                    .First(x => x.Id == team.Id);
                Assert.NotNull(retrievedTeam);
                Assert.Equal(team.Name, retrievedTeam.Name);
            }
        }

        [Fact]
        public void RemoveTeam()
        {
            var team = new Team
            {
                Admin = 2,
                Created = new DateTime(2017, 11, 25),
                MemberCount = 9,
                Name = "ICS",
                UsersInTeams = new List<UsersInTeam>()
                {
                    new UsersInTeam()
                    {
                        User = new User()
                        {
                            Name = "Fero Mrkvica",
                            Email = "ferko7@gmail.com",
                            Nick = "ferinho11",
                            Password = "ronaldo7"
                        }
                    }
                }
            };

            using (var dbContext = dbContextProject.CreateDbContext())
            {
                dbContext.Teams.Add(team);
                dbContext.SaveChanges();
            }

            using (var dbContext = dbContextProject.CreateDbContext())
            {
                dbContext.Teams.Remove(team);
                dbContext.SaveChanges();
            }

            using (var dbContext = dbContextProject.CreateDbContext())
            {
                var retrievedTeam = dbContext.Teams
                    .Include(x => x.UsersInTeams)
                    .ThenInclude(Name => Name.User)
                    .FirstOrDefault(x => x.Id == team.Id);
                Assert.Null(retrievedTeam);
            }
        }

        [Fact]
        public void CheckAdminNumType()
        {
            var team = new Team
            {
                Admin = 3,
                Created = new DateTime(2014, 1, 25),
                MemberCount = 2,
                Name = "Group",
                UsersInTeams = new List<UsersInTeam>()
                {
                    new UsersInTeam()
                    {
                        User = new User()
                        {
                            Name = "Eva Adamova",
                            Email = "evicka@seznam.cz",
                            Nick = "evka98",
                            Password = "heslo587"
                        }
                    }
                }
            };

            using (var dbContext = dbContextProject.CreateDbContext())
            {
                dbContext.Teams.Add(team);
                dbContext.SaveChanges();
            }

            using (var dbContext = dbContextProject.CreateDbContext())
            {
                var retrievedTeam = dbContext.Teams
                    .Include(x => x.UsersInTeams)
                    .ThenInclude(Name => Name.User)
                    .First(x => x.Id == team.Id);
                Assert.IsType<int>(retrievedTeam.Admin);
            }
        }
    }
}