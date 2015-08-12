using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.Devices.Notification;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// 空白頁項目範本已記錄在 http://go.microsoft.com/fwlink/?LinkID=390556

namespace HitPlay
{
    /// <summary>
    /// 可以在本身使用或巡覽至框架內的空白頁面。
    /// </summary>
    public sealed partial class Game_phone : Page, INotifyPropertyChanged
    {
        private string _point;
        private int score = 0;
        private int sec = 30;
        private int contiMinus = 0;
        private bool flag = true;
        public List<int> clickList = new List<int>();
        public List<int> answerList = new List<int>();
        public List<String> roleList = new List<String>();
        public List<String> holeNow = new List<String>();
        public List<String> statusList = new List<String>();
        private DispatcherTimer _timer;
        public event PropertyChangedEventHandler PropertyChanged;

        public Game_phone()
        {
            this.InitializeComponent();
            this.DataContext = this;
            Ini();
            Ini_timer();

        }

        public void Ini()
        {
            roleList.Add("a");
            roleList.Add("b");
            roleList.Add("c");
            statusList.Add("Eat");
            statusList.Add("Sleep");
            statusList.Add("Hit DongDong");
            Points = sec.ToString() + "秒";
            Score();
            ReStart();
            AssignAll();
        }

        public void Ini_timer()
        {
            int s = 0;
            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _timer.Tick += delegate
            {
                if (flag)
                {
                    if (sec <= 0) { End(); }
                    else
                    {
                        --sec; ++s;
                        Points = sec.ToString() + "秒";
                        if (sec < 18)
                        {
                            s = 0; Score();
                            ReStart();
                            AssignAll();
                        }
                        else if (sec >= 18 && s >= 2)
                        {
                            s = 0; Score();
                            ReStart();
                            AssignAll();
                        }
                    }
                }
            };
            _timer.Start();
        }

        public void AssignAll()
        {
            Random rnd = new Random();
            for (int i = 1; i <= 3; i++)
            {
                int hole = rnd.Next(1, 9);
                if (i == 8) { continue; }
                if (holeNow.Contains(hole.ToString())) { continue; } else { holeNow.Add(hole.ToString()); }
                string name = roleList.ElementAt((rnd.Next(0, 2) + rnd.Next(i, 9)) % 3).ToString() + hole.ToString();
                Image temp = (Image)FindName(name);
                if (temp != null)
                {
                    temp.Visibility = Windows.UI.Xaml.Visibility.Visible;
                }

            }

        }

        public void End()
        {
            _timer.Stop();
            Clear();
            endScreen.Visibility = Windows.UI.Xaml.Visibility.Visible;
            btnBackMenu.Visibility = Windows.UI.Xaml.Visibility.Visible;
            btnReplay.Visibility = Windows.UI.Xaml.Visibility.Visible;
            txtEnd.Visibility = Windows.UI.Xaml.Visibility.Visible;
            if (score >= 10)
            {
                txtEnd.Text = "Great Score! Give you a Cheese!";
                imgLead.Source = new BitmapImage(new Uri("ms-appx:///Assets/1.png"));

            }
            else if (score > 5 && score < 10)
            {
                txtEnd.Text = "Cheer up! Dong Dong Need you!";
                imgLead.Source = new BitmapImage(new Uri("ms-appx:///Assets/3.png"));
            }
            else
            {
                txtEnd.Text = "You Need a Sleep!";
                imgLead.Source = new BitmapImage(new Uri("ms-appx:///Assets/2.png"));
            }
        }

        public void ReStart()
        {
            Random rnd = new Random();
            int ans = rnd.Next(1, 4);
            if (ans == 4) { ans = 3; }
            Clear();
            answerList.Add(ans);
            imgLead.Source = new BitmapImage(new Uri("ms-appx:///Assets/" + ans.ToString() + ".png"));
                    
        }

        public void Score()
        {
            lblScore.Text = score.ToString() + "分";
            string a = "";
            string c = "";
            foreach (int i in answerList)
            {
                a = i.ToString() + ", ";
            } foreach (int i in clickList)
            {
                c = i.ToString() + ", ";
            }
        }
        public void Clear()
        {
            answerList.Clear();
            holeNow.Clear();
            gridBg.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 100));
            txtDiaogue.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            // Collapsed all roles 
            for (int i = 1; i <= 9; i++)
            {
                for (int j = 0; j <= 2; j++)
                {
                    string name = roleList.ElementAt(j).ToString() + i.ToString();
                    Image temp = (Image)FindName(name);
                    if (temp != null)
                    {
                        temp.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    }
                }
            }
        }
        public void CheckAnswer()
        {
            foreach (int c in clickList)
            {
                if (contiMinus < -1)
                {
                    txtDiaogue.Text = "Come on!";
                    txtDiaogue.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    contiMinus = 0;
                } if (contiMinus > 1)
                {
                    txtDiaogue.Text = "Cool! Great!";
                    txtDiaogue.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    contiMinus = 0;
                }
                if (answerList.Contains(c))
                {
                    VibrationDevice device = VibrationDevice.GetDefault();
                    if (device != null) device.Vibrate(TimeSpan.FromSeconds(0.1));
                    gridBg.Background = new SolidColorBrush(Color.FromArgb(220, 20, 60, 100));
                    
                    if (c == 3)
                    {
                        score += 2;
                    }
                    ++score;
                    ++contiMinus;
                }
                else
                {
                    VibrationDevice device = VibrationDevice.GetDefault();
                    if (device != null) device.Vibrate(TimeSpan.FromSeconds(0.3));
                   
                    gridBg.Background = new SolidColorBrush(Color.FromArgb(127, 255, 0, 100));
                    if (c == 3)
                    {
                        score -= 2;
                    }
                    --score;
                    --contiMinus;
                }

            }
            clickList.Clear();
        }


        public String Points
        {
            set
            {
                _point = value;
                NotifyPropertyChanged("Points");
            }
            get { return _point; }
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void a_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            clickList.Add(1);
            CheckAnswer();
            Score();
        }

        private void b_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            clickList.Add(2);
            CheckAnswer();
            Score();
        }

        private void c_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            clickList.Add(3);
            CheckAnswer();
            Score();
        }

        private void BackMenuButton_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void ReplayButton_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Game_phone));
        }

        private void ContinueButton_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            flag = true;
            btnContinuePlay.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            btnBackMenu.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            endScreen.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

        }

        private void PauseButton_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            flag = false;
            endScreen.Visibility = Windows.UI.Xaml.Visibility.Visible;
            btnContinuePlay.Visibility = Windows.UI.Xaml.Visibility.Visible;
            btnBackMenu.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        private void BackMenuButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void ReplayButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Game_phone));
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            flag = false;
            endScreen.Visibility = Windows.UI.Xaml.Visibility.Visible;
            btnContinuePlay.Visibility = Windows.UI.Xaml.Visibility.Visible;
            btnBackMenu.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            flag = true;
            btnContinuePlay.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            btnBackMenu.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            endScreen.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }
    }

}
