namespace WinUITester.Models
{
    public class SeriesPoint
    {
        public int Argument { get; }
        public double Value { get; }

        public SeriesPoint(int argument, double value)
        {
            Argument = argument;
            Value = value;
        }
    }
}
