using System;
using System.Collections.Generic;
using System.Net.Mail;
using Xunit;

namespace Fitter.DAL.Tests
{
    public class FitterDbContextAttachmentTest
    {
        private IFitterDbContext FitterDbContext;
        public FitterDbContextAttachmentTest()
        {
            FitterDbContext = new InMemoryFitterDbContext();
        }

        [Fact]
        public void CreateAttachment()
        {
            var attachment = new Attachment()
            {
                Name = "Attachment 1",
                FileSize = 1024,
                FileType = FileType.Picture,
                File = new byte[2048],
                Comment = new Comment()
                {
                    Author = 1,
                    Created = new DateTime(2014, 12, 1),
                    Text = "Comment attachment1",
                    Tags = new List<User>()
                    {
                        new User()
                        {
                            Name = "Jan Novak",
                            Email = "jannovak@gmail.com",
                            Nick = "janko123",
                            Password = "jankojesuper"
                        }
                    }
                }
            };

            using (var dbContext = FitterDbContext.CreateDbContext())
            {
                dbContext.Attachments.Add(attachment);
                dbContext.SaveChanges();
            }

            using (var dbContext = FitterDbContext.CreateDbContext())
            {
                var retrievedAttachment = dbContext.Attachments
                    .Include(x => x.Comment)
                    .Include(t => t.Comment.Tags)
                    .First(x => x.Id == attachment.Id);
                Assert.NotNull(retrievedAttachment);
                Assert.Equal(1, retrievedAttachment.Comment.Tags.Count);
            }
        }

        [Fact]
        public void UpdateAttachment()
        {
            var attachment = new Attachment()
            {
                Name = "Attachment 2",
                FileSize = 2014,
                FileType = FileType.File,
                File = new byte[2048],
                Comment = new Comment()
                {
                    Author = 1,
                    Created = new DateTime(2014, 12, 1),
                    Text = "Comment attachment2",
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
            };

            using (var dbContext = FitterDbContext.CreateDbContext())
            {
                dbContext.Attachments.Add(attachment);
                dbContext.SaveChanges();
            }

            attachment.FileType = FileType.Video;
            using (var dbContext = FitterDbContext.CreateDbContext())
            {
                dbContext.Attachments.Update(attachment);
                dbContext.SaveChanges();
            }

            using (var dbContext = FitterDbContext.CreateDbContext())
            {
                var retrievedAttachment = dbContext.Attachments
                    .Include(x => x.Comment)
                    .Include(t => t.Comment.Tags)
                    .First(x => x.Id == attachment.Id);
                Assert.NotNull(retrievedAttachment);
                Assert.Equal(FileType.Video, retrievedAttachment.FileType);
            }
        }

        [Fact]
        public void RemoveAttachment()
        {
            var attachment = new Attachment()
            {
                Name = "Attachment 3",
                FileSize = 512,
                FileType = FileType.File,
                File = new byte[1024],
                Comment = new Comment()
                {
                    Author = 1,
                    Created = new DateTime(2014, 12, 1),
                    Text = "Comment attachment3",
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
            };

            using (var dbContext = FitterDbContext.CreateDbContext())
            {
                dbContext.Attachments.Add(attachment);
                dbContext.SaveChanges();
            }

            using (var dbContext = FitterDbContext.CreateDbContext())
            {
                dbContext.Attachments.Remove(attachment);
                dbContext.SaveChanges();
            }

            using (var dbContext = FitterDbContext.CreateDbContext())
            {
                var retrievedAttachment = dbContext.Attachments
                    .Include(x => x.Comment)
                    .Include(t => t.Comment.Tags)
                    .FirstOrDefault(x => x.Id == attachment.Id);
                Assert.Null(retrievedAttachment);
            }
        }
    }
}