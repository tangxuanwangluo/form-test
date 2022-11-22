using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace 整体窗体测试项目
{
    /// <summary>
    /// 获取破解配置，判断是否需要破解的值是否处于打开
    /// </summary>
    public class PassWordCrackSetting
    {
        /// <summary>
        /// 确认破解的配置是否打开或打开了那些配置
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        public static string SeTtingOpen(string set)
        {
            string ipaddress = PassWordCrackApi.IpAddress;//获取IP地址
            string client = PassWordCrackApi.SqlClient;
            SqliteConnection dataConn = new SqliteConnection(client);
            dataConn.Open();
            try
            {
                if(dataConn.State == System.Data.ConnectionState.Open)
                {
                    string sql = "";//查看配置信息
                    SqliteCommand command1 = new SqliteCommand(sql, dataConn);
                    using(SqliteDataReader dr = command1.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        while (dr.Read())
                        {
                            string ports = "";//端口
                            //string notie = "";//描述
                            TcpClient tcp = new TcpClient(ipaddress,Convert.ToInt32(ports));
                            tcp.SendTimeout = 500;
                            string s1 = ports.ToString();
                            if(s1 == "3306")
                            {
                                //判断如果是配置确认是否打开，打开的情况下丢到任务中
                                //MysqlScan();//根据配置 回调参数在下面执行判断
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);//抛出异常
            }
            return set;
        }
        /// <summary>
        /// 确认mysql数据库端口是否打开
        /// </summary>
        /// <param name="mysql"></param>
        /// <returns></returns>
        public static string MysqlScan(string mysql)
        {

            string Ipaddress = PassWordCrackApi.IpAddress;//根据获取到的IP地址进行测试
            int port = 3306;
            try
            {
                Socket sock = null;
                IPAddress iP = IPAddress.Parse(Ipaddress);
                IPEndPoint point = new IPEndPoint(iP, port);
                sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                sock.Connect(point);
                Console.WriteLine("端口打开");//端口能打开后去执行的端口对应服务爆破的服务
                //回调事务
            }
            catch (SocketException ex)
            {
                PassWordCrackApi.ExOptions = ex.ToString();
            }
            return mysql;
        }
    }
}
