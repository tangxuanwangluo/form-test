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
using System.Timers;
using System.Windows.Forms;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;
using Timer = System.Windows.Forms.Timer;

namespace 整体窗体测试项目
{
    public partial class Form1 : Form
    {
        private Process p = new Process();
        Form2 b = null;//接收窗体二

        public Form1()
        {
            InitializeComponent();
            b = new Form2();//实例化b窗体
            b.MyEvent += new Form2.MyDelegate(b_MyEvent);//监听b窗体事件

            Control.CheckForIllegalCrossThreadCalls = false;
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;//重定向错误流
            p.StartInfo.RedirectStandardOutput = true;//重定向输入流
            p.StartInfo.RedirectStandardError = true;//重定向输出流
            p.StartInfo.CreateNoWindow = true;//隐藏窗口运行
            p.Start();
        }
        public string H1 { set; get; }
        string code;
        private void button1_Click(object sender, EventArgs e)
        {
            string start = @"C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe C:\Users\huaxi\source\repos\测试服务\测试服务\bin\Debug\测试服务.exe";
            p.StandardInput.WriteLine(start);
            p.StandardInput.WriteLine("exit");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //创建异步线程
            Thread s1 = new Thread(new ThreadStart(YibuThread));
            s1.Start();

        }
        public void YibuThread()
        {
            //异步线程执行
            System.Threading.Thread.Sleep(200);
            int s1 = 0;
            int s2 = 100000000;
            for (int i = s1; i <= s2; i++)
            {
                Console.WriteLine(i);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            code = GenerateCheckCode();
            Bitmap image = CreateCheckCodeImage(code, 64, 30);//长高
            pictureBox1.Image = image;
        }
        /// <summary>
        /// 得到验证码
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string GenerateCheckCode()
        {
            int number;
            char code;
            string checkCode = String.Empty;

            System.Random random = new Random();

            for (int i = 0; i < 4; i++)
            {
                number = random.Next();

                if (number % 2 == 0)
                    code = (char)('0' + (char)(number % 10));
                else if (number % 3 == 0)
                    code = (char)('A' + (char)(number % 26));
                else
                    code = (char)('a' + (char)(number % 26));
                checkCode += code.ToString();
            }
            return checkCode.ToUpper();
        }

        /// <summary>
        /// 生成验证码图片
        /// </summary>
        /// <param name="checkCode"></param>
        /// <param name="context"></param>
        private Bitmap CreateCheckCodeImage(string checkCode, int w = 54, int h = 30)
        {
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(w, h);
            Graphics g = Graphics.FromImage(image);

            g.Clear(Color.WhiteSmoke);//清除背景色

            Color[] c = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };//定义随机颜色

            string[] font = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋体" };
            Random rand = new Random();
            int z = 0;//干扰线条数
            for (int i = 0; i < z; i++)
            {
                int x1 = rand.Next(image.Width);
                int x2 = rand.Next(image.Width);
                int y1 = rand.Next(image.Height);
                int y2 = rand.Next(image.Height);
                g.DrawLine(new Pen(Color.LightGray, 1), x1, y1, x2, y2);//根据坐标画线
            }

            for (int i = 0; i < checkCode.Length; i++)
            {
                int cindex = rand.Next(7);
                int findex = rand.Next(5);

                Font f = new System.Drawing.Font(font[findex], 14, System.Drawing.FontStyle.Bold);
                Brush b = new System.Drawing.SolidBrush(c[cindex]);
                int ii = 4;
                if ((i + 1) % 2 == 0)
                {
                    ii = 2;
                }
                g.DrawString(checkCode.Substring(i, 1), f, b, 3 + (i * (14)), ii);
            }
            //image = TwistImage(image, true, 5, 1);//让字更模糊
            g.DrawRectangle(new Pen(Color.Black, 0), 0, 0, image.Width - 1, image.Height - 1);
            return image;
        }

        private const double PI = 3.1415926535897932384626433832795;
        private const double PI2 = 6.283185307179586476925286766559;

        /// <summary>
        /// 正弦曲线Wave扭曲图片（Edit By 51aspx.com）
        /// </summary>
        /// <param name="srcBmp">图片路径</param>
        /// <param name="bXDir">如果扭曲则选择为True</param>
        /// <param name="nMultValue">波形的幅度倍数，越大扭曲的程度越高，一般为3</param>
        /// <param name="dPhase">波形的起始相位，取值区间[0-2*PI)</param>
        /// <returns></returns>
        public System.Drawing.Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
        {
            System.Drawing.Bitmap destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);

            // 将位图背景填充为白色
            System.Drawing.Graphics graph = System.Drawing.Graphics.FromImage(destBmp);
            graph.FillRectangle(new SolidBrush(System.Drawing.Color.White), 0, 0, destBmp.Width, destBmp.Height);
            graph.Dispose();

            double dBaseAxisLen = bXDir ? (double)destBmp.Height : (double)destBmp.Width;

            for (int i = 0; i < destBmp.Width; i++)
            {
                for (int j = 0; j < destBmp.Height; j++)
                {
                    double dx = 0;
                    dx = bXDir ? (PI2 * (double)j) / dBaseAxisLen : (PI2 * (double)i) / dBaseAxisLen;
                    dx += dPhase;
                    double dy = Math.Sin(dx);

                    // 取得当前点的颜色
                    int nOldX = 0, nOldY = 0;
                    nOldX = bXDir ? i + (int)(dy * dMultValue) : i;
                    nOldY = bXDir ? j : j + (int)(dy * dMultValue);

                    System.Drawing.Color color = srcBmp.GetPixel(i, j);
                    if (nOldX >= 0 && nOldX < destBmp.Width
                     && nOldY >= 0 && nOldY < destBmp.Height)
                    {
                        destBmp.SetPixel(nOldX, nOldY, color);
                    }
                }
            }

            return destBmp;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            code = GenerateCheckCode();
            Bitmap image = CreateCheckCodeImage(code, 64, 30);//长高
            pictureBox1.Image = image;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("验证码不能为空");
                return;
            }
            if (textBox1.Text.Equals(code))
            {
                MessageBox.Show("一致");
            }
            else
            {
                MessageBox.Show("验证码错误，请注意大小写！");
                code = GenerateCheckCode();
                Bitmap image = CreateCheckCodeImage(code, 64, 30);//长高
                pictureBox1.Image = image;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {

            //创建端口扫描线程进行端口扫描
            Thread st = new Thread(new ThreadStart(InterScan));
            st.Start();

        }
        //public TcpClient s1 = null;
        public String IpAddress;//IP地址
        public int Port;//端口
        public int start;//开始端口
        public int end;//结束端口
        public int p1;//用来接端口
        public void InterScan()
        {
            start = 130;
            end = 140;
            IpAddress = "192.168.31.171";
            for (int i = start; i <= end; i++)
            {
                p1 = i;
                Thread o1 = new Thread(new ThreadStart(PortScan1));
                o1.Start();
            }
        }
        public void PortScan1()
        {
            try
            {
                int portnow = p1;
                //创建TcpClient对象，TcpClient用于为TCP网络服务提供客户端连接
                TcpClient objTCP = null;
                //扫描端口，成功则写入信息
                try
                {
                    //用TcpClient对象扫描端口
                    objTCP = new TcpClient(IpAddress, portnow);
                    //lbResult.Items.Add("端口 " + portnow.ToString() + " 开放!");
                }
                catch
                {
                    //lbResult.Items.Add("端口 " + portnow.ToString() + " 未开放!");
                }
            }
            catch (Exception ex)
            {
                //123
                Console.WriteLine(ex);
            }
            Console.WriteLine("线程自动结束");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Thread k1 = new Thread(SocketClient);
            k1.Start();
        }
        public void SocketClient()
        {
            //Socket sock = null;
            ////string ip = "192.168.31.171";
            //int port = 33061;
            //try
            //{
            //    IPAddress iP = IPAddress.Parse("127.0.0.1");
            //    IPEndPoint point = new IPEndPoint(iP, port);
            //    sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //    sock.Connect(point);
            //    MessageBox.Show("端口打开");
            //}
            //catch (SocketException ex)
            //{
            //    MessageBox.Show("计算机端口检测失败，错误消息为：" + ex.Message);
            //}
            //finally
            //{
            //    if (sock != null)
            //    {
            //        sock.Close();
            //        sock.Dispose();
            //    }
            //}
            try
            {
                IPAddress iP = IPAddress.Parse("127.0.0.1");
                IPEndPoint iPEndPoint = new IPEndPoint(iP, 3307);
                Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                //创建与远程主机的连接
                serverSocket.Connect(iPEndPoint);
                MessageBox.Show("端口开放");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //获取网站结构的方法
            Thread h1 = new Thread(new ThreadStart(TreeInfo));
            h1.Start();
        }
        public string url;
        public string UrlLength;//url长度
        public void TreeInfo()
        {
            url = "https://www.baidu.com";
            try
            {
                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                //request.Method = "GET";
                //request.ContentType = "text/html;charset=UTF-8";

                //HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                //Stream myResponseStream = response.GetResponseStream();
                //StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                //string retString = myStreamReader.ReadToEnd();
                //Console.WriteLine(retString);
                //UrlLength = Convert.ToString(retString.Length);
                HttpWebRequest res = (HttpWebRequest)WebRequest.Create(url);
                //res.Headers.Add("Cookie", "ASP.NET_SessionId=sesc5hfpgdxbxdpbqqtzmzoq");
                res.Method = "Get";
                res.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.5249.62 Safari/537.36";
                res.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
                HttpWebResponse respone = (HttpWebResponse)res.GetResponse();
                StreamReader red = new StreamReader(respone.GetResponseStream());
                string html = red.ReadToEnd();
                HtmlDocument doc = new HtmlDocument();
                HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//a/@href");//连同js一同提取出来 |//script
                Console.WriteLine(nodes);
                HtmlNode node;
                if(nodes == null)
                {
                    Console.WriteLine("为空");
                }
                for (int i = 0; i < nodes.Count; i++)
                {
                    node = nodes[i];
                    var attrList = node.Attributes;
                    foreach (var attr in attrList)
                    {
                        string s2 = attr.Value;
                        string s3 = attr.ValueLength.ToString();
                        Console.WriteLine(s2);
                        
                    }
                }
                robot_GetPublicContactEvent();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            
        }
        /// <summary>
        /// 这个主要是用于针对TREEVIEW添加根节点时无法添加的情况
        /// </summary>
        public delegate void CreateNodeDelegate();

        //子进程方法

        private void robot_GetPublicContactEvent()
        {
            if (this.InvokeRequired)
            {
                CreateNodeDelegate createNodeDelegate = new CreateNodeDelegate(CreateNode);
                this.Invoke(createNodeDelegate);
            }
        }
        public string f;//根节点 f这个参数用于获取XXX信息
        private void CreateNode()
        {
            //TreeNode treeNode = new TreeNode(url);//获取URL地址
            //treeView1.Nodes.Add("网站信息");//设置标题
            //treeView1.Nodes.Add(treeNode);//添加到TREE节点
            //treeView1.ImageList = imageList1;
            //treeView1.Select();


            //这种方式可能会出现卡顿
            int i = 0;//根节点
            int k = 0;//一级子节点
            int g = 0;//二级子节点
            treeView1.ImageList = imageList1;

            treeView1.Nodes.Add(url);//获取URL地址的根节点
            treeView1.Nodes[i].Nodes.Add("img");//获取路径的子节点一层
            treeView1.Nodes[i].Nodes[k].Nodes.Add(UrlLength);
            treeView1.Nodes[i].Nodes[k].Nodes[g].Nodes.Add("456");


            treeView1.Nodes[i].Nodes.Add("1 子节点");
            treeView1.Nodes[i].Nodes.Add(new TreeNode("312312"));
       
            treeView1.Nodes.Add("子域名"); i++;
            treeView1.Nodes[i].Nodes.Add("0 子节点");


            treeView1.ExpandAll();  //全展开
            treeView1.Scrollable = true; //带滚动条
            //treeView1.Dock = System.Windows.Forms.DockStyle.Fill; //全填充窗口
            //TreeNode tn1 = treeView1.Nodes.Add("组织结构"); //建立 3 个子节点
            //TreeNode Ntn1 = new TreeNode("C#部门"); 
            //TreeNode Ntn2 = new TreeNode("ASP.NET 部门"); 
            //TreeNode Ntn3 = new TreeNode("VB 部门"); 
            ////将3个子节点添加到父节点中
            //tn1.Nodes.Add(Ntn1);
            //tn1.Nodes.Add(Ntn2);
            //tn1.Nodes.Add(Ntn3); 
            ////设置imageList1 控件中显示的图像
            ////imageList1.Images.Add(Image.FromFile("1.png")); 
            ////imageList1.Images.Add(Image.FromFile("2.png")); 
            ////设置 treeView1 的 ImageList 属性为 imageList1
            //treeView1.ImageList = imageList1;
            //treeView1.ImageList = imageList2;
            //imageList1.ImageSize = new Size(16,16);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int start = 0;//开始
            int end = 1000000;//结束
            double x;
            string xian;
            toolStripProgressBar1.Minimum = start;
            toolStripProgressBar1.Maximum = end;
            for (int i = start; i <= end; i++)
            {
                x = (double)(i - start + 1) / (end - start + 1);
                xian = x.ToString("0%");
                Console.WriteLine(i);
                //调用端口i的扫描操作
                //进度条值改变
                label1.Text = xian;
                label1.Refresh();
                //lb.Refresh();
                toolStripProgressBar1.Value = i;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Ip地址为空");
                return;
            }
            PortScanApi.Url = textBox1.Text;
            Thread s1 = new Thread(ptScan);
            s1.Start();
        }
        public string H2 { set; get; }
        public void ptScan()
        {
            PortScan s3 = new PortScan();
            s3.Equals(PortScan.PortScanService(PortScanApi.S1));
            if (s3.Equals(PortScanApi.ExOptions) == false)
            {
                Console.WriteLine("打开");
            }
            if (s3.Equals(PortScanApi.ExOptions) != true)
            {
                Console.WriteLine(PortScanApi.ExOptions);
            }
            
        }

        private void button9_Click(object sender, EventArgs e)
        {
            PortScanApi.Purl = "https://www.baidu.com/";//定义接收的URL地址
            //PathScan.PathPassword(PortScanApi.PurlPath) ;//接收路径
            //后台扫描线程
            Thread rs = new Thread(Pascan);
            rs.Start();
        }
        public void Pascan()
        {
            string s1 = PathScan.WebPathScan(PortScanApi.HtmlCode);
            //PathScan.WebPathScan(PortScanApi.HtmlCode);
            Console.WriteLine(s1);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //执行类
            PathScan.PathPassword(PortScanApi.S1);
            Console.WriteLine(PortScanApi.PurlPath);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //类获取IP地址
            PortScanApi.Purl = "https://www.baidu.co";
            WebSiteInfo.WebSiteInfos(WebSiteInfoApi.H1);
            string s1 = WebSiteInfoApi.Ipaddress;//字符串s1 主要是用于获取IP地址，如果说为空的话 肯定由于什么错误所引发
            if(s1 == null)
            {
                Console.WriteLine(WebSiteInfoApi.Exoptions);
            }
            Console.WriteLine("域名：" + PortScanApi.Purl + "的IP地址为：" + s1);
            //获取网站标题
            WebSiteInfo.WebSiteTitleInfo(WebSiteInfoApi.H1);
            string s2 = WebSiteInfoApi.WebInfotitles;
            if(s2 == null)
            {
                Console.WriteLine(WebSiteInfoApi.Exoptions);//输出错误信息
            }
            Console.WriteLine("网站标题是：" + s2);
            //获取网站服务信息
            WebSiteInfo.WebSiteServiceInfo(WebSiteInfoApi.H1);
            string s3 = WebSiteInfoApi.WebServiceInfo;
            if(s3 == null)
            {
                Console.WriteLine(WebSiteInfoApi.Exoptions);
            }
            Console.WriteLine("网站服务是：" + s3);
            //PathScan.IpaddressINfo(PortScanApi.S1);
            //string s1 = PortScanApi.IPaddressInfos;
            //Console.WriteLine(s1);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            SpriderInfoApi.Urls = "https://www.cqdhxk.com";
            SpriderInfo.SpiderScan(SpriderInfoApi.A1);
            richTextBox1.Text = SpriderInfoApi.UrlsHtmlCode;
            string s1 = SpriderInfoApi.UrlsInfo;
            if(s1 == null)
            {
                Console.WriteLine(SpriderInfoApi.UrlsExoptions);
            }
            Console.WriteLine(s1 + "\r\n");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            //封装日志记录
            Console.WriteLine("输出爬虫日志");
            AppLog.WriteInfo("输出爬虫日志", true);
            Console.WriteLine("输出爬虫日志");

        }

        private void button14_Click(object sender, EventArgs e)
        {
            //命名主键名称
            CacheApi.CacheInsert("zhujian", textBox2.Text);
            CacheApi.CacheInsert("zhujian1", textBox3.Text);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            object o = CacheApi.CacheValue("zhujian");
            object o1 = CacheApi.CacheValue("zhujian1");
            string s = string.Empty;
            if(o != null)
            {
                s = o.ToString();
            }
            MessageBox.Show(s);
            Console.WriteLine(s);
            Console.WriteLine(o1);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            CacheApi.CacheNull("zhujian");
        }

        private void button17_Click(object sender, EventArgs e)
        {
            //打开窗体二后触发B事件
            b.ShowDialog();
            //Form2 s1 = new Form2();
            //s1.StartPosition = FormStartPosition.CenterScreen;
            //s1.ShowDialog();
        }
        /// <summary>
        /// timer1 事件执行后执行需要做的事情
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            

            if (timer1.Enabled == true)
            {
                timer1.Enabled = false;
                timer1.Stop();
                Test3();
            }
            //MessageBox.Show("123");

        }
        public void Test3()
        {
            string s1 = PortScanApi.S1;
            MessageBox.Show(s1);
            this.tabControl1.SelectedTab = tabPage2;
        }
        public static string Test(string s1)
        {
            //窗体关闭之后的事件
            Form1 s2 = new Form1();
            s2.textBox1.Text = "123";
            //s2.button18_Click(null,null);
            //s2.timer1.Start();
            return s1;
        }
        void b_MyEvent()
        {
            timer1.Start();
        }
        private void button18_Click(object sender, EventArgs e)
        {
            timer1.Start();
            //Test3();
            //Test2();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            //通过缓存获取另一个窗体中的数据
            object o = CacheApi.CacheValue("zhujian");
            object o1 = CacheApi.CacheValue("zhujian1");
            object o2 = CacheApi.CacheValue("zhujian2");
            string s = string.Empty;
            if (o != null)
            {
                s = o.ToString();
            }
            MessageBox.Show(s);
            Console.WriteLine(s);
            Console.WriteLine(o1);
            Console.WriteLine(o2);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox4.Text)&&string.IsNullOrEmpty(textBox5.Text))
            {
                MessageBox.Show("为空");
                return;
            }
            else
            {
                MessageBox.Show("123");
            }
        }
    }
}
