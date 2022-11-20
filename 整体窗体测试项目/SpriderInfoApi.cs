using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 整体窗体测试项目
{
    public class SpriderInfoApi
    {
        /// <summary>
        /// 用于接收所有数据的公共方法
        /// </summary>
        public static string A1 { set; get; }
        /// <summary>
        /// 爬虫地址
        /// </summary>
        public static string Urls { set; get; }
        /// <summary>
        /// 用于接收爬虫结果信息
        /// </summary>
        public static string UrlsInfo { set; get; }
        /// <summary>
        /// 用于接收在爬虫过程中的错误信息
        /// </summary>
        public static string UrlsExoptions { set; get; }
        /// <summary>
        /// 用于接收已经获取到的源代码
        /// </summary>
        public static string UrlsHtmlCode { set; get; }
    }
}
