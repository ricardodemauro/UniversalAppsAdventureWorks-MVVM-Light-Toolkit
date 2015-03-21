using GalaSoft.MvvmLight.Views;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AdventureWorksCatalog.ViewModel.Messages
{
    public class NavigateServiceHandler : INavigationService
    {
        public string CurrentPageKey
        {
            get
            {
                var frame = ((Frame)Window.Current.Content);
                return frame.CurrentSourcePageType.ToString();
            }
        }

        public void GoBack()
        {
            var frame = ((Frame)Window.Current.Content);

            if (frame.CanGoBack)
            {
                frame.GoBack();
            }
        }

        public void NavigateTo(string pageKey, object parameter)
        {
            var rootFrame = ((Frame)Window.Current.Content);

            var sourcePageType = Type.GetType(string.Format("AdventureWorksCatalog.View.{0}", pageKey));
            rootFrame.Navigate(sourcePageType, parameter);
        }

        public void NavigateTo(string pageKey)
        {
            var rootFrame = ((Frame)Window.Current.Content);

            var sourcePageType = Type.GetType(string.Format("AdventureWorksCatalog.View.{0}", pageKey));
            rootFrame.Navigate(sourcePageType);
        }
    }
}
