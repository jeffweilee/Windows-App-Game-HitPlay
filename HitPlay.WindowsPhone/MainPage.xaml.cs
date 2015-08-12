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

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// 在此頁面即將顯示在框架中時叫用。
        /// </summary>
        /// <param name="e">描述如何到達此頁面的事件資料。
        /// 這個參數通常用來設定頁面。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: 準備讓頁面在這裡顯示。

            // TODO: 如果您的應用程式包含數頁，為確保
            // 能夠處理硬體上一頁按鈕，請註冊
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed 事件。
            // 如果您使用一些範本所提供的 NavigationHelper，
            // 系統會為您處理這個事件。
        }

        private async void BtnStartGame_phone_Click(object sender, RoutedEventArgs e)
        {
            var md = new MessageDialog("Hit Target as under Hammer ！");
            md.Commands.Add(new UICommand("OK", UICommandInvokedHandler => { this.Frame.Navigate(typeof(Game)); }));
            await md.ShowAsync();
            // MessageDialog md = new MessageDialog("Hit the ones the same as the target under Hammer ！");
            // await md.ShowAsync();
            // this.Frame.Navigate(typeof(Game_phone));
        }

        private void BtnGameIntro_phone_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Intro));
        }
    }
}
