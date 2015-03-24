using AdventureWorksCatalog.DataSources;
using AdventureWorksCatalog.Interfaces.DataSources;
using AdventureWorksCatalog.Portable.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using AdventureWorksCatalog.Extensions;

namespace AdventureWorksCatalog.ViewModel
{
    public class SearchPageViewModel : AWViewModelBase
    {
        public ICommand NavigateToCategoryCommand { get; private set; }
        public ICommand NavigateHomeCommand { get; private set; }
        public ICommand NavigateToProductCommand { get; private set; }
        public IWindowsDataSource DataSource { get; private set; }
        public INavigationService NavigationService { get; private set; }

        private string _Query;
        public string Query
        {
            get { return _Query; }
            set { Set(ref this._Query, value); }
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

        public SearchPageViewModel(IWindowsDataSource datasource, INavigationService navigationService)
            : base(navigationService)
        {
            this.DataSource = datasource;

            NavigateHomeCommand = new RelayCommand(OnNavigateHomeCommand);
            NavigateToCategoryCommand = new RelayCommand<Category>(OnNavigateToCategoryCommand);
            NavigateToProductCommand = new RelayCommand<Product>(OnNavigateToProductCommand);
        }

        private void OnNavigateHomeCommand()
        {
            this.NavigationService.NavigateTo("HomePage");
        }

        private void OnNavigateToProductCommand(Product parameter)
        {
            this.NavigationService.NavigateTo("ProductPage", parameter);
        }

        private void OnNavigateToCategoryCommand(Category parameter)
        {
            this.NavigationService.NavigateTo("CategoryPage", parameter);
        }

        public async Task LoadAsync(string query)
        {
            Query = query;

            var categories = await this.DataSource.SearchCategoriesAndItemsAsync(query);
            Categories = new ObservableCollection<Category>();
            this.Categories.AddRange(categories);

            Company = await this.DataSource.GetCompanyAsync();
        }
    }
}
