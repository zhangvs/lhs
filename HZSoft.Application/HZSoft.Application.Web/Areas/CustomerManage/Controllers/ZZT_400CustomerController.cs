using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.Busines.CustomerManage;
using HZSoft.Util;
using HZSoft.Util.WebControl;
using System.Web.Mvc;
using System.Data;
using HZSoft.Util.Offices;
using System;
using System.IO;
using System.Web;
using HZSoft.Application.Busines.SystemManage;
using HZSoft.Application.Code;

namespace HZSoft.Application.Web.Areas.CustomerManage.Controllers
{
    /// <summary>
    /// �� �� 6.1
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2018-05-04 13:50
    /// �� ����400�ͻ�
    /// </summary>
    public class ZZT_400CustomerController : MvcControllerBase
    {
        private ZZT_400CustomerBLL zzt_400customerbll = new ZZT_400CustomerBLL();
        private CodeRuleBLL codeRuleBLL = new CodeRuleBLL();

        #region ��ͼ����
        /// <summary>
        /// �б�ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ZZT_400CustomerIndex()
        {
            return View();
        }
        /// <summary>
        /// ��ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ZZT_400CustomerForm()
        {
            return View();
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ZZT_400CustomerSea()
        {
            return View();
        }
        /// <summary>
        /// ����վ
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ZZT_400CustomerDelete()
        {
            return View();
        }
        /// <summary>
        /// �б�ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OptionTraceUser()
        {
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
            var data = zzt_400customerbll.GetPageList(pagination, queryJson);
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
            var data = zzt_400customerbll.GetList(queryJson);
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
            var data = zzt_400customerbll.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        #endregion

        #region ��֤����
        /// <summary>
        /// �˻������ظ�
        /// </summary>
        /// <param name="Mobile">�˻�ֵ</param>
        /// <param name="keyValue">����</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExistMobile(string Mobile, string keyValue)
        {
            bool IsOk = zzt_400customerbll.ExistMobile(Mobile, keyValue);
            return Content(IsOk.ToString());
        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValues">����ֵ</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveForm(string keyValues)
        {
            zzt_400customerbll.RemoveForm(keyValues);
            return Success("ɾ���ɹ���");
        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, ZZT_400CustomerEntity entity)
        {
            zzt_400customerbll.SaveForm(keyValue, entity);
            return Success("�����ɹ���");
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="keyValues">�ͻ�ids</param>
        /// <param name="TraceUserId">Ա��id</param>
        /// <param name="TraceUserName">Ա������</param>
        [HttpPost]
        public ActionResult SaveAssign(string keyValues, string TraceUserId, string TraceUserName)
        {
            zzt_400customerbll.SaveAssign(keyValues, TraceUserId, TraceUserName);
            return Success("�����ɹ���");
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="keyValues">�ͻ�ids</param>
        [HttpPost]
        public ActionResult SaveThrow(string keyValues)
        {
            zzt_400customerbll.SaveThrow(keyValues);
            return Success("�����ɹ���");
        }
        /// <summary>
        /// ��ȡ
        /// </summary>
        /// <param name="keyValues">�ͻ�ids</param>
        [HttpPost]
        public ActionResult SaveObtain(string keyValues)
        {
            zzt_400customerbll.SaveObtain(keyValues);
            return Success("�����ɹ���");
        }
        #endregion

        #region ��������400
        public FileResult GetFile400()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "Resource/ExcelTemplate/";
            string fileName = "400����ģ��.xlsx";
            return File(path + fileName, "application/ms-excel", fileName);
        }

        public ActionResult Import400()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Import400(HttpPostedFileBase filebase)
        {
            HttpPostedFileBase file = Request.Files["files"];
            string FileName;
            string savePath;
            if (file == null || file.ContentLength <= 0)
            {
                ViewBag.error = "�ļ�����Ϊ��";
                return View();
            }
            else
            {
                string filename = Path.GetFileName(file.FileName);
                int filesize = file.ContentLength;//��ȡ�ϴ��ļ��Ĵ�С��λΪ�ֽ�byte
                string fileEx = Path.GetExtension(filename);//��ȡ�ϴ��ļ�����չ��
                string NoFileName = Path.GetFileNameWithoutExtension(filename);//��ȡ����չ�����ļ���
                int Maxsize = 4000 * 1024;//�����ϴ��ļ������ռ��СΪ4M
                string FileType = ".csv";//�����ϴ��ļ��������ַ���

                FileName = NoFileName + DateTime.Now.ToString("yyyyMMddhhmmss") + fileEx;
                if (!FileType.Contains(fileEx))
                {
                    ViewBag.error = "�ļ����Ͳ��ԣ�ֻ�ܵ���.csv��ʽ���ļ�";
                    return View();
                }
                if (filesize >= Maxsize)
                {
                    ViewBag.error = "�ϴ��ļ�����4M�������ϴ�";
                    return View();
                }
                string path = AppDomain.CurrentDomain.BaseDirectory + "Resource/ExcelData/";
                savePath = Path.Combine(path, FileName);
                file.SaveAs(savePath);
            }

            DataTable dtSource = ExcelHelper.GetCsvData(savePath);

            //����������ƣ�����ʱ������ع�
            ViewBag.error = zzt_400customerbll.BatchAdd400(dtSource);

            System.Threading.Thread.Sleep(2000);
            return View();
        }
        #endregion
    }
}
