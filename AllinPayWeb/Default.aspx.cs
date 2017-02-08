using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AllinPayWeb.AllinPay;
using Newtonsoft.Json;

namespace AllinPayWeb
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void BtnAlipay_Click(object sender, EventArgs e)
        {
            ////////////////////////////////////////////请求参数////////////////////////////////////////////

            // 分期金额
            string amount = "1000.00";

            // 分期期数
            string nper = "12";

            // 支付渠道：0：pc   1：wap
            string channel = "0";

            // 支付流水号：商户生成的消费订单支付流水号，应保证易于标识或识别
            string order_id = GetDateNumber();

            // OpenAPI分配给应用的AppKey 通联商务提供
            string app_key = AllinPayConfig.app_key;

            // 接口方法名称   通联商务提供
            string method = AllinPayConfig.pay_method;

            // 时间戳
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");

            // 版本号，目前默认值：1.0  通联商务提供
            string v = AllinPayConfig.v;

            // 签名版本号，目前默认值：1   通联商务提供
            string sign_v = AllinPayConfig.sign_v;

            // 商户号：商户在通联商务注册的认证号码
            string mer_id = AllinPayConfig.mer_id;

            // 订单发生日期 yyyyMMdd
            string trade_date = DateTime.Now.ToString("yyyyMMdd");

            // 订单发生时间 HHmmss
            string trade_time = DateTime.Now.ToString("HHmmss");

            // 产品编号:固定值 0200
            string pdno = "0200";

            // 支付成功后返回到的页
            string return_url = AllinPayConfig.return_url;

            // 支付成功或失败后异步通知的商户URL地址
            string notify_url = AllinPayConfig.notify_url;

            // 持卡人姓名
            string creditName = "xxx";

            // 消费者证件类型（01：身份证）
            string cetitype = "01";

            // 消费者证件号码  传输过程中需要关键数据加密
            string idno = AllinPayCore.EncodeDES("xxxxxxxxxxxxxxxxxxxxxxxx");

            // 银行预留手机号码   传输过程中需要关键数据加密
            string phoneNo = AllinPayCore.EncodeDES("xxxxxxxxxxxxxxxxxxxxxxx");

            // 消费者支付银行卡卡号  传输过程中需要关键数据加密
            string creditNo = AllinPayCore.EncodeDES("xxxxxxxxxxxxxxxx");

            // 联系手机号 区别于银行预留手机号  两者可相同 传输过程中需要关键数据加密
            string relatePhone = AllinPayCore.EncodeDES("xxxxxxxxxxxxxxxx");

            //把请求参数打包成数组
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("app_key", app_key);
            sParaTemp.Add("method", method);
            sParaTemp.Add("timestamp", timestamp);
            sParaTemp.Add("v", v);
            sParaTemp.Add("sign_v", sign_v);
            sParaTemp.Add("mer_id", mer_id);
            sParaTemp.Add("channel", channel);
            sParaTemp.Add("order_id", order_id);
            sParaTemp.Add("amount", amount);
            sParaTemp.Add("trade_date", trade_date);
            sParaTemp.Add("trade_time", trade_time);
            sParaTemp.Add("nper", nper);
            sParaTemp.Add("pdno", pdno);
            sParaTemp.Add("return_url", return_url);
            sParaTemp.Add("notify_url", notify_url);
            sParaTemp.Add("creditName", creditName);
            sParaTemp.Add("cetitype", cetitype);
            sParaTemp.Add("idno", idno);
            sParaTemp.Add("phoneNo", phoneNo);
            sParaTemp.Add("creditNo", creditNo);
            sParaTemp.Add("relatePhone", relatePhone);

            string sHtmlText = AllinPaySubmit.BuildRequest(sParaTemp, "post", "确认");
            Response.Write(sHtmlText);
        }

        public static string GetDateNumber()
        {
            Random r = new Random();
            int i = r.Next(100000, 999999);
            string sde = DateTime.Now.ToString("yyyyMMddhhmmss") + i;
            return sde;
        }
    }
}