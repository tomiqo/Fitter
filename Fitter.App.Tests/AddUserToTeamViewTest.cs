using System;
using Fitter.App.ViewModels;
using Fitter.BL.Model;
using Fitter.BL.Repositories;
using Fitter.BL.Repositories.Interfaces;
using Fitter.BL.Services;
using Moq;
using Xunit;

namespace Fitter.App.Tests
{
    public class AddUserToTeamViewTest
    {
        private readonly Mock<IUsersRepository> _userRepositoryMock;
        private readonly Mock<Mediator> _mediatorMock;
        private readonly AddUScreenViewModel userScreenViewModelSUT;

        public AddUserToTeamViewTest()
        {
            this._userRepositoryMock = new Mock<IUsersRepository>();
            this._mediatorMock = new Mock<Mediator>() {CallBase = true};

            _userRepositoryMock.Setup(repository => repository
                    .GetById(new Guid("7d9b38d0-4fb9-4b49-b052-08d6ce33d4e9")))
                .Returns((() => new UserDetailModel()));
           
            this.userScreenViewModelSUT = new AddUScreenViewModel(_userRepositoryMock.Object, _mediatorMock.Object);
        }

        [Fact]
        public void Load_Calls_UsersRepository()
        {
            userScreenViewModelSUT.Load();
            _userRepositoryMock.Verify(repository => repository
                .GetById(new Guid("7d9b38d0-4fb9-4b49-b052-08d6ce33d4e9")), Times.Once);
        }
    }
}