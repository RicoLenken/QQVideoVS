using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QQVideo.Utils.Interface;
using QQVideo.Utils;
using System.Windows;
using System.Threading;

namespace QQVideo.Utils
{
    public class BandicamUtil
    {
        private WindowUtil windowUtil = new WindowUtil();
        private Process bandicamProcess;
        private Process[] processList;
        private string key = "BandicamPath";
        private IRegistryKeyUtil registryUtil = new RegistryKeyUtil();
        private string application = "QQVideo";
        public string BandicamPath { get; set; }//Bandicam路径

        public void GetRegistryKey()//取得路径
        {
            Dictionary<string, string> dic = registryUtil.GetKey(application, key);
            if (dic.Count > 0)
            {
                BandicamPath = dic[key];
            }
            else
            {
            }
        }
        public void SetRegistryKey()//设置路径
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            fileDialog.Filter = "快捷方式|*.lnk";
            fileDialog.Title = "选择Bnadicam快捷方式";
            if ((bool)fileDialog.ShowDialog())
            {
                registryUtil.SetKey(application, key, fileDialog.FileName);
                BandicamPath = fileDialog.FileName;
            }
        }

        public void StartProcess()
        {
            if (StartCheck())
            {
                try
                {
                    bandicamProcess = Process.Start(BandicamPath);//启动程序
                }
                catch
                {
                    MessageBox.Show("启动失败，请检查Bandicam路径!");
                }
            }
        }

        public void CloseProcess()
        {
            try
            {
                Process process = Process.GetProcessById(bandicamProcess.Id);
                process.Kill();
                this.bandicamProcess = null;

            }
            catch { }


        }
        public void SetScreen()
        {
            windowUtil.ClickThreeKey(keyCode.vbKeyControl, keyCode.vbKeyAlt, keyCode.vbKeyH);
            Thread.Sleep(100);//不能同时释放
            windowUtil.ReleaseTwoKey(keyCode.vbKeyControl, keyCode.vbKeyAlt);
        }


        public void StartRec()
        {

            windowUtil.ClickOneKey(keyCode.vbKeyF12);

        }
        public void StopRec()
        {


            windowUtil.ClickOneKey(keyCode.vbKeyF12);


        }
        public bool RecCheck()
        {
            processList = Process.GetProcessesByName("bdcam");
            if (processList ==null || !(processList.Length>0))
            {
                MessageBox.Show("请先启动录屏软件");
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool StartCheck()
        {
            processList = Process.GetProcessesByName("bdcam");
            if (processList == null || !(processList.Length > 0))
            {
                return true;
            }
            else
            {
                MessageBox.Show("录屏软件已启动，请开始录像！");
                return false;
            }
        }
    }
}
