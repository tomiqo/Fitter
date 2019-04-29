using System;
using System.Collections.Generic;
using Fitter.BL.Mapper.Interface;
using Fitter.BL.Model;
using System.Linq;
using Fitter.BL.Factories;
using Fitter.BL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fitter.BL.Repositories
{
    public class PostsRepository : IPostsRepository
    {
        private readonly IDbContextFactory _fitterDbContext;
        private readonly IMapper _mapper;

        public PostsRepository(IDbContextFactory fitterDbContext, IMapper mapper)
        {
            this._fitterDbContext = fitterDbContext;
            this._mapper = mapper;
        }

        public void Create(PostModel post)
        {
            using (var dbContext = _fitterDbContext.CreateDbContext())
            {
                var entity = _mapper.MapPostToEntity(post);
                dbContext.Posts.Add(entity);
                dbContext.Entry(entity.Team).State = EntityState.Unchanged;
                dbContext.Entry(entity.Team.Admin).State = EntityState.Unchanged;
                dbContext.Entry(entity.Author).State = EntityState.Unchanged;
                dbContext.SaveChanges();
            }
        }
        public void Update(PostModel post)
        {
            using (var dbContext = _fitterDbContext.CreateDbContext())
            {
                var entity = _mapper.MapPostToEntity(post);
                dbContext.Entry(entity).State = EntityState.Modified;
                dbContext.Posts.Update(entity);
                dbContext.SaveChanges();
            }
        }


        public void Delete(Guid id)
        {
            using (var dbContext = _fitterDbContext.CreateDbContext())
            {
                var entity = dbContext.Posts.First(t => t.Id == id);
                dbContext.Remove(entity);
                dbContext.SaveChanges();
            }
        }

        public void AddAttachments(List<AttachmentModel> attachments, Guid id)
        {
            using (var dbContext = _fitterDbContext.CreateDbContext())
            {
                foreach (var attachment in attachments)
                {
                    var entity = _mapper.MapAttachmentToEntity(attachment);
                    dbContext.Posts.First(p => p.Id == id).Attachments.Add(entity);
                }

                dbContext.SaveChanges();
            }
        }

        public void TagUsers(List<UserDetailModel> users, Guid id)
        {
            using (var dbContext = _fitterDbContext.CreateDbContext())
            {
                foreach (var user in users)
                {
                    var entity = _mapper.MapUserToEntity(user);
                    dbContext.Posts.First(p => p.Id == id).Tags.Add(entity);
                    //dbContext.Entry(entity).State = EntityState.Modified;
                    //dbContext.Entry(entity).State = EntityState.Unchanged;
                }

                dbContext.SaveChanges();
            }
        }

        public IList<PostModel> GetPostsForTeam(Guid id)
        {
            using (var dbContext = _fitterDbContext.CreateDbContext())
            {
                /*return (dbContext.Posts.Where(data => data.CurrentTeamId == id)
                        .Include(t => t.Team)
                        .Include(a => a.Author)
                        .Join(dbContext.Comments, data => data.Id, comm => comm.CurrentPostId, (data, comm) => new {data, comm})
                        .OrderByDescending(@t => @t.comm.Created)
                        .Select(@t => @t.data)).Distinct()
                        .Select(e => _mapper.MapPostModelFromEntity(e)).ToList();*/
                return dbContext.Posts.Include(t => t.Team)
                    .Include(a => a.Author)
                    .Where(data => data.Team.Id == id)
                    .Select(e => _mapper.MapPostModelFromEntity(e)).ToList();
            }
        }

        public IList<AttachmentModel> GetAttachmentsForPost(Guid id)
        {
            using (var dbContext = _fitterDbContext.CreateDbContext())
            {
                return dbContext.Posts
                    .First(p => p.Id == id)
                    .Attachments
                    .Select(e => _mapper.MapAttachmentModelFromEntity(e)).ToList();
            }
        }

        public IList<UserListModel> GetTagsForPost(Guid id)
        {
            using (var dbContext = _fitterDbContext.CreateDbContext())
            {
                return dbContext.Posts
                    .First(p => p.Id == id)
                    .Tags
                    .Select(e => _mapper.MapUserListModelFromEntity(e)).ToList();
            }
        }

        public PostModel GetById(Guid id)
        {
            using (var dbContext = _fitterDbContext.CreateDbContext())
            {
                var entity = dbContext.Posts
                    .Include(a => a.Author)
                    .Include(t => t.Team)
                    .First(t => t.Id == id);
                return _mapper.MapPostModelFromEntity(entity);
            }
        }

        public IList<Guid> SearchInPosts(string substring, Guid id)
        {
            using (var dbContext = _fitterDbContext.CreateDbContext())
            {
                return dbContext.Posts
                    .Include(a => a.Author)
                    .Include(t => t.Team)
                    .Where(t => t.CurrentTeamId == id)
                    .Where(e => e.Title.Contains(substring) || e.Text.Contains(substring))
                    .Select(e => e.Id).ToList();
            }
        }
    }
}