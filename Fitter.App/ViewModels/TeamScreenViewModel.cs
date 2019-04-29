using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Fitter.App.Commands;
using Fitter.App.ViewModels.Base;
using Fitter.App.Views;
using Fitter.BL.Extensions;
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
        private CommentModel _commentModel;
        private UserDetailModel _userDetailModel;
        private string _searchString;
        public ICommand AddUserToTeamCommand { get; set; }
        public ICommand DeletePostCommand { get; set; }
        public ICommand DeleteCommentCommand { get; set; }
        public ICommand CreatePostCommand { get; set; }
        public ICommand AddCommentCommand { get; set; }
        public ICommand TeamInfoCommand { get; set; }
        public ICommand SelectedPostCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand TagSelectedCommand { get; set; }

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

        public string SearchString
        {
            get => _searchString;
            set
            {
                if (Equals(value, SearchString))
                {
                    return;
                }
                _searchString = value;
                OnPropertyChanged();
            }
        }
        public PostModel PostModel {
            get => _postModel;
            set {
                if (Equals(value, PostModel))
                {
                    return;
                }
                _postModel = value;
                OnPropertyChanged();
            }
        }

        public CommentModel CommentModel
        {
            get => _commentModel;
            set {
                if (Equals(value, CommentModel))
                {
                    return;
                }
                _commentModel = value;
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

        private ObservableCollection<CommentModel> _comments;

        public ObservableCollection<CommentModel> Comments
        {
            get => _comments;
            set
            {
                if (Equals(value,Comments))
                {
                    return;
                }

                _comments = value;
                OnPropertyChanged();
            }
        }

        private List<PostModel> _searchResults;

        public List<PostModel> SearchResults {
            get => _searchResults;
            set {
                if (Equals(value, SearchResults))
                {
                    return;
                }

                _searchResults = value;
                OnPropertyChanged();
            }
        }

        private List<UserListModel> _usersInTeam;
        public List<UserListModel> UsersInTeam {
            get => _usersInTeam;
            set {
                if (Equals(value, UsersInTeam))
                {
                    return;
                }

                _usersInTeam = value;
                OnPropertyChanged();
            }
        }

        private List<UserDetailModel> _tagList;
        public List<UserDetailModel> TagList {
            get => _tagList;
            set {
                if (Equals(value, TagList))
                {
                    return;
                }

                _tagList = value;
                OnPropertyChanged();
            }
        }

        private string _tags;
        public string Tags {
            get => _tags;
            set {
                if (Equals(value, Tags))
                {
                    return;
                }

                _tags = value;
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
            DeletePostCommand = new RelayCommand<Guid>(CanDeletePost);
            DeleteCommentCommand = new RelayCommand<Guid>(CanDeleteComment);
            AddCommentCommand = new RelayCommand<Guid>(AddNewComment, CanAddComment);
            AddUserToTeamCommand = new RelayCommand(AddUserToTeam);
            TeamInfoCommand = new RelayCommand(ShowInfo);
            SelectedPostCommand = new RelayCommand<PostModel>(SelectedPost);
            SearchCommand = new RelayCommand(Search, CanSearch);
            TagSelectedCommand = new RelayCommand<UserListModel>(AppendTag);

            mediator.Register<TeamSelectedMessage>(SelectedTeam);
            mediator.Register<GoToHomeMessage>(GoToHome);
            mediator.Register<UserLoginMessage>(CreateAdmin);
            mediator.Register<NewPostMessage>(NewPost);
            mediator.Register<NewCommentMessage>(NewComment);
            mediator.Register<SearchMessage>(NewSearch);
        }

        private void AppendTag(UserListModel tag)
        {
            if (!TagList.Select(e => e.FullName).Contains(tag.Fullname))
            {
                TagList.Add(usersRepository.GetById(tag.Id));
                Tags += $"@{tag.Fullname}  ";
            }
        }

        private void CanDeleteComment(Guid id)
        {
            var commentAuthor = Posts.SelectMany(e => e.Comments).First(e => e.Id == id).Author;
            if (_userDetailModel.Id == commentAuthor.Id)
            {
                DeleteComment(id);
            }
            else
            {
                MessageBox.Show("Only the author can delete the comment!", "ERROR",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteComment(Guid id)
        {
            var result = MessageBox.Show("Are you sure you want to delete the comment?", "Delete comment", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                commentsRepository.Delete(id);
                OnLoad();
            }
        }

        private void CanDeletePost(Guid id)
        {
            var postAuthor = Posts.First(k => k.Id == id).Author;
            if (_userDetailModel.Id == postAuthor.Id)
            {
                DeletePost(id);
            }
            else
            {
                MessageBox.Show("Only the author can delete the post!", "ERROR",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeletePost(Guid id)
        {
            var result = MessageBox.Show("Are you sure you want to delete the post?", "Delete post", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                postsRepository.Delete(id);
                OnLoad();
            }
        }

        private bool CanSearch()
        {
            return SearchString != null && !string.IsNullOrWhiteSpace(SearchString);
        }

        private void NewSearch(SearchMessage obj)
        {
            LoadSearchResults();
        }

        private void Search()
        {
            var searchIds = new List<Guid>(postsRepository.SearchInPosts(SearchString, TeamDetailModel.Id));
            searchIds.AddRange(commentsRepository.SearchInComments(SearchString, TeamDetailModel.Id));
            searchIds = searchIds.Distinct().ToList();
            
            SearchResults = new List<PostModel>();
            foreach (var id in searchIds)
            {
                SearchResults.Add(postsRepository.GetById(id));
            }

            mediator.Send(new SearchMessage());
        }
        private void LoadSearchResults()
        {
            Posts = new ObservableCollection<PostModel>(SearchResults);

            foreach (var post in Posts)
            {
                post.Comments = new ObservableCollection<CommentModel>(commentsRepository.GetCommentsForPost(post.Id));
            }
        }

        private void NewComment(NewCommentMessage obj)
        {
            OnLoad();
        }

        private void NewPost(NewPostMessage obj)
        {
            OnLoad();
        }

        private void SelectedPost(PostModel post)
        {
            mediator.Send(new PostSelectedMessage{Id = post.Id});
        }

        private void ShowInfo()
        {
            mediator.Send(new TeamInfoMessage{Id = TeamDetailModel.Id});
        }

        private void AddUserToTeam()
        {
            mediator.Send(new AddUserToTeamMessage{Id = TeamDetailModel.Id});
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

        private bool CanAddComment(Guid id)
        {
            var comment = Posts.First(k => k.Id == id).NewComment;
            return comment != null && !string.IsNullOrWhiteSpace(comment.Text);
        }

        private void CreatePost()
        {
            PostModel.Id = Guid.NewGuid();
            PostModel.Team = TeamDetailModel;
            PostModel.Author = UserDetailModel;
            postsRepository.Create(PostModel);

            postsRepository.TagUsers(TagList, PostModel.Id);

            mediator.Send(new NewPostMessage());
            mediator.Send(new LastActivityMessage{LastPost = PostModel.Title});
        }

        private void AddNewComment(Guid id)
        {
            var comment = new CommentModel
            {
                Id = Guid.NewGuid(),
                Author = _userDetailModel,
                Post = postsRepository.GetById(id),
                Text = Posts.First(k => k.Id == id).NewComment.Text
            };
            
            commentsRepository.Create(comment);
            mediator.Send(new NewCommentMessage());
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
            CommentModel = new CommentModel();
            UsersInTeam = new List<UserListModel>(usersRepository.GetUsersInTeam(TeamDetailModel.Id));
            TagList = new List<UserDetailModel>();
            Tags = "";

            foreach (var post in Posts)
            {
                post.Comments = new ObservableCollection<CommentModel>(commentsRepository.GetCommentsForPost(post.Id));
                post.Tags = new ObservableCollection<UserListModel>(postsRepository.GetTagsForPost(post.Id));
            }
        }
    }
}
