﻿using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using QQVideo.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QQVideo.View
{
    public class ReplayView:NotificationObject
    {
        #region 对象
        #endregion
        #region 数据对象
        private List<string> videoList;
        private string videoPath;
        private DateTime searchDate =DateTime.Now;
        #endregion
        #region 数据属性
        public List<string> VideoList
        {
            get { return videoList; }
            set { videoList = value;
                RaisePropertyChanged("VideoList");
            }
        }

        public DateTime SearchDate
        {
            get { return searchDate; }
            set { searchDate = value;
               RaisePropertyChanged("SearchDate") ;
            }
        }

        #endregion
        #region 命令对象

        #endregion
        #region 命令属性
        public DelegateCommand SearchCommand { get; set; }//搜索视频
        #endregion
        #region 构造函数
        public ReplayView(Window window, ReplayUtil replayUtil)
        {
            this.videoPath = replayUtil.ReplayPath;
            GetFile();
            this.SearchCommand = new DelegateCommand(Search);
        }
        #endregion
        #region 命令
        private void Search()
        {



        }
        #endregion
        #region 函数
        private void GetFile()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(this.videoPath);
            FileInfo[] fileList = directoryInfo.GetFiles();
            List<string> fileNameList = new List<string>();
            foreach (FileInfo file in fileList)
            {
                fileNameList.Add(file.Name);
                
            }
            this.VideoList = fileNameList;

        }
        #endregion

    }
}
