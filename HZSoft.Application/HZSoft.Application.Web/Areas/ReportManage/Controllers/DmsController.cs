using HZSoft.Application.Busines;
using HZSoft.Application.Entity.ReportManage;
using HZSoft.Util;
using HZSoft.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using HZSoft.Application.Busines.ReportManage;
using HZSoft.Application.Entity.AuthorizeManage;
using HZSoft.Application.Busines.AuthorizeManage;
using HZSoft.Application.Code;

namespace HZSoft.Application.Web.Areas.ReportManage.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创建人：刘晓雷
    /// 日 期：2016.1.14 14:27
    /// 描 述：报表管理
    /// </summary>
    public class DmsController : MvcControllerBase
    {
        DmsBLL dmsBLL = new DmsBLL();

        #region 视图功能
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult dms_emp()
        {
            return View();
        }
        /// <summary>
        /// 员工报表
        /// </summary>
        /// <param name="queryJson">起始，结束日期json</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult getDateOrder_emp(string queryJson)
        {
            var dt = dmsBLL.GetDateOrder_emp(queryJson);
            return Content(dt.ToJson());
        }

        /// <summary>
        /// 分析页面
        /// </summary>
        /// <returns></returns>
        public ActionResult dms_analysis()
        {
            return View();
        }
        /// <summary>
        /// 销售号码分析报表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetAnalysis(string queryJson)
        {
            var dt = dmsBLL.GetAnalysis(queryJson);
            return Content(dt.ToJson());
        }
        /// <summary>
        /// 通话时长页面
        /// </summary>
        /// <returns></returns>
        public ActionResult dms_calllog()
        {
            return View();
        }
        /// <summary>
        /// 通话时长
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetCallLog(string queryJson)
        {
            var dt = dmsBLL.GetCallLog(queryJson);
            return Content(dt.ToJson());
        }

        #endregion

    }
}
