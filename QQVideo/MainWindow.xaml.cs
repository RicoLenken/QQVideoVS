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

namespace QQVideo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        int length = 200;
        int windowHeight = 200;
        float time = 0.2f;
        private QQVideoView qqVideoView; 

        public MainWindow()
        {
           
            InitializeComponent();
            #region 初始位置
            this.Height = windowHeight;
            double left = (SystemParameters.PrimaryScreenWidth - this.Width) / 2;
            this.Left = left;
            //MessageBox.Show(SystemParameters.PrimaryScreenWidth.ToString() + "," + this.Width + "," + left);
            this.Top = 100;
            #endregion
            #region ViewModel
            this.qqVideoView = new QQVideoView(this);
            this.DataContext = qqVideoView;
            #endregion
        }
     
       
        private void Window_Closed(object sender, EventArgs e)
        {
                   
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            qqVideoView.CloseProcess();
            Application.Current.Shutdown();
        }

        private void btnSetUp_Click(object sender, RoutedEventArgs e)
        {
     
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = this.Height;
            doubleAnimation.To = windowHeight + length;
            doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(time));
            Storyboard story = new Storyboard();
            story.Children.Add(doubleAnimation);
            Storyboard.SetTargetName(doubleAnimation, this.Name);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(Window.HeightProperty));
            story.Begin(this); 

        }

        private void winQQVideo_MouseLeave(object sender, MouseEventArgs e)
        {
            if (this.Height>windowHeight)
            {
                DoubleAnimation doubleAnimation = new DoubleAnimation();
                doubleAnimation.From = this.Height;
                doubleAnimation.To = windowHeight;
                doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(time));

                Storyboard story = new Storyboard();
                story.Children.Add(doubleAnimation);
                Storyboard.SetTargetName(doubleAnimation, this.Name);
                Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(Window.HeightProperty));

                story.Begin(this);

            }
        }

    
    }
}
