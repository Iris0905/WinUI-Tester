using WinUITester.Models;

namespace WinUITester.Interfaces
{
    public interface IConfigurationService
    {
        LanguageConfig GetLanguageConfig();

        bool SetCurrentLanguage(string language);
    }
}
