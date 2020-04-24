using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.IService.CustomerManage;
using HZSoft.Application.Service.CustomerManage;
using HZSoft.Util.WebControl;
using System.Collections.Generic;
using System;
using System.Data;

namespace HZSoft.Application.Busines.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创 建：超级管理员
    /// 日 期：2018-05-04 13:50
    /// 描 述：400客户
    /// </summary>
    public class ZZT_400CustomerBLL
    {
        private ZZT_400CustomerIService service = new ZZT_400CustomerService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<ZZT_400CustomerEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<ZZT_400CustomerEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ZZT_400CustomerEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 账户不能重复
        /// </summary>
        /// <param name="Mobile">账户值</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistMobile(string Mobile, string keyValue = "")
        {
            return service.ExistMobile(Mobile, keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValues">主键</param>
        public void RemoveForm(string keyValues)
        {
            try
            {
                service.RemoveForm(keyValues);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, ZZT_400CustomerEntity entity)
        {
            try
            {
                service.SaveForm(keyValue, entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保存跟进人
        /// </summary>
        /// <param name="keyValues">客户ids</param>
        /// <param name="TraceUserId">员工id</param>
        /// <param name="TraceUserName">员工名称</param>
        public void SaveAssign(string keyValues, string TraceUserId, string TraceUserName)
        {
            try
            {
                service.SaveAssign(keyValues, TraceUserId, TraceUserName);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 作废跟进人
        /// </summary>
        /// <param name="keyValues">客户ids</param>
        public void SaveThrow(string keyValues)
        {
            try
            {
                service.SaveThrow(keyValues);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 领取
        /// </summary>
        /// <param name="keyValues">客户ids</param>
        public void SaveObtain(string keyValues)
        {
            try
            {
                service.SaveObtain(keyValues);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        /// <summary>
        /// 保存表单400（新增、修改）
        /// </summary>
        /// <returns></returns>
        public string BatchAdd400(DataTable dtSource)
        {
            try
            {
                string returnMsg = service.BatchAdd400(dtSource);
                return returnMsg;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}
