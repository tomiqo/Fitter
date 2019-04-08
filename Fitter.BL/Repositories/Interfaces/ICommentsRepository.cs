using System;
using System.Collections.Generic;
using Fitter.BL.Model;

namespace Fitter.BL.Repositories.Interfaces
{
    public interface ICommentsRepository
    {
        void Create(CommentModel comment);
        void Delete(Guid id);
        void TagUsers(List<UserDetailModel> users, Guid id);

        IList<CommentModel> GetCommentsForPost(Guid id);
        IList<UserListModel> GetTagsForComment(Guid id);
    }
}