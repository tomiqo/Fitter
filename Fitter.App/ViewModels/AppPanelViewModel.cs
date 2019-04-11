using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fitter.App.ViewModels.Base;
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
       // public UserDetailModel Model { get; set; }
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

            mediator.Register<UserLoginMessage>(UserLog);
        }

        private void UserLog(UserLoginMessage obj)
        {
            Model = usersRepository.GetById(obj.Id);
        }
    }
}
