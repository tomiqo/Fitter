using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Fitter.App.Commands;
using Fitter.App.ViewModels.Base;
using Fitter.BL.Messages;
using Fitter.BL.Model;
using Fitter.BL.Repositories.Interfaces;
using Fitter.BL.Services;

namespace Fitter.App.ViewModels
{
    public class UserInfoViewModel : ViewModelBase
    {
        private readonly IMediator mediator;
        private readonly IUsersRepository usersRepository;
        private readonly ITeamsRepository teamsRepository;
        private UserDetailModel _userModel;
        public ICommand GoBackCommand { get; set; }
        private ObservableCollection<TeamListModel> _teams;

        public ObservableCollection<TeamListModel> Teams
        {
            get { return _teams; }
            set
            {
                if (Equals(value, _teams)) return;
                {
                    _teams = value;
                    OnPropertyChanged();
                }
            }
        }
        public UserDetailModel UserModel
        {
            get { return _userModel; }
            set
            {
                if (Equals(value, UserModel))
                    return;

                _userModel = value;
                OnPropertyChanged();
            }
        }

        public UserInfoViewModel(IMediator mediator,
            IUsersRepository usersRepository, ITeamsRepository teamsRepository)
        {
            this.mediator = mediator;
            this.usersRepository = usersRepository;
            GoBackCommand = new RelayCommand(GoBack);
            mediator.Register<UserInfoMessage>(ShowInfo);
            mediator.Register<GoToHomeMessage>(GoHome);
        }

        private void GoHome(GoToHomeMessage obj)
        {
            UserModel = null;
        }

        private void GoBack()
        {
            UserModel = null;
        }

        private void ShowInfo(UserInfoMessage obj)
        {
            UserModel = usersRepository.GetById(obj.Id);
            OnLoad();
        }

        public void OnLoad()
        {
            Teams = new ObservableCollection<TeamListModel>(teamsRepository.GetTeamsForUser(UserModel.Id));
        }
    }
}
