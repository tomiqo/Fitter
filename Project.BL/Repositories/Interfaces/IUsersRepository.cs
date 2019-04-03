using System;
using System.Collections.Generic;
using Fitter.BL.Model;

namespace Fitter.BL.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        UserDetailModel Create(UserDetailModel user);
        UserDetailModel GetById(Guid id);
        UserDetailModel GetByEmail(string email);

        IEnumerable<UserListModel> GetUsersInTeam();
    }
}