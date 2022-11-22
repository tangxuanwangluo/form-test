using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 后端路由
/// </summary>
namespace 整体窗体测试项目
{
    public class WebSiteInfoApi
    {
        /// <summary>
        /// 用于接收所有字段的公共接口
        /// </summary>
        public static string H1 { set; get; }
        /// <summary>
        /// 获取IP地址接口
        /// </summary>
        public static string Ipaddress { set; get; }
        /// <summary>
        /// 用于接收所有错误的公共错误信息接口
        /// </summary>
        public static string Exoptions { set; get; }
        /// <summary>
        /// 获取网站标题接口
        /// </summary>
        public static string WebInfotitles { set; get; }
        /// <summary>
        /// 用于获取网站服务接口
        /// </summary>
        public static string WebServiceInfo { set; get; }
    }
}
