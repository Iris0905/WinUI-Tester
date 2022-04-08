using WinUITester.Models;

namespace WinUITester.ViewModels
{
    public class FirstPageViewModel : ViewModelBase
    {
        public DataGenerator Data1 { get; }
        public DataGenerator Data2 { get; }

        public const int PointsCountPerSeries = 100000;

        public FirstPageViewModel()
        {
            Data1 = new DataGenerator(PointsCountPerSeries);
            Data2 = new DataGenerator(PointsCountPerSeries);
        }
    }
}