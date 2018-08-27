using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Runtime.InteropServices;
using System.Threading;
using QQVideo.Utils.Interface;
using QQVideo.Utils;
using System.Collections;

namespace QQVideo.Utils
{
    public class QQUtil
    {

        private Process qqProcess;
        private IRegistryKeyUtil registryUtil=new RegistryKeyUtil();
        private string key = "QQPath";
        private string application = "QQVideo";
        public string QQPath { get; set; }//QQ路径
                                          //WM_SYSKEYDOWN = 0x0104;
        public void GetRegistryKey()//取得路径
        {
            Dictionary<string, string> dic = registryUtil.GetKey(application, key);
            if (dic.Count > 0)
            {
                QQPath = dic[key];
            }
            else
            {
                //MessageBox.Show("找不到QQ的路径，请重新设置!");
            }
        }


        public void SetRegistryKey()//设置路径
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            fileDialog.Filter = "快捷方式|*.lnk";
            fileDialog.Title = "选择QQ快捷方式";
            if ((bool)fileDialog.ShowDialog())
            {
                registryUtil.SetKey(application, key, fileDialog.FileName);
                QQPath = fileDialog.FileName;
            }
        }

        public void StartProcess()//启动进程
        {
            try
            {
                qqProcess = Process.Start(QQPath);//启动程序
            }
            catch {
                MessageBox.Show("启动失败，请检查QQ路径!");
            }
              
        }
  
        public void CloseProcess()
        {
            try
            {
                Process[] processList = Process.GetProcessesByName("QQ");
                foreach (Process process in processList)
                {
                    process.Kill();
                }
              
            }
            catch { }
            
        }
        public void SendKey()
        {



        }





    }
}
