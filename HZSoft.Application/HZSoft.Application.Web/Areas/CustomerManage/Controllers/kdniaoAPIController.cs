using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.Busines.CustomerManage;
using HZSoft.Util;
using HZSoft.Util.WebControl;
using System.Web.Mvc;
using System;

namespace HZSoft.Application.Web.Areas.CustomerManage.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创 建：超级管理员
    /// 日 期：2017-11-04 16:22
    /// 描 述：话单
    /// </summary>
    public class KdniaoAPIController : Controller
    {
        private OrderBLL orderBLL = new OrderBLL();


        #region 提交数据
        /// <summary>
        /// 推送接口
        /// </summary>
        /// <param name="requestType">101-轨迹查询结果</param>
        /// <param name="requestData">请求内容需进行URL(utf-8)编码。请求内容只支持JSON格式。</param>
        /// <param name="DataSign">	数据内容签名（把(请求内容(未编码)+AppKey)进行MD5加密，然后Base64编码）</param>
        /// <returns></returns>
        public ActionResult SavePushRecord(string requestType, string requestData, string DataSign)
        {
            string url = Request.Url.ToString();
            WriteInLog log = new WriteInLog();
            log.writeInLog(url);
            log.writeInLog(requestType);
            log.writeInLog(requestData);
            log.writeInLog(DataSign);
            try
            {
                TracesPushRecord entity = Newtonsoft.Json.JsonConvert.DeserializeObject<TracesPushRecord>(requestData);
                log.writeInLog("序列化完毕");
                //保存跟进记录
                orderBLL.SavePushRecord(entity);
                return Content(new PushReturnMessage { EBusinessID = "1363273", UpdateTime = DateTime.Now, Success = true, Reason = "成功" }.ToString());
            }
            catch (Exception ex)
            {
                return Content(new PushReturnMessage { EBusinessID = "1363273", UpdateTime = DateTime.Now, Success = false, Reason = "写入数据库失败" }.ToString());
            }

        }
        #endregion
    }
}
