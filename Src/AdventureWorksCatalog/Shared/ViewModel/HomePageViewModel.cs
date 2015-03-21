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
using Windows.Foundation;

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

        private string _lastPosition = "Click somewhere";
        public string LastPosition
        {
            get
            {
                return _lastPosition;
            }
            set
            {
                Set(ref _lastPosition, value);
            }
        }

        private RelayCommand<Point> _showPositionCommand;

        public RelayCommand<Point> Tap
        {
            get
            {
                return _showPositionCommand
                        ?? (_showPositionCommand = new RelayCommand<Point>(
                            point =>
                            {
                                LastPosition = string.Format("{0:N1}, {1:N1}", point.X, point.Y);
                            }));
            }
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
