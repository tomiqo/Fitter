using Fitter.BL.Model;
using Xunit;

namespace Fitter.BL.Tests
{
    public class UsersRepositoryTests : IClassFixture<FitterRepositoryTestsFixture>
    {
        private readonly FitterRepositoryTestsFixture fixture;

        public UsersRepositoryTests(FitterRepositoryTestsFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void CreateUser()
        {
            var model = new UserDetailModel
            {
                FirstName = "jozef",
                LastName = "kovac",
                Password = "asdfg"
            };

            var returnedModel = fixture.Repository.Create(model);

            Assert.NotNull(returnedModel);
        }
    }
}