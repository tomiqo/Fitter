using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Fitter.App.Commands;
using Fitter.App.ViewModels.Base;
using Fitter.BL.Extensions;
using Fitter.BL.Messages;
using Fitter.BL.Model;
using Fitter.BL.Repositories.Interfaces;
using Fitter.BL.Services;

namespace Fitter.App.ViewModels
{
    public class AppPanelViewModel : ViewModelBase
    {
        private readonly ITeamsRepository teamsRepository;
        private readonly IUsersRepository usersRepository;
        private readonly IMediator mediator;

        private UserDetailModel _model;
        private ObservableCollection<TeamListModel> _teams;

        public ObservableCollection<TeamListModel> Teams
        {
            get { return _teams; }
            set
            {
                if (Equals(value,_teams))return;
                {
                    _teams = value;
                    OnPropertyChanged();
                }
            }
        }
        public ICommand TeamSelectedCommand { get; set; }
        public ICommand UserInfoCommand { get; set; }
        public ICommand GoToHomeCommand { get; set; }
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
        public AppPanelViewModel(ITeamsRepository teamsRepository, IMediator mediator, IUsersRepository usersRepository)
        {
            this.teamsRepository = teamsRepository;
            this.usersRepository = usersRepository;
            this.mediator = mediator;
            TeamSelectedCommand = new RelayCommand<TeamListModel>(TeamSelected);
            UserInfoCommand = new RelayCommand(UserInfo);
            GoToHomeCommand = new RelayCommand(GoToHome);
            mediator.Register<UserLoginMessage>(UserLog);
            mediator.Register<LogOutMessage>(UserLogOut);
        }

        private void UserInfo()
        {
            mediator.Send(new UserInfoMessage{Id = Model.Id});
        }

        private void GoToHome()
        {
            mediator.Send(new GoToHomeMessage());
        }

        private void UserLogOut(LogOutMessage obj)
        {
            Model = null;
        }

        private void TeamSelected(TeamListModel team)
        {
            mediator.Send(new TeamSelectedMessage{Id = team.Id});
        }

        private void UserLog(UserLoginMessage obj)
        {
            Model = usersRepository.GetById(obj.Id);
            OnLoad();
        }

        public void OnLoad()
        {
            Teams = new ObservableCollection<TeamListModel>(teamsRepository.GetTeamsForUser(Model.Id));
        }
    }
}
