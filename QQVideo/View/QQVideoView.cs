using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QQVideo.Utils;
using Prism.Commands;
using System.Windows;
using System.Timers;

namespace QQVideo.View
{
    public class QQVideoView:NotificationObject
    {

        #region 对象
        private  QQUtil qqUtil;//qq
        private BandicamUtil bandicamUtil;//Bandicam
        private MainWindow mainWindow;
        private Time time;//计时窗口
        private Timer timer = new Timer();
        #endregion
        #region 数据属性对象
        private string qqPath;
        private string bandicamPath;
        private string now;
        #endregion
        #region 数据属性
        public string QQPath //QQ快捷方式路径
        {
            get
            {
                return qqPath;
            }
            set
            {
                this.qqPath = value;
                RaisePropertyChanged("QQPath");

            }

        }
        public string BandicamPath
        {
            get
            {
                return bandicamPath;
            }
            set
            {
                this.bandicamPath = value;
                RaisePropertyChanged("BandicamPath");

            }

        }
        public string Now
        {
            get { return now; }
            set { now = value; RaisePropertyChanged("Now"); }
        }//时间

        #endregion
        #region 命令属性
        public DelegateCommand StartQQCommand { get; set; }//启动软件
        public DelegateCommand SetUpQQCommand { get; set; }//设置路径
        public DelegateCommand StartBandicamCommand { get; set; }
        public DelegateCommand SetUpBandicamCommand { get; set; }
        public DelegateCommand SetUpScreenCommand { get; set; }//设置屏幕
        public DelegateCommand StartRecCommand { get; set; }//开启录像
        public DelegateCommand StopRecCommand { get; set; }//停止录像
        public DelegateCommand StartTimeRecCommand { get; set; }//定时录像
        #endregion

        #region 构造
        public QQVideoView(MainWindow mainWindow)
        {
            timer.Interval = 1000;
            timer.Elapsed += new ElapsedEventHandler(Timer_Elapsed);
            timer.Enabled = true;
            timer.Start();
            qqUtil = new QQUtil();
            bandicamUtil = new BandicamUtil();
            this.QQPath="test";
            this.mainWindow = mainWindow;
            qqUtil.GetRegistryKey();//读取路径   
            bandicamUtil.GetRegistryKey();
            BandicamPath = bandicamUtil.BandicamPath;
            QQPath = qqUtil.QQPath;
            this.StartQQCommand = new DelegateCommand(StartQQ);
            this.SetUpQQCommand = new DelegateCommand(SetUpQQ);
            this.StartBandicamCommand = new DelegateCommand(StartBandicam);
            this.SetUpBandicamCommand = new DelegateCommand(SetUpBandicam);
            this.SetUpScreenCommand = new DelegateCommand(SetUpScreen);
            this.StartRecCommand = new DelegateCommand(StartRec);
            this.StopRecCommand = new DelegateCommand(StopRec);
            this.StartTimeRecCommand = new DelegateCommand(StartTimeRec);
        }
        #endregion

        #region 命令
        public void StartQQ()//启动QQ
        {      
            qqUtil.StartProcess();//启动
        }
        public void SetUpQQ()//设置QQ快捷方式
        {
          
            qqUtil.SetRegistryKey();
            this.QQPath = qqUtil.QQPath;
        }
        public void StartBandicam()
        {
            if (bandicamUtil.bandicamProcess==null)
            {
                bandicamUtil.StartProcess();
            }

           
        }
        public void SetUpBandicam()
        {

            bandicamUtil.SetRegistryKey();
            this.BandicamPath = bandicamUtil.BandicamPath;
        }
        public void SetUpScreen()
        {
            bandicamUtil.SetScreen();
  
        }
        public void StartRec()
        {
            if (CheckBandicam())
            {
                bandicamUtil.StartRec();
            }
        }
        public void StopRec()
        {
          
            bandicamUtil.StopRec();        
         
        }
        public void StartTimeRec()
        {
            ////开启定时
            if (CheckBandicam())
            {
                //弹窗选择时间
                this.mainWindow.Hide();
                time =Time.CreateTime(this.mainWindow, bandicamUtil);
                time.Show();

            }


        }
        #endregion
        #region 函数
        public void CloseProcess()
        {
           
            bandicamUtil.CloseProcess();
            CloseQQProcess();

        }
        public void CloseQQProcess()
        {
            qqUtil.CloseProcess();
        }
        public bool CheckBandicam()
        {
            if (bandicamUtil.bandicamProcess == null)
            {
                MessageBox.Show("请先启动录屏软件");
                return false;
            }
            else
            {
                return true;
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.Now = DateTime.Now.ToString("F");
        }
        #endregion
    }
}
