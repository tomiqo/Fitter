using Fitter.App.ViewModels.Base;
using Fitter.BL.Messages;
using Fitter.BL.Model;
using Fitter.BL.Services;

namespace Fitter.App.ViewModels
{
    public class AddTScreenViewModel : ViewModelBase
    {
        private readonly IMediator mediator;
        public UserDetailModel Model { get; set; }
        public bool isSet = false;
        public AddTScreenViewModel()
        {
            
        }
        
    }
}
