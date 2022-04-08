using System;
using System.Diagnostics;
using System.Text.Json;
using Windows.ApplicationModel;
using Microsoft.Extensions.Configuration;
using WinUITester.Interfaces;
using WinUITester.Models;
using Path = System.IO.Path;

namespace WinUITester.Services;

public class ConfigurationService: IConfigurationService
{
    private readonly IConfigurationRoot _configurationRoot;
    private static readonly string BasePath = Package.Current.InstalledLocation.Path;
    private const string FileName = "appsettings.json";

    public ConfigurationService()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(BasePath)
            .AddJsonFile(FileName, optional: false);

        _configurationRoot = builder.Build();
    }

    public LanguageConfig GetLanguageConfig()
    {
        var languageConfig = new LanguageConfig
        {
            CurrentLanguage = _configurationRoot.GetSection("CurrentLanguage")?.Value ?? "en-US",
        };

        return languageConfig;
    }

    public bool SetCurrentLanguage(string language)
    {
        try
        {
            var languageConfig = new LanguageConfig
            {
                CurrentLanguage = language
            };

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            var json = JsonSerializer.Serialize(languageConfig, options);
            System.IO.File.WriteAllText(Path.Combine(BasePath, FileName), json);

            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Exception saving {FileName}: {ex.Message}");
            return false;
        }
    }
}