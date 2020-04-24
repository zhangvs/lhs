using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.Busines.CustomerManage;
using HZSoft.Util;
using HZSoft.Util.WebControl;
using System.Web.Mvc;
using HZSoft.Application.Code;
using HZSoft.Application.Busines.LOHAS_CustomerManage;
using System;
using System.IO;
using System.Data;
using HZSoft.Util.Offices;
using System.Web;

namespace HZSoft.Application.Web.Areas.CustomerManage.Controllers
{
    /// <summary>
    /// �� �� 6.1
    /// 
    /// �� �����ܴ���
    /// �� �ڣ�2016-03-14 09:47
    /// �� �����ͻ���Ϣ
    /// </summary>
    public class LOHAS_CustomerController : MvcControllerBase
    {
        private LOHAS_CustomerBLL customerbll = new LOHAS_CustomerBLL();
        private CustomerContactBLL customercontactbll = new CustomerContactBLL();

        #region ��ͼ����
        /// <summary>
        /// �ͻ��б�ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// �ͻ���ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult Form()
        {
            return View();
        }

        /// <summary>
        /// �ͻ���ϸҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult Detail()
        {
            return View();
        }
        /// <summary>
        /// ��ϵ���б�
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult ContactIndex()
        {
            return View();
        }
        /// <summary>
        /// ��ϵ�˱�
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ContactForm()
        {
            return View();
        }
        #endregion

        #region ��ȡ����
        /// <summary>
        /// ��ȡ�ͻ��б�
        /// </summary>
        /// <returns>�����б�Json</returns>
        [HttpGet]
        public ActionResult GetListJson()
        {
            var data = customerbll.GetList();
            return ToJsonResult(data);
        }
        /// <summary>
        /// ��ȡ�ͻ��б�
        /// </summary>
        /// <param name="pagination">��ҳ����</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = customerbll.GetPageList(pagination, queryJson);
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
        /// ��ȡ�ͻ�ʵ�� 
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns>���ض���Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = customerbll.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        /// <summary>
        /// ��ȡ��ϵ���б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�Json</returns>
        [HttpGet]
        public ActionResult GetContactListJson(string queryJson)
        {
            var data = customercontactbll.GetList(queryJson);
            return ToJsonResult(data);
        }
        /// <summary>
        /// ��ȡ��ϵ��ʵ�� 
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns>���ض���Json</returns>
        [HttpGet]
        public ActionResult GetContactFormJson(string keyValue)
        {
            var data = customercontactbll.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        #endregion

        #region ��֤����
        /// <summary>
        /// �ͻ����Ʋ����ظ�
        /// </summary>
        /// <param name="FullName">����</param>
        /// <param name="keyValue">����</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExistFullName(string FullName, string keyValue)
        {
            bool IsOk = customerbll.ExistFullName(FullName, keyValue);
            return Content(IsOk.ToString());
        }
        /// <summary>
        /// �ֻ������ظ�
        /// </summary>
        /// <param name="Mobile">����</param>
        /// <param name="keyValue">����</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExistMobile(string Mobile, string keyValue)
        {
            bool IsOk = customerbll.ExistMobile(Mobile, keyValue);
            return Content(IsOk.ToString());
        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ���ͻ�����
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        [HttpPost]
        
        [AjaxOnly]
        
        public ActionResult RemoveForm(string keyValue)
        {
            customerbll.RemoveForm(keyValue);
            return Success("ɾ���ɹ���");
        }
        /// <summary>
        /// ����ͻ������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        [HttpPost]
        
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, LOHAS_CustomerEntity entity)
        {
            customerbll.SaveForm(keyValue, entity);
            return Success("�����ɹ���");
        }
        /// <summary>
        /// ɾ����ϵ������
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        [HttpPost]
        
        [AjaxOnly]
        public ActionResult RemoveContactForm(string keyValue)
        {
            customercontactbll.RemoveForm(keyValue);
            return Success("ɾ���ɹ���");
        }
        /// <summary>
        /// ������ϵ�˱����������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        [HttpPost]
        
        [AjaxOnly]
        public ActionResult SaveContactForm(string keyValue, CustomerContactEntity entity)
        {
            customercontactbll.SaveForm(keyValue, entity);
            return Success("�����ɹ���");
        }


        #region ��������
        public FileResult GetFile()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "Resource/ExcelTemplate/";
            string fileName = "�ͻ�����ģ��.xls";
            return File(path + fileName, "application/ms-excel", fileName);
        }

        public ActionResult Import()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Import(HttpPostedFileBase filebase)
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
                string FileType = ".csv,.xls,.xlsx";//�����ϴ��ļ��������ַ���

                FileName = NoFileName + DateTime.Now.ToString("yyyyMMddhhmmss") + fileEx;
                if (!FileType.Contains(fileEx))
                {
                    ViewBag.error = "�ļ����Ͳ���!";
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

            DataTable dtSource = ExcelHelper.ImportExcel(savePath);

            //����������ƣ�����ʱ������ع�
            ViewBag.error = customerbll.BatchAdd(dtSource);

            System.Threading.Thread.Sleep(2000);
            return View();
        }
        #endregion
        #endregion
    }
}
