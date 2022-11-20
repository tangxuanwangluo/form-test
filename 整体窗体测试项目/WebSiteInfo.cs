using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace 整体窗体测试项目
{
    public class WebSiteInfo
    {
        /// <summary>
        /// 获取IP地址信息
        /// </summary>
        /// <param name="ipinfo"></param>
        /// <returns></returns>
        public static string WebSiteInfos(string ipinfo)
        {
            try
            {
                string url = PortScanApi.Purl;//获取域名
                UriBuilder ub = new UriBuilder(url);
                string host = (ub.Host.Replace("https://", "http://"));//去掉http://
                IPAddress[] addresslist = Dns.GetHostAddresses(host);
                foreach (IPAddress theaddress in addresslist)
                {
                    WebSiteInfoApi.Ipaddress = theaddress.ToString();
                    //AppLog.WriteInfo("IP地址：" + "\r\n" + theaddress.ToString(), true);
                    //Console.WriteLine("IP地址：" + "\r\n" + theaddress.ToString());
                    //Console.WriteLine(linkLabel5.Text = theaddress.ToString());
                    //LogMessage("创建时间：" + DateTime.Now + "   -------   " + "正在获取IP地址" + "\r\n");
                }
            }
            catch (Exception ex)
            {
                WebSiteInfoApi.Exoptions = ex.ToString();
                //AppLog.WriteError("错误提示：" + "\r\n" + "时间：" + DateTime.Now + "\r\n" + ex, true);
                //MessageBox.Show("错误提示：" + "\r\n" + ex);
            }
            return ipinfo;
        }
        /// <summary>
        /// 获取网站标题名称信息
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public static string WebSiteTitleInfo(string title)
        {
            try
            {
                string url = PortScanApi.Purl;
                WebRequest res = WebRequest.Create(url);
                WebResponse respone = res.GetResponse();
                StreamReader red = new StreamReader(respone.GetResponseStream());
                string html = red.ReadToEnd();
                Match m1 = Regex.Match(html, "<title>(.*)</title>");
                //获取名称
                //linkLabel3.Text = m1.Groups[1].Value;
                //Console.WriteLine("信息提示：" + "---------------     " + "\r\n" + m1.Groups[1].Value + "请求时间：" + DateTime.Now);
                //Console.WriteLine("网站标题：" + "\r\n" + m1.Groups[1].Value);
                WebSiteInfoApi.WebInfotitles = m1.Groups[1].Value;
                //Console.WriteLine(linkLabel3.Text = m1.Groups[1].Value);
                //LogMessage("创建时间：" + DateTime.Now + "   -------   " + "正在获取网站标题" + "\r\n");
                //label6.Text = "25%";
                //progressBar1.Value = 25;
                //RquestCount++;
                //toolStripStatusLabel8.Text = RquestCount.ToString();
                //progressBar1.Maximum = 25;
            }

            catch (Exception ex)
            {
                WebSiteInfoApi.Exoptions = ex.ToString();
                //AppLog.WriteError("错误提示：" + "\r\n" + "时间：" + DateTime.Now + "\r\n" + ex, true);
                //MessageBox.Show("错误提示：" + "\r\n" + ex);
            }
            return title;
        }
        /// <summary>
        /// 获取网站服务名称
        /// </summary>
        /// <param name="server"></param>
        /// <returns></returns>
        public static string WebSiteServiceInfo(string server)
        {
            try
            {
                string url = PortScanApi.Purl;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "HEAD";
                request.AllowAutoRedirect = false;
                HttpWebResponse reponse = (HttpWebResponse)request.GetResponse();
                string cc = reponse.GetResponseHeader("Server");
                WebSiteInfoApi.WebServiceInfo = cc;
                //Console.WriteLine(linkLabel4.Text = cc);
                //progressBar1.Maximum = 30;
            }
            catch (Exception ex)
            {
                WebSiteInfoApi.Exoptions = ex.ToString();
                AppLog.WriteError(ex.ToString(), true);
                //AppLog.WriteError("错误提示：" + "\r\n" + "时间：" + DateTime.Now + "\r\n" + ex, true);
                //MessageBox.Show("错误提示：" + "\r\n" + ex);
            }
            return server;
        }
    }
}
