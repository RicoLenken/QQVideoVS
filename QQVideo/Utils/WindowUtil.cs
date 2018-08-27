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


        [DllImport("user32.dll")]
        public static extern void ShowWindow(IntPtr hWnd,int nCmdShow);

        [DllImport("user32.dll")]
        public static extern void GetWindowRect(IntPtr hWnd, out Rect rect);

        [DllImport("user32.dll")]
        public static extern void GetClientRect(IntPtr hWnd, out Rect rect);


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
        public static List<IntPtr> FindWindow(int id)
        {

            Container container = new Container(MyFindCall);
            MyId = id;
            EnumWindows(container, 0);
            MessageBox.Show(test);
            return MyHandleList;

        }
        private static bool MyFindCall(IntPtr hwnd,int lParam)
        {
            int thisId = 0;
            GetWindowThreadProcessId(hwnd, out thisId);
            if (thisId==MyId && IsWindow(hwnd) &&IsWindowEnabled(hwnd) && IsWindowVisible(hwnd))
            {
                StringBuilder sb = new StringBuilder(512);
                GetWindowText(hwnd, sb, sb.Capacity);
                test += sb.ToString()+"\n";
                MyHandleList.Add(hwnd);         
            }

            return true;
        }
      
      
        public void ClickOneKey(byte key)
        {
            keybd_event(key, 0, 0, 0);
            keybd_event(key, 0, 2, 0);
        }

        public void ClickThreeKey(byte key1,byte key2,byte key3)
        {
            keybd_event(key1, 0, 0, 0);
            keybd_event(key2, 0, 0, 0);
            keybd_event(key3, 0, 0, 0);
            keybd_event(key3, 0, 2, 0);
        
        }
        public void ReleaseTwoKey(byte key1,byte key2)
        {
            keybd_event(key1, 0, 2, 0);
            keybd_event(key2, 0, 2, 0);
        }


    }

}
