using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using WinUITester.Interfaces;
using WinUITester.Services;
using WinUITester.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUITester
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App
    {
        public Window MainWindow { get; private set; }

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            ConfigureServices();

            InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            MainWindow = new MainWindow();
            MainWindow.Activate();

            ApplyLanguage();
            LoadTheme();
            ApplyIconAndTitle();
        }

        private static void ConfigureServices()
        {
            Ioc.Default.ConfigureServices(
                new ServiceCollection()
                    .AddSingleton<IConfigurationService, ConfigurationService>() //Services
                    .AddSingleton<IResourceService, ResourceService>()
                    .AddSingleton<IThemeService, ThemeService>()
                    .AddTransient<FirstPageViewModel>() //ViewModels
                    .AddTransient<SecondPageViewModel>()
                    .AddTransient<SettingsPageViewModel>()
                    .BuildServiceProvider());
        }

        private static void LoadTheme()
        {
            var themeService = Ioc.Default.GetService<IThemeService>();
            themeService?.Initialize();
        }

        private static void ApplyLanguage()
        {
            var configurationService = Ioc.Default.GetService<IConfigurationService>();
            if (configurationService != null)
            {
                var language = configurationService.GetLanguageConfig().CurrentLanguage;
                Windows.ApplicationModel.Resources.Core.ResourceContext.SetGlobalQualifierValue("Language", language);
            }
        }

        private void ApplyIconAndTitle()
        {
            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(MainWindow);

            var windowId = Win32Interop.GetWindowIdFromWindow(hWnd);

            var appWindow = AppWindow.GetFromWindowId(windowId);

            appWindow.SetIcon("Assets/status.ico");
            appWindow.Title = "WinUI Tester";
        }
    }
}
