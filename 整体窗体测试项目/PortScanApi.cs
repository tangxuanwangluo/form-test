using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 整体窗体测试项目
{
    public class PortScanApi
    {
        public static string S1 { set; get; }//测试
        public static string Url { set; get; }//url地址
        public static string ExOptions { set; get; }//接收错误信息
        public static string Purl { set; get; }//接收后台扫描URL地址
        public static string HtmlCode { set; get; }//接收HTMl解析的源代码
        public static string PurlPath { set; get; }//用于接收后台路径
        public static string IPaddressInfos { set; get; }//接收IP地址信息
    }
}
