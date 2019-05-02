using System;
using Fitter.App.API;
using Fitter.App.ViewModels;
using Fitter.BL.Model;
using Fitter.BL.Repositories.Interfaces;
using Fitter.BL.Services;
using Moq;
using Xunit;

namespace Fitter.App.Test
{
    public class AddUScreenViewModelTest
    {
        private readonly Mock<IUsersRepository> _usersRepositoryMock;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly AddUScreenViewModel _addUScreenViewModelSUT;

        public AddUScreenViewModelTest()
        {
            this._usersRepositoryMock = new Mock<IUsersRepository>();
            this._mediatorMock = new Mock<IMediator>(){CallBase = true};

            _usersRepositoryMock.Setup(repository => repository.GetById(Guid.Empty))
                .Returns((() => new UserDetailModel()));

            this._addUScreenViewModelSUT = new AddUScreenViewModel(_mediatorMock.Object, new APIClient("https://fitterswaggerapi.azurewebsites.net/index.html"));
        }

        [Fact]
        public void Load_Calls_GetById()
        {
            _addUScreenViewModelSUT.Load();

            _usersRepositoryMock.Verify(repository => repository.GetById(new Guid("7d9b38d0-4fb9-4b49-b052-08d6ce33d4e9")), Times.Once);
        }
    }
}