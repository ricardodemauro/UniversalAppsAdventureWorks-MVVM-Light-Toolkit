using AdventureWorksCatalog.Interfaces.DataSources;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace AdventureWorksCatalog.ViewModel
{
    public class AWViewModelBase : ViewModelBase
    {
        protected INavigationService NavigationService { get; private set; }
        public ICommand NavigateBackCommand { get; private set; }

        public AWViewModelBase(INavigationService navigationService)
        {
            this.NavigationService = navigationService;
            NavigateBackCommand = new RelayCommand(OnNavigateBackCommand);
        }

        public virtual void Initialize(object parameter)
        { }

        private void OnNavigateBackCommand()
        {
            this.NavigationService.GoBack();
        }
    }
}
