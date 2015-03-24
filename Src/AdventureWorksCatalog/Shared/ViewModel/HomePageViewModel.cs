using AdventureWorksCatalog.Portable.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using AdventureWorksCatalog.ViewModel.Commands;
using AdventureWorksCatalog.ViewModel.Messages;
using GalaSoft.MvvmLight;
using AdventureWorksCatalog.DataSources;
using AdventureWorksCatalog.Interfaces.DataSources;
using GalaSoft.MvvmLight.Command;

namespace AdventureWorksCatalog.ViewModel
{
    public class HomePageViewModel : ViewModelBase
    {
        public ICommand NavigateToCategoryCommand { get; private set; }
        public ICommand NavigateToProductCommand { get; private set; }

        public IWindowsDataSource DataSource { get; private set; }

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

        public HomePageViewModel(IWindowsDataSource datasource)
        {
            NavigateToCategoryCommand = new RelayCommand<Category>(OnNavigateToCategoryCommand);
            NavigateToProductCommand = new RelayCommand<Product>(OnNavigateToProductCommand);
            DataSource = datasource;

            this.LoadAsync();
        }

        private void OnNavigateToProductCommand(Product parameter)
        {
            MessengerInstance.Send<NavigateMessage>(new NavigateMessage("ProductPage", parameter));
        }

        private void OnNavigateToCategoryCommand(Category parameter)
        {
            MessengerInstance.Send<NavigateMessage>(new NavigateMessage("CategoryPage", parameter));
        }

        public async Task LoadAsync()
        {
            var categories = await this.DataSource.GetCategoriesAndItemsAsync(4);
            Categories = new ObservableCollection<Category>();
            foreach (var category in categories)
            {
                Categories.Add(category);
            }
            Company = await this.DataSource.GetCompanyAsync();
        }
    }
}
