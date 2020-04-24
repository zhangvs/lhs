using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Util.WebControl;
using System.Collections.Generic;
using System.Data;

namespace HZSoft.Application.IService.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创 建：超级管理员
    /// 日 期：2018-05-10 09:15
    /// 描 述：拼多多客户
    /// </summary>
    public interface ZZT_PDDCustomerIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<ZZT_PDDCustomerEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<ZZT_PDDCustomerEntity> GetList(string queryJson);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        ZZT_PDDCustomerEntity GetEntity(string keyValue);
        #endregion

        #region 验证数据
        /// <summary>
        /// 账户不能重复
        /// </summary>
        /// <param name="account">账户值</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        bool ExistMobile(string Mobile, string keyValue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RemoveForm(string keyValues);
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        void SaveForm(string keyValue, ZZT_PDDCustomerEntity entity);
        #endregion

        /// <summary>
        /// 保存跟进人
        /// </summary>
        /// <param name="keyValues">客户ids</param>
        /// <param name="TraceUserId">员工id</param>
        /// <param name="TraceUserName">员工名称</param>
        void SaveAssign(string keyValues, string TraceUserId, string TraceUserName);
        /// <summary>
        /// 作废跟进人
        /// </summary>
        /// <param name="keyValues">客户ids</param>
        void SaveThrow(string keyValues);
        /// <summary>
        /// 领取
        /// </summary>
        /// <param name="keyValues">客户ids</param>
        void SaveObtain(string keyValues);

        /// <summary>
        /// 批量（新增）PDD
        /// </summary>
        /// <param name="dtSource">实体对象</param>
        /// <returns></returns>
        string BatchAddPDD(DataTable dtSource);
    }
}
