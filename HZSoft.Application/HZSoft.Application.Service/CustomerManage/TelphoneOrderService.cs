using HZSoft.Application.Code;
using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.IService.CustomerManage;
using HZSoft.Application.IService.SystemManage;
using HZSoft.Application.Service.SystemManage;
using HZSoft.Data.Repository;
using HZSoft.Util;
using HZSoft.Util.Extension;
using HZSoft.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HZSoft.Application.Service.CustomerManage
{
    /// <summary>
    /// �� �� 6.1
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2017-09-22 15:43
    /// �� �������붩��
    /// </summary>
    public class TelphoneOrderService : RepositoryFactory<TelphoneOrderEntity>, TelphoneOrderIService
    {
        private ICodeRuleService coderuleService = new CodeRuleService();
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<TelphoneOrderEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<TelphoneOrderEntity>();
            var queryParam = queryJson.ToJObject();
            string strSql = "select * from TelphoneOrder where 1=1";
            //��������
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and CreateDate BETWEEN '" + startTime + "' and '" + endTime + "'";
            }
            //���ݱ��
            if (!queryParam["Telphone"].IsEmpty())
            {
                string Telphone = queryParam["Telphone"].ToString();
                strSql += " and Telphone = '" + Telphone + "'";
            }
            //������
            if (!queryParam["SellerId"].IsEmpty())
            {
                string SellerId = queryParam["SellerId"].ToString();
                strSql += " and SellerId = '" + SellerId + "'";
            }
            else
            {
                if (!OperatorProvider.Provider.Current().IsSystem)
                {
                    string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                    strSql += " and SellerId in (" + dataAutor + ")";
                }
            }
            //�ջ���
            if (!queryParam["Consignee"].IsEmpty())
            {
                int SellMark = queryParam["Consignee"].ToInt();
                strSql += " and Consignee = " + SellMark;
            }

            return this.BaseRepository().FindList(strSql.ToString(), pagination);
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<TelphoneOrderEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public TelphoneOrderEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
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
                TelphoneOrderEntity entity = this.BaseRepository().FindEntity(keyValue);
                var telphone_Data = db.FindEntity<TelphoneSourceEntity>(t => t.Telphone == entity.Telphone);
                if (telphone_Data != null)
                {
                    telphone_Data.SellMark = 0;
                    telphone_Data.SellerId = "";
                    telphone_Data.SellerName = "";
                    telphone_Data.Description = entity.SellerName + "���˻�";
                    telphone_Data.Modify(telphone_Data.TelphoneID);
                    db.Update(telphone_Data);
                }
                //�޸�ϴ�ų��к�����۳�״̬
                //TelphoneWashService tsw = new TelphoneWashService();
                //var telphone_wash = db.FindEntity<TelphoneWashEntity>(t => t.Telphone == entity.Contact);
                //if (telphone_wash != null)
                //{
                //    telphone_wash.SellMark = 0;
                //    telphone_wash.SellerId = null;
                //    telphone_wash.SellerName = null;
                //    telphone_wash.CallDescription = entity.SellerName + "�˻�";
                //    telphone_wash.Modify(telphone_wash.TelphoneID);
                //    db.Update(telphone_wash);
                //}
                //�޸�ϴ�ų���β�ź��������ۺ�����ͬ���۳�״̬,һ�������Ӧ���β��
                string wei = entity.Telphone.Substring(7);
                var telphone_washList = db.FindList<TelphoneWashEntity>(t => t.Number == wei);
                foreach (var item in telphone_washList)
                {
                    item.SellMark = 0;
                    item.CallDescription = entity.SellerName + "���˻�";
                    item.Modify(item.TelphoneID);
                    db.Update(item);
                }

                db.Commit();
                this.BaseRepository().Delete(keyValue);

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
        /// <returns></returns>
        public void SaveForm(string keyValue, TelphoneOrderEntity entity)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();

            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                try
                {
                    entity.Create();
                    this.BaseRepository().Insert(entity);
                    //�޸ĺ�����к�����۳�״̬
                    TelphoneSourceService tss = new TelphoneSourceService();
                    var telphone_Data = db.FindEntity<TelphoneSourceEntity>(t => t.Telphone == entity.Telphone);
                    if (telphone_Data != null)
                    {
                        telphone_Data.SellMark = 1;
                        telphone_Data.SellerId = entity.SellerId;
                        telphone_Data.SellerName = entity.SellerName;
                        telphone_Data.Description = entity.SellerName + "���۳�";
                        telphone_Data.Modify(telphone_Data.TelphoneID);
                        db.Update(telphone_Data);
                    }
                    //�޸�ϴ�ų���β�ź��������ۺ�����ͬ���۳�״̬,һ�������Ӧ���β��
                    string wei = entity.Telphone.Substring(7);
                    var telphone_washList = db.FindList<TelphoneWashEntity>(t => t.Number == wei);
                    foreach (var item in telphone_washList)
                    {
                        item.SellMark = 1;
                        item.CallDescription = entity.SellerName + "���۳�";
                        item.Modify(item.TelphoneID);
                        db.Update(item);
                    }
                    //TelphoneWashService tsw = new TelphoneWashService();
                    //var telphone_wash = db.FindEntity<TelphoneWashEntity>(t => t.Number == wei);
                    //if (telphone_wash != null)
                    //{

                    //}
                    //ռ�õ��ݺ�
                    coderuleService.UseRuleSeed(SystemInfo.CurrentModuleId, db);
                    //coderuleService.UseRuleSeed(entity.CreateUserId, "", ((int)CodeRuleEnum.Telphone_OrderCode).ToString(), db);//ռ�õ��ݺ�
                    db.Commit();
                }
                catch (Exception)
                {
                    db.Rollback();
                    throw;
                }
            }

        }
        #endregion
    }
}
