using AdventureWorksCatalog.Interfaces.DataSources;
using AdventureWorksCatalog.Locator;
using AdventureWorksCatalog.View;
using AdventureWorksCatalog.ViewModel;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AdventureWorksCatalog
{
    public partial class App
    {
        protected async override void OnSearchActivated(Windows.ApplicationModel.Activation.SearchActivatedEventArgs args)
        {
            var navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
            var dataSource = ServiceLocator.Current.GetInstance<IWindowsDataSource>();

            await dataSource.LoadAsync();

            var previousContent = Window.Current.Content;
            var frame = previousContent as Frame;

            if (frame == null)
            {
                frame = new Frame();
                navigationService.NavigateTo(PagesName.HomePageName);
            }

            if (frame.CurrentSourcePageType == typeof(SearchPage))
            {
                var searchPage = frame.Content as SearchPage;
                AWViewModelBase datacontext = searchPage.DataContext as AWViewModelBase;
                if (datacontext != null)
                {
                    datacontext.Initialize(args.QueryText);
                }
            }
            else
            {
                navigationService.NavigateTo(PagesName.SearchPageName, args.QueryText);
            }

            Window.Current.Content = frame;
            Window.Current.Activate();

            base.OnSearchActivated(args);
        }
    }
}
