using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace QQVideo.Utils
{
    public class HotKeyUtil
    {

        #region 引入
        /// <summary>
        /// 注册热键
        /// </summary>
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint controlKey, uint virtualKey);

        /// <summary>
        /// 注销热键
        /// </summary>
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        /// <summary>
        /// 向原子表中添加全局原子
        /// </summary>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern short GlobalAddAtom(string lpString);

        /// <summary>
        /// 在表中搜索全局原子
        /// </summary>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern short GlobalFindAtom(string lpString);

        /// <summary>
        /// 在表中删除全局原子
        /// </summary>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern short GlobalDeleteAtom(string nAtom);

        #endregion
        #region 对象
        private  IntPtr handle;
        private Window window;
        public delegate void OnHotkeyEventHandeler();           //热键事件委托
        public event OnHotkeyEventHandeler OnHotKey = null;   //热键事件    
        static Hashtable KeyPair = new Hashtable();             //热键哈希表
        private const int WM_HOTKEY = 0x0312;       // 热键消息编号
        #endregion

        public enum KeyModifiers //组合键枚举  

        {

            MOD_ALT = 0x1,
            MOD_CONTROL = 0x2,
            MOD_SHIFT = 0x4,
            MOD_WIN = 0x8,
            MOD_CONTROLALT = 0x2 + 0x1

        }


        public HotKeyUtil(Window window,KeyModifiers control,uint key)
        {
            handle = new WindowInteropHelper(window).Handle;
            int keyId = (int)control + (int)key * 10;
            RegisterHotKey(handle, keyId, (uint)control, key);
            InstallHotKeyHook(this);
            KeyPair.Add(keyId,this);

        }


        static private bool InstallHotKeyHook(HotKeyUtil hk)//安装热键处理挂钩
        {
           

            //获得消息源
             HwndSource source = HwndSource.FromHwnd(hk.handle);

            //挂接事件
            source.AddHook(HotKeyUtil.HotKeyHook);
            return true;
        }

        static private IntPtr HotKeyHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)//热键处理过程
        {
            if (msg == WM_HOTKEY)
            {
                HotKeyUtil hk = (HotKeyUtil)KeyPair[(int)wParam];
                if (hk.OnHotKey != null)
                { hk.OnHotKey(); }
            }
            return IntPtr.Zero;
        }









    }
}
