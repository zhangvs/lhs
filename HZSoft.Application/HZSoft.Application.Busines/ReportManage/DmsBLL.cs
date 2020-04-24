using HZSoft.Application.Entity.AuthorizeManage;
using HZSoft.Application.Entity.ReportManage;
using HZSoft.Application.IService.ReportManage;
using HZSoft.Application.Service.ReportManage;
using System;
using System.Collections.Generic;
using System.Data;

namespace HZSoft.Application.Busines.ReportManage
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创建人：刘晓雷
    /// 日 期：2016.1.14 14:27
    /// 描 述：报表模板管理
    /// </summary>
    public class DmsBLL
    {
        private IDmsService service = new DmsService();
        /// <summary>
        /// 员工报表
        /// </summary>
        /// <param name="postData">起始，结束日期json</param>
        /// <returns></returns>
        public DataTable GetDateOrder_emp(string queryJson)
        {
            return service.GetDateOrder_emp(queryJson);
        }
        /// <summary>
        /// 销售号码分析报表
        /// </summary>
        /// <param name="postData">起始，结束日期json</param>
        /// <returns></returns>
        public DataTable GetAnalysis(string queryJson)
        {
            return service.GetAnalysis(queryJson);
        }
        /// <summary>
        /// 话单通话时长
        /// </summary>
        /// <param name="postData">起始，结束日期json</param>
        /// <returns></returns>
        public DataTable GetCallLog(string queryJson)
        {
            return service.GetCallLog(queryJson);
        }
        #region 获取数据

        #endregion
    }
}
