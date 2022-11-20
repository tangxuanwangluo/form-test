using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace 整体窗体测试项目
{
    class SpriderInfo
    {
        /// <summary>
        /// Sec-scan 第一层网络爬虫
        /// </summary>
        /// <param name="spider"></param>
        /// <returns></returns>
        public static string SpiderScan(string spider)
        {
            string url = SpriderInfoApi.Urls;//获取爬虫地址
            try
            {
                HttpWebRequest res = (HttpWebRequest)WebRequest.Create(url);
                //res.Headers.Add("Cookie", "ASP.NET_SessionId=sesc5hfpgdxbxdpbqqtzmzoq");
                res.Method = "Get";
                res.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.5249.62 Safari/537.36";
                res.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
                HttpWebResponse respone = (HttpWebResponse)res.GetResponse();
                StreamReader red = new StreamReader(respone.GetResponseStream());
                string html = red.ReadToEnd();
                SpriderInfoApi.UrlsHtmlCode = html;
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(SpriderInfoApi.UrlsHtmlCode);
                HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//a/@href");//连同js一同提取出来 |//script
                HtmlNode node;
                for (int i = 0; i < nodes.Count; i++)
                {
                    node = nodes[i];
                    var attrList = node.Attributes;
                    foreach (var attr in attrList)
                    {
                        string s2 = attr.Value;
                        string s3 = attr.ValueLength.ToString();
                        //UrlsTr 提取域名
                        if (regex.UrlsTr(s2.ToString()))
                        {
                            //输出域名格式的，并进行排除,这里加入了爬取js文件
                            //Console.WriteLine(attr.Value); 
                            //AppLog.WriteInfo("爬虫信息：未在规则内" + url + attr.Value, true);
                            SpriderInfoApi.UrlsInfo = url + attr.Value;
                            //LogMessage("创建时间：" + DateTime.Now + "   -------   " + "正在爬取：" + url + attr.Value + "\r\n");
                            //dataGridView4.Rows.Add(url, attr.Value, s3);
                        }
                        else
                        {
                            if (regex.UrlPath(s2.ToString()))
                            {
                                SpriderInfoApi.UrlsInfo = url + attr.Value;
                                //提取接口
                                //AppLog.WriteInfo("爬虫信息：" + url + attr.Value, true);
                                //Console.WriteLine(url + attr.Value);
                                //LogMessage("创建时间：" + DateTime.Now + "   -------   " + "正在爬取：" + url + attr.Value + "\r\n");
                                //dataGridView4.Rows.Add(url, attr.Value, s3);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SpriderInfoApi.UrlsExoptions = ex.ToString();
                //AppLog.WriteError("爬虫错误：" + ex.ToString(), true);//记录错误日志
                //Console.WriteLine(ex.ToString());//控制台输出报错
            }
            return spider;
        }
    }
}
