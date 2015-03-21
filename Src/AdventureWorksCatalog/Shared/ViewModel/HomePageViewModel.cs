using AdventureWorksCatalog.Portable.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using AdventureWorksCatalog.ViewModel.Commands;
using AdventureWorksCatalog.ViewModel.Messages;
using GalaSoft.MvvmLight;
using AdventureWorksCatalog.DataSources;
using GalaSoft.MvvmLight.Views;
using AdventureWorksCatalog.Interfaces.DataSources;
using GalaSoft.MvvmLight.Command;

namespace AdventureWorksCatalog.ViewModel
{
    public class HomePageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IWindowsDataSource _service;

        public ICommand NavigateToCategoryCommand { get; private set; }
        public RelayCommand<Product> NavigateToProductCommand { get; private set; }

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

        public HomePageViewModel(IWindowsDataSource service, INavigationService navigationService)
        {
            _service = service;
            _navigationService = navigationService;

            NavigateToCategoryCommand = new DelegateCommand(OnNavigateToCategoryCommand);
            NavigateToProductCommand = new RelayCommand<Product>(OnNavigateToProductCommand);

            this.LoadAsync();
        }

        private void OnNavigateToProductCommand(Product parameter)
        {
            _navigationService.NavigateTo("ProductPage", parameter);
        }

        private void OnNavigateToCategoryCommand(object parameter)
        {
            _navigationService.NavigateTo("CategoryPage", parameter);
        }

        public async Task LoadAsync()
        {
            var categories = await _service.GetCategoriesAndItemsAsync(4);
            Categories = new ObservableCollection<Category>();
            foreach (var category in categories)
            {
                Categories.Add(category);
            }
            Company = await _service.GetCompanyAsync();
        }
    }
}
