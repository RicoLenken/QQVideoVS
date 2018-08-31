using QQVideo.Utils;
using QQVideo.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QQVideo
{
    /// <summary>
    /// Replay.xaml 的交互逻辑
    /// </summary>
    /// 

    public partial class Replay : Window
    {
   
        #region 对象
        private static Replay singleReplay;
        private ReplayView replayView;
        private Timer timer;

        #endregion
        public Replay(Window window, ReplayUtil replayUtil)//构造
        {
            InitializeComponent();
            replayView = new ReplayView(this, replayUtil,new VideoUtil(mediaPlayer));
            this.DataContext = replayView;
            this.Owner = window;

        }


        public static Replay CreateReplay(Window window, ReplayUtil replayUtil)
        {
            if (singleReplay == null)
            {
                singleReplay = new Replay(window, replayUtil);
            }
            return singleReplay;
        }
        protected override void OnClosed(EventArgs e)
        {
            this.Close();
            this.Owner.Show();
            singleReplay = null;
        }

        private void lbxVideo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            replayView.MediaPlay();
        }

        private void sliderTime_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            replayView.MediaSet();
        }

        private void sliderTime_MouseEnter(object sender, MouseEventArgs e)
        {
            replayView.StopTick();
        }

        private void sliderTime_MouseLeave(object sender, MouseEventArgs e)
        {
            replayView.StartTick();
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            replayView.MediaControl();
        }
    }



}

