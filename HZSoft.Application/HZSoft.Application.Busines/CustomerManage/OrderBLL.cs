using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.IService.CustomerManage;
using HZSoft.Application.Service.CustomerManage;
using HZSoft.Util.WebControl;
using System.Collections.Generic;
using System;

namespace HZSoft.Application.Busines.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创 建：佘赐雄
    /// 日 期：2016-03-16 13:54
    /// 描 述：订单管理
    /// </summary>
    public class OrderBLL
    {
        private IOrderService service = new OrderService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<OrderEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        public IEnumerable<OrderEntity> GetCheckPageList(Pagination pagination, string queryJson)
        {
            return service.GetCheckPageList(pagination, queryJson);
        }
        public IEnumerable<OrderEntity> GetAccCheckPageList(Pagination pagination, string queryJson)
        {
            return service.GetAccCheckPageList(pagination, queryJson);
        }
        public IEnumerable<OrderEntity> GetKeeperPageList(Pagination pagination, string queryJson)
        {
            return service.GetKeeperPageList(pagination, queryJson);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public OrderEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        /// <summary>
        /// 获取前单、后单 数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="type">类型（1-前单；2-后单）</param>
        /// <returns>返回实体</returns>
        public OrderEntity GetPrevOrNextEntity(string keyValue, int type)
        {
            return service.GetPrevOrNextEntity(keyValue, type);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 修改订单状态
        /// </summary>
        /// <param name="keyValue">客户ids</param>
        public void SaveOrderState(string keyValue, int? orderState)
        {
            try
            {
                service.SaveOrderState(keyValue, orderState);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            try
            {
                service.RemoveForm(keyValue);
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
        /// <param name="entitys">明细实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, OrderEntity entity, List<OrderEntryEntity> entitys)
        {
            try
            {
                service.SaveForm(keyValue, entity, entitys);
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
        /// <param name="entitys">明细实体对象</param>
        /// <returns></returns>
        public void SaveKeeperForm(string keyValue, OrderEntity entity, List<OrderEntryEntity> entitys)
        {
            try
            {
                service.SaveKeeperForm(keyValue, entity, entitys);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 保持追踪记录
        /// </summary>
        /// <param name="entity"></param>
        public void SavePushRecord(TracesPushRecord entity)
        {
            try
            {
                service.SavePushRecord(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}