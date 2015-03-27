using AdventureWorksCatalog.Interfaces.DataSources;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace AdventureWorksCatalog.ViewModel
{
    public class AWViewModelBase : ViewModelBase
    {
        protected INavigationService NavigationService { get; private set; }
        public ICommand NavigateBackCommand { get; private set; }
        protected DataTransferManager DataTransferManager { get; private set; }

        public AWViewModelBase(INavigationService navigationService)
        {
            this.NavigationService = navigationService;
            NavigateBackCommand = new RelayCommand(OnNavigateBackCommand);
        }

        public virtual void Initialize(object parameter)
        {
        }

        private void RegisterDataRequestEvent()
        {
            if (ReferenceEquals(this.DataTransferManager, null))
            {
                DataTransferManager = DataTransferManager.GetForCurrentView();
            }
            DataTransferManager.DataRequested += this.DataTransfer_DataRequested;
            DataTransferManager.TargetApplicationChosen += this.DataTransfer_TargetApplicationChosen;
        }

        private void UnregisterDataRequestEvent()
        {
            if (ReferenceEquals(this.DataTransferManager, null))
            {
                DataTransferManager = DataTransferManager.GetForCurrentView();
            }
            DataTransferManager.DataRequested -= this.DataTransfer_DataRequested;
            DataTransferManager.TargetApplicationChosen -= this.DataTransfer_TargetApplicationChosen;
        }

        private void OnNavigateBackCommand()
        {
            this.NavigationService.GoBack();
        }

        public virtual void DataTransfer_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        { }

        public virtual void DataTransfer_TargetApplicationChosen(DataTransferManager sender, TargetApplicationChosenEventArgs args)
        { }
    }
}
