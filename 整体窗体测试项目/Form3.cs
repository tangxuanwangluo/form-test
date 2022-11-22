using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace 整体窗体测试项目
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;

        }
        public Thread th;
        private void button1_Click(object sender, EventArgs e)
        {
            //创建一个线程去爬取所有图片
            th = new Thread(ImagesCra);
            th.Start();
        }
        public void ImagesCra()
        {
            string url = "https://www.cqdhxk.com";
            try
            {
                HttpWebRequest res = (HttpWebRequest)WebRequest.Create(url);
                res.Method = "GET";

                HttpWebResponse respone = (HttpWebResponse)res.GetResponse();
                StreamReader red = new StreamReader(respone.GetResponseStream());
                string html = red.ReadToEnd();
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);
                HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//a/@href");
                ///html/body//script/@src
                ////img/@src
                // //a/@href
                HtmlNode node;
                for(int i = 0;i < nodes.Count; i++)
                {
                    node = nodes[i];
                    var attrlist = node.Attributes;
                    foreach(var attr in attrlist)
                    {
                        string s2 = attr.Value;
                        if(s2.Length < 1)
                        {
                            Console.WriteLine("存在空内容");
                        }
                        else
                        {
                            dataGridView1.Rows.Add(s2);
                            Console.WriteLine(s2);
                        } 
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
