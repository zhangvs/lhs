using HZSoft.Application.Entity.BaseManage;
using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.IService.CustomerManage;
using HZSoft.Application.IService.SystemManage;
using HZSoft.Application.Service.SystemManage;
using HZSoft.Application.Service.BaseManage;
using HZSoft.Data.Repository;
using HZSoft.Util;
using HZSoft.Util.Extension;
using HZSoft.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using HZSoft.Application.Code;

namespace HZSoft.Application.Service.CustomerManage
{
    /// <summary>
    /// �� �� 6.1
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2017-12-15 16:52
    /// �� �������ۺ�ͬ��
    /// </summary>
    public class Sales_ContractService : RepositoryFactory, Sales_ContractIService
    {
        private ICodeRuleService coderuleService = new CodeRuleService();
        POS_ProductService prooductService = new POS_ProductService();
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<Sales_ContractEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<Sales_ContractEntity>();
            var queryParam = queryJson.ToJObject();
            string strSql = "select * from Sales_Contract where 1=1";
            //��������
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and CreateTime >= '" + startTime + "' and CreateTime < '" + endTime + "'";
            }
            //�ͻ���
            if (!queryParam["CustomerName"].IsEmpty())
            {
                string CustomerName = queryParam["CustomerName"].ToString();
                strSql += " and CustomerName = '" + CustomerName + "'";
            }
            //�ͻ���˾��
            if (!queryParam["CustomerCompany"].IsEmpty())
            {
                string CustomerCompany = queryParam["CustomerCompany"].ToString();
                strSql += " and CustomerCompany = '" + CustomerCompany + "'";
            }
            //״̬
            if (!queryParam["State"].IsEmpty())
            {
                string State = queryParam["State"].ToString();
                strSql += " and State = '" + State + "'";
            }

            //������
            if (!queryParam["UserId"].IsEmpty())
            {
                string UserId = queryParam["UserId"].ToString();
                strSql += " and UserId = '" + UserId + "'";
            }
            else
            {
                if (!OperatorProvider.Provider.Current().IsSystem)
                {
                    string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                    strSql += " and (UserId in (" + dataAutor + "))";
                }
            }

            return this.BaseRepository().FindList<Sales_ContractEntity>(strSql.ToString(), pagination);
        }
        // <summary>
        // ��ȡ�б�
        // </summary>
        // <param name = "pagination" > ��ҳ </ param >
        // < param name="queryJson">��ѯ����</param>
        // <returns>���ط�ҳ�б�</returns>
        public IEnumerable<Sales_ContractEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<Sales_ContractEntity>();
            var queryParam = queryJson.ToJObject();

            string strSql = "select * from Sales_Contract where 1 = 1 ";
            string strOrder = " ORDER BY CreateDate desc";

            //������
            if (!queryParam["SearchName"].IsEmpty())
            {
                string SearchName = queryParam["SearchName"].ToString();
                strSql += " and CustomerName = '" + SearchName + "'";
            }
            //Ա��Id
            if (!OperatorProvider.Provider.Current().IsSystem)
            {
                string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                strSql += " and (UserId in (" + dataAutor + ") or UserId is null)";
            }
            strSql += strOrder;

            return this.BaseRepository().FindList<Sales_ContractEntity>(strSql.ToString());
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public Sales_ContractEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<Sales_ContractEntity>(keyValue);
        }
        /// <summary>
        /// ��ȡ�ӱ���ϸ��Ϣ
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public IEnumerable<Sales_Contract_ItemEntity> GetDetails(string keyValue)
        {
            return this.BaseRepository().FindList<Sales_Contract_ItemEntity>("select * from Sales_Contract_Item where ContractId='" + keyValue + "'");
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
                db.Delete<Sales_ContractEntity>(keyValue);
                db.Delete<Sales_Contract_ItemEntity>(t => t.ContractId.Equals(keyValue));
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
        public void SaveForm(string keyValue, Sales_ContractEntity entity, List<Sales_Contract_ItemEntity> entryList)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    //����
                    entity.Modify(keyValue);
                    db.Update(entity);

                    //��ϸ
                    db.Delete<Sales_Contract_ItemEntity>(t => t.ContractId.Equals(keyValue));

                    foreach (Sales_Contract_ItemEntity item in entryList)
                    {
                        item.Create();
                        item.ContractId = entity.Id;
                        db.Insert(item);
                        //����ż����
                        if (entity.Status == 1)
                        {
                            MinusWareGoods(db, item);
                        }
                    }
                    
                }
                else
                {
                    //����
                    entity.Create();
                    db.Insert(entity);

                    //�����ٲ�������ʼ����������
                    if (entity.Status == 1)
                    {
                        //Sale_CustomerEntity saleCustomer = db.FindEntity<Sale_CustomerEntity>(t => t.CustomerId.Equals(entity.CustomerId));
                        //if (saleCustomer != null)
                        //{
                        //    saleCustomer.SumTotalAmount = saleCustomer.SumTotalAmount + entity.TotalAmount;
                        //    saleCustomer.SumTotalCount = saleCustomer.SumTotalCount + entity.TotalCount;
                        //    saleCustomer.ModifyUserId = entity.UserId;
                        //    saleCustomer.ModifyUserName = entity.UserName;
                        //    saleCustomer.Modify(saleCustomer.CustomerId);
                        //    db.Update(saleCustomer);
                        //}
                        //else
                        //{
                        //    Sale_CustomerEntity saleCustomerEntity = new Sale_CustomerEntity()
                        //    {
                        //        CustomerId = entity.CustomerId,
                        //        CustomerCompany = entity.CustomerCompany,
                        //        SumTotalAmount = entity.TotalAmount,
                        //        SumTotalCount = entity.TotalCount,
                        //        CreateUserId = entity.UserId,
                        //        CreateUserName = entity.UserName,
                        //    };
                        //    saleCustomerEntity.Create();
                        //    db.Insert(saleCustomerEntity);
                        //}
                    }




                    //��ϸ
                    int sort = 0;
                    foreach (Sales_Contract_ItemEntity item in entryList)
                    {
                        item.Sort=sort++;
                        item.Create();
                        item.ContractId = entity.Id;
                        db.Insert(item);
                        //����ż����
                        if (entity.Status==1)
                        {
                            //����ż����
                            MinusWareGoods(db, item);

                            //#region ������Ϣ
                            ////����ų�ʼ�������ӱ�
                            //Sale_Customer_ItemEntity saleCustomerItem = db.FindEntity<Sale_Customer_ItemEntity>(t => t.CustomerId.Equals(entity.CustomerId) && t.ProductId.Equals(item.ProductId));
                            ////��Ʒ�Ѿ�����һ�λ������ϴν����������ۼ��ܽ�����
                            //if (saleCustomerItem!=null)
                            //{
                            //    saleCustomerItem.UnitPrice = item.UnitPrice;
                            //    saleCustomerItem.SumAmount = saleCustomerItem.SumAmount + item.Amount;
                            //    saleCustomerItem.SumCount = saleCustomerItem.SumCount + item.Count;
                            //    saleCustomerItem.Modify(saleCustomerItem.Id);
                            //    db.Update(saleCustomerItem);
                            //}
                            //else
                            //{
                            //    //��һ�ν�������������Ʒ
                            //    Sale_Customer_ItemEntity saleCustomerItemEntity = new Sale_Customer_ItemEntity()
                            //    {
                            //        CustomerId = entity.CustomerId,
                            //        ProductId = item.ProductId,
                            //        ProductCode = item.ProductCode,
                            //        ProductName = item.ProductName,
                            //        UnitPrice = item.UnitPrice,
                            //        SumAmount = item.Amount,
                            //        SumCount = item.Count,
                            //        Sort = item.Sort,
                            //    };
                            //    saleCustomerItemEntity.Create();
                            //    db.Insert(saleCustomerItemEntity);
                            //}
                            //#endregion
                        }

                    }
                    //ռ�õ��ݺ�
                    coderuleService.UseRuleSeed(SystemInfo.CurrentModuleId, db);
                }
                db.Commit();
            }
            catch (Exception ex)
            {
                db.Rollback();
                throw;
            }
        }

        private static readonly object _locker = new object(); // ������

        /// <summary>
        /// �������
        /// </summary>
        /// <param name="db"></param>
        /// <param name="item"></param>
        private static void AddWareGoods(IRepository db, Sales_Contract_ItemEntity item)
        {
            lock (_locker)
            {
                POS_ProductEntity op = db.FindEntity<POS_ProductEntity>(t => t.Id.Equals(item.ProductId));
                if (op != null)
                {
                    op.Stock += item.Count;
                    db.Update(op);
                }
                else
                {
                    throw new Exception(string.Format("ϵͳ�в����ڣ�{0}������ά���ò�Ʒ��", item.ProductName));
                }
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="db"></param>
        /// <param name="item"></param>
        private static void MinusWareGoods(IRepository db, Sales_Contract_ItemEntity item)
        {
            lock (_locker)
            {
                POS_ProductEntity op = db.FindEntity<POS_ProductEntity>(t => t.Id.Equals(item.ProductId));
                if (op != null)
                {
                    op.Stock -= item.Count;
                    if (op.Stock >= 0)
                    {
                        db.Update(op);
                    }
                    else
                    {
                        throw new Exception(string.Format("�ֿ�����治�㣬�����Ϣ:{0}, ���������{1}�� ����������{2}", item.ProductName, op.Stock, item.Count));
                    }
                }
                else
                {
                    throw new Exception(string.Format("�ֿ�����治�㣬�����Ϣ:{0}, ���������{1}�� ����������{2}", item.ProductName, op.Stock, item.Count));
                }
            }
        }
        #endregion
    }
}
