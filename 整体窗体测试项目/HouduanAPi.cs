using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 整体窗体测试项目
{
    public class HouduanAPi
    {
        /// <summary>
        /// 定义公共接收实参的方法
        /// </summary>
        public static string A1 { set; get; }
        /// <summary>
        /// 用于接收获取URL域名地址
        /// </summary>
        public static string Curl { set; get; }
        /// <summary>
        /// 获取状态
        /// </summary>
        public static string Status { set; get; }
        /// <summary>
        /// 用于接收返回的所有错误信息
        /// </summary>
        public static string ExOptions { set; get; }
    }
}
