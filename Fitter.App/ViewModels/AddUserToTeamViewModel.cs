﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Fitter.App.Commands;
using Fitter.App.ViewModels.Base;
using Fitter.BL.Messages;
using Fitter.BL.Model;
using Fitter.BL.Repositories.Interfaces;
using Fitter.BL.Services;

namespace Fitter.App.ViewModels
{
    public class AddUserToTeamViewModel : ViewModelBase
    {
        private readonly IMediator mediator;
        private readonly ITeamsRepository teamsRepository;
        private readonly IUsersRepository usersRepository;
        private TeamDetailModel _teamModel;

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
        public ICommand AddUserCommand { get; set; }

        public TeamDetailModel TeamModel
        {
            get => _teamModel;
            set
            {
                if (Equals(value, TeamModel))
                {
                    return;
                }

                _teamModel = value;
                OnPropertyChanged();
            }
        }

        public AddUserToTeamViewModel(ITeamsRepository teamsRepository, IMediator mediator,
            IUsersRepository usersRepository)
        {
            this.mediator = mediator;
            this.teamsRepository = teamsRepository;
            this.usersRepository = usersRepository;
            GoBackCommand = new RelayCommand(GoBack);
            AddUserCommand = new RelayCommand<UserListModel>(AddUserToTeam);
            mediator.Register<AddUserToTeamMessage>(AddUser);
            mediator.Register<GoToHomeMessage>(GoToHome);
        }

        private void AddUserToTeam(UserListModel obj)
        {
            try
            {
                UserDetailModel user = usersRepository.GetById(obj.Id);
                teamsRepository.AddUserToTeam(user, TeamModel.Id);
                TeamModel = null;
            }
            catch (Exception)
            {
                MessageBox.Show("User does not exists!");
            }
        }

        private void GoToHome(GoToHomeMessage obj)
        {
            TeamModel = null;
        }

        private void GoBack()
        {
            TeamModel = null;
        }

        private void AddUser(AddUserToTeamMessage obj)
        {
            TeamModel = teamsRepository.GetById(obj.Id);
            Users = new ObservableCollection<UserListModel>(usersRepository.GetUsersNotInTeam(TeamModel.Id));
        }
    }
}
