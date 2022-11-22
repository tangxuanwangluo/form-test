using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 整体窗体测试项目
{
    /// <summary>
    /// 封装所有密码破解
    /// </summary>
    public class PassWordCrack
    {
        /// <summary>
        /// mysql 数据库密码破解，注意仅限于mysql数据库
        /// </summary>
        /// <param name="mysql"></param>
        /// <returns></returns>
        public static string MysqlCrack(string mysql)
        {
            string ipaddress = PassWordCrackApi.IpAddress;//破解的IP地址
            string mysqls = PassWordCrackApi.SqlClient;//破解的密码的数据
            SqliteConnection dataConn = new SqliteConnection(mysqls);
            dataConn.Open();
            try
            {
                if(dataConn.State == System.Data.ConnectionState.Open)
                {
                    string sql = "";//需要破解的端口
                    string sql1 = "";//查询端口信息
                    string sql2 = "";//读取数据库
                    SqliteCommand command1 = new SqliteCommand(sql, dataConn);
                    SqliteCommand command2 = new SqliteCommand(sql1, dataConn);
                    SqliteCommand command3 = new SqliteCommand(sql2, dataConn);
                    using (SqliteDataReader dr = command1.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        using (SqliteDataReader dr1 = command2.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            using (SqliteDataReader dr2 = command3.ExecuteReader(CommandBehavior.CloseConnection))
                            {
                                while (dr.Read())
                                {
                                    while (dr1.Read())
                                    {
                                        while (dr2.Read())
                                        {
                                            string s11 = dr1["port"].ToString();//读取扫描的端口
                                            string s12 = dr1["username"].ToString();//读取需要破解的账号
                                            string s14 = dr1["timeout"].ToString();//读取扫描的端口
                                            string s13 = dr2["mysql"].ToString();//读取mysql密码
                                            string s1 = dr["mysql"].ToString();//mysql数据
                                            string s2 = dr["msssql"].ToString();//sql server 数据
                                            string s3 = dr["oracle"].ToString();//sql server 数据
                                            string s4 = dr["postgres"].ToString();//sql server 数据
                                            string s5 = dr["mongodb"].ToString();//sql server 数据
                                            string s6 = dr["linux"].ToString();//sql server 数据
                                            string s7 = dr["windows"].ToString();//sql server 数据
                                            string s8 = dr["ftp"].ToString();//sql server 数据

                                            MySqlConnectionStringBuilder mysqlcli = new MySqlConnectionStringBuilder();//实例化mysql
                                            mysqlcli.Server = ipaddress;//读取破解的IP地址
                                            mysqlcli.Port = Convert.ToUInt32(s11);//在数据库读取破解的端口
                                            mysqlcli.UserID = s12;//破解的账号
                                            mysqlcli.Password = s13;//破解的密码
                                            mysqlcli.ConnectionTimeout = Convert.ToUInt32(s14);//该类型是uint,读取超时配置
                                            //开始建立连接，创建try 捕捉错误信息
                                            try
                                            {
                                                MySqlConnection bs = new MySqlConnection(mysqlcli.ToString());
                                                bs.Open();
                                                if (bs.State == ConnectionState.Open)
                                                {
                                                    //破解成功，获取到成功的IP地址、端口、账号、密码
                                                    //分段传输
                                                    PassWordCrackApi.IpAddress = ipaddress;
                                                    PassWordCrackApi.IpAddress = s11;
                                                    PassWordCrackApi.IpAddress = s12;
                                                    PassWordCrackApi.IpAddress = s13;
                                                }
                                            }
                                            catch(Exception ex)
                                            {
                                                PassWordCrackApi.ExOptions = ex.ToString();
                                                PassWordCrackApi.ExOptionsInfo = ipaddress + s11 + s12 + s13;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return mysql;
        }
    }
}
