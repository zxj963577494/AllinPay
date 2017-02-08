using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AllinPayWeb.AllinPay;
using Newtonsoft.Json;

namespace AllinPayWeb
{
    public partial class notify_url : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SortedDictionary<string, string> sPara = GetRequestPost();
            string sign = Request.Form["sign"];//获取sign
            AllinPayNotify allinNotify = new AllinPayNotify();
            bool verifyResult = allinNotify.Verify(sPara, sign);
            if (verifyResult)
            {
                //商户订单号
                string orderId = Request.Form["orderId"];

                //状态码  : 待支付 1：支付成功2：支付失败 3：订单失效 4 : 退货待审批 5：退货成功
                string result = Request.Form["result"];

                //中文信息提示
                string msg = Request.Form["msg"];

                //最终支付金额 (支付失败，订单失效等情况不会出现 )
                string totalAmt = Request.Form["totalAmt"];

                //分期期数(支付失败，订单失效等情况不会出现 )
                string nper = Request.Form["nper"];

                //——请根据您的业务逻辑来编写程序（以上代码仅作参考）——
                Response.Write("success");  //请不要修改或删除
            }
            else
            {
                Response.Write("sign fail!");
            }
        }

        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public SortedDictionary<string, string> GetRequestPost()
        {
            int i = 0;
            SortedDictionary<string, string> sArraytemp = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = Request.Form;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0 ; i < requestItem.Length ; i++)
            {
                sArraytemp.Add(requestItem[i], Request.Form[requestItem[i]]);
            }
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            foreach (KeyValuePair<string, string> temp in sArraytemp)
            {
                sArray.Add(temp.Key, temp.Value);
            }
            return sArray;
        }
    }

}