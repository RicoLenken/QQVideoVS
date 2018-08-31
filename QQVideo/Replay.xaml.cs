using QQVideo.Utils;
using QQVideo.View;
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
using System.Windows.Shapes;

namespace QQVideo
{
    /// <summary>
    /// Replay.xaml 的交互逻辑
    /// </summary>
    public partial class Replay : Window
    {
        #region 对象
        private static Replay singleReplay;
        private ReplayView replayView;
        #endregion
        public Replay(Window window, ReplayUtil replayUtil)
        {
            replayView = new ReplayView(this, replayUtil);
            this.DataContext = replayView;
            this.Owner = window;
            InitializeComponent();
        }
        public static Replay CreateReplay(Window window,ReplayUtil replayUtil)
        {
            if (singleReplay==null)
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
    }
}
