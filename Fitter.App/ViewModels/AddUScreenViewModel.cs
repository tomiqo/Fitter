using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Fitter.App.Commands;
using Fitter.App.ViewModels.Base;
using Fitter.BL.Messages;
using Fitter.BL.Model;
using Fitter.BL.Repositories.Interfaces;
using Fitter.BL.Services;

namespace Fitter.App.ViewModels
{
    public class AddUScreenViewModel : ViewModelBase
    {
        private readonly IUsersRepository usersRepository;
        private readonly IMediator mediator;
        private UserDetailModel _model;
        public ICommand AddUserCommand { get; set; }
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
        public AddUScreenViewModel(IUsersRepository usersRepository, IMediator mediator)
        {
            this.usersRepository = usersRepository;
            this.mediator = mediator;
            AddUserCommand = new RelayCommand(AddUser, CanAddUser);
            mediator.Register<AddUMessage>(NewUser);
        }

        private bool CanAddUser(object obj)
        {
            PasswordBox pwBox = obj as PasswordBox;
            return Model != null && !string.IsNullOrWhiteSpace(Model.Email)
                   && !string.IsNullOrWhiteSpace(pwBox.Password) 
                   && !string.IsNullOrWhiteSpace(Model.FirstName) 
                   && !string.IsNullOrWhiteSpace(Model.LastName);
        }

        private void AddUser(object obj)
        {
            PasswordBox pwBox = obj as PasswordBox;
            Model.Password = pwBox.Password;
            
            try
            {
                usersRepository.GetByEmail(Model.Email);
                MessageBox.Show("User with that email address already exists!");
            }
            catch 
            {
                usersRepository.Create(Model);
                Model = null;
            }
        }

        private void NewUser(AddUMessage obj)
        {
            Model = new UserDetailModel();
        }
    }
}
