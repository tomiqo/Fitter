using System;
using System.Windows;
using System.Windows.Input;
using Fitter.App.Commands;
using Fitter.App.ViewModels.Base;
using Fitter.BL.Messages;
using Fitter.BL.Model;
using Fitter.BL.Repositories;
using Fitter.BL.Repositories.Interfaces;
using Fitter.BL.Services;

namespace Fitter.App.ViewModels
{
    public class AddTScreenViewModel : ViewModelBase
    {
        private readonly ITeamsRepository teamsRepository;
        private readonly IUsersRepository usersRepository;
        private readonly IMediator mediator;
        private UserDetailModel _model;
        private TeamDetailModel _teamModel;

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
        public ICommand AddTeamCommand { get; set; }
        public ICommand GoBackCommand { get; set; }
        public UserDetailModel Model
        {
            get => _model;
            set
            {
                if (Equals(value, Model))
                    return;

                _model = value;
                OnPropertyChanged();
            }
        }
        public AddTScreenViewModel(ITeamsRepository teamsRepository, IMediator mediator, IUsersRepository usersRepository)
        {
            this.mediator = mediator;
            this.teamsRepository = teamsRepository;
            this.usersRepository = usersRepository;
            GoBackCommand = new RelayCommand(GoBack);
            AddTeamCommand = new RelayCommand(AddTeam, CanAddTeam);
            mediator.Register<GoToHomeMessage>(GoToHome);
            mediator.Register<AddTMessage>(NewTeam);
        }

        private void GoToHome(GoToHomeMessage obj)
        {
            Model = null;
        }

        private void GoBack()
        {
            Model = null;
        }

        private void AddTeam()
        {
            if (teamsRepository.Exists(TeamModel.Name))
            {
                MessageBox.Show("Team with typed name already exists!");
            }
            else
            {
                TeamModel = teamsRepository.Create(TeamModel);
                TeamModel.Admin = Model;
                teamsRepository.AddUserToTeam(TeamModel.Admin, TeamModel.Id);
                teamsRepository.Update(TeamModel);
                mediator.Send(new UpdatedTeamsMessage());
                TeamModel = null;
                Model = null;
            }
        }

        private bool CanAddTeam()
        {
            return Model != null && TeamModel != null
                                 && !string.IsNullOrWhiteSpace(TeamModel.Name)
                                 && !string.IsNullOrWhiteSpace(TeamModel.Description);
        }

        private void NewTeam(AddTMessage obj)
        {
            Model = usersRepository.GetById(obj.Id);
            TeamModel = new TeamDetailModel();
        }


    }
}
