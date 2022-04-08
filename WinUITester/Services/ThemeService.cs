using System;
using Windows.Storage;
using Microsoft.UI.Xaml;
using WinUITester.Interfaces;

namespace WinUITester.Services
{
    public class ThemeService: IThemeService
    {
        private const string SettingsKey = "AppBackgroundRequestedTheme";

        private ElementTheme _theme = ElementTheme.Default;

        public void Initialize()
        {
            _theme = LoadThemeFromSettings();

            SetRequestedTheme();
        }

        public ElementTheme GetTheme()
        {
            return _theme;
        }

        public void SetTheme(ElementTheme theme)
        {
            _theme = theme;

            SetRequestedTheme();
            SaveThemeInSettings(_theme);
        }

        private void SetRequestedTheme()
        {
            ((FrameworkElement)((MainWindow)(Application.Current as App).MainWindow).Content).RequestedTheme = _theme;
        }

        private ElementTheme LoadThemeFromSettings()
        {
            var cacheTheme = ElementTheme.Default;
            var themeName = (string)ApplicationData.Current.LocalSettings.Values[SettingsKey];

            if (!string.IsNullOrEmpty(themeName))
            {
                _ = Enum.TryParse(themeName, out cacheTheme);
            }

            return cacheTheme;
        }

        private static void SaveThemeInSettings(ElementTheme theme)
        {
            ApplicationData.Current.LocalSettings.Values[SettingsKey] = theme.ToString();
        }
    }
}
