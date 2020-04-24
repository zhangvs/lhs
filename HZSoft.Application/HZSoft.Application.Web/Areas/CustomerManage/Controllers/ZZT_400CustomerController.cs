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
    /// 版 本 6.1
    /// 
    /// 创 建：超级管理员
    /// 日 期：2018-05-04 13:50
    /// 描 述：400客户
    /// </summary>
    public class ZZT_400CustomerController : MvcControllerBase
    {
        private ZZT_400CustomerBLL zzt_400customerbll = new ZZT_400CustomerBLL();
        private CodeRuleBLL codeRuleBLL = new CodeRuleBLL();

        #region 视图功能
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ZZT_400CustomerIndex()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ZZT_400CustomerForm()
        {
            return View();
        }
        /// <summary>
        /// 公海
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ZZT_400CustomerSea()
        {
            return View();
        }
        /// <summary>
        /// 垃圾站
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ZZT_400CustomerDelete()
        {
            return View();
        }
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OptionTraceUser()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
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
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = zzt_400customerbll.GetList(queryJson);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 获取实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = zzt_400customerbll.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 账户不能重复
        /// </summary>
        /// <param name="Mobile">账户值</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExistMobile(string Mobile, string keyValue)
        {
            bool IsOk = zzt_400customerbll.ExistMobile(Mobile, keyValue);
            return Content(IsOk.ToString());
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValues">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveForm(string keyValues)
        {
            zzt_400customerbll.RemoveForm(keyValues);
            return Success("删除成功。");
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, ZZT_400CustomerEntity entity)
        {
            zzt_400customerbll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }
        /// <summary>
        /// 分配
        /// </summary>
        /// <param name="keyValues">客户ids</param>
        /// <param name="TraceUserId">员工id</param>
        /// <param name="TraceUserName">员工名称</param>
        [HttpPost]
        public ActionResult SaveAssign(string keyValues, string TraceUserId, string TraceUserName)
        {
            zzt_400customerbll.SaveAssign(keyValues, TraceUserId, TraceUserName);
            return Success("操作成功。");
        }
        /// <summary>
        /// 回收
        /// </summary>
        /// <param name="keyValues">客户ids</param>
        [HttpPost]
        public ActionResult SaveThrow(string keyValues)
        {
            zzt_400customerbll.SaveThrow(keyValues);
            return Success("操作成功。");
        }
        /// <summary>
        /// 领取
        /// </summary>
        /// <param name="keyValues">客户ids</param>
        [HttpPost]
        public ActionResult SaveObtain(string keyValues)
        {
            zzt_400customerbll.SaveObtain(keyValues);
            return Success("操作成功。");
        }
        #endregion

        #region 批量导入400
        public FileResult GetFile400()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "Resource/ExcelTemplate/";
            string fileName = "400导入模板.xlsx";
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
                string FileType = ".csv";//定义上传文件的类型字符串

                FileName = NoFileName + DateTime.Now.ToString("yyyyMMddhhmmss") + fileEx;
                if (!FileType.Contains(fileEx))
                {
                    ViewBag.error = "文件类型不对，只能导入.csv格式的文件";
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

            DataTable dtSource = ExcelHelper.GetCsvData(savePath);

            //引用事务机制，出错时，事物回滚
            ViewBag.error = zzt_400customerbll.BatchAdd400(dtSource);

            System.Threading.Thread.Sleep(2000);
            return View();
        }
        #endregion
    }
}
