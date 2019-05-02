using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Fitter.App.API;
using Fitter.App.API.Models;
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
        private readonly IMediator _mediator;
        private readonly APIClient _apiClient;
        private TeamDetailModelInner _teamDetailModel;
        private ObservableCollection<UserListModelInner> _users;

        public ObservableCollection<UserListModelInner> Users
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
        public TeamDetailModelInner TeamDetailModel
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

        public TeamInfoViewModel(IMediator mediator, APIClient apiClient)
        {
            this._mediator = mediator;
            _apiClient = apiClient;

            GoBackCommand = new RelayCommand(GoBack);
            GoToUserCommand = new RelayCommand<UserListModelInner>(GoToUser);
            AddUserToTeamCommand = new RelayCommand(AddUser);
            RemoveUserFromTeamCommand = new RelayCommand(RemoveUser);
            mediator.Register<TeamInfoMessage>(ShowTeamInfo);
            mediator.Register<GoToHomeMessage>(GoHome);
        }

        private void RemoveUser()
        {
            _mediator.Send(new RemoveUserFromTeamMessage{Id = TeamDetailModel.Id});
            TeamDetailModel = null;
        }

        private void AddUser()
        {
            _mediator.Send(new AddUserToTeamMessage { Id = TeamDetailModel.Id });
            TeamDetailModel = null;
        }

        private void GoToUser(UserListModelInner user)
        {
            _mediator.Send(new UserInfoMessage{Id = user.Id});
        }

        private void GoBack()
        {
            TeamDetailModel = null;
        }

        private void GoHome(GoToHomeMessage obj)
        {
            TeamDetailModel = null;
        }

        private async void ShowTeamInfo(TeamInfoMessage obj)
        {
            TeamDetailModel = await _apiClient.GetTeamByIdAsync(obj.Id);
            Users = new ObservableCollection<UserListModelInner>(await _apiClient.UsersInTeamAsync(TeamDetailModel.Id));
        }
    }
}
