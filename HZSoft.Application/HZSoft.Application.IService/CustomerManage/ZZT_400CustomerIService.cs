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
    /// 日 期：2018-05-04 13:50
    /// 描 述：400客户
    /// </summary>
    public interface ZZT_400CustomerIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<ZZT_400CustomerEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<ZZT_400CustomerEntity> GetList(string queryJson);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        ZZT_400CustomerEntity GetEntity(string keyValue);
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
        /// <param name="keyValues">主键</param>
        void RemoveForm(string keyValues);
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        void SaveForm(string keyValue, ZZT_400CustomerEntity entity);
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
        #endregion

        /// <summary>
        /// 批量（新增）400
        /// </summary>
        /// <param name="dtSource">实体对象</param>
        /// <returns></returns>
        string BatchAdd400(DataTable dtSource);
    }
}
