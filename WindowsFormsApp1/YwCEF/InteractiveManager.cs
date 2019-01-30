using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;

namespace YwCEF
{
    public class InteractiveManager
    {
        /// <summary>
        /// 注意，js中调用时要小写首字母 = im.vehicleForm
        /// </summary>
        /// <param name="content"></param>
        /// <param name="title"></param>
        public void VehicleForm(string title)
        {
            MessageBox.Show("假设现在调用了登录，弹出车辆信息窗口", title);
        }

        public string GetHphm()
        {
            return "渝Buy103";
        }
    }
}