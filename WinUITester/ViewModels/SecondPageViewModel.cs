using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage.Pickers;
using CommunityToolkit.Mvvm.Input;
using WinUITester.Interfaces;
using WinUITester.Models;

namespace WinUITester.ViewModels
{
    public class SecondPageViewModel : ViewModelBase
    {
        #region Fields
        private string _selectedFile = "Selected File: ";
        #endregion Fields

        #region Commands
        public ICommand OpenFileExplorerCommand { get; }
        #endregion Commands

        #region Properties
        public string SelectedFile
        {
            get => _selectedFile;
            private set
            {
                if (_selectedFile == value) return;
                _selectedFile = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<TestResult> Results { get; private set; }

        public string InfoMessage { get; }
        public string ErrorMessage { get; }
        public string WarningMessage { get; }
        public string SuccessMessage { get; }
        #endregion Properties

        #region Ctor
        public SecondPageViewModel(IResourceService resourceService)
        {
            OpenFileExplorerCommand = new AsyncRelayCommand(SelectFileAsync);

            InfoMessage = resourceService.GetString("Info_InfoMessage");
            ErrorMessage = resourceService.GetString("Info_ErrorMessage");
            WarningMessage = resourceService.GetString("Info_WarningMessage");
            SuccessMessage = resourceService.GetString("Info_SuccessMessage");

            GenerateTestResults();
        }
        #endregion Ctor

        #region Methods
        private async Task SelectFileAsync()
        {
            var hwnd = GetActiveWindow();

            var picker = new FileOpenPicker
            {
                SuggestedStartLocation = PickerLocationId.Desktop,
                ViewMode = PickerViewMode.List
            };
            picker.FileTypeFilter.Add(".txt");

            // Need to initialize the picker object with the hwnd / IInitializeWithWindow 
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hwnd);

            var file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                SelectedFile = "Selected File: " + file.Name;
            }
        }

        private void GenerateTestResults()
        {
            Results = new ObservableCollection<TestResult>(Enumerable.Range(1, 1000).Select(i => new TestResult
            {
                Barcode = $"Sample{1:000}",
                Assay = (i % 3) switch
                {
                    1 => "CT/GC",
                    2 => "ZIKV",
                    _ => "SARSCoV2"
                },
                Result = (i % 7) switch
                {
                    1 => "Positive",
                    2 => "Invalid",
                    _ => "Negative"
                },
                CompletedAt = new DateTime(2022, 3, i % 30 + 1, i % 20 + 1, i % 60, 0)
            }));
        }
        #endregion Methods

        #region User32
        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto, PreserveSig = true, SetLastError = false)]
        public static extern IntPtr GetActiveWindow();
        #endregion User32
    }
}
