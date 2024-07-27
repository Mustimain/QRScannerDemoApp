using System;
using System.Linq;
using System.Windows.Input;
using ZXing.Net.Maui;

namespace QRScannerDemoApp.ViewModels
{
    public class ScannerPageViewModel : BaseViewModel
    {
        public ScannerPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _customBarcodeReaderOptions = new BarcodeReaderOptions
            {
                Formats = BarcodeFormat.QrCode,
                AutoRotate = true,
                Multiple = true
            };
        }

        private BarcodeReaderOptions _customBarcodeReaderOptions;
        public BarcodeReaderOptions CustomBarcodeReaderOptions
        {
            get
            {
                return _customBarcodeReaderOptions;
            }
            set
            {
                _customBarcodeReaderOptions = value; RaisePropertyChanged();
            }
        }

        private string _barcodeText;
        public string BarcodeText
        {
            get
            {
                return _barcodeText;
            }
            set
            {
                _barcodeText = value; RaisePropertyChanged();
            }
        }

        public ICommand BarcodesDetectedCommand
        {
            get
            {
                return new Command(async (object param) =>
                {

                    if (param is BarcodeDetectionEventArgs e)
                    {
                        var barcode = e.Results.FirstOrDefault();
                        if (barcode != null)
                        {
                            Console.WriteLine($"Detected barcode: {barcode.Value} (Type: {barcode.Format})");

                        }

                    }
                    else
                    {
                        Console.WriteLine("Parameter is not of type BarcodeDetectionEventArgs");
                    }

                });
            }
        }
    }
}

