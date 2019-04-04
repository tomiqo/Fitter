using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using Fitter.DAL.Entity;
using Fitter.DAL.Enums;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Sdk;

namespace Fitter.DAL.Tests
{
    public class InMemoryFitterRelationsTests
    {
        public class FitterDbContextTests
        {
            private IFitterDbContext dbContextFitter;
            public FitterDbContextTests()
            {
                dbContextFitter = new InMemoryFitterDbContext();
            }

            [Fact]
            public void AddUserToTeam()
            {
                var userJozef = new User()
                {
                    FirstName = "Jozef",
                    LastName = "Dlhy",
                    Email = "jozefdlhy@seznam.cz",
                    Password = "jozkojesuper"
                };

                var userInTeamAdam = new User()
                {
                    FirstName = "Adam",
                    LastName = "Zly",
                    Email = "adamko@seznam.cz",
                    Password = "asdfg"
                };

                var userInTeamEva = new User()
                {
                    FirstName = "Eva",
                    LastName = "Vianocna",
                    Email = "evicka128@seznam.cz",
                    Password = "123456"
                };

                var team1 = new Team()
                {
                    Admin = userJozef,
                    Description = "Skupina Jozefa Dlheho",
                    Name = "The bests",
                    UsersInTeams = new List<UsersInTeam>()
                    {
                        new UsersInTeam { User = userInTeamAdam},
                        new UsersInTeam { User = userInTeamEva}
                    }
                };

                using (var dbContext = dbContextFitter.CreateDbContext())
                {
                    dbContext.Users.Add(userJozef);
                    dbContext.Users.Add(userInTeamAdam);
                    dbContext.Users.Add(userInTeamEva);
                    dbContext.Teams.Add(team1);
                    dbContext.SaveChanges();
                }

                using (var dbContext = dbContextFitter.CreateDbContext())
                {
                    var retrievedTeam = dbContext.Teams
                        .Include(x => x.UsersInTeams)
                        .Include(a => a.Admin)
                        .First(x => x.Id == team1.Id);
                    Assert.Equal(retrievedTeam.Admin.FullName, userJozef.FullName);
                    Assert.Equal(retrievedTeam.Admin.Id, userJozef.Id);
                    Assert.Equal(2, retrievedTeam.UsersInTeams.Count);
                    Assert.Equal(retrievedTeam.Name, team1.Name);
                }

                using (var dbContext = dbContextFitter.CreateDbContext())
                {
                    dbContext.Remove(userInTeamAdam);
                    dbContext.Remove(userInTeamEva);
                    dbContext.SaveChanges();
                }

                using (var dbContext = dbContextFitter.CreateDbContext())
                {
                    var retrievedTeam = dbContext.Teams
                        .Include(x => x.UsersInTeams)
                        .Include(a => a.Admin)
                        .FirstOrDefault(x => x.Id == team1.Id);
                    Assert.Equal(0, retrievedTeam.UsersInTeams.Count);
                    Assert.Equal(retrievedTeam.Admin.Id, userJozef.Id);
                }
            }

            [Fact]
            public void AddPostAndComment()
            {
                var userAlfonz = new User()
                {
                    LastName = "Kruty",
                    FirstName = "Alfonz",
                    Email = "kruty@gmail.com",
                    Password = "qwerty"
                };

                var userMichaela = new User()
                {
                    LastName = "Michaela",
                    FirstName = "Velka",
                    Email = "misicka@gmail.com",
                    Password = "abcdefgh"
                };

                var userDaniel = new User()
                {
                    LastName = "Michaela",
                    FirstName = "Velka",
                    Email = "misicka@gmail.com",
                    Password = "abcdefgh"
                };

                var attachmentPDF = new Attachment()
                {
                    File = new byte[2],
                    FileType = FileType.File,
                    Name = "Subor PDF"
                };

                var postInMichaelaTeam = new Post()
                {
                    Author = userAlfonz,
                    Attachments = new List<Attachment>() { attachmentPDF },
                    Created = DateTime.Today,
                    Text = "Post na subor PDF",
                    Title = "PDF",
                    Tags = new List<User>() { userDaniel}
                };

                var commentInMichaelaTeam = new Comment()
                {
                    Author = userDaniel,
                    Post = postInMichaelaTeam,
                    Text = "komentar na post PDF",
                    Created = new DateTime(2019, 4, 4)
                };

                var teamMichaela = new Team()
                {
                    Admin = userMichaela,
                    Description = "Spolocna skupina pre dievcata a chlapcov",
                    Name = "Girls&Boys",
                    UsersInTeams = new List<UsersInTeam>()
                    {
                        new UsersInTeam() {User = userDaniel},
                        new UsersInTeam() {User = userAlfonz}
                    },
                    Posts = new List<Post>() { postInMichaelaTeam }
                };

                using (var dbContext = dbContextFitter.CreateDbContext())
                {
                    dbContext.Users.Add(userAlfonz);
                    dbContext.Users.Add(userMichaela);
                    dbContext.Users.Add(userDaniel);
                    dbContext.Teams.Add(teamMichaela);
                    dbContext.Attachments.Add(attachmentPDF);
                    dbContext.Posts.Add(postInMichaelaTeam);
                    dbContext.Comments.Add(commentInMichaelaTeam);
                    dbContext.SaveChanges();
                }

                using (var dbContext = dbContextFitter.CreateDbContext())
                {
                    var retrievedTeam = dbContext.Teams
                        .Include(x => x.UsersInTeams)
                        .Include(a => a.Admin)
                        .Include(p => p.Posts)
                        .Include(a => a.Admin)
                        .ThenInclude(c => c.Comments)
                        .First(x => x.Id == teamMichaela.Id);
                    Assert.Equal(retrievedTeam.Admin.Id, teamMichaela.Admin.Id);
                    Assert.Equal(2, retrievedTeam.UsersInTeams.Count);
                    Assert.Equal(1,retrievedTeam.Posts.Count);
                }

                using (var dbContext = dbContextFitter.CreateDbContext())
                {
                    var retrievedPost = dbContext.Posts
                        .Include(c => c.Comments)
                        .Include(a => a.Attachments)
                        .Include(t => t.Tags)
                        .Include(t => t.Team)
                        .ThenInclude(d => d.Admin)
                        .Include(a => a.Author)
                        .ThenInclude(u => u.UsersInTeams)
                        .ThenInclude(t => t.User)
                        .First(x => x.Id == postInMichaelaTeam.Id);
                    Assert.NotNull(retrievedPost);
                    Assert.Equal(retrievedPost.Author.Id, userAlfonz.Id);
                    Assert.Equal(1, retrievedPost.Attachments.Count);
                    Assert.Equal(retrievedPost.Team.Id, teamMichaela.Id);
                    Assert.Equal(1, retrievedPost.Tags.Count);
                }

                using (var dbContext = dbContextFitter.CreateDbContext())
                {
                    dbContext.Remove(postInMichaelaTeam);
                    dbContext.SaveChanges();
                }

                using (var dbContext = dbContextFitter.CreateDbContext())
                {
                    var retrievedPost = dbContext.Posts
                        .FirstOrDefault(x => x.Id == postInMichaelaTeam.Id);
                    Assert.Null(retrievedPost);

                    var retrievedComment = dbContext.Comments
                        .FirstOrDefault(x => x.Id == commentInMichaelaTeam.Id);
                    Assert.Null(retrievedComment);

                    var retrievedAttachment = dbContext.Attachments
                        .FirstOrDefault(x => x.Id == attachmentPDF.Id);
                    Assert.Null(retrievedAttachment);
                }
            }
        }
    }
}