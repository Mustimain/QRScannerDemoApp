using System;
using System.Linq;
using System.Windows.Input;
using Plugin.Maui.Audio;
using ZXing.Net.Maui;

namespace QRScannerDemoApp.ViewModels
{
    public class ScannerPageViewModel : BaseViewModel
    {

        private readonly IAudioManager _audioManager;
        public ScannerPageViewModel(INavigationService navigationService, IAudioManager audioManager) : base(navigationService)
        {

            _audioManager = audioManager;
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

        private string _scannerButtonText = "TARAMAYI BAŞLAT";
        public string ScannerButtonText
        {
            get
            {
                return _scannerButtonText;
            }
            set
            {
                _scannerButtonText = value; RaisePropertyChanged();
            }
        }

        private bool _imageVisible = true;
        public bool ImageVisible
        {
            get
            {
                return _imageVisible;
            }
            set
            {
                _imageVisible = value; RaisePropertyChanged();
            }
        }

        private bool _scannerVisible = false;
        public bool ScannerVisible
        {
            get
            {
                return _scannerVisible;
            }
            set
            {
                _scannerVisible = value; RaisePropertyChanged();
            }
        }

        private bool _isDetecting = false;
        public bool IsDetecting
        {
            get
            {
                return _isDetecting;
            }
            set
            {
                _isDetecting = value; RaisePropertyChanged();
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

                        try
                        {
                            var barcode = e.Results.FirstOrDefault();
                            if (barcode != null)
                            {
                                var cleanBarcodeValue = barcode.Value.Trim().Replace("\n", "").Replace("\r", "");
                                BarcodeText = cleanBarcodeValue;
                                var soundValue = cleanBarcodeValue + ".mp3";

                                var audioPlayer = _audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync(soundValue));
                                audioPlayer.Play();

                            }

                            IsDetecting = false;
                            ImageVisible = true;
                            ScannerVisible = false;
                        }
                        catch
                        {

                            IsDetecting = false;
                            ImageVisible = true;
                            ScannerVisible = false;
                        }

                    }

                });
            }
        }

        public ICommand StartScanCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (IsDetecting == false)
                    {
                        ScannerButtonText = "TARAMAYI BİTİR";
                        BarcodeText = "";
                        ImageVisible = false;
                        ScannerVisible = true;
                        IsDetecting = true;
                    }
                    else
                    {
                        ScannerButtonText = "TARAMAYI BAŞLAT";
                        BarcodeText = "";
                        ImageVisible = true;
                        ScannerVisible = false;
                        IsDetecting = false;
                    }


                });
            }
        }
    }
}

