using CommonLib;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace LoginServer.Http
{
    public partial class HttpServer
    {
        /// <summary>
        /// sdk支付回调通知
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        void HttpPayCall(HttpListenerContext context)
        {
            //返回status含义：
            //0，接收失败
            //1，接收成功
            //if (Glob.net.loginToCenterClient.IsConnecting)
            //{
            //    ResponseOutput(context.Response, PFCyMsg.ResultString("", 0));
            //    return;
            //}
            PayNotifyData payArgs;
            try
            {
                using (StreamReader reader = new StreamReader(context.Request.InputStream))
                {
                    string notifyArgs = reader.ReadToEnd();
                    Logger.PayLog($"收到支付通知：============================{context.Request.RemoteEndPoint.Address.ToString()}");
                    Logger.PayLog($"{notifyArgs}");
                    payArgs = PayNotifyData.Deserialize(notifyArgs);
                    reader.Close();
                }
            }
            catch(Exception ex)
            {
                Logger.LogError(ex);
                ResponseOutput(context.Response, ex.ToString());
                return;
            }

            //验证回调签名后 
            if(payArgs != null && payArgs.VerifySign())
            {
                //再请求订单验证
                //if (!Glob.platformMgr.pfCySdk.PayVerify(payArgs))
                //{
                //    ResponseOutput(context.Response, PFCyMsg.ResultString("", 0));
                //    return;
                //}

                //向中央服务器请求发货
                //CS_Pay_Succeed msg = new CS_Pay_Succeed();
                //msg.OrderId = payArgs.GetOrderId();
                //msg.GoodsId = payArgs.GetGoodsId();
                //msg.PFOrderId = payArgs.GetPFOrderId();
                //msg.ChannelId = payArgs.GetChannelId();
                //Glob.net.loginToCenterClient.Send(msg);

                //给回调返回status
                // ResponseOutput(context.Response, PFCyMsg.ResultString("", 1));
            }
            else
            {
                //ResponseOutput(context.Response, PFCyMsg.ResultString("", 0));
            }
            
        }
    }
}