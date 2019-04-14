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
        private readonly IPostsRepository postsRepository;
        private readonly ICommentsRepository commentsRepository;
        private UserDetailModel _userModel;
        private string _lastActivity;
        public ICommand GoBackCommand { get; set; }
        public ICommand TeamSelectedCommand { get; set; }

        public string LastActivity
        {
            get { return _lastActivity; }
            set
            {
                if (Equals(value,_lastActivity)) return;
                {
                    _lastActivity = value;
                    OnPropertyChanged();
                }
            }
        }
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
            IUsersRepository usersRepository, ITeamsRepository teamsRepository, ICommentsRepository commentsRepository,
            IPostsRepository postsRepository)
        {
            this.mediator = mediator;
            this.usersRepository = usersRepository;
            this.teamsRepository = teamsRepository;
            TeamSelectedCommand = new RelayCommand<TeamListModel>(TeamSelected);
            GoBackCommand = new RelayCommand(GoBack);
            mediator.Register<UserInfoMessage>(ShowInfo);
            mediator.Register<GoToHomeMessage>(GoHome);
            mediator.Register<LastActivityMessage>(Activity);
        }

        private void TeamSelected(TeamListModel team)
        {
            mediator.Send(new TeamSelectedMessage { Id = team.Id });
            UserModel = null;
        }

        private void Activity(LastActivityMessage obj)
        {
            if (obj.LastComment == null)
            {
                LastActivity = "Creating Post " + obj.LastPost;
            }
            else
            {
                LastActivity = "Commenting on Post " + obj.LastPost + " - " + obj.LastComment;
            }
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
            if (LastActivity == null)
            {
                LastActivity = "-";
            }
        }
    }
}
