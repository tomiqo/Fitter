using Fitter.App.ViewModels.Base;

namespace Fitter.App.ViewModels
{
    public class HomeScreenViewModel : ViewModelBase
    {
        private readonly IUsersRepository usersRepository;
        private readonly IMediator mediator;
        private readonly ITeamsRepository teamsRepository;

        public HomeScreenViewModel(IUsersRepository usersRepository, IMediator mediator,
            ITeamsRepository teamsRepository)
        {

        }
    }
}