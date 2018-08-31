using Microsoft.Win32;
using QQVideo.Utils.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQVideo.Utils
{
    public class ReplayUtil
    {
        private IRegistryKeyUtil registryUtil = new RegistryKeyUtil();
        private string key = "ReplayPath";
        private string application = "QQVideo";
        public string ReplayPath { get; set; }//录像回放路径

        public void GetRegistryKey()//取得路径
        {
            Dictionary<string, string> dic = registryUtil.GetKey(application, key);
            if (dic.Count > 0)
            {
                ReplayPath = dic[key];
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
            fileDialog.Filter = "文件夹|*.*";
            fileDialog.Title = "选择回放路径文件夹";
            if ((bool)fileDialog.ShowDialog())
            {
                registryUtil.SetKey(application, key, fileDialog.FileName);
                ReplayPath = fileDialog.FileName;
            }
        }


    }
}
