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

namespace QQVideo.Utils
{
    public class QQUtil
    {
       


        public string QQPath { get; set; }//QQ路径
        public IntPtr QQHandle { get; set; }//句柄
        private const int f11 = 122;
        private const int f12 = 123;
        private static int WM_KEYDOWN = 0x0100;
        private static int WM_KEYUP = 0x0101;
        public void GetQQPath()//获得qq的快捷方式
        {

            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "快捷方式|*.lnk";
            fileDialog.Multiselect = false;
            if ((bool)fileDialog.ShowDialog())
            {
                QQPath = fileDialog.FileName;
                Process qqProcess = Process.Start(QQPath);//启动程序
                Thread.Sleep(5000);
                MessageBox.Show("程序进程是"+qqProcess.Id.ToString());
                QQHandle = WindowUtil.FindWindow("注册");
            }

        }

       
        public void SendKey() 
        {
            //MessageBox.Show(QQHandle.ToString());
            StringBuilder sb = new StringBuilder(512);
            WindowUtil.GetWindowText(QQHandle, sb, sb.Capacity);
            MessageBox.Show(sb.ToString());



        }


        private void GetQQRegistryKey()//尝试从注册表中找到QQ快捷方式路径
        {


        }
        private void SetQQRegistryKey()//将QQ快捷方式写入注册表
        {
           

        } 



    }
}
