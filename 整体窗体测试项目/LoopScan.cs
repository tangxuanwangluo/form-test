using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace 整体窗体测试项目
{
    /// <summary>
    /// 漏洞扫描方法
    /// </summary>
    public class LoopScan
    {
        /// <summary>
        /// 漏洞扫描具体实现
        /// </summary>
        /// <param name="loop"></param>
        /// <returns></returns>
        public static string LoopScanApp(string loop)
        {
            string url = "https://www.cqdhxk.com/";//获取扫描地址
            string loops = "";//获取本地漏洞数据库
            SqliteConnection vs = new SqliteConnection(loops);
            vs.Open();
            try
            {
                if(vs.State == System.Data.ConnectionState.Open)
                {
                    string sql = "";//执行SQL命令的地方
                    SqliteCommand command = new SqliteCommand(sql, vs);
                    using(SqliteDataReader dr = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        while (dr.Read())
                        {
                            string s1 = "/index/index/about.html";//加载有效载荷
                            HttpWebRequest res = (HttpWebRequest)WebRequest.Create(url + s1);
                            res.Method = "GET";
                            res.Method = "GET";//获取请求方式
                            res.Accept = "*/*";
                            res.ServicePoint.Expect100Continue = false;//加快载入速度
                            res.ServicePoint.UseNagleAlgorithm = false;//禁止nagle算法加快载入速度
                            res.AllowWriteStreamBuffering = false;//禁止缓冲加快载入速度
                            res.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");//定义gzip压缩页面支持
                            res.ContentType = "application/x-www-form-urlencoded";//定义文档类型及编码
                            res.AllowAutoRedirect = false;//禁止自动跳转
                            res.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.102 Safari/537.36";
                            res.Timeout = 2000;//请求超时
                            res.KeepAlive = true;//启用长连接
                            res.ServicePoint.ConnectionLimit = int.MaxValue;//定义最大连接数
                            HttpWebResponse response = (HttpWebResponse)res.GetResponse();
                            Stream receiveStream = response.GetResponseStream();
                            StreamReader readStream = new StreamReader(receiveStream, Encoding.GetEncoding("UTF-8"));//设置网站编码
                            string html = readStream.ReadToEnd();//获取源代码
                            if(html.Length < 1)
                            {
                                //如果没有获取到解析的源代码情况下，表示直接无法继续并给一个值进行输出
                                Console.WriteLine("无法解析、无法获取到源代码！");
                            }
                            string s;
                            s = html;//把解析的源码传值给S

                            string key = "";//需要匹配的关键词
                            for(int i = 0; i < 1; i++)
                            {
                                if (System.Text.RegularExpressions.Regex.IsMatch(s, key))
                                {
                                    if(html.Length >= 1)
                                    {
                                        //表示存在漏洞
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);//控制所有错误信息输出
            }
            return loop;

        }
    }
}
