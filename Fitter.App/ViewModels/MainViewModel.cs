using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fitter.App.ViewModels.Base;
using Fitter.BL.Services;

namespace Fitter.App.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private IViewModel _currViewModel;
        private List<IViewModel> _viewModels;
        public List<IViewModel> ViewModels => _viewModels ?? (_viewModels = new List<IViewModel>());

        public IViewModel CurrViewModel
        {
            get => _currViewModel;
            set
            {
                _currViewModel = value;
                OnPropertyChanged("CurrentPageViewModel");
            }
        }

        private void ChangeViewModel(IViewModel viewModel)
        {
            if (!ViewModels.Contains(viewModel))
            {
                ViewModels.Add(viewModel);
            }

            CurrViewModel = ViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }

        private void OnGoToCreateU(object obj)
        {
            ChangeViewModel(ViewModels[0]);
        }

        private void OnGoToCreateT(object obj)
        {
            ChangeViewModel(ViewModels[1]);
        }

        public MainViewModel()
        {
            ViewModels.Add(new AddUScreenViewModel());
            ViewModels.Add(new AddTScreenViewModel());
            CurrViewModel = ViewModels[0];
           /* Mediator.Subscribe("GoToCreateU", OnGoToCreateU);
            Mediator.Subscribe("GoToCreateT", OnGoToCreateT);*/
        }
    }
}
