using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Windows;

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
        public static extern bool  IsWindowEnabled(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        #endregion
        public static string test = "";
        public static IntPtr MyHandle { get; set; } = IntPtr.Zero;//句柄
        public static string MySoftware { get; set; }//进程id
        public static IntPtr FindWindow(string software)
        {

            Container container = new Container(MyFindCall);
            MySoftware = software;
            EnumWindows(container, 0);
            MessageBox.Show(test);
            return MyHandle;

        }
        private static bool MyFindCall(IntPtr hwnd,int lParam)
        {
            StringBuilder sb = new StringBuilder(512);                
            GetWindowText(MyHandle, sb, sb.Capacity);
            if (IsWindow(hwnd))
            {
                test += sb + "\r\n";
            }
     
            return true;
        }

    }

}
