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
using QQVideo.Utils;
using QQVideo.View;

namespace QQVideo
{

    /// <summary>
    /// Time.xaml 的交互逻辑
    /// </summary>
    public partial class Time : Window
    {
        private static Time time;    
        private TimeView timeView;
        public Time(Window window, BandicamUtil bandicamUtil)
        {

            timeView = new TimeView (this,bandicamUtil);
            this.Owner = window;
            this.DataContext = timeView;
            InitializeComponent();
            this.Left = 0;
            this.Top = 0;        
 
        }
        public static Time CreateTime(Window window, BandicamUtil bandicamUtil)
        {
            if(time==null){
                time = new Time(window,bandicamUtil);
            }
            return (time);
        }

        protected override void OnClosed(EventArgs e)
        {
            time = null;
            ((MainWindow)this.Owner).ShowAnimation(null,null);
  
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
