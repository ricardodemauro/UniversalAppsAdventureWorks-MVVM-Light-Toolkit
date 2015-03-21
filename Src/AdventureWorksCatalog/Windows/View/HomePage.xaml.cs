using AdventureWorksCatalog.Common;
using AdventureWorksCatalog.Portable.Model;
using AdventureWorksCatalog.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace AdventureWorksCatalog.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            this.InitializeComponent();
        }

        private void CategoryBorder_Tapped(object sender, TappedRoutedEventArgs e)
        {
            // This code is used to trigger the command even though
            // Blend Behaviors (and thus EventToCommand) are not available
            // for Windows 8.

            var element = (FrameworkElement)sender;
            var item = element.DataContext as Product;

            if (item != null)
            {
                var vm = (HomePageViewModel)DataContext;
                vm.NavigateToProductCommand.Execute(item);
            }
        }

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void Grid_PointerPressed(object sender, PointerRoutedEventArgs e)
        {

        }
    }
}
