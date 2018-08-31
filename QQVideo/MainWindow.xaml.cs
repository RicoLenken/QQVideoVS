using QQVideo.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using QQVideo.View;
using System.Windows.Media.Animation;
using System.Windows.Interop;
using System.Collections.ObjectModel;

namespace QQVideo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        #region 对象
        int length = 250;
        int windowHeight = 200;
        float time = 1f;
        private QQVideoView qqVideoView;
        private System.Windows.Forms.NotifyIcon icon;
        ElasticEase ease = new ElasticEase();
        private bool moveFlag = false;
        #endregion
        public MainWindow()
        {
            #region ViewModel
            this.qqVideoView = new QQVideoView(this);
            this.DataContext = qqVideoView;
            #endregion
            //缓动     
            ease.EasingMode = EasingMode.EaseOut;
            ease.Oscillations = 3;
            ease.Springiness = 8;
            InitializeComponent();
            #region 初始位置
            this.Height = windowHeight;
            double left = (SystemParameters.PrimaryScreenWidth - this.Width) / 2;
            this.Left = left;
            //MessageBox.Show(SystemParameters.PrimaryScreenWidth.ToString() + "," + this.Width + "," + left);
            this.Top = 100;
            ShowAnimation(null, null);
            #endregion

            //注册热键
            HotKeyUtil hk = new HotKeyUtil(this,HotKeyUtil.KeyModifiers.MOD_ALT,keyCode.vbKeyF1);
            hk.OnHotKey += this.OnHotkey;

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
              
            MessageBoxResult result = MessageBox.Show("确认退出？", "", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                qqVideoView.CloseProcess();
                Application.Current.Shutdown();
            }
            else
            {
                return;
            }
           
        }

        private void btnSetUp_Click(object sender, RoutedEventArgs e)
        {


            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = this.Height;
            doubleAnimation.To = windowHeight + length;
            doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(time));
            doubleAnimation.EasingFunction = ease;//缓动
            Storyboard story = new Storyboard();
            story.Children.Add(doubleAnimation);
            Storyboard.SetTargetName(doubleAnimation, this.Name);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(Window.HeightProperty));
            story.Begin(this);

        }

        private void winQQVideo_MouseLeave(object sender, MouseEventArgs e)
        {
            if (this.Height > windowHeight)
            {
                DoubleAnimation doubleAnimation = new DoubleAnimation();
                doubleAnimation.From = this.Height;
                doubleAnimation.To = windowHeight;
                doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(time));
                doubleAnimation.EasingFunction = ease;//缓动
                Storyboard story = new Storyboard();
                story.Children.Add(doubleAnimation);
                Storyboard.SetTargetName(doubleAnimation, this.Name);
                Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(Window.HeightProperty));

                story.Begin(this);

            }
        }

        public void btnHide_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            icon = new System.Windows.Forms.NotifyIcon();
            icon.BalloonTipText = "软件已隐藏，点击图标显示";
            icon.Icon = new System.Drawing.Icon("Image/QQVideo.ico");
            icon.Visible = true;
            icon.ShowBalloonTip(1000);
            icon.Click += ShowAnimation;
        }

        public void ShowAnimation(object sender, EventArgs e)
        {
            this.Show();
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = -this.Top / 2;
            doubleAnimation.To = this.Top;
            doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(time));
            doubleAnimation.EasingFunction = ease;//缓动
            Storyboard story = new Storyboard();
            story.Children.Add(doubleAnimation);
            Storyboard.SetTargetName(doubleAnimation, this.Name);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(Window.TopProperty));
            story.Begin(this);
        }

        #region 快捷键事件


        private void OnHotkey()
        {
            qqVideoView.CloseQQProcess();
        }

        #endregion

        #region 鼠标移动
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            moveFlag = true;
            if (moveFlag)
            {
                this.DragMove();
            }      
        }


        #endregion


    }
}
