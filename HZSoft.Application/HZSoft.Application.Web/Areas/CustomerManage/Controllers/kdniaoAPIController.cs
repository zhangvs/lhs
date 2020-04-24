using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.Busines.CustomerManage;
using HZSoft.Util;
using HZSoft.Util.WebControl;
using System.Web.Mvc;
using System;

namespace HZSoft.Application.Web.Areas.CustomerManage.Controllers
{
    /// <summary>
    /// �� �� 6.1
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2017-11-04 16:22
    /// �� ��������
    /// </summary>
    public class KdniaoAPIController : Controller
    {
        private OrderBLL orderBLL = new OrderBLL();


        #region �ύ����
        /// <summary>
        /// ���ͽӿ�
        /// </summary>
        /// <param name="requestType">101-�켣��ѯ���</param>
        /// <param name="requestData">�������������URL(utf-8)���롣��������ֻ֧��JSON��ʽ��</param>
        /// <param name="DataSign">	��������ǩ������(��������(δ����)+AppKey)����MD5���ܣ�Ȼ��Base64���룩</param>
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
                log.writeInLog("���л����");
                //���������¼
                orderBLL.SavePushRecord(entity);
                return Content(new PushReturnMessage { EBusinessID = "1363273", UpdateTime = DateTime.Now, Success = true, Reason = "�ɹ�" }.ToString());
            }
            catch (Exception ex)
            {
                return Content(new PushReturnMessage { EBusinessID = "1363273", UpdateTime = DateTime.Now, Success = false, Reason = "д�����ݿ�ʧ��" }.ToString());
            }

        }
        #endregion
    }
}
