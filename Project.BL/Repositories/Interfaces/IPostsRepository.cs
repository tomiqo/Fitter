using System;
using System.Collections.Generic;
using Fitter.BL.Model;

namespace Fitter.BL.Repositories.Interfaces
{
    public interface IPostsRepository
    {
        void Create(PostModel post);
        void Delete(Guid id);
        void AddAttachments(List<AttachmentModel> attachments, Guid id);
        void TagUsers(List<UserDetailModel> users, Guid id);

        IEnumerable<PostModel> GetPostsForTeam(Guid id);
        IEnumerable<AttachmentModel> GetAttachmentsForPost(Guid id);
        IEnumerable<UserListModel> GetTagsForPost(Guid id);
    }
}