using Fitter.BL.Factories;
using Fitter.BL.Mapper;
using Fitter.BL.Repositories;
using Fitter.BL.Repositories.Interfaces;
using Fitter.BL.Services;

namespace Fitter.App.ViewModels
{
    public class ViewModelLocator
    {
        private readonly IMediator mediator;
        private readonly IDbContextFactory dbContextFactory;
        private readonly ICommentsRepository commentsRepository;
        private readonly IPostsRepository postsRepository;
        private readonly IUsersRepository userRepository;
        private readonly ITeamsRepository teamsRepository;

        public HomeScreenViewModel HomeScreenViewModel => new HomeScreenViewModel(userRepository, mediator, teamsRepository);
        public ViewModelLocator()
       {
           mediator = new Mediator();
           dbContextFactory = new DbContextFactory();
           commentsRepository = new CommentsRepository(dbContextFactory, new Mapper());
           postsRepository = new PostsRepository(dbContextFactory, new Mapper());
           userRepository = new UsersRepository(dbContextFactory, new Mapper());
           teamsRepository = new TeamsRepository(dbContextFactory, new Mapper());
       }
    }
}
