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
    /// 日 期：2020-04-11 21:25
    /// 描 述：客户信息管理
    /// </summary>
    public interface LOHAS_CustomerIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<LOHAS_CustomerEntity> GetList();
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<LOHAS_CustomerEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        LOHAS_CustomerEntity GetEntity(string keyValue);
        #endregion

        #region 验证数据
        /// <summary>
        /// 客户名称不能重复
        /// </summary>
        /// <param name="fullName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        bool ExistFullName(string fullName, string keyValue);
        /// <summary>
        /// 手机不能重复
        /// </summary>
        /// <param name="Mobile">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        bool ExistMobile(string Mobile, string keyValue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RemoveForm(string keyValue);
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        void SaveForm(string keyValue, LOHAS_CustomerEntity entity);

        /// <summary>
        /// 保存表单(新增/修改)
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        /// <param name="moduleId">模块</param>
        void SaveForm(string keyValue, LOHAS_CustomerEntity entity, string moduleId);
        #endregion

        /// <summary>
        /// 批量（新增）400
        /// </summary>
        /// <param name="dtSource">实体对象</param>
        /// <returns></returns>
        string BatchAdd(DataTable dtSource);
    }
}
