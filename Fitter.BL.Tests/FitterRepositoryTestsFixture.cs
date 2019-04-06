using Fitter.BL.Mapper.Interface;
using Fitter.BL.Repositories;
using Fitter.BL.Repositories.Interfaces;
using Fitter.DAL.Tests;

namespace Fitter.BL.Tests
{
    public class FitterRepositoryTestsFixture
    {
        private readonly IUsersRepository repository;
        private readonly IMapper mapper;
        public FitterRepositoryTestsFixture()
        {
            repository = new UsersRepository(new InMemoryFitterDbContext(), mapper);
        }
        public IUsersRepository Repository => repository;
    }
}