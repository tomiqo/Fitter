using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Fitter.App.Commands;
using Fitter.App.ViewModels.Base;
using Fitter.BL.Model;
using Fitter.BL.Repositories.Interfaces;
using Fitter.BL.Services;
using Fitter.BL.Messages;

namespace Fitter.App.ViewModels
{
    public class LoginPanelViewModel : ViewModelBase
    {
        private readonly IUsersRepository usersRepository;
        private readonly IMediator mediator;
        public UserDetailModel Model { get; set; }
        /*private string _email;
        private string _password;

        public string Email
        {
            get => this._email;
            set
            {
                if (string.Equals(this._email, value)) return;
                this._email = value;
                this.OnPropertyChanged();
            }
        }

        public string Password
        {
            get => this._password;
            set
            {
                if (string.Equals(this._password, value)) return;
                this._password = value;
                this.OnPropertyChanged();
            }
        }*/
        public ICommand NewUserCommand { get; set; }
        public LoginPanelViewModel(IUsersRepository usersRepository, IMediator mediator)
        {
            this.usersRepository = usersRepository;
            this.mediator = mediator;
            
            NewUserCommand = new RelayCommand(UserSelect);
        }

        private void UserNew(UserNewMessage userLoginMessage)
        {
            Model = new UserDetailModel();
        }

        private void UserSelected(UserLoginMessage userLoginMessage)
        {
            Model = usersRepository.GetByEmail(userLoginMessage.Email);
        }

        private void UserSelect()
        {
            Model = usersRepository.GetByEmail(Model.Email);
            MessageBox.Show(Model.Email);
        }

    }
}
