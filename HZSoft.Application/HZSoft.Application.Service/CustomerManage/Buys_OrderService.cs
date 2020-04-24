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
    /// �� �� 6.1
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2018-06-01 16:52
    /// �� ����������
    /// </summary>
    public class Buys_OrderService : RepositoryFactory, Buys_OrderIService
    {
        private ICodeRuleService coderuleService = new CodeRuleService();
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<Buys_OrderEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var queryParam = queryJson.ToJObject();

            string strSql = "select * from Buys_Order where DeleteMark=0 and EnabledMark=1  ";
            //��������
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and OrderDate BETWEEN '" + startTime + "' and '" + endTime + "'";
            }
            //�ͻ����
            if (!queryParam["OrderCode"].IsEmpty())
            {
                string OrderCode = queryParam["OrderCode"].ToString();
                strSql += " and OrderCode like '%" + OrderCode + "%'";
            }
            //��Ӧ������
            if (!queryParam["SupplierName"].IsEmpty())
            {
                string SupplierName = queryParam["SupplierName"].ToString();
                strSql += " and SupplierName like '%" + SupplierName + "%'";
            }
            return this.BaseRepository().FindList<Buys_OrderEntity>(strSql.ToString(), pagination);
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public Buys_OrderEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<Buys_OrderEntity>(keyValue);
        }
        /// <summary>
        /// ��ȡ�ӱ���ϸ��Ϣ
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public IEnumerable<Buys_OrderItemEntity> GetDetails(string keyValue)
        {
            //return this.BaseRepository().FindList<Buys_OrderItemEntity>("select * from Buys_OrderItem where OrderId='"+keyValue+ "'");
            return this.BaseRepository().IQueryable<Buys_OrderItemEntity>(t => t.OrderId.Equals(keyValue)).OrderByDescending(t => t.SortCode).ToList();
        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
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
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <param name="entryList">��ϸ����</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, Buys_OrderEntity entity,List<Buys_OrderItemEntity> entryList)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    List<Buys_OrderItemEntity> oldEntityList = this.BaseRepository().IQueryable<Buys_OrderItemEntity>(t => t.OrderId.Equals(keyValue)).OrderByDescending(t => t.SortCode).ToList();
                    //��ȥ�޸�ǰ
                    MinusWareGoods(db, oldEntityList);

                    //����
                    entity.Modify(keyValue);
                    db.Update(entity);
                    //��ϸ
                    db.Delete<Buys_OrderItemEntity>(t => t.OrderId.Equals(keyValue));

                    foreach (Buys_OrderItemEntity item in entryList)
                    {
                        item.Create();
                        item.OrderId = entity.OrderId;
                        db.Insert(item);
                    }
                    //���ϱ������
                    AddWareGoods(db, entryList);
                }
                else
                {
                    //����
                    entity.Create();
                    db.Insert(entity);
                    coderuleService.UseRuleSeed(SystemInfo.CurrentModuleId, db);
                    //��ϸ
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

        #region ��������
        private static readonly object _locker = new object(); // ������

        /// <summary>
        /// ��������
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
                            throw new Exception(string.Format("�ֿ�����治�㣬�����Ϣ:{0}, ���������{1}�� ����������{2}", item.ProductName, op.Stock, item.Qty));
                        }
                    }
                    else
                    {
                        throw new Exception(string.Format("�ֿ�����治�㣬�����Ϣ:{0}, ���������{1}�� ����������{2}", item.ProductName, op.Stock, item.Qty));
                    }
                }
            }
        }

        /// <summary>
        /// �������
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
                        throw new Exception(string.Format("ϵͳ�в����ڣ�{0}������ά���ò�Ʒ��", item.ProductName));
                    }
                }
            }
        }


        #endregion
    }
}
