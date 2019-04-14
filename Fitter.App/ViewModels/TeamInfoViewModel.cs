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
    public class TeamInfoViewModel : ViewModelBase
    {
        private readonly IMediator mediator;
        private readonly ITeamsRepository teamsRepository;
        private readonly IUsersRepository usersRepository;
        private TeamDetailModel _teamDetailModel;
        private ObservableCollection<UserListModel> _users;

        public ObservableCollection<UserListModel> Users
        {
            get => _users;
            set
            {
                if (Equals(value, Users))
                    return;

                _users = value;
                OnPropertyChanged();
            }
        }
        public ICommand GoBackCommand { get; set; }
        public ICommand GoToUserCommand { get; set; }
        public ICommand AddUserToTeamCommand { get; set; }
        public ICommand RemoveUserFromTeamCommand { get; set; }
        public TeamDetailModel TeamDetailModel
        {
            get => _teamDetailModel;
            set
            {
                if (Equals(value, TeamDetailModel))
                    return;

                _teamDetailModel = value;
                OnPropertyChanged();
            }
        }

        public TeamInfoViewModel(IMediator mediator, ITeamsRepository teamsRepository, IUsersRepository usersRepository)
        {
            this.mediator = mediator;
            this.teamsRepository = teamsRepository;
            this.usersRepository = usersRepository;
            GoBackCommand = new RelayCommand(GoBack);
            GoToUserCommand = new RelayCommand<UserListModel>(GoToUser);
            AddUserToTeamCommand = new RelayCommand(AddUser);
            RemoveUserFromTeamCommand = new RelayCommand(RemoveUser);
            mediator.Register<TeamInfoMessage>(ShowTeamInfo);
            mediator.Register<GoToHomeMessage>(GoHome);
        }

        private void RemoveUser()
        {
            mediator.Send(new RemoveUserFromTeamMessage{Id = TeamDetailModel.Id});
            TeamDetailModel = null;
        }

        private void AddUser()
        {
            mediator.Send(new AddUserToTeamMessage { Id = TeamDetailModel.Id });
            TeamDetailModel = null;
        }

        private void GoToUser(UserListModel user)
        {
            mediator.Send(new UserInfoMessage{Id = user.Id});
        }

        private void GoBack()
        {
            TeamDetailModel = null;
        }

        private void GoHome(GoToHomeMessage obj)
        {
            TeamDetailModel = null;
        }

        private void ShowTeamInfo(TeamInfoMessage obj)
        {
            TeamDetailModel = teamsRepository.GetById(obj.Id);
            Users = new ObservableCollection<UserListModel>(usersRepository.GetUsersInTeam(TeamDetailModel.Id));
        }
    }
}
