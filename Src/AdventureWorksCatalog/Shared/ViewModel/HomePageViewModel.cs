﻿using AdventureWorksCatalog.Portable.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using AdventureWorksCatalog.ViewModel.Messages;
using GalaSoft.MvvmLight;
using AdventureWorksCatalog.DataSources;
using AdventureWorksCatalog.Interfaces.DataSources;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using AdventureWorksCatalog.Extensions;

namespace AdventureWorksCatalog.ViewModel
{
    public class HomePageViewModel : AWViewModelBase
    {
        public ICommand NavigateToCategoryCommand { get; private set; }
        public ICommand NavigateToProductCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }
        public IWindowsDataSource DataSource { get; private set; }

        private bool _loading;

        public bool Loading
        {
            get
            {
                return _loading;
            }
            set
            {
                Set(ref _loading, value);
            }
        }

        private Company _Company;
        public Company Company
        {
            get { return _Company; }
            set { Set(ref this._Company, value); }
        }

        private ObservableCollection<Category> _Categories;
        public ObservableCollection<Category> Categories
        {
            get { return _Categories; }
            set { Set(ref this._Categories, value); }
        }

        public HomePageViewModel(IWindowsDataSource datasource, INavigationService navigationService)
            : base(navigationService)
        {
            this.NavigateToCategoryCommand = new RelayCommand<Category>(OnNavigateToCategoryCommand);
            this.NavigateToProductCommand = new RelayCommand<Product>(OnNavigateToProductCommand);

            this.RefreshCommand = new RelayCommand(() => RefreshAsync(), () => !this.Loading);

            this.DataSource = datasource;

            this.RefreshAsync();
        }

        private void OnNavigateToProductCommand(Product parameter)
        {
            this.NavigationService.NavigateTo("ProductPage", parameter);
        }

        private void OnNavigateToCategoryCommand(Category parameter)
        {
            this.NavigationService.NavigateTo("CategoryPage", parameter);
        }

        public async Task RefreshAsync()
        {
            this.Loading = true;

#if DEBUG
            await Task.Delay(5000);
#endif
            var categories = await this.DataSource.GetCategoriesAndItemsAsync(4);

            Categories = new ObservableCollection<Category>();
            Categories.AddRange(categories);

            Company = await this.DataSource.GetCompanyAsync();

            this.Loading = false;
        }
    }
}
