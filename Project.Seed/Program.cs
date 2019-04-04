using System;
using System.Collections.Generic;
using Fitter.DAL;
using Fitter.DAL.Entity;
using Fitter.DAL.Enums;
using Microsoft.EntityFrameworkCore;

namespace Fitter.Seed
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var dbContext = CreateDbContext())
            {
                ClearDatabase(dbContext);
                SeedData(dbContext);
            }
        }

        private static void SeedData(FitterDbContext dbContext)
        {
            var user1 = new User
            {
                FirstName = "ikkmlm",
                LastName = "Boros",
                Email = "dartwader128@azet.sk",
                Password = "pumkli28",
            };
            dbContext.Users.Add(user1);

            var user2 = new User
            {
                FirstName = "Djkjg",
                LastName = "Danko",
                Email = "optimus13@pokec.sk",
                Password = "danulko13",
            };
            dbContext.Users.Add(user2);
            dbContext.SaveChanges();
            var team = new Team
            {
                Name = "Sicaci",
                Admin = user1,
                Description = "Team for seeding data",
                UsersInTeams = 
                {
                    new UsersInTeam()
                    {
                        User = user1
                    }
                }
            };
            dbContext.Teams.Add(team);
            team.UsersInTeams.Add(new UsersInTeam(){User = user2});
            dbContext.Teams.Add(team);

            var post = new Post
            {
                Author = user1,
                Text = "Pekna praca!",
                Created = DateTime.Now,
                Title = "Vysocina",
                Attachments =
                {
                    new Attachment()
                    {
                        FileType = FileType.Picture,
                        File = new byte[5],
                        Name = "Kuraptomuj"
                    }
                },
                Tags =
                {
                    user2
                },
                Team = team
            };
            dbContext.Posts.Add(post);

            var comment = new Comment
            {
                Author = user1,
                Text = "Velmi dobre to ide.",
                Created = DateTime.Now,
                Post = post,
                Tags =
                {
                    user2
                }
            };
            dbContext.Comments.Add(comment);
            var comment2 = new Comment
            {
                Author = user1,
                Text = "Asi hej",
                Created = DateTime.Now,
                Post = post,
            };
            dbContext.SaveChanges();
            dbContext.Users.Remove(user1);
            dbContext.Teams.Remove(team);
            dbContext.SaveChanges();
        }

        private static void ClearDatabase(FitterDbContext dbContext)
        {
            dbContext.RemoveRange(dbContext.Attachments);
            dbContext.RemoveRange(dbContext.Comments);
            dbContext.RemoveRange(dbContext.Posts);
            dbContext.RemoveRange(dbContext.Teams);
            dbContext.RemoveRange(dbContext.Users);
            dbContext.RemoveRange(dbContext.UsersInTeams);
            dbContext.SaveChanges();
        }

        private static FitterDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<FitterDbContext>();
            optionsBuilder.UseSqlServer(
                @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog = FitterDB;MultipleActiveResultSets = True;Integrated Security = True; ");
            return new FitterDbContext(optionsBuilder.Options);
        }
    }
}
