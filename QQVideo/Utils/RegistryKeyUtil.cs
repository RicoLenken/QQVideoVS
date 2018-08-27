using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using QQVideo.Utils.Interface;
namespace QQVideo.Utils
{
    public class RegistryKeyUtil : IRegistryKeyUtil
    {
        public void SetKey(string application,string key,string value)
        {
            using (RegistryKey software = Registry.CurrentUser.OpenSubKey("SOFTWARE", true))
            {
                RegistryKey appKey = software.CreateSubKey(application);
                appKey.SetValue(key, value);
            }

        }
        public Dictionary<string, string> GetKey(string application,string key )
        {
            Dictionary<string, string> keyValue = new Dictionary<string, string>();
            using (RegistryKey software = Registry.CurrentUser.OpenSubKey("SOFTWARE"))
            {
                RegistryKey appKey = software.OpenSubKey(application);
               
                try
                {
                    keyValue.Add(key, appKey.GetValue(key).ToString());
                }
                catch
                {
                    
                }

            }
            return keyValue;
        }
    }
}
