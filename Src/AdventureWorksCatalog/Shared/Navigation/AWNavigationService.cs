using AdventureWorksCatalog.Portable.Model;
using AdventureWorksCatalog.ViewModel;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AdventureWorksCatalog.Navigation
{
    public class AWNavigationService : NavigationService
    {
        public override void NavigateTo(string pageKey, object parameter)
        {
            ((Frame)Window.Current.Content).Navigated += OnFrameNavigated;

            base.NavigateTo(pageKey, parameter);
        }

        private void OnFrameNavigated(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            var view = e.Content as FrameworkElement;
            if (view == null)
                return;

            var viewModel = view.DataContext as AWViewModelBase;
            if (viewModel != null)
            {
                if (!(e.NavigationMode == Windows.UI.Xaml.Navigation.NavigationMode.Back &&
                    (((Page)e.Content).NavigationCacheMode == Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled ||
                    (((Page)e.Content).NavigationCacheMode == Windows.UI.Xaml.Navigation.NavigationCacheMode.Required))))
                {
                    viewModel.Initialize(e.Parameter);
                }
            }
        }
    }
}
