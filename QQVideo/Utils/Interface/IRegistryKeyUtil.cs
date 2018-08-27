using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQVideo.Utils.Interface
{
    public interface IRegistryKeyUtil
    {
       void SetKey(string application, string key, string value);//设置内容
        Dictionary<string, string> GetKey(string application, string key);//获得内容
    }
}
