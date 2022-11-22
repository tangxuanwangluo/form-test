using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace 整体窗体测试项目
{
    public  class Houduan
    {
        /// <summary>
        /// 引用测试数据
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        public static string Honkon(string test)
        {
            try
            {
                string url = HouduanAPi.Curl;//需要从接口中获取
                //这里采用爬虫的方式去进行请求
                HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)httpReq.GetResponse();
                var v = response.StatusCode;//初始化状态
                if (v == HttpStatusCode.OK)
                {
                    string s3 = "网站可访问";
                    HouduanAPi.Status = s3;
                }
                    //string url = PortScanApi.Purl;//获取域名
                    //UriBuilder ub = new UriBuilder(url);
                    //string host = (ub.Host.Replace("https://", "http://"));//去掉http://
                    //IPAddress[] addresslist = Dns.GetHostAddresses(host);
                    //foreach (IPAddress theaddress in addresslist)
                    //{
                    //    HouduanAPi.Status = theaddress.ToString();
                    //    //AppLog.WriteInfo("IP地址：" + "\r\n" + theaddress.ToString(), true);
                    //    //Console.WriteLine("IP地址：" + "\r\n" + theaddress.ToString());
                    //    //Console.WriteLine(linkLabel5.Text = theaddress.ToString());
                    //    //LogMessage("创建时间：" + DateTime.Now + "   -------   " + "正在获取IP地址" + "\r\n");
                    //}
                }
            catch (Exception ex)
            {
                HouduanAPi.ExOptions = ex.ToString();
            }
            return test;
        }
    }
}
