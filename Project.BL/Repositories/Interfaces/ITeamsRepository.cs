using System;
using System.Collections.Generic;
using Fitter.BL.Model;

namespace Fitter.BL.Repositories.Interfaces
{
    public interface ITeamsRepository
    {
        TeamDetailModel Create(TeamDetailModel team);
        TeamDetailModel GetById(Guid id);

        void AddUserToTeam(UserDetailModel user, Guid id);
        void RemoveUserFromTeam(UserDetailModel user, Guid id);
        bool Exists(string name);
        void Delete(Guid id);

        IEnumerable<TeamListModel> GetTeamsForUser(Guid id);
    }
}