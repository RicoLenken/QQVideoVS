using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;

namespace QQVideo.Utils
{


    public delegate bool Container(IntPtr hwnd, int lParam);  //回调函数容器
    public class WindowUtil
    {
        #region 导入函数
        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        public static extern bool SendMessage(IntPtr hWnd, Int32 msg, Int32 wParam, Int32 lParam);
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")] 
        public static extern int EnumWindows(Container x, int y);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);

        [DllImport("User32.dll")]
        public static extern int GetWindowText(IntPtr WinHandle, StringBuilder Title, int size);

        [DllImport("user32.dll")]
        public static extern bool IsWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool IsWindowEnabled(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);
   

        public static void F11Clic()
        {
            //模拟F11按键
            keybd_event(0x7A, 0,0, 0); //按下F11
            keybd_event(0x7A, 0, 2, 0); //按下后松开F11
        }

        #endregion
        public static string test ;
        public static int MyId =0;//进程id
        public static List<IntPtr> MyHandleList { get; set; } = new List<IntPtr>(); //句柄
        public static string MySoftware { get; set; }
        public static List<IntPtr> FindWindow(int id,string software)
        {

            Container container = new Container(MyFindCall);
            MyId = id;
            MySoftware = software;
            EnumWindows(container, 0);
            MessageBox.Show(test);
            return MyHandleList;

        }
        private static bool MyFindCall(IntPtr hwnd,int lParam)
        {
            int thisId = 0;
            GetWindowThreadProcessId(hwnd, out thisId);
            if (thisId==MyId)
            {
                MyHandleList.Add(hwnd);         
            }

            return true;
        }

    }

}
