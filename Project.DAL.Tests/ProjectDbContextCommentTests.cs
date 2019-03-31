using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Project.DAL.Entity;
using Project.DAL.Enums;
using Xunit;

namespace Project.DAL.Tests
{
    public class ProjectDbContextCommentTests
    {
        private IDbContextProject dbContextProject;
        public ProjectDbContextCommentTests()
        {
            dbContextProject = new InMemoryProjectDbContext();
        }

        [Fact]
        public void CreateComment()
        {
            var comment = new Comment
            {
                Author = 2,
                Text = "First commentar in group",
                Created = new DateTime(2017, 5, 9)
            };

            using (var dbContext = dbContextProject.CreateDbContext())
            {
                dbContext.Comments.Add(comment);
                dbContext.SaveChanges();
            }

            using (var dbContext = dbContextProject.CreateDbContext())
            {
                var retrievedComment = dbContext.Comments
                    .Include(x => x.Attachments)
                    .Include(u => u.Comments)
                    .Include(t => t.Tags)
                    .First(x => x.Id == comment.Id);
                Assert.NotNull(retrievedComment);
                Assert.Equal(comment.Text, retrievedComment.Text);
            }
        }

        [Fact]
        public void AddAttachmentTagsToComment()
        {
            var comment = new Comment
            {
                Author = 1,
                Text = "Second commentar in group",
                Created = new DateTime(2011, 11, 10),
                Attachments = new List<Attachment>()
                {
                    new Attachment()
                    {
                        FileType = FileType.Video,
                        File = new byte[4098],
                        FileSize = 2049,
                        Name = "FIT video"
                    }
                },
                Tags = new List<User>()
                {
                    new User()
                    {
                        Name = "Eva Adamova",
                        Email = "evicka@seznam.cz",
                        Nick = "evka98",
                        Password = "heslo587",
                    }
                }
            };

            using (var dbContext = dbContextProject.CreateDbContext())
            {
                dbContext.Comments.Add(comment);
                dbContext.SaveChanges();
            }

            using (var dbContext = dbContextProject.CreateDbContext())
            {
                var retrievedComment = dbContext.Comments
                    .Include(x => x.Attachments)
                    .Include(u => u.Comments)
                    .Include(t => t.Tags)
                    .First(x => x.Id == comment.Id);
                Assert.NotNull(retrievedComment);
                Assert.Equal(1,retrievedComment.Tags.Count);
                Assert.Equal(1, retrievedComment.Attachments.Count);
            }
        }

        [Fact]
        public void RemoveComment()
        {
            var comment = new Comment
            {
                Author = 7,
                Text = "Commentar in group",
                Created = new DateTime(2021, 11, 10),
                Attachments = new List<Attachment>()
                {
                    new Attachment()
                    {
                        FileType = FileType.Picture,
                        File = new byte[712],
                        FileSize = 256,
                        Name = "FIT picture"
                    }
                },
                Tags = new List<User>()
                {
                    new User()
                    {
                        Name = "Jozef Kovac",
                        Email = "jovi67@pokec.sk",
                        Nick = "jozsi67",
                        Password = "adobe12345",
                    }
                }
            };

            using (var dbContext = dbContextProject.CreateDbContext())
            {
                dbContext.Comments.Add(comment);
                dbContext.SaveChanges();
            }

            using (var dbContext = dbContextProject.CreateDbContext())
            {
                dbContext.Comments.Remove(comment);
                dbContext.SaveChanges();
            }

            using (var dbContext = dbContextProject.CreateDbContext())
            {
                var retrievedComment = dbContext.Comments
                    .Include(x => x.Attachments)
                    .Include(u => u.Comments)
                    .Include(t => t.Tags)
                    .FirstOrDefault(x => x.Id == comment.Id);
                Assert.NotNull(retrievedComment);
            }
        }
    }
}