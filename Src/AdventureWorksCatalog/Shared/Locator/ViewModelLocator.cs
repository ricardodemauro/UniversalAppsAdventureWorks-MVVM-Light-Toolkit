using AdventureWorksCatalog.DataSources;
using AdventureWorksCatalog.Interfaces.DataSources;
using AdventureWorksCatalog.Navigation;
using AdventureWorksCatalog.View;
using AdventureWorksCatalog.ViewModel;
using AdventureWorksCatalog.ViewModel.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using System;

namespace AdventureWorksCatalog.Locator
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public partial class ViewModelLocator
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
#if !WINDOWS_PHONE_APP
            SimpleIoc.Default.Register<CategoryPageViewModel>();
            SimpleIoc.Default.Register<SearchPageViewModel>();
#endif
            SimpleIoc.Default.Register<ProductPageViewModel>();

            SimpleIoc.Default.Register<INavigationService>(CreateNavigationService, true);
        }

        private static INavigationService CreateNavigationService()
        {
            AWNavigationService service = new AWNavigationService();
            service.Configure(PagesName.HomePageName, PagesName.HomePageType);
            service.Configure(PagesName.ProductPageName, PagesName.ProductPageType);
#if !WINDOWS_PHONE_APP
            service.Configure(PagesName.CategoryPageName, PagesName.CategoryPageType);
            service.Configure(PagesName.SearchPageName, PagesName.SearchPageType);
#endif
            return service;
        }
        //https://marcominerva.wordpress.com/2013/04/22/adding-share-support-to-viewmodel-with-mvvm-light-in-winrt/
        internal static void RegisterSharingService()
        {
            SimpleIoc.Default.Register<SharingService>(createInstanceImmediately: true);
        }

        public static INavigationService NavigationService
        {
            get
            {
                return SimpleIoc.Default.GetInstance<INavigationService>();
            }
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public HomePageViewModel HomePage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<HomePageViewModel>();
            }
        }



        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public ProductPageViewModel ProductPage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ProductPageViewModel>();
            }
        }

#if !WINDOWS_PHONE_APP
        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public CategoryPageViewModel CategoryPage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CategoryPageViewModel>();
            }
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public SearchPageViewModel SearchPage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SearchPageViewModel>();
            }
        }
#endif
    }

    public static class PagesName
    {
        public static readonly Type HomePageType = typeof(HomePage);
        public static readonly string HomePageName = "HomePage";

        public static readonly Type ProductPageType = typeof(ProductPage);
        public static readonly string ProductPageName = "ProductPage";
#if !WINDOWS_PHONE_APP
        public static readonly Type CategoryPageType = typeof(CategoryPage);
        public static readonly string CategoryPageName = "CategoryPage";

        public static readonly Type SearchPageType = typeof(SearchPage);
        public static readonly string SearchPageName = "SearchPage";
#endif
    }
}