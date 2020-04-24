using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.Busines.CustomerManage;
using HZSoft.Util;
using HZSoft.Util.WebControl;
using System.Web.Mvc;
using System.Web;
using System.IO;
using System;
using Newtonsoft.Json;
using System.Data;
using HZSoft.Util.Offices;

namespace HZSoft.Application.Web.Areas.CustomerManage.Controllers
{
    /// <summary>
    /// �� �� 6.1
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2018-03-23 16:28
    /// �� ������˾��
    /// </summary>
    public class Ku_CompanyController : MvcControllerBase
    {
        private Ku_CompanyBLL ku_companybll = new Ku_CompanyBLL();

        #region ��ͼ����
        /// <summary>
        /// �б�ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Ku_CompanyIndex()
        {
            return View();
        }
        /// <summary>
        /// �б�ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MyKu_CompanyIndex()
        {
            return View();
        }
        /// <summary>
        /// �б�ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Ok_CompanyIndex()
        {
            return View();
        }
        /// <summary>
        /// ��ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Ku_CompanyForm()
        {
            return View();
        }
        /// <summary>
        /// �б�ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Ok_CompanyForm()
        {
            return View();
        }

        /// <summary>
        /// ��ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Detail()
        {
            return View();
        }
        /// <summary>
        /// ��ҳ��
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
            var data = ku_companybll.GetPageList(pagination, queryJson);
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
        /// <param name="pagination">��ҳ����</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�Json</returns>
        [HttpGet]
        public ActionResult MyGetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = ku_companybll.MyGetPageList(pagination, queryJson);
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
        /// <param name="pagination">��ҳ����</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�Json</returns>
        [HttpGet]
        public ActionResult OkGetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = ku_companybll.OkGetPageList(pagination, queryJson);
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
            var data = ku_companybll.GetList(queryJson);
            return ToJsonResult(data );
        }
        /// <summary>
        /// ��ȡʵ�� 
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns>���ض���Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(int? keyValue)
        {
            var data = ku_companybll.GetEntity(keyValue);
            return ToJsonResult(data);
        }

        /// <summary>
        /// ��ȡ�б��Զ���ȫ��˾����
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CompanyNameAuto(string CompanyName)
        {
            var entity = ku_companybll.GetListByName(CompanyName);
            return Content(JsonConvert.SerializeObject(entity));
        }
        /// <summary>
        /// ��ȡ�б��Զ���ȫ��˾���ƣ�˽�أ�
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CompanyNameAutoSi(string CompanyName)
        {
            var entity = ku_companybll.GetListByNameSi(CompanyName);
            return Content(JsonConvert.SerializeObject(entity));
        }
        #endregion

        #region ��֤����
        /// <summary>
        /// �ͻ����Ʋ����ظ�
        /// </summary>
        /// <param name="CompanyName">����</param>
        /// <param name="keyValue">����</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExistFullName(string CompanyName, string keyValue)
        {
            bool IsOk = ku_companybll.ExistFullName(CompanyName, keyValue);
            return Content(IsOk.ToString());
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
        public ActionResult RemoveForm(int? keyValue)
        {
            ku_companybll.RemoveForm(keyValue);
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
        public ActionResult SaveForm(int? keyValue, Ku_CompanyEntity entity)
        {
            ku_companybll.SaveForm(keyValue, entity);
            return Success("�����ɹ���");
        }

        /// <summary>
        /// ��ȡ
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public ActionResult ObtainForm(int? keyValue)
        {
            ku_companybll.ObtainForm(keyValue);
            return Success("�����ɹ���");
        }

        public ActionResult BatchObtain()
        {
            return View();
        }

        [HttpPost]
        public ActionResult BatchObtain(HttpPostedFileBase filebase)
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
                string FileType = ".xls,.xlsx";//�����ϴ��ļ��������ַ���

                FileName = NoFileName + DateTime.Now.ToString("yyyyMMddhhmmss") + fileEx;
                if (!FileType.Contains(fileEx))
                {
                    ViewBag.error = "�ļ����Ͳ��ԣ�ֻ�ܵ���xls��xlsx��ʽ���ļ�";
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
            ViewBag.error = ku_companybll.BatchObtain(dtSource);

            System.Threading.Thread.Sleep(2000);
            return View();
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="keyValue">�ͻ�ids</param>
        /// <param name="ObtainUserId">Ա��id</param>
        /// <param name="ObtainUserName">Ա������</param>
        [HttpPost]
        public ActionResult SaveAssign(int? keyValue, string ObtainUserId, string ObtainUserName)
        {
            ku_companybll.SaveAssign(keyValue, ObtainUserId, ObtainUserName);
            return Success("�����ɹ���");
        }
        /// <summary>
        /// ���빫��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public ActionResult ThrowForm(int? keyValue)
        {
            ku_companybll.ThrowForm(keyValue);
            return Success("�����ɹ���");
        }
        #endregion


        /// <summary>
        /// �ϴ�ͼƬ
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadPicture()
        {
            string Message = "";
            if (Request.Files.Count > 0)
            {
                HttpPostedFile file = System.Web.HttpContext.Current.Request.Files[0];
                if (file.ContentLength > 0)
                {
                    string fileExt = file.FileName.Substring(file.FileName.LastIndexOf('.'));//��׺
                    try
                    {
                        string fileGuid = Guid.NewGuid().ToString();
                        string uploadDate = DateTime.Now.ToString("yyyyMMdd");
                        string dir = string.Format("/Resource/DocumentFile/Company/{0}/", uploadDate);
                        if (Directory.Exists(Server.MapPath(dir)) == false)//��������ھʹ���file�ļ���
                        {
                            Directory.CreateDirectory(Server.MapPath(dir));
                        }
                        string newfileName = Guid.NewGuid().ToString();
                        //ԭͼ
                        string fullDir1 = dir + newfileName + fileExt;
                        string imgFilePath = Request.MapPath(fullDir1);
                        file.SaveAs(imgFilePath);

                        return Content(new JsonMessage { Success = true, Code = "0", Message = fullDir1 }.ToString());

                    }
                    catch (Exception ex)
                    {
                        Message = HttpUtility.HtmlEncode(ex.Message);
                        return Content(new JsonMessage { Success = false, Code = "-1", Message = Message }.ToString());
                    }
                }
                Message = "��ѡ��Ҫ�ϴ����ļ���";
                return Content(new JsonMessage { Success = false, Code = "-1", Message = Message }.ToString());
            }
            Message = "��ѡ��Ҫ�ϴ����ļ���";
            return Content(new JsonMessage { Success = false, Code = "-1", Message = Message }.ToString());
        }
    }
}
