using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;

namespace AllinPayWeb.AllinPay
{
    /// <summary>
    /// 基础配置类
    /// </summary>
    public class AllinPayConfig
    {

        /// <summary>
        /// OpenAPI分配给应用的AppKey
        /// </summary>
        public static string app_key = "xxxxxx";


        /// <summary>
        /// 商户号：商户在通联商务注册的认证号码
        /// </summary>
        public static string mer_id = "xxxxxxxxxxxxxxxxxxxx";

        /// <summary>
        /// 商户的私钥
        /// </summary>
        /// <returns></returns>
        public static string GetPrivatekey()
        {
            try
            {
                string sPath = HttpRuntime.AppDomainAppPath.ToString() + "key\\private.pfx";
                X509Certificate2 c3 = AllinPayRSA.GetCertificateFromPfxFile(sPath, "123456");
                return c3.PrivateKey.ToXmlString(true);
            }
            catch (Exception e)
            {
                throw;
            }
           
        }

        /// <summary>
        /// 通联支付公钥
        /// </summary>
        /// <returns></returns>
        public static string GetPublickey()
        {
            string sPath = HttpRuntime.AppDomainAppPath.ToString() + "key\\public.cer";
            X509Certificate2 c3 = AllinPayRSA.GetCertFromCerFile(sPath);
            return c3.PublicKey.Key.ToXmlString(false);
        }

        /// <summary>
        /// DES密钥，必须8位
        /// </summary>
        public static string des_key = "xxxxxxxx";

        /// <summary>
        /// 版本号，目前默认值：1.0
        /// </summary>
        public static string v = "1.0";

        /// <summary>
        /// 签名方式
        /// </summary>
        public static string sign_method = "RSA";

        /// <summary>
        /// 签名版本号
        /// </summary>
        public static string sign_v = "1";

        /// <summary>
        /// 响应数据格式
        /// </summary>
        public static string format = "json";

        // 服务器异步通知页面路径，需http://格式的完整路径，不能加?id=123这类自定义参数,必须外网可以正常访问
        public static string notify_url = "http://www.xxxxxxxxxxx.com/notify_url.aspx";

        // 页面跳转同步通知页面路径，需http://格式的完整路径，不能加?id=123这类自定义参数，必须外网可以正常访问
        public static string return_url = "http://www.xxxxxxxxxxx.com/return_url.aspx";

        /// <summary>
        /// 在线分期消费据，接口方法名称
        /// </summary>
        public static string pay_url = "http://xxxxxxxxxxx";

        /// <summary>
        /// 在线分期消费据，接口方法名称
        /// </summary>
        public static string pay_method = "allinpay.order.orderstage.add.test";

        /// <summary>
        /// 订单状态查询，接口方法名称
        /// </summary>
        public static string order_url = "http://xxxxxxxxxxx";

        /// <summary>
        /// 订单状态查询，接口方法名称
        /// </summary>
        public static string order_method = "allinpay.order.orderinstall.query";

        /// <summary>
        /// 字符编码格式 目前支持utf-8
        /// </summary>
        public static string input_charset = "utf-8";

        // 调试用，创建TXT日志文件夹路径，见AlipayCore.cs类中的LogResult(string sWord)打印方法。
        public static string log_path = HttpRuntime.AppDomainAppPath.ToString() + "log\\";

    }
}
