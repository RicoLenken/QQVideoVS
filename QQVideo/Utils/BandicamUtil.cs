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
        private bool playFlag = false;
        private WindowUtil windowUtil = new WindowUtil();
        public Process bandicamProcess;
        private string key= "BandicamPath";
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
            try
            {
                bandicamProcess = Process.Start(BandicamPath);//启动程序
            }
            catch
            {
                MessageBox.Show("启动失败，请检查Bandicam路径!");
            }

        }

        public void CloseProcess()
        {
            try
            {
                Process process = Process.GetProcessById(bandicamProcess.Id);
                process.Kill();            

            }
            catch { }

        }
        public void SetScreen()
        {
            windowUtil.ClickThreeKey(keyCode.vbKeyControl,keyCode.vbKeyAlt,keyCode.vbKeyH);
            Thread.Sleep(100);//不能同时释放
            windowUtil.ReleaseTwoKey(keyCode.vbKeyControl, keyCode.vbKeyAlt);
        }


        public void StartRec()
        {
            if (!playFlag)
            {
                windowUtil.ClickOneKey(keyCode.vbKeyF12);
                playFlag = true;
            }
          

        }
        public void StopRec()
        {
            if (playFlag)
            {
                windowUtil.ClickOneKey(keyCode.vbKeyF12);
                playFlag = false;
            }

        }



    }
}
