using System;
using System.ComponentModel;
using System.Windows.Input;
using Fitter.App.Commands;
using Fitter.App.ViewModels.Base;
using Fitter.BL.Messages;
using Fitter.BL.Model;
using Fitter.BL.Repositories.Interfaces;
using Fitter.BL.Services;

namespace Fitter.App.ViewModels
{
    public class HomeScreenViewModel : ViewModelBase
    {
        private readonly IMediator mediator;
        private readonly IUsersRepository usersRepository;
        private UserDetailModel _model;

        public ICommand GoToCreateT { get; set; }
        public ICommand GoToCreateU { get; set; }
        public ICommand LogOut { get; set; }

        public UserDetailModel Model
        {
            get { return _model; }
            set
            {
                if (Equals(value, Model))
                    return;

                _model = value;
                OnPropertyChanged();
            }
        }

        public HomeScreenViewModel(IUsersRepository usersRepository, IMediator mediator)
        {
            this.mediator = mediator;
            this.usersRepository = usersRepository;
            GoToCreateT = new RelayCommand(NewTeam);
            GoToCreateU = new RelayCommand(NewUser);
            LogOut = new RelayCommand(LogOutUser);
            mediator.Register<UserLoginMessage>(UserLogin);
        }

        private void LogOutUser()
        {
            Model = null;
            mediator.Send(new LogOutMessage());
        }

        private void NewUser()
        {
            mediator.Send(new AddUMessage());
        }

        private void NewTeam()
        {
            mediator.Send(new AddTMessage{Id = Model.Id});
        }

        private void UserLogin(UserLoginMessage obj)
        {
            Model = usersRepository.GetById(obj.Id);
        }
    }
}