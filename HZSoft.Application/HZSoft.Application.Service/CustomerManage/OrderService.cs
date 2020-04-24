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
    /// 创 建：佘赐雄
    /// 日 期：2016-03-16 13:54
    /// 描 述：订单管理
    /// </summary>
    public class OrderService : RepositoryFactory<OrderEntity>, IOrderService
    {
        private ICodeRuleService coderuleService = new CodeRuleService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<OrderEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var queryParam = queryJson.ToJObject();

            string strSql = "select * from Client_Order where DeleteMark=0 and EnabledMark=1  ";
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
            //客户名称
            if (!queryParam["CustomerName"].IsEmpty())
            {
                string CustomerName = queryParam["CustomerName"].ToString();
                strSql += " and CustomerName like '%" + CustomerName + "%'";
            }
            //销售人员
            if (!queryParam["SellerId"].IsEmpty())
            {
                string SellerId = queryParam["SellerId"].ToString();
                strSql += " and SellerId = '%" + SellerId + "%'";
            }
            else
            {
                if (!OperatorProvider.Provider.Current().IsSystem && OperatorProvider.Provider.Current().Account != "admin")
                {
                    string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                    strSql += " and SellerId in (" + dataAutor + ")";
                }
            }
            //收款状态
            if (!queryParam["PaymentState"].IsEmpty())
            {
                int PaymentState = queryParam["PaymentState"].ToInt();
                strSql += " and PaymentState = " + PaymentState;
            }

            //订单状态
            if (!queryParam["OrderState"].IsEmpty())
            {
                int OrderState = queryParam["OrderState"].ToInt();
                strSql += " and OrderState = " + OrderState;
            }

            //物流状态
            if (!queryParam["ExpressState"].IsEmpty())
            {
                int ExpressState = queryParam["ExpressState"].ToInt();
                strSql += " and ExpressState = " + ExpressState;
            }
            return this.BaseRepository().FindList(strSql.ToString(), pagination);
        }

        /// <summary>
        /// 审单
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<OrderEntity> GetCheckPageList(Pagination pagination, string queryJson)
        {
            var queryParam = queryJson.ToJObject();

            string strSql = "select * from Client_Order where DeleteMark=0 and EnabledMark=1  ";
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
            //客户名称
            if (!queryParam["CustomerName"].IsEmpty())
            {
                string CustomerName = queryParam["CustomerName"].ToString();
                strSql += " and CustomerName like '%" + CustomerName + "%'";
            }
            //销售人员
            if (!queryParam["SellerId"].IsEmpty())
            {
                string SellerId = queryParam["SellerId"].ToString();
                strSql += " and SellerId = '%" + SellerId + "%'";
            }
            //else
            //{
            //    if (!OperatorProvider.Provider.Current().IsSystem && OperatorProvider.Provider.Current().Account != "admin")
            //    {
            //        string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
            //        strSql += " and SellerId in (" + dataAutor + ")";
            //    }
            //}
            //收款状态
            if (!queryParam["PaymentState"].IsEmpty())
            {
                int PaymentState = queryParam["PaymentState"].ToInt();
                strSql += " and PaymentState = " + PaymentState;
            }

            //订单状态
            if (!queryParam["OrderState"].IsEmpty())
            {
                int OrderState = queryParam["OrderState"].ToInt();
                strSql += " and OrderState = " + OrderState;
            }
            else
            {
                strSql += " and OrderState in(0,1,-1)";//录入，审核通过,审核不通过
            }
            //物流状态
            if (!queryParam["ExpressState"].IsEmpty())
            {
                int ExpressState = queryParam["ExpressState"].ToInt();
                strSql += " and ExpressState = " + ExpressState;
            }
            return this.BaseRepository().FindList(strSql.ToString(), pagination);
        }


        /// <summary>
        /// 会计审核
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<OrderEntity> GetAccCheckPageList(Pagination pagination, string queryJson)
        {
            var queryParam = queryJson.ToJObject();

            string strSql = "select * from Client_Order where DeleteMark=0 and EnabledMark=1  ";
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
            //客户名称
            if (!queryParam["CustomerName"].IsEmpty())
            {
                string CustomerName = queryParam["CustomerName"].ToString();
                strSql += " and CustomerName like '%" + CustomerName + "%'";
            }
            //销售人员
            if (!queryParam["SellerId"].IsEmpty())
            {
                string SellerId = queryParam["SellerId"].ToString();
                strSql += " and SellerId = '%" + SellerId + "%'";
            }
            //else
            //{
            //    if (!OperatorProvider.Provider.Current().IsSystem && OperatorProvider.Provider.Current().Account != "admin")
            //    {
            //        string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
            //        strSql += " and SellerId in (" + dataAutor + ")";
            //    }
            //}
            //收款状态
            if (!queryParam["PaymentState"].IsEmpty())
            {
                int PaymentState = queryParam["PaymentState"].ToInt();
                strSql += " and PaymentState = " + PaymentState;
            }

            //订单状态
            if (!queryParam["OrderState"].IsEmpty())
            {
                int OrderState = queryParam["OrderState"].ToInt();
                strSql += " and OrderState = " + OrderState;
            }
            else
            {
                strSql += " and OrderState in(1,2,-2,3,-3)";//会计审核通过
            }
            //物流状态
            if (!queryParam["ExpressState"].IsEmpty())
            {
                int ExpressState = queryParam["ExpressState"].ToInt();
                strSql += " and ExpressState = " + ExpressState;
            }
            return this.BaseRepository().FindList(strSql.ToString(), pagination);
        }

        /// <summary>
        /// 仓管
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<OrderEntity> GetKeeperPageList(Pagination pagination, string queryJson)
        {
            var queryParam = queryJson.ToJObject();

            string strSql = "select * from Client_Order where DeleteMark=0 and EnabledMark=1  ";
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
            //客户名称
            if (!queryParam["CustomerName"].IsEmpty())
            {
                string CustomerName = queryParam["CustomerName"].ToString();
                strSql += " and CustomerName like '%" + CustomerName + "%'";
            }
            //销售人员
            if (!queryParam["SellerId"].IsEmpty())
            {
                string SellerId = queryParam["SellerId"].ToString();
                strSql += " and SellerId = '%" + SellerId + "%'";
            }
            //else
            //{
            //    if (!OperatorProvider.Provider.Current().IsSystem && OperatorProvider.Provider.Current().Account != "admin")
            //    {
            //        string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
            //        strSql += " and SellerId in (" + dataAutor + ")";
            //    }
            //}
            //收款状态
            if (!queryParam["PaymentState"].IsEmpty())
            {
                int PaymentState = queryParam["PaymentState"].ToInt();
                strSql += " and PaymentState = " + PaymentState;
            }

            //订单状态
            if (!queryParam["OrderState"].IsEmpty())
            {
                int OrderState = queryParam["OrderState"].ToInt();
                strSql += " and OrderState = " + OrderState;
            }
            else
            {
                strSql += " and OrderState in(2,3,-3)";//会计审核通过
            }
            //物流状态
            if (!queryParam["ExpressState"].IsEmpty())
            {
                int ExpressState = queryParam["ExpressState"].ToInt();
                strSql += " and ExpressState = " + ExpressState;
            }
            return this.BaseRepository().FindList(strSql.ToString(), pagination);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public OrderEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        /// <summary>
        /// 获取前单、后单 数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="type">类型（1-前单；2-后单）</param>
        /// <returns>返回实体</returns>
        public OrderEntity GetPrevOrNextEntity(string keyValue, int type)
        {
            OrderEntity entity = this.GetEntity(keyValue);
            if (type == 1)
            {
                entity = this.BaseRepository().IQueryable().Where(t => t.CreateDate >entity.CreateDate).OrderBy(t => t.CreateDate).FirstOrDefault();
            }
            else if (type == 2)
            {
                entity = this.BaseRepository().IQueryable().Where(t => t.CreateDate < entity.CreateDate).OrderByDescending(t => t.CreateDate).FirstOrDefault();
            }
            return entity;
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 修改订单状态
        /// </summary>
        /// <param name="keyValue">客户ids</param>
        /// <param name="orderState">状态</param>
        public void SaveOrderState(string keyValue, int? orderState)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                OrderEntity oldEntity = this.BaseRepository().FindEntity(keyValue);
                //如果状态改变才修改
                if (oldEntity.OrderState!= orderState)
                {
                    oldEntity.Modify(keyValue);
                    oldEntity.OrderState = orderState;
                    this.BaseRepository().Update(oldEntity);                    
                }
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                db.Delete<OrderEntity>(keyValue);
                db.Delete<OrderEntryEntity>(t => t.OrderId.Equals(keyValue));
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
        /// <param name="orderEntity">实体对象</param>
        /// <param name="orderEntryList">明细实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, OrderEntity orderEntity, List<OrderEntryEntity> orderEntryList)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    //主表
                    orderEntity.Modify(keyValue);
                    db.Update(orderEntity);
                    //明细
                    db.Delete<OrderEntryEntity>(t => t.OrderId.Equals(keyValue));
                    foreach (OrderEntryEntity orderEntryEntity in orderEntryList)
                    {
                        orderEntryEntity.OrderId = orderEntity.OrderId;
                        db.Insert(orderEntryEntity);
                    }
                }
                else
                {
                    //主表
                    orderEntity.Create();
                    db.Insert(orderEntity);
                    coderuleService.UseRuleSeed(orderEntity.CreateUserId, "", ((int)CodeRuleEnum.Customer_OrderCode).ToString(), db);//占用单据号
                    //明细
                    foreach (OrderEntryEntity orderEntryEntity in orderEntryList)
                    {
                        orderEntryEntity.Create();
                        orderEntryEntity.OrderId = orderEntity.OrderId;
                        db.Insert(orderEntryEntity);
                    }
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

        #region
        /// <summary>
        /// 保存跟踪记录（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SavePushRecord(TracesPushRecord entity)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                //主表
                TracesPushRecordEntity recordEntity = new TracesPushRecordEntity
                {
                    Id = Guid.NewGuid().ToString(),
                    EBusinessID = entity.EBusinessID,
                    Count = entity.Count,
                    PushTime = entity.PushTime
                };
                db.Insert(recordEntity);

                foreach (TracesData item in entity.Data)
                {
                    //重复快递号不再添加，只修改状态
                    TracesDataEntity itemEntity = db.FindEntity<TracesDataEntity>(t => t.LogisticCode.Equals(item.LogisticCode));
                    if (itemEntity != null)
                    {
                        db.Delete<TracesDataEntity>(t => t.LogisticCode.Equals(item.LogisticCode));
                        db.Delete<TracesItemEntity>(t => t.LogisticCode.Equals(item.LogisticCode));
                    }

                    //保持跟进数据记录
                    TracesDataEntity tracesEntity = new TracesDataEntity
                    {
                        PushId = recordEntity.Id,
                        LogisticCode = item.LogisticCode,
                        EBusinessID = item.EBusinessID,
                        OrderCode = item.OrderCode,
                        ShipperCode = item.ShipperCode,
                        Success = item.Success,
                        Reason = item.Reason,
                        State = item.State,
                        CallBack = item.CallBack
                    };
                    db.Insert(tracesEntity);


                    foreach (var ItemTraces in item.Traces)
                    {
                        //保持跟进详细记录
                        TracesItemEntity ItemTracesEntity = new TracesItemEntity
                        {
                            Id = Guid.NewGuid().ToString(),
                            LogisticCode = item.LogisticCode,
                            AcceptTime = ItemTraces.AcceptTime,
                            AcceptStation = ItemTraces.AcceptStation,
                            Remark = ItemTraces.Remark,
                        };

                        db.Insert(ItemTracesEntity);
                    }
                }

                db.Commit();
            }
            catch (Exception ex)
            {
                db.Rollback();
                throw;
            }
        }
        #endregion



        #region 出入库操作
        /// <summary>
        /// 发货
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="orderEntity">实体对象</param>
        /// <param name="orderEntryList">明细实体对象</param>
        /// <returns></returns>
        public void SaveKeeperForm(string keyValue, OrderEntity orderEntity, List<OrderEntryEntity> orderEntryList)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    OrderEntity oldEntity = this.BaseRepository().FindEntity(keyValue);

                    //如果状态改变才修改
                    if (oldEntity.OrderState != orderEntity.OrderState)
                    {
                        //主表
                        orderEntity.Modify(keyValue);
                        db.Update(orderEntity);

                        //发货出库
                        if (orderEntity.OrderState == 3)
                        {
                            MinusWareGoods(db, orderEntryList);
                        }

                        //退货入库
                        if (orderEntity.OrderState == -3)
                        {
                            AddWareGoods(db, orderEntryList);
                        }
                    }
                }
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }


        private static readonly object _locker = new object(); // 互斥锁

        /// <summary>
        /// 出库存操作
        /// </summary>
        /// <param name="db"></param>
        /// <param name="orderEntryList"></param>
        private static void MinusWareGoods(IRepository db, List<OrderEntryEntity> orderEntryList)
        {
            lock (_locker)
            {
                foreach (OrderEntryEntity item in orderEntryList)
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
        private static void AddWareGoods(IRepository db, List<OrderEntryEntity> orderEntryList)
        {
            lock (_locker)
            {
                foreach (OrderEntryEntity item in orderEntryList)
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