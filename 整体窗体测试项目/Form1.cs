using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 整体窗体测试项目
{
    public partial class Form1 : Form
    {
        private Process p = new Process();
        public Form1()
        {
            InitializeComponent();
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
                Thread o1 = new Thread(new ThreadStart(PortScan));
                o1.Start();
            }
        }
        public void PortScan()
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
                //
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
    }
}
