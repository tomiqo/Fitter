using Fitter.BL.Model;
using Fitter.DAL.Entity;

namespace Fitter.BL.Mapper.Interface
{
    public interface IMapper
    {
        User MapUserToEntity(UserDetailModel model);
        UserDetailModel MapUserDetailModelFromEntity(User user);
        UserListModel MapUserListModelFromEntity(User user);

        Team MapTeamToEntity(TeamDetailModel model);
        TeamDetailModel MapTeamDetailModelFromEntity(Team team);
        TeamListModel MapTeamListModelFromEntity(Team team);

        Post MapPostToEntity(PostDetailModel model);
        PostDetailModel MapPostDetailModelFromEntity(Post post);
        PostListModel MapPostListModelFromEntity(Post post);

        Comment MapCommentToEntity(CommentModel model);
        CommentModel MapCommentModelFromEntity(Comment comment);

        Attachment MapAttachmentToEntity(AttachmentModel model);
        AttachmentModel MapAttachmentModelFromEntity(Attachment attachment);
    }
}