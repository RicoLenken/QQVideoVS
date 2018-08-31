using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using QQVideo.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;

namespace QQVideo.View
{
    public class ReplayView : NotificationObject
    {


        #region 对象
        private List<string> fileNameList = new List<string>();
        List<string> searchNameList = new List<string>();
        private List<FileInfo> fileInfoList = new List<FileInfo>();
        List<string> videoType = new List<string>() { ".mp4", ".avi", ".MP4" ,".mov",".flv",
            ".AVI", ".MPEG", ".MPG",".rmvb" };
        private Timer timer = new Timer();
        private VideoUtil videoUtil;
        private bool pauseFlag = false;
        #endregion
        #region 数据对象
        private List<string> videoList;
        private DateTime searchDate = DateTime.Now;
        private string playfileName;
        private string selectVideo;
        private double sliderValue;
        private string currentTime;
        private string totalTime;
        private string playImage;
        #endregion
        #region 数据属性
        public string VideoPath { get; set; }
        public List<string> VideoList//视频文件列表
        {
            get { return videoList; }
            set
            {
                videoList = value;
                RaisePropertyChanged("VideoList");
            }
        }
        public DateTime SearchDate//搜索日期
        {
            get { return searchDate; }
            set
            {
                searchDate = value;
                RaisePropertyChanged("SearchDate");
            }
        }
        public string PlayfileName//播放视频对象
        {
            get { return playfileName; }
            set
            {
                playfileName = value;
                RaisePropertyChanged("PlayfileName");
            }
        }
        public string SelectVideo//要播放的视频路径
        {
            get { return selectVideo; }
            set
            {
                selectVideo = value;
                RaisePropertyChanged("SelectVideo");
            }
        }
        public double SliderValue//滑块值
        {
            get { return sliderValue; }
            set
            {
                sliderValue = value;
                RaisePropertyChanged("SliderValue");
            }
        }
        public string CurrentTime
        {
            get { return currentTime; }
            set
            {
                currentTime = value;
                RaisePropertyChanged("CurrentTime");
            }
        }
        public string TotalTime
        {
            get { return totalTime; }
            set
            {
                totalTime = value;
                RaisePropertyChanged("TotalTime");
            }
        }
        public string PlayImage
        {
            get { return playImage; }
            set
            {
                playImage = value;
                RaisePropertyChanged("PlayImage");
            }
        }


        #endregion
        #region 命令对象

        #endregion
        #region 命令属性
        public DelegateCommand SearchCommand { get; set; }//搜索视频
        public DelegateCommand SearchAllCommand { get; set; }
        public DelegateCommand PlayVideoCommand { get; set; }
        #endregion
        #region 构造函数
        public ReplayView(Window window, ReplayUtil replayUtil, VideoUtil videoUtil)
        {
            this.videoUtil = videoUtil;
            this.VideoPath = replayUtil.ReplayPath;
            GetFile();
            this.SearchCommand = new DelegateCommand(Search);
            this.SearchAllCommand = new DelegateCommand(SearchAll);
            timer = new Timer();//时间函数
            timer.Interval = 50;
            timer.Elapsed += new ElapsedEventHandler(Time_Elapsed);
            timer.Start();
            PlayImage = "Image/Play.png";

        }
        #endregion
        #region 命令
        private void Search()
        {
            searchNameList = new List<string>();
            foreach (string video in fileNameList)
            {
                if (video.Contains(searchDate.Date.ToShortDateString().Trim()))//根据日期查找
                {
                    searchNameList.Add(video);
                }

            }
            this.VideoList = searchNameList;


        }
        private void SearchAll()
        {
            this.VideoList = fileNameList;

        }
        #endregion
        #region 函数
        private void GetFile()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(this.VideoPath);
            FileInfo[] fileList = directoryInfo.GetFiles();

            foreach (FileInfo file in fileList)
            {
                string fileExtenction = Path.GetExtension(file.FullName);
                if (videoType.Contains(fileExtenction))
                {
                    fileNameList.Add(file.Name);
                    fileInfoList.Add(file);
                }

            }
            this.VideoList = fileNameList;

        }
        private void Time_Elapsed(object sender, ElapsedEventArgs e)//时间函数
        {
            SliderValue = videoUtil.ReturnValue();
            CurrentTime = TimeSpan.FromSeconds((int)videoUtil.current.TotalSeconds).ToString();
            TotalTime = TimeSpan.FromSeconds((int)videoUtil.duration.TotalSeconds).ToString();

        }
        #region 视频控制
        public void MediaPlay()
        {
            videoUtil.PlayVideo(VideoPath + "/" + SelectVideo);
            timer.Start();
            PlayImage = "Image/Pause.png";

        }
        public void MediaControl()
        {
            if (!pauseFlag)
            {
                videoUtil.PauseVideo();
                pauseFlag = true;
                PlayImage = "Image/Play.png";
            }
            else
            {
                videoUtil.ResumeVideo();
                pauseFlag = false;
                PlayImage = "Image/Pause.png";
            }

        }
        public void MediaSet()
        {
            videoUtil.SetVideo(SliderValue / 100);
        }
        public void StopTick()
        {
            timer.Stop();
        }
        public void StartTick()
        {
            timer.Start();
        }


        #endregion




        #endregion

    }
}
