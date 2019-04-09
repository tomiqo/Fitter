using System.Windows.Input;
using Fitter.App.Commands;
using Fitter.App.ViewModels.Base;
using Fitter.BL.Repositories.Interfaces;
using Fitter.BL.Services;

namespace Fitter.App.ViewModels
{
    public class HomeScreenViewModel : ViewModelBase
    {
        private readonly IMediator mediator;

        public ICommand GoToCreateU { get; set; }
        public ICommand GoToCreateT { get; set; }
        

        
    }
}