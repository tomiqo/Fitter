using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Fitter.App.API;
using Fitter.App.API.Models;
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
        private readonly APIClient _apiClient;
        private readonly IMediator _mediator;
        public UserDetailModelInner Model { get; set; }

        public string Email { get; set; }
        
        public ICommand NewUserCommand { get; set; }
        public LoginPanelViewModel(IMediator mediator, APIClient apiClient)
        {
            _apiClient = apiClient;
            _mediator = mediator;
            
            NewUserCommand = new RelayCommand(LoginUser, CanLogin);
        }

        private async void LoginUser(object obj)
        {
            PasswordBox pwBox = obj as PasswordBox;
            try
            {
                Model = await _apiClient.UserGetByEmailAsync(Email);
                var data = new PasswordComparer();
                if (data.ComparePassword(pwBox.Password, Model.Password))
                {
                    _mediator.Send(new UserLoginMessage { Id = Model.Id });
                }
                else
                {
                    MessageBox.Show("Wrong Password!");
                }
                pwBox.Clear();
            }
            catch (Exception)
            {
                MessageBox.Show("User does not exists!");
            }
            Model = null;
        }
        private bool CanLogin(object obj)
        {
            PasswordBox pwBox = obj as PasswordBox;
            return !string.IsNullOrWhiteSpace(Email)
                   && !string.IsNullOrWhiteSpace(pwBox.Password);
        }

    }
}
