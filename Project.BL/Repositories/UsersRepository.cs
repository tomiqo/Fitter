using System;
using System.Collections.Generic;
using System.Linq;
using Fitter.BL.Mapper.Interface;
using Fitter.BL.Model;
using Fitter.BL.Repositories.Interfaces;
using Fitter.BL.Services;
using Fitter.DAL;

namespace Fitter.BL.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IFitterDbContext _fitterDbContext;
        private readonly IMapper _mapper;
        public UsersRepository(IFitterDbContext fitterDbContext, IMapper mapper)
        {
            this._fitterDbContext = fitterDbContext;
            this._mapper = mapper;
        }

        public UserDetailModel Create(UserDetailModel user)
        {
            using (var dbContext = _fitterDbContext.CreateDbContext())
            {
                var passwordHasher = new PasswordHasher(user.Password);
                user.Password = passwordHasher.GetHashedPassword();

                var entity = _mapper.MapUserToEntity(user);
                dbContext.Users.Add(entity);
                dbContext.SaveChanges();
                return _mapper.MapUserDetailModelFromEntity(entity);
            }
        }

        public UserDetailModel GetById(Guid id)
        {
            using (var dbContext = _fitterDbContext.CreateDbContext())
            {
                var entity = dbContext.Users.First(t => t.Id == id);
                return _mapper.MapUserDetailModelFromEntity(entity);
            }
        }

        public UserDetailModel GetByEmail(string email)
        {
            using (var dbContext = _fitterDbContext.CreateDbContext())
            {
                var entity = dbContext.Users.First(t => t.Email == email);
                return _mapper.MapUserDetailModelFromEntity(entity);
            }
        }

        public IEnumerable<UserListModel> GetUsersInTeam(Guid id)
        {
            using (var dbContext = _fitterDbContext.CreateDbContext())
            {
                return dbContext.Users
                        .Where(p => p.UsersInTeams
                            .All(k => k.TeamId == id))
                        .Select(e => _mapper.MapUserListModelFromEntity(e));
            }
        }

        public IEnumerable<UserListModel> GetUsersNotInTeam(Guid id)
        {
            using (var dbContext = _fitterDbContext.CreateDbContext())
            {
                return dbContext.Users
                    .Where(p => p.UsersInTeams
                        .All(k => k.TeamId != id))
                    .Select(e => _mapper.MapUserListModelFromEntity(e));
            }
        }
    }
}
