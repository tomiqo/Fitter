using System;
using System.ComponentModel;
using System.Windows.Input;
using Fitter.App.Commands;
using Fitter.App.ViewModels.Base;
using Fitter.BL.Messages;
using Fitter.BL.Repositories.Interfaces;
using Fitter.BL.Services;

namespace Fitter.App.ViewModels
{
    public class HomeScreenViewModel : ViewModelBase
    {
        private readonly IMediator mediator;

        public ICommand GoToCreateU { get; set; }
        public ICommand GoToCreateT { get; set; }
        private object currentView;

        public object CurrentView
        {
            get { return currentView; }
            set { currentView = value; OnPropertyChanged("CurrentView"); }
        }
        public HomeScreenViewModel(IMediator mediator)
        {
            GoToCreateT = new RelayCommand(GoToT);
            GoToCreateU = new RelayCommand(GoToU);
        }

        private void GoToU()
        {
            CurrentView = new AddUScreenViewModel();
        }

        private void GoToT(object obj)
        {
            CurrentView = new AddTScreenViewModel();
        }

        public new event PropertyChangedEventHandler PropertyChanged;

        private new void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}