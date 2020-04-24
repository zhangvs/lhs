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
    /// 版 本 6.1
    /// 
    /// 创 建：佘赐雄
    /// 日 期：2016-03-14 09:47
    /// 描 述：客户信息
    /// </summary>
    public class LOHAS_CustomerController : MvcControllerBase
    {
        private LOHAS_CustomerBLL customerbll = new LOHAS_CustomerBLL();
        private CustomerContactBLL customercontactbll = new CustomerContactBLL();

        #region 视图功能
        /// <summary>
        /// 客户列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 客户表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult Form()
        {
            return View();
        }

        /// <summary>
        /// 客户明细页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult Detail()
        {
            return View();
        }
        /// <summary>
        /// 联系人列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        
        public ActionResult ContactIndex()
        {
            return View();
        }
        /// <summary>
        /// 联系人表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ContactForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取客户列表
        /// </summary>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson()
        {
            var data = customerbll.GetList();
            return ToJsonResult(data);
        }
        /// <summary>
        /// 获取客户列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
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
        /// 获取客户实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = customerbll.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 获取联系人列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetContactListJson(string queryJson)
        {
            var data = customercontactbll.GetList(queryJson);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 获取联系人实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetContactFormJson(string keyValue)
        {
            var data = customercontactbll.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 客户名称不能重复
        /// </summary>
        /// <param name="FullName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExistFullName(string FullName, string keyValue)
        {
            bool IsOk = customerbll.ExistFullName(FullName, keyValue);
            return Content(IsOk.ToString());
        }
        /// <summary>
        /// 手机不能重复
        /// </summary>
        /// <param name="Mobile">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExistMobile(string Mobile, string keyValue)
        {
            bool IsOk = customerbll.ExistMobile(Mobile, keyValue);
            return Content(IsOk.ToString());
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除客户数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        
        [AjaxOnly]
        
        public ActionResult RemoveForm(string keyValue)
        {
            customerbll.RemoveForm(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 保存客户表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, LOHAS_CustomerEntity entity)
        {
            customerbll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }
        /// <summary>
        /// 删除联系人数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        
        [AjaxOnly]
        public ActionResult RemoveContactForm(string keyValue)
        {
            customercontactbll.RemoveForm(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 保存联系人表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        
        [AjaxOnly]
        public ActionResult SaveContactForm(string keyValue, CustomerContactEntity entity)
        {
            customercontactbll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }


        #region 批量导入
        public FileResult GetFile()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "Resource/ExcelTemplate/";
            string fileName = "客户导入模板.xls";
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
                ViewBag.error = "文件不能为空";
                return View();
            }
            else
            {
                string filename = Path.GetFileName(file.FileName);
                int filesize = file.ContentLength;//获取上传文件的大小单位为字节byte
                string fileEx = Path.GetExtension(filename);//获取上传文件的扩展名
                string NoFileName = Path.GetFileNameWithoutExtension(filename);//获取无扩展名的文件名
                int Maxsize = 4000 * 1024;//定义上传文件的最大空间大小为4M
                string FileType = ".csv,.xls,.xlsx";//定义上传文件的类型字符串

                FileName = NoFileName + DateTime.Now.ToString("yyyyMMddhhmmss") + fileEx;
                if (!FileType.Contains(fileEx))
                {
                    ViewBag.error = "文件类型不对!";
                    return View();
                }
                if (filesize >= Maxsize)
                {
                    ViewBag.error = "上传文件超过4M，不能上传";
                    return View();
                }
                string path = AppDomain.CurrentDomain.BaseDirectory + "Resource/ExcelData/";
                savePath = Path.Combine(path, FileName);
                file.SaveAs(savePath);
            }

            DataTable dtSource = ExcelHelper.ImportExcel(savePath);

            //引用事务机制，出错时，事物回滚
            ViewBag.error = customerbll.BatchAdd(dtSource);

            System.Threading.Thread.Sleep(2000);
            return View();
        }
        #endregion
        #endregion
    }
}
