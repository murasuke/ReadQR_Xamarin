using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ReadQR
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

        }

        //void OnQR(object sender, EventArgs e)
        //{
        //    Navigation.PushAsync(new QRScanPage());
        //}

        //public bool IsScaning { get; set; } = false;

        public string QRButtonText { 
            get{ return zxing.IsAnalyzing ? "Stop Scanning" : "Start Scanning"; } 
        }

        void pictureButton_Clicked(object sender, EventArgs e)
        {
            zxing.IsScanning = !zxing.IsScanning;
            zxing.IsAnalyzing = zxing.IsScanning;
           
            btnQR.Text = QRButtonText;
        }
        void Handle_OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                zxing.IsAnalyzing = false;  //読み取り停止
                label2.Text = result.Text;
                await DisplayAlert("通知", "次の値を読み取りました：" + result.Text, "OK");
                zxing.IsAnalyzing = true;   //読み取り再開
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            zxing.IsScanning = true;
            btnQR.Text = QRButtonText;
        }

        protected override void OnDisappearing()
        {
            zxing.IsScanning = false;
            btnQR.Text = QRButtonText;

            base.OnDisappearing();
        }
    }
}
