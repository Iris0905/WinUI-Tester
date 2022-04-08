using System.Diagnostics;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using WinUITester.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUITester.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        public SettingsPageViewModel ViewModel;

        public SettingsPage()
        {
            ViewModel = Ioc.Default.GetService<SettingsPageViewModel>();

            this.InitializeComponent();
        }

        private void List_DragOver(object sender, DragEventArgs e)
        {
            Debug.WriteLine("Drag over");
        }

        private void List_Drop(object sender, DragEventArgs e)
        {
            Debug.WriteLine("Drop");
        }
    }
}
