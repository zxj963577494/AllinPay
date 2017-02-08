using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace AllinPayWeb.AllinPay
{
    public class AllinPayNotify
    {
        #region 字段
        private string _mer_id = "";               //合作身份者ID
        private string allinpay_public_key = "";            //通联支付的公钥
        private string _input_charset = "";         //编码格式
        private string _sign_type = "";             //签名方式
        #endregion


        /// <summary>
        /// 构造函数
        /// 从配置文件中初始化变量
        /// </summary>
        /// <param name="inputPara">通知返回参数数组</param>
        /// <param name="notify_id">通知验证ID</param>
        public AllinPayNotify()
        {
            //初始化基础配置信息
            _mer_id = AllinPayConfig.mer_id.Trim();
            allinpay_public_key = AllinPayConfig.GetPublickey().Trim();
            _input_charset = AllinPayConfig.input_charset.Trim().ToLower();
            _sign_type = AllinPayConfig.sign_method.Trim().ToUpper();
        }

        /// <summary>
        ///  验证消息是否是通联支付发出的合法消息
        /// </summary>
        /// <param name="inputPara">通知返回参数数组</param>
        /// <param name="sign">通联支付生成的签名结果</param>
        /// <returns>验证结果</returns>
        public bool Verify(SortedDictionary<string, string> inputPara, string sign)
        {
            //获取返回时的签名验证结果
            bool isSign = GetSignVeryfy(inputPara, sign);

            //判断responsetTxt是否为true，isSign是否为true
            //responsetTxt的结果不是true，与服务器设置问题、合作身份者ID、notify_id一分钟失效有关
            //isSign不是true，与安全校验码、请求时的参数格式（如：带自定义参数等）、编码格式有关
            if (isSign)//验证成功
            {
                return true;
            }
            else//验证失败
            {
                return false;
            }
        }

        /// <summary>
        /// 获取返回时的签名验证结果
        /// </summary>
        /// <param name="inputPara">通知返回参数数组</param>
        /// <param name="sign">对比的签名结果</param>
        /// <returns>签名验证结果</returns>
        private bool GetSignVeryfy(SortedDictionary<string, string> inputPara, string sign)
        {
            Dictionary<string, string> sPara = new Dictionary<string, string>();

            //过滤空值、sign与sign_type参数
            sPara = AllinPayCore.FilterPara(inputPara);

            //获取待签名字符串
            string preSignStr = AllinPayCore.CreateLinkString(sPara);

            //获得签名验证结果
            bool isSgin = false;
            if (sign != null && sign != "")
            {
                switch (_sign_type)
                {
                    case "RSA":
                        isSgin = AllinPayRSA.verify(preSignStr, sign, allinpay_public_key, _input_charset);
                        break;
                    default:
                        break;
                }
            }

            return isSgin;
        }
    }
}