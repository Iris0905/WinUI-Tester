using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using DevExpress.WinUI.Charts;
using Microsoft.UI.Xaml;
using WinUITester.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUITester.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FirstPage : Page
    {
        public FirstPageViewModel ViewModel;

        public FirstPage()
        {
            this.InitializeComponent();
            ViewModel = Ioc.Default.GetService<FirstPageViewModel>();

            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            chart.ActualAxisX.VisualRange = new VisualAxisRange
            {
                StartValueInternal = FirstPageViewModel.PointsCountPerSeries / 10,
                EndValueInternal = FirstPageViewModel.PointsCountPerSeries / 5
            };
        }
    }
}
