using System;
using System.Globalization;
using ZXing.Net.Maui;

namespace QRScannerDemoApp.Converters
{
    public class BarcodeDetectionEventArgsConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value as BarcodeDetectionEventArgs;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

