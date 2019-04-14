using Fitter.BL.Factories;
using Fitter.BL.Mapper;
using Fitter.BL.Repositories;
using Fitter.BL.Repositories.Interfaces;
using Fitter.BL.Services;

namespace Fitter.App.ViewModels
{
    public class ViewModelLocator
    {
        private readonly IMediator _mediator;
        private readonly IDbContextFactory _dbContextFactory;
        private readonly ICommentsRepository _commentsRepository;
        private readonly IPostsRepository _postsRepository;
        private readonly IUsersRepository _userRepository;
        private readonly ITeamsRepository _teamsRepository;

        public HomeScreenViewModel HomeScreenViewModel => new HomeScreenViewModel(_userRepository,_mediator);
        public AddUScreenViewModel AddUScreenViewModel => new AddUScreenViewModel(_userRepository, _mediator);
        public AddTScreenViewModel AddTScreenViewModel => new AddTScreenViewModel(_teamsRepository, _mediator, _userRepository);
        public LoginPanelViewModel LoginPanelViewModel => new LoginPanelViewModel(_userRepository, _mediator);
        public AppPanelViewModel AppPanelViewModel => new AppPanelViewModel(_teamsRepository, _mediator, _userRepository);
        public TeamScreenViewModel TeamScreenViewModel => new TeamScreenViewModel(_teamsRepository, _userRepository,_mediator, _postsRepository, _commentsRepository);
        public AddUserToTeamViewModel AddUserToTeamViewModel => new AddUserToTeamViewModel(_teamsRepository, _mediator, _userRepository);
        public UserInfoViewModel UserInfoViewModel => new UserInfoViewModel(_mediator, _userRepository, _teamsRepository, _commentsRepository, _postsRepository);
        public TeamInfoViewModel TeamInfoViewModel => new TeamInfoViewModel(_mediator, _teamsRepository, _userRepository);
        public RemoveUserFromTeamViewModel RemoveUserFromTeamViewModel => new RemoveUserFromTeamViewModel(_teamsRepository, _mediator, _userRepository);
        public ViewModelLocator()
       {
           _mediator = new Mediator();
           _dbContextFactory = new DbContextFactory();
           _commentsRepository = new CommentsRepository(_dbContextFactory, new Mapper());
           _postsRepository = new PostsRepository(_dbContextFactory, new Mapper());
           _userRepository = new UsersRepository(_dbContextFactory, new Mapper());
           _teamsRepository = new TeamsRepository(_dbContextFactory, new Mapper());
       }
    }
}
