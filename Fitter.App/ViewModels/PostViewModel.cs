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
    public class PostViewModel : ViewModelBase
    {
        private readonly IMediator mediator;
        private readonly ICommentsRepository commentsRepository;
        private readonly IPostsRepository postsRepository;
        private PostModel _postModel;
        private ObservableCollection<CommentModel> _comments;

        public ObservableCollection<CommentModel> Comments
        {
            get => _comments;
            set
            {
                if (Equals(value, Comments))
                {
                    return;
                }

                _comments = value;
                OnPropertyChanged();
            }
        }
        public ICommand GoBackCommand { get; set; }
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

        public PostViewModel(IMediator mediator, ICommentsRepository commentsRepository, IPostsRepository postsRepository)
        {
            this.mediator = mediator;
            this.postsRepository = postsRepository;
            this.commentsRepository = commentsRepository;
            GoBackCommand = new RelayCommand(GoBack);
            mediator.Register<PostSelectedMessage>(SelectedPost);
            mediator.Register<GoToHomeMessage>(GoToHome);
        }

        private void GoBack()
        {
            PostModel = null;
        }

        private void GoToHome(GoToHomeMessage obj)
        {
            PostModel = null;
        }

        private void SelectedPost(PostSelectedMessage post)
        {
            PostModel = postsRepository.GetById(post.Id);
            GetComments();
        }

        private void GetComments()
        {
            Comments = new ObservableCollection<CommentModel>(commentsRepository.GetCommentsForPost(PostModel.Id));
        }
    }
}
