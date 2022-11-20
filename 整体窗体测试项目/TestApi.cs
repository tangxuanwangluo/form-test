using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 整体窗体测试项目
{
    public class TestApi
    {
        public static string A1 { set; get; }
        public static string A2 { set; get; }
        public static string A3 { set; get; }

        public static string KoNginx(string s5)
        {
            string s1 = A1;
            string s2 = A2;
            string s3 = A3;
            return s5;
        }
    }
}
