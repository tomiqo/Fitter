using System;
using System.Collections.Generic;
using System.Text;
using Fitter.BL.Mapper.Interface;
using Fitter.BL.Model;
using Fitter.DAL.Entity;

namespace Fitter.BL.Mapper
{
    public class Mapper : IMapper
    {
        #region User Mapping

        public User MapUserToEntity(UserDetailModel model)
        {
            return new User
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password
            };
        }

        public UserDetailModel MapUserDetailModelFromEntity(User user)
        {
            return new UserDetailModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password
            };
        }

        public UserListModel MapUserListModelFromEntity(User user)
        {
            return new UserListModel
            {
                Id = user.Id,
                Fullname = user.FullName
            };

        }

        #endregion

        #region Team Mapping

        public Team MapTeamToEntity(TeamDetailModel model)
        {
            return new Team
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Admin = MapUserToEntity(model.Admin)
            };
        }

        public TeamDetailModel MapTeamDetailModelFromEntity(Team team)
        {
            return new TeamDetailModel
            {
                Id = team.Id,
                Name = team.Name,
                Description = team.Description,
                Admin = MapUserDetailModelFromEntity(team.Admin)
            };
        }

        public TeamListModel MapTeamListModelFromEntity(Team team)
        {
            return new TeamListModel
            {
                Id = team.Id,
                Name = team.Name
            };
        }

        #endregion

        #region Post Mapping

        public Post MapPostToEntity(PostDetailModel model)
        {
            return new Post
            {
                Id = model.Id,
                Title = model.Title,
                Author = MapUserToEntity(model.Author),
                Text = model.Text,
                Created = model.Created,
                Team = MapTeamToEntity(model.Team)
            };
        }

        public PostDetailModel MapPostDetailModelFromEntity(Post post)
        {
            return new PostDetailModel
            {
                Id = post.Id,
                Author = MapUserDetailModelFromEntity(post.Author),
                Created = post.Created,
                Team = MapTeamDetailModelFromEntity(post.Team),
                Text = post.Text,
                Title = post.Title
            };
        }

        public PostListModel MapPostListModelFromEntity(Post post)
        {
            return new PostListModel
            {
                Id = post.Id,
                Author = MapUserDetailModelFromEntity(post.Author),
                Created = post.Created,
                Title = post.Title
            };
        }

        #endregion

        #region Comment Mapping

        public Comment MapCommentToEntity(CommentModel model)
        {
            return new Comment
            {
                Id = model.Id,
                Author = MapUserToEntity(model.Author),
                Created = model.Created,
                Text = model.Text,
                Post = MapPostToEntity(model.Post)
            };
        }

        public CommentModel MapCommentModelFromEntity(Comment comment)
        {
            return new CommentModel
            {
                Id = comment.Id,
                Author = MapUserDetailModelFromEntity(comment.Author),
                Created = comment.Created,
                Text = comment.Text,
                Post = MapPostDetailModelFromEntity(comment.Post)
            };
        }

        #endregion

        #region Attachment Mapping

        public Attachment MapAttachmentToEntity(AttachmentModel model)
        {
            return new Attachment
            {
                Id = model.Id,
                Name = model.Name,
                FileType = model.FileType,
                File = model.File,
                Post = MapPostToEntity(model.Post)
            };
        }

        public AttachmentModel MapAttachmentModelFromEntity(Attachment attachment)
        {
            return new AttachmentModel
            {
                Id = attachment.Id,
                Name = attachment.Name,
                File = attachment.File,
                FileType = attachment.FileType,
                Post = MapPostDetailModelFromEntity(attachment.Post)
            };
        }

        #endregion
    }
}
