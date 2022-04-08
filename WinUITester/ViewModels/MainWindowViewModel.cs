using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace WinUITester.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Fields
        private string _display;
        #endregion Fields

        #region Commands
        public ICommand ClickCommend { get; }
        #endregion Commands

        #region Properties
        public string Display
        {
            get => _display;
            private set
            {
                if (_display == value) return;
                _display = value;
                OnPropertyChanged();
            }
        }
        #endregion Properties

        #region Ctor
        public MainWindowViewModel()
        {
            ClickCommend = new RelayCommand(() =>
            {
                Display = "Clicked";
            });
        }
        #endregion Ctor

        #region Methods



        #endregion Methods
    }
}
