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
    public class CommentsRepository : ICommentsRepository
    {
        private readonly IDbContextFactory _fitterDbContext;
        private readonly IMapper _mapper;
        public CommentsRepository(IDbContextFactory fitterDbContext, IMapper mapper)
        {
            this._fitterDbContext = fitterDbContext;
            this._mapper = mapper;
        }

        public void Create(CommentModel comment)
        {
            using (var dbContext = _fitterDbContext.CreateDbContext())
            {
                var entity = _mapper.MapCommentToEntity(comment);
                dbContext.Comments.Add(entity);
                dbContext.SaveChanges();
            }
        }

        public void Delete(Guid id)
        {
            using (var dbContext = _fitterDbContext.CreateDbContext())
            {
                var entity = dbContext.Comments.First(t => t.Id == id);
                dbContext.Remove(entity);
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
                    dbContext.Comments.First(p => p.Id == id).Tags.Add(entity);
                }

                dbContext.SaveChanges();
            }
        }

        public IEnumerable<CommentModel> GetCommentsForPost(Guid id)
        {
            using (var dbContext = _fitterDbContext.CreateDbContext())
            {
                return dbContext.Posts
                    .Include(c => c.Comments)
                    .First(p => p.Id == id)
                    .Comments
                    .Select(e => _mapper.MapCommentModelFromEntity(e));
            }
        }

        public IEnumerable<UserListModel> GetTagsForComment(Guid id)
        {
            using (var dbContext = _fitterDbContext.CreateDbContext())
            {
                return dbContext.Comments
                    .Include(t => t.Tags)
                    .First(p => p.Id == id)
                    .Tags
                    .Select(e => _mapper.MapUserListModelFromEntity(e));
            }
        }
    }
}