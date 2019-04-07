using System;
using System.Collections.Generic;
using Fitter.BL.Mapper.Interface;
using Fitter.BL.Model;
using Fitter.BL.Repositories.Interfaces;
using System.Linq;
using Fitter.BL.Factories;
using Fitter.DAL.Entity;

namespace Fitter.BL.Repositories
{
    public class TeamsRepository : ITeamsRepository
    {
        private readonly IDbContextFactory _fitterDbContext;
        private readonly IMapper _mapper;
        public TeamsRepository(IDbContextFactory fitterDbContext, IMapper mapper)
        {
            this._fitterDbContext = fitterDbContext;
            this._mapper = mapper;
        }

        public TeamDetailModel Create(TeamDetailModel team)
        {
            using (var dbContext = _fitterDbContext.CreateDbContext())
            {
                var entity = _mapper.MapTeamToEntity(team);
                dbContext.Teams.Add(entity);
                dbContext.SaveChanges();
                return _mapper.MapTeamDetailModelFromEntity(entity);
            }
        }

        public TeamDetailModel GetById(Guid id)
        {
            using (var dbContext = _fitterDbContext.CreateDbContext())
            {
                var entity = dbContext.Teams.FirstOrDefault(t => t.Id == id);
                return _mapper.MapTeamDetailModelFromEntity(entity);
            }
        }

        public void AddUserToTeam(UserDetailModel user, Guid id)
        {
            using (var dbContext = _fitterDbContext.CreateDbContext())
            {
                var entity = _mapper.MapUserToEntity(user);
                dbContext.Teams
                    .First(k => k.Id == id)
                    .UsersInTeams
                    .Add(new UsersInTeam()
                        {
                            User = entity
                        });
                dbContext.SaveChanges();
            }
        }

        public void RemoveUserFromTeam(UserDetailModel user, Guid id)
        {
            using (var dbContext = _fitterDbContext.CreateDbContext())
            {
                var entity = _mapper.MapUserToEntity(user);
                var selected = (dbContext.UsersInTeams
                        .Where(data => (data.TeamId == id && data.User == entity)))
                        .First();

                dbContext.UsersInTeams.Remove(selected);
                dbContext.SaveChanges();
            }
        }

        public IEnumerable<TeamListModel> GetTeamsForUser(Guid id)
        {
            using (var dbContext = _fitterDbContext.CreateDbContext())
            {
                return dbContext.Teams
                    .Where(p => p.UsersInTeams
                        .All(k => k.UserId == id))
                    .Select(e => _mapper.MapTeamListModelFromEntity(e));
            }
        }

        public bool Exists(string name)
        {
            using (var dbContext = _fitterDbContext.CreateDbContext())
            {
                return dbContext.Teams.Any(t => t.Name == name);
            }
        }

        public void Delete(Guid id)
        {
            using (var dbContext = _fitterDbContext.CreateDbContext())
            {
                var entity = dbContext.Teams.First(t => t.Id == id);
                dbContext.Remove(entity);
                dbContext.SaveChanges();
            }
        }
    }
}
