using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 整体窗体测试项目
{
    /// <summary>
    /// 密码破解所有接口信息
    /// </summary>
    public class PassWordCrackApi
    {
        /// <summary>
        /// 所有密码破解相关的公共接口
        /// </summary>
        public static string P1 { set; get; }
        /// <summary>
        /// 全局连接SQL数据库
        /// </summary>
        public static string SqlClient { set; get; }
        /// <summary>
        /// 获取ip地址信息
        /// </summary>
        public static string IpAddress { set; get; }
        /// <summary>
        /// 获取域名信息
        /// </summary>
        public static string DomAinUrl { set; get; }
        /// <summary>
        /// 获取单独端口扫描的端口信息
        /// </summary>
        public static string PortOne { set; get; }
        /// <summary>
        /// 获取端口描述信息
        /// </summary>
        public static string PortNotie { set; get; }
        /// <summary>
        /// 执行所有SQL命令公共接口
        /// </summary>
        public static string SqlCmd { set; get; }
        /// <summary>
        /// 获取数据库账号
        /// </summary>
        public static string UserName { set; get; }
        /// <summary>
        /// 获取数据库密码
        /// </summary>
        public static string PassWord { set; get; }
        /// <summary>
        /// 获取所有错误信息
        /// </summary>
        public static string ExOptions { set; get; }
        /// <summary>
        /// 用于接收所有错误信息，并返回错误信息的值
        /// </summary>
        public static string ExOptionsInfo { set; get; }
    }
}
