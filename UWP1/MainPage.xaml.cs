using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWP1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            dt.Tick += Refresh;
            dt.Start();
        }

        private async void Refresh(object sender, object e)
        {
            var cli = new HttpClient();

            var res = await cli.GetStringAsync("http://api.openweathermap.org/data/2.5/weather?q=Minsk&mode=json&units=metric&APPID=" + AppID);
            dynamic x = Newtonsoft.Json.JsonConvert.DeserializeObject(res);

            textBlock.Text = $"{ x.main.temp} C";
            BitmapImage img = new BitmapImage(new Uri($"http://api.openweathermap.org/img/w/{x.weather[0].icon}.png"));
            Img.Source = img;
        }

        DispatcherTimer dt = new DispatcherTimer() { Interval = TimeSpan.FromMinutes(10) };

        public string AppID = "47ee080e09607c9a68582c4368d42402";

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh(sender, null);
        }
    }
}
