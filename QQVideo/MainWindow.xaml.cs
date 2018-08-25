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

namespace QQVideo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private QQUtil qqUtil = new QQUtil();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            qqUtil.GetQQPath();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            qqUtil.SendKey();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            qqUtil.KillProcess();
        }
    }
}
