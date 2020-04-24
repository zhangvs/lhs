using HZSoft.Application.Entity.AuthorizeManage;
using HZSoft.Application.Entity.ReportManage;
using System.Collections.Generic;
using System.Data;

namespace HZSoft.Application.IService.ReportManage
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创建人：刘晓雷
    /// 日 期：2016.1.14 14:27
    /// 描 述：报表模板管理
    /// </summary>
    public interface IDmsService
    {
        #region 获取数据
        /// <summary>
        /// 员工报表
        /// </summary>
        /// <param name="postData">起始，结束日期json</param>
        /// <returns></returns>
        DataTable GetDateOrder_emp(string queryJson);
        /// <summary>
        /// 销售号码分析报表
        /// </summary>
        /// <param name="postData">起始，结束日期json</param>
        /// <returns></returns>
        DataTable GetAnalysis(string queryJson);
        /// <summary>
        /// 话单通话时长
        /// </summary>
        /// <param name="postData">起始，结束日期json</param>
        /// <returns></returns>
        DataTable GetCallLog(string queryJson);
        #endregion

    }
}
