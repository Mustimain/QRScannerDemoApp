namespace QRScannerDemoApp.Views;

public partial class ScannerPage : ContentPage
{
	public ScannerPage()
	{
		InitializeComponent();
	}

    void barcodeReader_BarcodesDetected(System.Object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
    {
    }
}
