using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace 整体窗体测试项目
{
    public class regex
    {
        //(?<=script(.)*?>)([\\s\\S](?!script))*?(?=</script>)
        /// <summary>
        /// 匹配javascript的所有内容
        /// </summary>
        /// <param name="xv"></param>
        /// <returns></returns>
        public static bool JavaScriptA(string xv)
        {
            Regex validipregex = new Regex(@"^(?<=script(.)*?>)([\\url](?!script))*?(?=</script>)$");
            return (xv != "" && validipregex.IsMatch(xv.Trim())) ? true : false;
        }
        /// <summary>
        /// 匹配javascript 中存在的路径
        /// </summary>
        /// <param name="xk"></param>
        /// <returns></returns>
        public static bool JavaScripta(string xk)
        {
            Regex validipregex = new Regex(@"^(url|path|src)\:$");
            return (xk != "" && validipregex.IsMatch(xk.Trim())) ? true : false;
        }
        /// <summary>
        /// IP地址格式验证
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public static bool ValidateIPAddress(string ipAddress)
        {
            Regex validipregex = new Regex(@"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$");
            return (ipAddress != "" && validipregex.IsMatch(ipAddress.Trim())) ? true : false;
        }
        /// 验证字符串是否是域名 是直接去掉http与https的
        /// </summary> 如：www.baidu.com | baidu.com
        /// <param name="str">指定字符串</param>
        /// <returns></returns>
        public static bool IsDomain(string str)
        {
            Regex validipregex = new Regex(@"^[a-zA-Z0-9][-a-zA-Z0-9]{0,62}(\.[a-zA-Z0-9][-a-zA-Z0-9]{0,62})+$");
            return (str != "" && validipregex.IsMatch(str.Trim())) ? true : false;
        }
        /// <summary>
        /// 域名正则验证  支持：http https ftp
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool UrlsTr(string url)
        {
            Regex us = new Regex(@"^(http|https|ftp)\://");
            return (url != "" && us.IsMatch(url.Trim())) ? true : false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool UrlPath(string path)
        {
            Regex us = new Regex("/");
            return (path != "" && us.IsMatch(path.Trim())) ? true : false;
        }
        /// <summary>
        /// 去URL中的title 标题
        /// </summary>
        /// <param name="titles"></param>
        /// <returns></returns>
        public static bool TitleReg(string titles)
        {
            Regex tts = new Regex("<title>(.*)</title>");
            return (titles != "" && tts.IsMatch(titles.Trim())) ? true : false;
        }
        /// <summary>
        /// 只允许输入英文或数字
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static bool Engin(string en)
        {
            Regex ens = new Regex(@"^[A-Za-z0-9]+$");
            return (en != "" && ens.IsMatch(en.Trim())) ? true : false;
        }
        /// <summary>
        /// 验证手机号码格式
        /// </summary>
        /// <param name="ph"></param>
        /// <returns></returns>
        public static bool Phone(string ph)
        {
            Regex phs = new Regex(@"^(13[0-9]|14[5|7]|15[0|1|2|3|5|6|7|8|9]|18[0|1|2|3|5|6|7|8|9])\d{8}$");
            return (ph != "" && phs.IsMatch(ph.Trim())) ? true : false;
        }
        /// <summary>
        /// 强密码(必须包含大小写字母和数字的组合，不能使用特殊字符，长度在8-10之间)
        /// </summary>
        /// <param name="ps"></param>
        /// <returns></returns>
        public static bool PassWords(string ps)
        {
            Regex pss = new Regex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,10}$");
            return (ps != "" && pss.IsMatch(ps.Trim())) ? true : false;
        }
        /// <summary>
        /// 网站格式校验
        /// </summary>
        /// <param name="wbs"></param>
        /// <returns></returns>
        public static bool WebHz(string wbs)
        {
            Regex wb = new Regex(@"(/.com/|/.net/|/.cn/|/.org/|/.gov/|/.xyz/|/.cn/)");
            return (wbs != "" && wb.IsMatch(wbs.Trim())) ? true : false;
        }
        /// <summary>
        /// 字符换行分割表达
        /// </summary>
        /// <param name="h1"></param>
        /// <returns></returns>
        public static bool Hblong(string h1)
        {
            Regex s2 = new Regex(@"^(<div>|</div>)");
            return (h1 != "" && s2.IsMatch(h1.Trim())) ? true : false;
        }
        /// <summary>
        /// 正则匹配 url 中的图片
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static bool ImaGes(string img)
        {
            Regex s3 = new Regex(@"<img[\s]+[^<>]*src=(?:""|')([^<>""']+(?:jpg|jpeg|png|gif))(?:""|')[^<>]*>", RegexOptions.IgnoreCase);
            return (img != "" && s3.IsMatch(img.Trim())) ? true : false;
        }
        /// <summary>
        /// 正则匹配访问路径
        /// </summary>
        /// <param name="pa"></param>
        /// <returns></returns>
        public static bool PathUrl(string pa)
        {
            Regex s4 = new Regex(@"<a[\s]+[^<>]*href=(?:""|')([^<>""']+)(?:""|')[^<>]*>[^<>]+</a>", RegexOptions.IgnoreCase);
            return (pa != "" && s4.IsMatch(pa.Trim())) ? true : false;
        }
        public static bool Phone1(string ph)
        {
            Regex phs = new Regex(@"^(13[0-9]|14[5|7]|15[0|1|2|3|5|6|7|8|9]|18[0|1|2|3|5|6|7|8|9])\.{8}$");
            return (ph != "" && phs.IsMatch(ph.Trim())) ? true : false;
        }
        //public static string Jsfind(string js)
        //{
            
            
        //}
    }
}
