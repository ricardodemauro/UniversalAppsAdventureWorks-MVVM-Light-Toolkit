﻿using AdventureWorksCatalog.Portable.Model;
using System.Threading.Tasks;
using System.Windows.Input;
using AdventureWorksCatalog.ViewModel.Commands;
using AdventureWorksCatalog.ViewModel.Messages;
using GalaSoft.MvvmLight;
using AdventureWorksCatalog.DataSources;
using AdventureWorksCatalog.Interfaces.DataSources;

namespace AdventureWorksCatalog.ViewModel
{
    public class CategoryPageViewModel : ViewModelBase
    {
        private readonly IWindowsDataSource _service;

        public ICommand NavigateHomeCommand { get; private set; }
        public ICommand NavigateToProductCommand { get; private set; }

        private Company _Company;
        public Company Company
        {
            get { return _Company; }
            set { Set(ref this._Company, value); }
        }

        private Category _Category;
        public Category Category
        {
            get { return _Category; }
            set { Set(ref this._Category, value); }
        }

        public CategoryPageViewModel(IWindowsDataSource service)
        {
            _service = service;

            NavigateHomeCommand = new DelegateCommand(OnNavigateHomeCommand);
            NavigateToProductCommand = new DelegateCommand(OnNavigateToProductCommand);
        }

        private void OnNavigateToProductCommand(object parameter)
        {
            MessengerInstance.Send<NavigateMessage>(new NavigateMessage("ProductPage", parameter));
            //PublishMessage(new NavigateMessage("ProductPage", parameter));
        }

        private void OnNavigateHomeCommand(object parameter)
        {
            MessengerInstance.Send<NavigateMessage>(new NavigateMessage("HomePage", parameter));
            //PublishMessage(new NavigateMessage("HomePage", parameter));
        }

        public async Task LoadAsync(int categoryId)
        {
            Category = await _service.GetCategoryAsync(categoryId);
            Company = await _service.GetCompanyAsync();
        }
    }
}
