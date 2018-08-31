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
    public class QQVideoView : NotificationObject
    {
        #region 对象
        private QQUtil qqUtil =new QQUtil();//qq
        private BandicamUtil bandicamUtil =new BandicamUtil();//Bandicam     
        private MainWindow mainWindow;
        private Time time;//计时窗口
        private Replay replay;
        private Timer timer = new Timer();
        private bool startFlag = false;
        private ReplayUtil replayUtil = new ReplayUtil();//回放
        #endregion
        #region 数据属性对象
        private string qqPath;
        private string bandicamPath;
        private string videoPath;
        private string now;
        private string qqString = "启动QQ";
        private string qqImage = "Image/QQ.png";
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
        public string VideoPath
        {
            get
            {
                return videoPath;
            }
            set
            {
                this.videoPath = value;
                RaisePropertyChanged("VideoPath");

            }

        }
        public string Now
        {
            get { return now; }
            set { now = value; RaisePropertyChanged("Now"); }
        }//时间
        public string QQString 
        {
            get { return qqString; }
            set { qqString = value;
                RaisePropertyChanged("QQString");
            }
        }
        public string QQImage
        {
            get { return qqImage; }
            set { qqImage = value;
                RaisePropertyChanged("QQImage");
            }
        }
        #endregion
        #region 命令属性对象
        private DelegateCommand qqCommand;
        #endregion
        #region 命令属性
        public DelegateCommand QQCommand
        {
            get { return qqCommand; }
            set { this.qqCommand = value;
                RaisePropertyChanged("QQCommand");}
        }//启动软件
        public DelegateCommand SetUpQQCommand { get; set; }//设置路径
        public DelegateCommand StartBandicamCommand { get; set; }
        public DelegateCommand SetUpBandicamCommand { get; set; }
        public DelegateCommand SetUpScreenCommand { get; set; }//设置屏幕
        public DelegateCommand StartRecCommand { get; set; }//开启录像
        public DelegateCommand StopRecCommand { get; set; }//停止录像
        public DelegateCommand StartTimeRecCommand { get; set; }//定时录像
        public DelegateCommand SetUpReplayCommand { get; set; }//设置录像搜索路径
        public DelegateCommand ReplayCommand { get; set; }//回放
        #endregion
        #region 构造
        public QQVideoView(MainWindow mainWindow)
        {
            timer.Interval = 1000;
            timer.Elapsed += new ElapsedEventHandler(Timer_Elapsed);
            timer.Enabled = true;
            timer.Start();
            this.QQPath = "test";
            this.mainWindow = mainWindow;
            #region 路径
            qqUtil.GetRegistryKey();//读取路径   
            bandicamUtil.GetRegistryKey();
            replayUtil.GetRegistryKey();
            BandicamPath = bandicamUtil.BandicamPath;
            QQPath = qqUtil.QQPath;
            videoPath = replayUtil.ReplayPath;
            #endregion
            #region 命令属性赋值
            this.QQCommand = new DelegateCommand(StartQQ);
            this.SetUpQQCommand = new DelegateCommand(SetUpQQ);
            this.StartBandicamCommand = new DelegateCommand(StartBandicam);
            this.SetUpBandicamCommand = new DelegateCommand(SetUpBandicam);
            this.SetUpScreenCommand = new DelegateCommand(SetUpScreen);
            this.StartRecCommand = new DelegateCommand(StartRec);
            this.StopRecCommand = new DelegateCommand(StopRec);
            this.StartTimeRecCommand = new DelegateCommand(StartTimeRec);
            this.SetUpReplayCommand = new DelegateCommand(SetUpReplay);
            this.ReplayCommand = new DelegateCommand(StartReplay);
            #endregion     
        }
        #endregion
        #region 命令
        public void StartQQ()//启动QQ
        {
            qqUtil.StartProcess();//启动
            this.QQCommand = new DelegateCommand(StopQQ);
            this.QQString = "关闭QQ";
            this.QQImage = "Image/QQClose.png";
        }
        public void StopQQ()
        {
            CloseQQProcess();
            this.QQCommand = new DelegateCommand(StartQQ);
            this.QQString = "启动QQ";
            this.QQImage = "Image/QQ.png";

        }
        public void SetUpQQ()//设置QQ快捷方式
        {

            qqUtil.SetRegistryKey();
            this.QQPath = qqUtil.QQPath;
        }
        public void StartBandicam()
        { 
            bandicamUtil.StartProcess();
            System.Threading.Thread hideThread = new System.Threading.Thread(HideRec);
            hideThread.Start();


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
            if (bandicamUtil.RecCheck())
            {
                if (!startFlag)
                {
                    bandicamUtil.StartRec();
                    startFlag = true;
                }

            }
        }
        public void StopRec()
        {
            if (startFlag)
            {
                bandicamUtil.StopRec();
                startFlag = false;
            }


        }
        public void StartTimeRec()
        {
            ////开启定时
            if (bandicamUtil.RecCheck())
            {
                if (!startFlag)
            {
                //弹窗选择时间
                this.mainWindow.Hide();
                time = Time.CreateTime(this.mainWindow, bandicamUtil);
                time.Show();
            }


            }

        }
        public void SetUpReplay()
        {
            replayUtil.SetRegistryKey();
            this.VideoPath = replayUtil.ReplayPath;
        }
        public void StartReplay()//开始回放
        {
            replay= Replay.CreateReplay(this.mainWindow,replayUtil);
            replay.Show();

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
       
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.Now = DateTime.Now.ToString("F");
        }
        public void HideRec()
        {
            System.Threading.Thread.Sleep(7000);
            SetUpScreen();//隐藏屏幕
         
        }
        #endregion
    }
}
