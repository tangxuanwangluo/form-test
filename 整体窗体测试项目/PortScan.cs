using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace 整体窗体测试项目
{
   public class PortScan
    {
        public static string PortScanService(string port)
        {
            port = PortScanApi.S1;
            try
            {
                TcpClient k1 = new TcpClient();
                IPAddress iP = IPAddress.Parse(PortScanApi.Url);
                IPEndPoint iPEndPoint = new IPEndPoint(iP, 3306);
                Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //创建与远程主机的连接
                serverSocket.Connect(iPEndPoint);
            }
            catch(Exception ex)
            {
                PortScanApi.ExOptions = ex.ToString();
            }
            return port;
        }
    }
}
