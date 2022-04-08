using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using WinUITester.Enums;
using WinUITester.Interfaces;
using WinUITester.Models;

namespace WinUITester.ViewModels
{
    public class SettingsPageViewModel: ViewModelBase
    {
        #region Fields
        private readonly IThemeService _themeService;
        private readonly IConfigurationService _configurationService;
        private Instrument _selectedInstrument;
        private string _selectedLanguage;
        private bool _isDarkThemeOn;
        private bool _selectedLanguageChanged;
        #endregion Fields

        #region Properties
        public ObservableCollection<Instrument> Instruments { get; }

        public Instrument SelectedInstrument
        {
            get => _selectedInstrument;
            set
            {
                if (_selectedInstrument == value) return;
                _selectedInstrument = value;
                OnPropertyChanged();
            }
        }

        public List<string> Languages { get; }

        public string SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                if (_selectedLanguage == value) return;
                _selectedLanguage = value;
                OnPropertyChanged();

                _selectedLanguageChanged = true;
                ((AsyncRelayCommand) SaveLanguageCommand)?.NotifyCanExecuteChanged();
            }
        }

        public bool IsDarkThemeOn
        {
            get => _isDarkThemeOn;
            set
            {
                if (_isDarkThemeOn == value) return;
                _isDarkThemeOn = value;
                OnPropertyChanged();

                UpdateTheme();
            }
        }
        #endregion Properties

        #region Commands
        public ICommand SaveLanguageCommand { get; }
        #endregion Commands

        #region Ctor
        public SettingsPageViewModel(IResourceService resourceService, IConfigurationService configurationService, IThemeService themeService)
        {
            _themeService = themeService;
            _configurationService = configurationService;

            Instruments = new ObservableCollection<Instrument>(Enumerable.Range(1, 16).Select(i => new Instrument
            {
                Name = $"Instrument{i}",
                State = GetInstrumentState(i),
                Description = $"This is the description of Instrument{i}"
            }));

            Languages = resourceService.GetAllLanguages();
            var currentLanguage = configurationService.GetLanguageConfig().CurrentLanguage;
            SelectedLanguage = Languages.FirstOrDefault(l => string.Equals(l, currentLanguage, StringComparison.CurrentCultureIgnoreCase));
            _selectedLanguageChanged = false;

            IsDarkThemeOn = themeService.GetTheme() == ElementTheme.Dark;

            SaveLanguageCommand = new AsyncRelayCommand(SaveLanguageAsync, () => _selectedLanguageChanged);
        }
        #endregion Ctor

        #region Methods
        private static InstrumentState GetInstrumentState(int index)
        {
            switch (index % 6)
            {
                case 0:
                    return InstrumentState.Error;
                case 1:
                    return InstrumentState.Idle;
                default:
                    return InstrumentState.Running;
            }
        }

        private async Task SaveLanguageAsync()
        {
            _selectedLanguageChanged = false;
            ((AsyncRelayCommand)SaveLanguageCommand).NotifyCanExecuteChanged();

            var dialog = new ContentDialog
            {
                Title = "Save Language",
                PrimaryButtonText = "OK",
                CloseButtonText = "Cancel",
                DefaultButton = ContentDialogButton.Primary,
                Content = "You must restart software to apply the language",
                XamlRoot = ((App)Application.Current).MainWindow.Content.XamlRoot
            };

            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                if (!_configurationService.SetCurrentLanguage(_selectedLanguage))
                {
                    Debug.WriteLine($"Failed to save language to {_selectedLanguage}");
                }
            }
        }

        private void UpdateTheme()
        {
            _themeService.SetTheme(IsDarkThemeOn ? ElementTheme.Dark : ElementTheme.Light);
        }
        #endregion Methods
    }
}
