using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.Busines.CustomerManage;
using HZSoft.Util;
using HZSoft.Util.WebControl;
using System.Web.Mvc;

namespace HZSoft.Application.Web.Areas.CustomerManage.Controllers
{
    /// <summary>
    /// �� �� 6.1
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2017-11-04 16:22
    /// �� ��������
    /// </summary>
    public class CRMAPIController : Controller
    {
        private CRM_CallLogBLL crm_calllogbll = new CRM_CallLogBLL();


        #region �ύ����
        /// <summary>
        /// �ϴ�����
        /// </summary>
        /// <param name="calllog">���ܻ���json�ַ���</param>
        /// <returns></returns>

        public ActionResult PushFollowUpRecord(string calllog)
        {
            string url = Request.Url.ToString();
            //WriteInLog log = new WriteInLog();
            //log.writeInLog(url);
            //log.writeInLog(calllog);
            string calllogBit = calllog.Replace(":true", ":1").Replace(":false", ":0");
            //log.writeInLog(calllogBit);

            try
            {
                CRM_CallLogEntity entity = Newtonsoft.Json.JsonConvert.DeserializeObject<CRM_CallLogEntity>(calllogBit);
                //log.writeInLog("���л����");
                crm_calllogbll.SaveForm(entity);
                return Content(new ReturnMessage { Success = true, Message = "�ϴ��ɹ�", Value = null, ErrorCode = 0 }.ToString());
                //int row = crm_calllogbll.SaveForm(entity);
                //if (row == 1)
                //{
                //    return Content(new ReturnMessage { Success = true, Message = "�ϴ��ɹ�", Value = null, ErrorCode = 0 }.ToString());
                //}
                //else
                //{
                //    return Content(new ReturnMessage { Success = false, Message = "�ϴ�ʧ��", Value = null, ErrorCode = 0 }.ToString());
                //}
            }
            catch (System.Exception)
            {
                return Content(new ReturnMessage { Success = false, Message = "д�����ݿ�ʧ��", Value = null, ErrorCode = 0 }.ToString());
            }

        }
        #endregion
    }
}
