using QQVideo.Utils.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.Description = "请选择录像文件所在的文件夹！";
            if (folderDialog.ShowDialog()==DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(folderDialog.SelectedPath))
                {

                    registryUtil.SetKey(application, key, folderDialog.SelectedPath);
                    ReplayPath = folderDialog.SelectedPath;

                }
                else
                { MessageBox.Show("文件夹不能为空！"); }
             
            }
        }


    }
}
