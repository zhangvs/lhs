using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.IService.CustomerManage;
using HZSoft.Data.Repository;
using HZSoft.Util.Extension;
using HZSoft.Util.WebControl;
using HZSoft.Util;
using System.Collections.Generic;
using System.Linq;
using System;
using HZSoft.Application.Service.SystemManage;
using HZSoft.Application.IService.SystemManage;
using HZSoft.Application.Code;
using HZSoft.Application.Entity.BaseManage;

namespace HZSoft.Application.Service.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创 建：超级管理员
    /// 日 期：2018-06-01 16:52
    /// 描 述：进货单
    /// </summary>
    public class Buys_OrderService : RepositoryFactory, Buys_OrderIService
    {
        private ICodeRuleService coderuleService = new CodeRuleService();
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Buys_OrderEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var queryParam = queryJson.ToJObject();

            string strSql = "select * from Buys_Order where DeleteMark=0 and EnabledMark=1  ";
            //单据日期
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and OrderDate BETWEEN '" + startTime + "' and '" + endTime + "'";
            }
            //客户编号
            if (!queryParam["OrderCode"].IsEmpty())
            {
                string OrderCode = queryParam["OrderCode"].ToString();
                strSql += " and OrderCode like '%" + OrderCode + "%'";
            }
            //供应商名称
            if (!queryParam["SupplierName"].IsEmpty())
            {
                string SupplierName = queryParam["SupplierName"].ToString();
                strSql += " and SupplierName like '%" + SupplierName + "%'";
            }
            return this.BaseRepository().FindList<Buys_OrderEntity>(strSql.ToString(), pagination);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Buys_OrderEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<Buys_OrderEntity>(keyValue);
        }
        /// <summary>
        /// 获取子表详细信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IEnumerable<Buys_OrderItemEntity> GetDetails(string keyValue)
        {
            //return this.BaseRepository().FindList<Buys_OrderItemEntity>("select * from Buys_OrderItem where OrderId='"+keyValue+ "'");
            return this.BaseRepository().IQueryable<Buys_OrderItemEntity>(t => t.OrderId.Equals(keyValue)).OrderByDescending(t => t.SortCode).ToList();
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                db.Delete<Buys_OrderEntity>(keyValue);
                db.Delete<Buys_OrderItemEntity>(t => t.OrderId.Equals(keyValue));
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="entryList">明细对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, Buys_OrderEntity entity,List<Buys_OrderItemEntity> entryList)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    List<Buys_OrderItemEntity> oldEntityList = this.BaseRepository().IQueryable<Buys_OrderItemEntity>(t => t.OrderId.Equals(keyValue)).OrderByDescending(t => t.SortCode).ToList();
                    //减去修改前
                    MinusWareGoods(db, oldEntityList);

                    //主表
                    entity.Modify(keyValue);
                    db.Update(entity);
                    //明细
                    db.Delete<Buys_OrderItemEntity>(t => t.OrderId.Equals(keyValue));

                    foreach (Buys_OrderItemEntity item in entryList)
                    {
                        item.Create();
                        item.OrderId = entity.OrderId;
                        db.Insert(item);
                    }
                    //加上本次入库
                    AddWareGoods(db, entryList);
                }
                else
                {
                    //主表
                    entity.Create();
                    db.Insert(entity);
                    coderuleService.UseRuleSeed(SystemInfo.CurrentModuleId, db);
                    //明细
                    foreach (Buys_OrderItemEntity item in entryList)
                    {
                        item.Create();
                        item.OrderId = entity.OrderId;
                        db.Insert(item);
                    }

                    AddWareGoods(db, entryList);
                }
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        #endregion

        #region 出入库操作
        private static readonly object _locker = new object(); // 互斥锁

        /// <summary>
        /// 出库存操作
        /// </summary>
        /// <param name="db"></param>
        /// <param name="orderEntryList"></param>
        private static void MinusWareGoods(IRepository db, List<Buys_OrderItemEntity> orderEntryList)
        {
            lock (_locker)
            {
                foreach (Buys_OrderItemEntity item in orderEntryList)
                {
                    POS_ProductEntity op = db.FindEntity<POS_ProductEntity>(t => t.Id.Equals(item.ProductId));
                    if (op != null)
                    {
                        op.Stock -= item.Qty;
                        if (op.Stock >= 0)
                        {
                            db.Update(op);
                        }
                        else
                        {
                            throw new Exception(string.Format("仓库存货库存不足，存货信息:{0}, 存货数量：{1}， 出库数量：{2}", item.ProductName, op.Stock, item.Qty));
                        }
                    }
                    else
                    {
                        throw new Exception(string.Format("仓库存货库存不足，存货信息:{0}, 存货数量：{1}， 出库数量：{2}", item.ProductName, op.Stock, item.Qty));
                    }
                }
            }
        }

        /// <summary>
        /// 入库存操作
        /// </summary>
        /// <param name="db"></param>
        /// <param name="orderEntryList"></param>
        private static void AddWareGoods(IRepository db, List<Buys_OrderItemEntity> orderEntryList)
        {
            lock (_locker)
            {
                foreach (Buys_OrderItemEntity item in orderEntryList)
                {
                    POS_ProductEntity op = db.FindEntity<POS_ProductEntity>(t => t.Id.Equals(item.ProductId));
                    if (op != null)
                    {
                        op.Stock += item.Qty;
                        db.Update(op);
                    }
                    else
                    {
                        throw new Exception(string.Format("系统中不存在：{0}，请先维护该产品。", item.ProductName));
                    }
                }
            }
        }


        #endregion
    }
}
