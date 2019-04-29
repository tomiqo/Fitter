using System;
using System.Collections.Generic;
using Fitter.BL.Model;

namespace Fitter.BL.Repositories.Interfaces
{
    public interface IPostsRepository
    {
        void Create(PostModel post);
        void Update(PostModel post);
        void Delete(Guid id);
        void AddAttachments(List<AttachmentModel> attachments, Guid id);
        void TagUsers(List<UserDetailModel> users, Guid id);

        IList<PostModel> GetPostsForTeam(Guid id);
        IList<AttachmentModel> GetAttachmentsForPost(Guid id);
        IList<UserListModel> GetTagsForPost(Guid id);
        IList<Guid> SearchInPosts(string substring, Guid id);

        PostModel GetById(Guid id);
    }
}