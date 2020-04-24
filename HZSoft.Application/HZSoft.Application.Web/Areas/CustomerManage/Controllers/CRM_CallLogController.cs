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
    public class CRM_CallLogController : MvcControllerBase
    {
        private CRM_CallLogBLL crm_calllogbll = new CRM_CallLogBLL();

        #region ��ͼ����
        /// <summary>
        /// �б�ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CRM_CallLogIndex()
        {
            return View();
        }
        /// <summary>
        /// ��ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CRM_CallLogForm()
        {
            return View();
        }
        /// <summary>
        /// ��ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PlayWav(string RecordingFile, string WorkerName,string WorkerUserName, string CallNumber,string CallDuration)
        {
            ViewBag.RecordingFile = RecordingFile;
            ViewBag.WorkerName = WorkerName;
            ViewBag.WorkerUserName = WorkerUserName;
            ViewBag.CallNumber = CallNumber;
            TimeSpan ts = new TimeSpan(0, 0, Convert.ToInt32(CallDuration));
            string str = "";
            if (ts.Hours > 0)
            {
                str = ts.Hours.ToString() + "Сʱ " + ts.Minutes.ToString() + "���� " + ts.Seconds + "��";
            }
            if (ts.Hours == 0 && ts.Minutes > 0)
            {
                str = ts.Minutes.ToString() + "���� " + ts.Seconds + "��";
            }
            if (ts.Hours == 0 && ts.Minutes == 0)
            {
                str = ts.Seconds + "��";
            }
            ViewBag.CallDuration = str;
            return View();
        }
        
        #endregion

        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ����</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = crm_calllogbll.GetPageList(pagination, queryJson);
            var jsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return ToJsonResult(jsonData);
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = crm_calllogbll.GetList(queryJson);
            return ToJsonResult(data);
        }
        /// <summary>
        /// ��ȡʵ�� 
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns>���ض���Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = crm_calllogbll.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveForm(string keyValue)
        {
            crm_calllogbll.RemoveForm(keyValue);
            return Success("ɾ���ɹ���");
        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[AjaxOnly]
        //public ActionResult SaveForm(string keyValue, CRM_CallLogEntity entity)
        //{
        //    crm_calllogbll.SaveForm(keyValue, entity);
        //    return Success("�����ɹ���");
        //}

        public ActionResult SaveForm(CRM_CallLogEntity entity)
        {
            crm_calllogbll.SaveForm(entity);
            return Success("�����ɹ���");
            //var jsonData = new
            //{
            //    Success = true,
            //    Message = "",
            //    Value = "",
            //    ErrorCode = 0
            //};
            //return ToJsonResult(jsonData);
        }
        #endregion
    }
}
