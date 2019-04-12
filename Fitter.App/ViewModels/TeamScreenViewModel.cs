﻿using System;
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
    public class TeamScreenViewModel : ViewModelBase
    {
        private readonly IMediator mediator;
        private readonly IUsersRepository usersRepository;
        private readonly ITeamsRepository teamsRepository;
        private readonly IPostsRepository postsRepository;
        private readonly ICommentsRepository commentsRepository;
        private TeamDetailModel _teamDetailModel;
        private PostModel _postModel;
        private UserDetailModel _userDetailModel;
        public ICommand CreatePostCommand { get; set; }

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

        public PostModel PostModel
        {
            get => _postModel;
            set
            {
                if (Equals(value, PostModel))
                {
                    return;
                }
                _postModel = value;
                OnPropertyChanged();
            }
        }

        public UserDetailModel UserDetailModel
        {
            get => _userDetailModel;
            set
            {
                if (Equals(value,UserDetailModel))
                    return;
                _userDetailModel = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<PostModel> _posts;

        public ObservableCollection<PostModel> Posts
        {
            get => _posts;
            set
            {
                if (Equals(value, Posts))
                    return;

                _posts = value;
                OnPropertyChanged();
            }
        }

        public TeamScreenViewModel(ITeamsRepository teamsRepository, IUsersRepository usersRepository,
            IMediator mediator, IPostsRepository postsRepository, ICommentsRepository commentsRepository)
        {
            this.mediator = mediator;
            this.teamsRepository = teamsRepository;
            this.usersRepository = usersRepository;
            this.postsRepository = postsRepository;
            this.commentsRepository = commentsRepository;
            CreatePostCommand = new RelayCommand(CreatePost, CanCreatePost);
            mediator.Register<TeamSelectedMessage>(SelectedTeam);
            mediator.Register<GoToHomeMessage>(GoToHome);
            mediator.Register<UserLoginMessage>(CreateAdmin);
        }

        private void CreateAdmin(UserLoginMessage obj)
        {
            UserDetailModel = usersRepository.GetById(obj.Id);
        }

        private bool CanCreatePost()
        {
            return PostModel != null && !string.IsNullOrWhiteSpace(PostModel.Text)
                && !string.IsNullOrWhiteSpace(PostModel.Title);

        }

        private void CreatePost()
        {
            PostModel.Author = UserDetailModel;
            PostModel.Team = TeamDetailModel;
            postsRepository.Create(PostModel);
            PostModel = null;
        }

        private void GoToHome(GoToHomeMessage obj)
        {
            TeamDetailModel = null;

        }

        private void SelectedTeam(TeamSelectedMessage obj)
        {
            TeamDetailModel = teamsRepository.GetById(obj.Id);
            OnLoad();
        }

        private void OnLoad()
        {
            Posts = new ObservableCollection<PostModel>(postsRepository.GetPostsForTeam(TeamDetailModel.Id));
            PostModel = new PostModel();
        }
    }
}
