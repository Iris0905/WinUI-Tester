using System;
using Microsoft.UI.Xaml.Data;
using WinUITester.Enums;

namespace WinUITester.Converter
{
    public class InstrumentStateToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var state = (InstrumentState) value;
            return state switch
            {
                InstrumentState.Running => "Running",
                InstrumentState.Idle => "Idle",
                InstrumentState.Error => "Error",
                _ => "--"
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
