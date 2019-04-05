using System;
using System.Collections.Generic;
using Fitter.BL.Mapper.Interface;
using Fitter.BL.Model;
using Fitter.DAL;
using System.Linq;
using Fitter.BL.Repositories.Interfaces;

namespace Fitter.BL.Repositories
{
    public class PostsRepository : IPostsRepository
    {
        private readonly IFitterDbContext _fitterDbContext;
        private readonly IMapper _mapper;

        public PostsRepository(IFitterDbContext fitterDbContext, IMapper mapper)
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
                }

                dbContext.SaveChanges();
            }
        }

        public IEnumerable<PostModel> GetPostsForTeam(Guid id)
        {
            using (var dbContext = _fitterDbContext.CreateDbContext())
            {
                /* TODO
                 Příspěvky v týmu jsou zobrazeny i s komentáři a seřazeny dle data vytvoření posledního komentáře, 
                 ne data zveřejnění příspěvku. Komentáře se řadí chronologicky.

                Příspěvek č. 1
                    odpověď č. 1 na příspěvek č. 1
                    odpověď č. N na příspěvek č. 1
                Příspěvek č. 2
                    odpověď č. 1 na příspěvek č. 2
                    odpověď č. N na příspěvek č. 2
                 */

                /*var selected = (from data in dbContext.Teams
                                where data.Id == id
                                orderby data.
                                select data)*/

                return dbContext.Teams
                    .First(p => p.Id == id)
                    .Posts
                    .Select(e => _mapper.MapPostModelFromEntity(e));
            }
        }

        public IEnumerable<AttachmentModel> GetAttachmentsForPost(Guid id)
        {
            using (var dbContext = _fitterDbContext.CreateDbContext())
            {
                return dbContext.Posts
                    .First(p => p.Id == id)
                    .Attachments
                    .Select(e => _mapper.MapAttachmentModelFromEntity(e));
            }
        }

        public IEnumerable<UserListModel> GetTagsForPost(Guid id)
        {
            using (var dbContext = _fitterDbContext.CreateDbContext())
            {
                return dbContext.Posts
                    .First(p => p.Id == id)
                    .Tags
                    .Select(e => _mapper.MapUserListModelFromEntity(e));
            }
        }
    }
}