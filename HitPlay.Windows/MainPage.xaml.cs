using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白頁項目範本已記錄在 http://go.microsoft.com/fwlink/?LinkId=234238

namespace HitPlay
{
    /// <summary>
    /// 可以在本身使用或巡覽至框架內的空白頁面。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void BtnStartPlay_Click(object sender, RoutedEventArgs e)
        {
            var md = new MessageDialog("Hit Target as under Hammer ！");
            md.Commands.Add(new UICommand("OK", UICommandInvokedHandler => { this.Frame.Navigate(typeof(Game)); }));
            await md.ShowAsync();
        }

        private void BtnGameIntro_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Intro));

        }
    }
}
