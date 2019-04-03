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
    }
}
