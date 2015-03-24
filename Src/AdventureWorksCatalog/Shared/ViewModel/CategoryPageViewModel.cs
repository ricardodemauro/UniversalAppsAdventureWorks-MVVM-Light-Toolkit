using AdventureWorksCatalog.Portable.Model;
using System.Threading.Tasks;
using System.Windows.Input;
using AdventureWorksCatalog.ViewModel.Messages;
using GalaSoft.MvvmLight;
using AdventureWorksCatalog.DataSources;
using System;
using GalaSoft.MvvmLight.Command;
using AdventureWorksCatalog.Interfaces.DataSources;
using GalaSoft.MvvmLight.Views;
using AdventureWorksCatalog.Locator;

namespace AdventureWorksCatalog.ViewModel
{
    public class CategoryPageViewModel : AWViewModelBase
    {
        public ICommand NavigateHomeCommand { get; private set; }
        public ICommand NavigateToProductCommand { get; private set; }
        public IWindowsDataSource DataSource { get; private set; }

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

        public CategoryPageViewModel(IWindowsDataSource datasource, INavigationService navigationService)
            : base(navigationService)
        {
            this.DataSource = datasource;

            NavigateHomeCommand = new RelayCommand(OnNavigateHomeCommand);
            NavigateToProductCommand = new RelayCommand<Product>(OnNavigateToProductCommand);
        }

        private void OnNavigateToProductCommand(Product parameter)
        {
            this.NavigationService.NavigateTo(PagesName.ProductPageName, parameter);
        }

        private void OnNavigateHomeCommand()
        {
            this.NavigationService.NavigateTo(PagesName.HomePageName);
        }

        public async Task LoadAsync(int categoryId)
        {
            Category = await this.DataSource.GetCategoryAsync(categoryId);
            Company = await this.DataSource.GetCompanyAsync();
        }

        public override void Initialize(object parameter)
        {
            Category category = parameter as Category;
            if (category == null)
            {
                throw new ArgumentNullException("parameter", "parameter cannot be null");
            }
            this.LoadAsync(category.Id);

            base.Initialize(parameter);
        }
    }
}
