/*
  In App.xaml:
  <Application.Resources>
      <vm:MvvmViewModelLocator1 xmlns:vm="using:AdventureWorksCatalog"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using AdventureWorksCatalog.DataSources;
using AdventureWorksCatalog.Interfaces.DataSources;
using AdventureWorksCatalog.ViewModel;
using AdventureWorksCatalog.ViewModel.Messages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;

namespace AdventureWorksCatalog.Locator
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<IWindowsDataSource, DataSourceWindows>();
            }
            else
            {
                SimpleIoc.Default.Register<IWindowsDataSource, DataSourceWindows>();
            }

            SimpleIoc.Default.Register<HomePageViewModel>();
            SimpleIoc.Default.Register<CategoryPageViewModel>();
            SimpleIoc.Default.Register<SearchPageViewModel>();
            SimpleIoc.Default.Register<ProductPageViewModel>();

            //SimpleIoc.Default.Register<MessageHandler>();
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public HomePageViewModel HomePage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<HomePageViewModel>();
            }
        }

        public CategoryPageViewModel CategoryPage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CategoryPageViewModel>();
            }
        }

        public ProductPageViewModel ProductPage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ProductPageViewModel>();
            }
        }
    }
}