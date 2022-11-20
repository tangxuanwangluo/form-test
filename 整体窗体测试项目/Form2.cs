using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 整体窗体测试项目
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        //定义委托
        public delegate void MyDelegate();
        //定义事件
        public event MyDelegate MyEvent;
        private void button1_Click(object sender, EventArgs e)
        {
            if (MyEvent != null)
                MyEvent();//引发事件
            this.Close();
            ////执行完成后 触发事件
            //Form1 s1 = new Form1();
            //s1.Test2();
            ////PathScan.S1info(PortScanApi.S1);
            //Close();

        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            //PathScan.S1info(PortScanApi.S1);
            if (MyEvent != null)
                MyEvent();//引发事件
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
        public static string S5 { set; get; }
        private void button2_Click(object sender, EventArgs e)
        {
            CacheApi.CacheInsert("zhujian", textBox1.Text);
            CacheApi.CacheInsert("zhujian1", textBox2.Text);
            CacheApi.CacheInsert("zhujian2", textBox3.Text);
            //string s1 = textBox1.Text;
            //string s2 = textBox2.Text;
            //string s3 = textBox3.Text;
            //s1 = TestApi.A1;
            //s2 = TestApi.A2;
            //s3 = TestApi.A3;
            //TestApi.KoNginx(S5);
            Close();
        }
    }
}
