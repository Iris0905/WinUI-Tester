using System;
using System.Linq;
using Microsoft.UI.Xaml.Controls;
using WinUITester.Views;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUITester
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow
    {
        #region Ctor
        public MainWindow()
        {
            this.InitializeComponent();

            // start from first page
            PagesPanel.SelectedItem = PagesPanel.MenuItems.OfType<NavigationViewItem>().First();
        }
        #endregion Ctor

        #region Methods
        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                ContentFrame.Navigate(typeof(SettingsPage), null, args.RecommendedNavigationTransitionInfo);
            }
            else
            {
                var selectedItem = (NavigationViewItem)args.SelectedItem;
                ContentFrame.Navigate(Type.GetType("WinUITester.Views." + (string)selectedItem.Tag), null,
                    args.RecommendedNavigationTransitionInfo);
            }
        }

        private void MainNavigationView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (ContentFrame.CanGoBack)
            {
                ContentFrame.GoBack();
            }
        }
        #endregion Methods
    }
}
