using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QQVideo.Utils;
using System.Timers;
using Microsoft.Practices.Prism.ViewModel;
using System.Windows;
using Microsoft.Practices.Prism.Commands;

namespace QQVideo.View
{
    public class TimeView : NotificationObject
    {
        private delegate void delegateMain();
        #region 对象
        private BandicamUtil bandicamUtil;
        private Timer countDown = new Timer();
        private Window window;
        private bool startFlag = false;
        #endregion
        #region 数据对象
        private string timeLeft;
        private string timeNow;
        private int timeHour;
        private int timeMinute;
        private DateTime tsSatrt;
        private TimeSpan tsLeft;
        private TimeSpan tsLength;
        #endregion
        #region 数据属性
        public string TimeLeft
        {
            get { return timeLeft; }
            set
            {
                timeLeft = value;
                RaisePropertyChanged("TimeLeft");
            }
        }
        public int TimeHour
        {
            get { return timeHour; }
            set
            {
                timeHour = value;
                RaisePropertyChanged("TimeHour");
            }
        }
        public int TimeMinute
        {
            get { return timeMinute; }
            set
            {
                timeMinute = value;
                RaisePropertyChanged("TimeMinute");
            }
        }
        public string TimeNow
        {
            get { return timeNow; }
            set
            {
                timeNow = value;
                RaisePropertyChanged("TimeNow");
            }
        }
        #endregion
        #region 命令属性
        public DelegateCommand HourUpCommand { get; set; }
        public DelegateCommand HourDownCommand { get; set; }
        public DelegateCommand MinuteUpCommand { get; set; }
        public DelegateCommand MinuteDownCommand { get; set; }
        public DelegateCommand MinuteUpUpCommand { get; set; }
        public DelegateCommand MinuteDownDownCommand { get; set; }
        public DelegateCommand CloseCommand { get; set; }
        #endregion
        #region 构造
        public TimeView(Window window, BandicamUtil bandicamUtil)
        {
            this.window = window;
            this.bandicamUtil = bandicamUtil;
            countDown.Interval = 1000;
            countDown.Enabled = true;
            countDown.Elapsed += new ElapsedEventHandler(Time_Elapsed);
            countDown.Stop();
            this.TimeHour = 0;
            this.timeMinute = 0;
            this.HourUpCommand = new DelegateCommand(HourUp);
            this.HourDownCommand = new DelegateCommand(HourDown);
            this.MinuteUpCommand = new DelegateCommand(MinuteUp);
            this.MinuteDownCommand = new DelegateCommand(MinuteDown);
            this.MinuteUpUpCommand = new DelegateCommand(MinuteUpUp);
            this.MinuteDownDownCommand = new DelegateCommand(MinuteDownDown);
            this.CloseCommand = new DelegateCommand(Close);
            //test
            //tsLength = TimeSpan.FromSeconds(10);
            //tsSatrt = DateTime.Now;
        }
        #endregion
        #region 命令
        private void Close()
        {
            StopRec();
        }
        private void Calulate()
        {
            this.tsLength = TimeSpan.FromMinutes(TimeHour * 60 + TimeMinute);
            if ((TimeHour > 0 || TimeMinute > 0) &&!startFlag)
            {               
                tsSatrt = DateTime.Now;
                countDown.Start();
                StartRec();         
                startFlag = true;
            }
 
        }
        private void HourUp()
        {

            if (this.TimeHour < 24)
            {
                this.TimeHour += 1;
            }
            Calulate();
        }
        private void HourDown()
        {

            if (this.TimeHour > 0)
            {
                this.TimeHour -= 1;
            }

            Calulate();
        }
        private void MinuteUp()
        {
            if (this.TimeMinute < 60)
            {
                this.TimeMinute += 1;
            }

            Calulate();
        }
        private void MinuteDown()
        {
            if (timeMinute > 0)
            {
                this.TimeMinute -= 1;
            }

            Calulate();
        }
        private void MinuteUpUp()
        {
            if (this.TimeMinute < 60)
            {
                this.TimeMinute += 10;
            }

            Calulate();
        }
        private void MinuteDownDown()
        {
            if (timeMinute > 0)
            {
                this.TimeMinute -=10;
            }

            Calulate();
        }
        #endregion
        #region 函数
        private void StartRec()
        {
            //MessageBox.Show("开始录像");
            bandicamUtil.StartRec();//开始录像       
               
        }

        private void StopRec()
        {
            if (startFlag)
            {
                bandicamUtil.StopRec();//停止录像     
                System.Threading.Thread.Sleep(200);
                countDown.Close();
            }

            this.window.Dispatcher.Invoke(new delegateMain(HideMain));

        }
        #endregion
        private void Time_Elapsed(object sender, ElapsedEventArgs e)//时间函数
        {
          
                this.TimeNow = DateTime.Now.ToString("F");
                tsLeft = tsSatrt + tsLength - DateTime.Now;
                this.TimeLeft = (tsLeft.Hours > 0 ? tsLeft.Hours + "时" : "") +
                               (tsLeft.Minutes > 0 || tsLeft.Hours > 0 ? tsLeft.Minutes + "分" : "") +
                               (tsLeft.Seconds > 0 ? tsLeft.Seconds + "秒" : "");
                if (tsLeft.TotalSeconds < 1)
                {
                    StopRec();
                }
          
        }
        private void HideMain()
        {
            this.window.Close();
        }

    }



}


