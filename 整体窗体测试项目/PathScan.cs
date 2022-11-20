using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace 整体窗体测试项目
{
    public class PathScan
    {
        public static string WebPathScan(string scan)
        {
            scan = null;
            try
            {
                HttpWebRequest res = (HttpWebRequest)WebRequest.Create(PortScanApi.Purl + PortScanApi.PurlPath);//获取URL及接收路径
                res.Method = "GET";
                HttpWebResponse respone = (HttpWebResponse)res.GetResponse();
                StreamReader red = new StreamReader(respone.GetResponseStream());
                PortScanApi.HtmlCode = red.ReadToEnd();
            }
            catch(Exception ex)
            {
                PortScanApi.ExOptions = ex.ToString();
            }

            return scan;
        }
        public static string PathPassword(string pass)
        {
            for (int i = 0;i <= 1000; i++)
            {
                string s1 = i.ToString();
                PortScanApi.PurlPath = s1.ToString();
            }
            return pass;
        }
        public static string IpaddressINfo(string ipinfo)
        {
            try
            {
                System.Threading.Thread.Sleep(500);
                string url = PortScanApi.Purl;//获取域名
                UriBuilder ub = new UriBuilder(url);
                string host = (ub.Host.Replace("https://", "http://"));//去掉http://
                IPAddress[] addresslist = Dns.GetHostAddresses(host);
                foreach (IPAddress theaddress in addresslist)
                {
                    PortScanApi.IPaddressInfos = theaddress.ToString();
                    //AppLog.WriteInfo("IP地址：" + "\r\n" + theaddress.ToString(), true);
                    //Console.WriteLine("IP地址：" + "\r\n" + theaddress.ToString());
                    //Console.WriteLine(linkLabel5.Text = theaddress.ToString());
                    //LogMessage("创建时间：" + DateTime.Now + "   -------   " + "正在获取IP地址" + "\r\n");
                }
            }
            catch (Exception ex)
            {
                PortScanApi.ExOptions = ex.ToString();
                //AppLog.WriteError("错误提示：" + "\r\n" + "时间：" + DateTime.Now + "\r\n" + ex, true);
                //MessageBox.Show("错误提示：" + "\r\n" + ex);
            }
            return ipinfo;
        }
        public static string S1info(string s1)
        {
            Form1 s2 = new Form1();
            s2.Test3();
           // string s3 = "123456";
           //s3 = Form1.Test(PortScanApi.S1);
            return s1;
        }
    }
}
