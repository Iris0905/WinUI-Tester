using Microsoft.UI.Xaml;

namespace WinUITester.Interfaces
{
    public interface IThemeService
    {
        void Initialize();
        ElementTheme GetTheme();
        void SetTheme(ElementTheme theme);
    }
}

