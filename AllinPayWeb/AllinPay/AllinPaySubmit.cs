using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;

namespace AllinPayWeb.AllinPay
{
    public class AllinPaySubmit
    {
        #region 字段
        //通联支付网关地址（新）
        private static string GATEWAY_NEW = AllinPayConfig.pay_url;
        //商户的私钥
        private static string _private_key = "";
        //编码格式
        private static string _input_charset = "UTF-8";
        //签名方式
        private static string _sign_type = "";
        #endregion

        static AllinPaySubmit()
        {
            _private_key = AllinPayConfig.GetPrivatekey().Trim();
            _input_charset = AllinPayConfig.input_charset.Trim().ToLower();
            _sign_type = AllinPayConfig.sign_method.Trim().ToUpper();
        }

        /// <summary>
        /// 生成请求时的签名
        /// </summary>
        /// <param name="sPara">请求给通联支付的参数数组</param>
        /// <returns>签名结果</returns>
        private static string BuildRequestMysign(Dictionary<string, string> sPara)
        {
            //把数组所有元素，按照“参数=参数值”的模式
            string prestr = AllinPayCore.CreateLinkString(sPara);

            //把最终的字符串签名，获得签名结果
            string mysign = "";
            switch (_sign_type)
            {
                case "RSA":
                    mysign = AllinPayRSA.sign(prestr, _private_key, _input_charset);
                    break;
                default:
                    mysign = "";
                    break;
            }

            return mysign;
        }

        /// <summary>
        /// 生成要请求给通联支付的参数数组
        /// </summary>
        /// <param name="sParaTemp">请求前的参数数组</param>
        /// <returns>要请求的参数数组</returns>
        private static Dictionary<string, string> BuildRequestPara(SortedDictionary<string, string> sParaTemp)
        {
            //待签名请求参数数组
            Dictionary<string, string> sPara = new Dictionary<string, string>();
            //签名结果
            string mysign = "";

            //过滤签名参数数组
            sPara = AllinPayCore.FilterPara(sParaTemp);

            //获得签名结果
            mysign = BuildRequestMysign(sPara);

            //签名结果与签名方式加入请求提交参数组中
            sPara.Add("sign", mysign);

            return sPara;
        }

        /// <summary>
        /// 生成要请求给通联支付的参数数组
        /// </summary>
        /// <param name="sParaTemp">请求前的参数数组</param>
        /// <param name="code">字符编码</param>
        /// <returns>要请求的参数数组字符串</returns>
        private static string BuildRequestParaToString(SortedDictionary<string, string> sParaTemp, Encoding code)
        {
            //待签名请求参数数组
            Dictionary<string, string> sPara = new Dictionary<string, string>();
            sPara = BuildRequestPara(sParaTemp);

            //把参数组中所有元素，按照“参数参数值”的模式并对参数值做urlencode
            string strRequestData = AllinPayCore.CreateLinkString(sPara);

            return strRequestData;
        }

        /// <summary>
        /// 建立请求，以表单HTML形式构造（默认）
        /// </summary>
        /// <param name="sParaTemp">请求参数数组</param>
        /// <param name="strMethod">提交方式。两个值可选：post、get</param>
        /// <param name="strButtonValue">确认按钮显示文字</param>
        /// <returns>提交表单HTML文本</returns>
        public static string BuildRequest(SortedDictionary<string, string> sParaTemp, string strMethod, string strButtonValue)
        {
            //待请求参数数组
            Dictionary<string, string> dicPara = new Dictionary<string, string>();
            dicPara = BuildRequestPara(sParaTemp);

            StringBuilder sbHtml = new StringBuilder("<meta charset=\"utf-8\"/>");

            sbHtml.Append("<form id='pay_form' name='pay_form' action='" + GATEWAY_NEW + "' method='" + strMethod.ToLower().Trim() + "'>");

            foreach (KeyValuePair<string, string> temp in dicPara)
            {
                sbHtml.Append("<input type='hidden' name='" + temp.Key + "' value='" + temp.Value + "'/>");
            }

            //submit按钮控件请不要含有name属性
            sbHtml.Append("<input type='submit' value='" + strButtonValue + "' style='display:none;'></form>");

            sbHtml.Append("<script>document.forms['pay_form'].submit();</script>");

            return sbHtml.ToString();
        }

        /// <summary>
        /// 生成string类型的参数数组
        /// </summary>
        /// <param name="sParaTemp">请求参数数组</param>
        /// <returns></returns>
        public static string BuildParams(SortedDictionary<string, string> sParaTemp)
        {
            return BuildRequestParaToString(sParaTemp, Encoding.UTF8);
        }

        /// <summary>
        /// 获得签名结果
        /// </summary>
        /// <param name="sParaTemp"></param>
        /// <returns></returns>
        public static string BuildSign(SortedDictionary<string, string> sParaTemp)
        {
            //待签名请求参数数组
            Dictionary<string, string> sPara = new Dictionary<string, string>();
            //签名结果
            string mysign = "";

            //过滤签名参数数组
            sPara = AllinPayCore.FilterPara(sParaTemp);

            //获得签名结果
            mysign = BuildRequestMysign(sPara);

            return mysign;
        }
    }
}