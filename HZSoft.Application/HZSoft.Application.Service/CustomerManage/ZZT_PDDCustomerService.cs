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
using System.Data;
using System.Linq;

namespace HZSoft.Application.Service.CustomerManage
{
    /// <summary>
    /// �� �� 6.1
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2018-05-10 09:15
    /// �� ����ƴ���ͻ�
    /// </summary>
    public class ZZT_PDDCustomerService : RepositoryFactory<ZZT_PDDCustomerEntity>, ZZT_PDDCustomerIService
    {
        private ICodeRuleService coderuleService = new CodeRuleService();

        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<ZZT_PDDCustomerEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<ZZT_PDDCustomerEntity>();
            var queryParam = queryJson.ToJObject();

            string strSql = "select * from ZZT_PDDCustomer where 1=1 ";

            //����
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and OrderTime BETWEEN '" + startTime + "' and '" + endTime + "'";
            }
            //�ͻ����
            if (!queryParam["CustNo"].IsEmpty())
            {
                string CustNo = queryParam["CustNo"].ToString();
                strSql += " and CustNo like '%" + CustNo + "%'";
            }
            //�ֻ���
            if (!queryParam["Mobile"].IsEmpty())
            {
                string Mobile = queryParam["Mobile"].ToString();
                strSql += " and Mobile like '%" + Mobile + "%'";
            }
            //����
            if (!queryParam["FullName"].IsEmpty())
            {
                string FullName = queryParam["FullName"].ToString();
                strSql += " and FullName like '%" + FullName + "%'";
            }
            //����״̬
            if (!queryParam["AssignMark"].IsEmpty())
            {
                int AssignMark = queryParam["AssignMark"].ToInt();
                strSql += " and AssignMark = " + AssignMark;
            }
            //����ͻ�
            if (!queryParam["IntentionMark"].IsEmpty())
            {
                int IntentionMark = queryParam["IntentionMark"].ToInt();
                strSql += " and IntentionMark = " + IntentionMark;
            }
            //�۳�״̬
            if (!queryParam["SellMark"].IsEmpty())
            {
                int SellMark = queryParam["SellMark"].ToInt();
                strSql += " and SellMark = " + SellMark;
            }
            
            //����վ
            if (!queryParam["DeleteMark"].IsEmpty())
            {
                strSql += " and DeleteMark = 1";//����վ�����жϹ������ñ�ʶ��Ҳ�����ж�Ȩ��
            }
            else
            {
                strSql += " and DeleteMark = 0";//��Ϊ����վ���ж��Ƿ��ǹ���

                //����
                if (!queryParam["EnabledMark"].IsEmpty())
                {
                    strSql += " and EnabledMark = 0";//��Ϊ����վ��Ϊ����,Ҳ�����ж�Ȩ��
                }
                else
                {
                    strSql += " and EnabledMark = 1";//��Ϊ����վ�����������ж�Ȩ��
                    //������
                    if (!queryParam["TraceUserId"].IsEmpty())
                    {
                        string TraceUserId = queryParam["TraceUserId"].ToString();
                        strSql += " and TraceUserId = '" + TraceUserId + "'";
                    }
                    else
                    {
                        if (!OperatorProvider.Provider.Current().IsSystem && OperatorProvider.Provider.Current().Account != "admin")
                        {
                            string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                            strSql += " and TraceUserId in (" + dataAutor + ")";
                        }
                    }
                }
            }
            return this.BaseRepository().FindList(strSql.ToString(), pagination);
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<ZZT_PDDCustomerEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public ZZT_PDDCustomerEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region ��֤����
        /// <summary>
        /// �˻������ظ�
        /// </summary>
        /// <param name="Mobile">�˻�ֵ</param>
        /// <param name="keyValue">����</param>
        /// <returns></returns>
        public bool ExistMobile(string Mobile, string keyValue)
        {
            var expression = LinqExtensions.True<ZZT_PDDCustomerEntity>();
            expression = expression.And(t => t.Mobile == Mobile);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.Id != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValues">����</param>
        public void RemoveForm(string keyValues)
        {
            //this.BaseRepository().Delete(keyValues);
            if (!string.IsNullOrEmpty(keyValues))
            {
                string[] custIds = keyValues.Split(',');
                for (int i = 0; i < custIds.Length; i++)
                {
                    ZZT_PDDCustomerEntity entity = this.BaseRepository().FindEntity(custIds[i]);
                    entity.Modify(custIds[i]);
                    entity.DeleteMark = 1;//��ɾ��
                    this.BaseRepository().Update(entity);
                }
            }
        }

        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, ZZT_PDDCustomerEntity entity)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            if (!string.IsNullOrEmpty(keyValue))
            {
                var old_Data = db.FindEntity<ZZT_PDDCustomerEntity>(t => t.Id == keyValue);
                //�޸ı�ע������µĸ�����¼
                if (old_Data.Description != entity.Description && entity.Description != "&nbsp;")
                {
                    //���������¼
                    TrailRecordEntity trailRecordEntity = new TrailRecordEntity
                    {
                        ObjectSort = 4,
                        ObjectId = keyValue,
                        TrackContent = entity.Description
                    };
                    trailRecordEntity.Create();
                    db.Insert(trailRecordEntity);
                }
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
                db.Commit();
            }
            else
            {
                entity.Create();
                entity.AssignMark = 1;
                entity.TraceUserId = OperatorProvider.Provider.Current().UserId;//������Ϊ�Լ�
                entity.TraceUserName = OperatorProvider.Provider.Current().UserName;
                entity.CustNo = coderuleService.SetBillCode(entity.CreateUserId, SystemInfo.CurrentModuleId, "", db);//���ָ��ģ����߱�ŵĵ��ݺ�
                db.Insert(entity);

                //���������¼
                TrailRecordEntity trailRecordEntity = new TrailRecordEntity
                {
                    ObjectSort = 4,
                    ObjectId = entity.Id,
                    TrackContent = entity.Description
                };
                trailRecordEntity.Create();
                db.Insert(trailRecordEntity);
                db.Commit();
            }

        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="keyValues">�ͻ�ids</param>
        /// <param name="TraceUserId">Ա��id</param>
        /// <param name="TraceUserName">Ա������</param>
        public void SaveAssign(string keyValues, string TraceUserId, string TraceUserName)
        {
            if (!string.IsNullOrEmpty(keyValues))
            {
                string[] custIds = keyValues.Split(',');
                for (int i = 0; i < custIds.Length; i++)
                {
                    ZZT_PDDCustomerEntity entity = this.BaseRepository().FindEntity(custIds[i]);
                    entity.Modify(custIds[i]);
                    entity.AssignMark = 1;
                    entity.TraceUserId = TraceUserId;
                    entity.TraceUserName = TraceUserName;
                    entity.EnabledMark = 1;//���ã��ӹ�����ɿ��ÿͻ�
                    entity.DeleteMark = 0;//����վ��ԭ��ĳ�δɾ��
                    this.BaseRepository().Update(entity);
                }
            }
        }
        /// <summary>
        /// ���빫��
        /// </summary>
        /// <param name="keyValues">�ͻ�ids</param>
        public void SaveThrow(string keyValues)
        {
            if (!string.IsNullOrEmpty(keyValues))
            {
                string[] custIds = keyValues.Split(',');
                for (int i = 0; i < custIds.Length; i++)
                {
                    ZZT_PDDCustomerEntity entity = this.BaseRepository().FindEntity(custIds[i]);
                    entity.Modify(custIds[i]);
                    entity.AssignMark = 0;//������
                    //entity.TraceUserId = "";
                    //entity.TraceUserName = "";
                    entity.EnabledMark = 0;//�����ã��������ݴ˲�ѯ
                    entity.DeleteMark = 0;//����վ��ԭ��ĳ�δɾ��
                    this.BaseRepository().Update(entity);
                }
            }
        }
        /// <summary>
        /// ��ȡ
        /// </summary>
        /// <param name="keyValues">�ͻ�ids</param>
        public void SaveObtain(string keyValues)
        {
            if (!string.IsNullOrEmpty(keyValues))
            {
                string[] custIds = keyValues.Split(',');
                for (int i = 0; i < custIds.Length; i++)
                {
                    ZZT_PDDCustomerEntity entity = this.BaseRepository().FindEntity(custIds[i]);
                    entity.Modify(custIds[i]);
                    entity.AssignMark = 1;//������
                    entity.TraceUserId = OperatorProvider.Provider.Current().UserId;
                    entity.TraceUserName = OperatorProvider.Provider.Current().UserName;
                    entity.EnabledMark = 1;//�����ã��������ݴ˲�ѯ
                    this.BaseRepository().Update(entity);
                }
            }
        }
        #endregion

        #region PDD����
        /// <summary>
        /// ������������
        /// </summary>
        /// <param name="dtSource">ʵ�����</param>
        /// <returns></returns>
        public string BatchAddPDD(DataTable dtSource)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                for (int i = 0; i < dtSource.Rows.Count; i++)
                {
                    string mobile = dtSource.Rows[i][4].ToString();
                    string telphone = dtSource.Rows[i][5].ToString();

                    if (!string.IsNullOrWhiteSpace(mobile))
                    {
                        string CustNo = "";
                        DateTime? NullTime = null;
                        string orderTimeStr = dtSource.Rows[i][16].ToString();
                        string payTimeStr = dtSource.Rows[i][17].ToString();
                        string deliveryTimeStr = dtSource.Rows[i][18].ToString();
                        string printingTimeStr = dtSource.Rows[i][19].ToString();
                        DateTime? orderTime=string.IsNullOrEmpty(orderTimeStr)? NullTime : DateTime.ParseExact(orderTimeStr, "yyyy/M/d H:m:s", System.Globalization.CultureInfo.CurrentCulture);
                        DateTime? payTime = string.IsNullOrEmpty(payTimeStr) ? NullTime : DateTime.ParseExact(payTimeStr, "yyyy/M/d H:m:s", System.Globalization.CultureInfo.CurrentCulture);
                        DateTime? deliveryTime = string.IsNullOrEmpty(deliveryTimeStr) ? NullTime : DateTime.ParseExact(deliveryTimeStr, "yyyy/M/d H:m:s", System.Globalization.CultureInfo.CurrentCulture);
                        DateTime? printingTime = string.IsNullOrEmpty(printingTimeStr) ? NullTime : DateTime.ParseExact(printingTimeStr, "yyyy/M/d H:m:s", System.Globalization.CultureInfo.CurrentCulture);

                        var old_Data = db.FindEntity<ZZT_PDDCustomerEntity>(t => t.Mobile == mobile);
                        //�Ƿ��ǰ�����
                        if (old_Data != null)
                        {
                            CustNo = old_Data.CustNo;
                            old_Data.OrderNo = dtSource.Rows[i][1].ToString();
                            old_Data.OrderTime = orderTime;
                            old_Data.Province = dtSource.Rows[i][6].ToString();
                            old_Data.City = dtSource.Rows[i][7].ToString();
                            old_Data.County = dtSource.Rows[i][8].ToString();
                            old_Data.Address = dtSource.Rows[i][9].ToString();
                            old_Data.GoodsName = dtSource.Rows[i][15].ToString();
                            old_Data.Modify(old_Data.Id);
                            db.Update(old_Data);
                        }
                        else
                        {
                            //�½�PDD�ͻ���
                            CustNo = coderuleService.SetBillCode(OperatorProvider.Provider.Current().UserId, SystemInfo.CurrentModuleId, "", db);//���ָ��ģ����߱�ŵĵ��ݺ�
                            ZZT_PDDCustomerEntity entity = new ZZT_PDDCustomerEntity()
                            {
                                CustNo = CustNo,
                                OrderNo = dtSource.Rows[i][1].ToString(),
                                OrderTime = orderTime,
                                Mobile = mobile,
                                Telphone = telphone,
                                Province = dtSource.Rows[i][6].ToString(),
                                City = dtSource.Rows[i][7].ToString(),
                                County = dtSource.Rows[i][8].ToString(),
                                Address = dtSource.Rows[i][9].ToString(),
                                GoodsName = dtSource.Rows[i][15].ToString(),
                            };
                            entity.Create();
                            db.Insert(entity);
                        }

                        //����PDD��־
                        string parceNo = ChangeDataToD(dtSource.Rows[i][0].ToString()).ToString();
                        ZZT_PDDLogEntity logEntity = new ZZT_PDDLogEntity
                        {
                            CustNo = CustNo,
                            ParcelNo = parceNo,
                            OrderNo = dtSource.Rows[i][1].ToString(),
                            NickName = dtSource.Rows[i][2].ToString(),
                            FullName = dtSource.Rows[i][3].ToString(),
                            Mobile = mobile,
                            Telphone = telphone,
                            Province = dtSource.Rows[i][6].ToString(),
                            City = dtSource.Rows[i][7].ToString(),
                            County = dtSource.Rows[i][8].ToString(),
                            Address = dtSource.Rows[i][9].ToString(),
                            ZipCode = dtSource.Rows[i][10].ToString(),
                            Express = dtSource.Rows[i][11].ToString(),
                            ExpressNo = dtSource.Rows[i][12].ToString(),
                            Weight = dtSource.Rows[i][13].ToString(),
                            Freight = dtSource.Rows[i][14].ToString(),
                            GoodsName = dtSource.Rows[i][15].ToString(),
                            OrderTime = orderTime,
                            PayTime = payTime,
                            DeliveryTime = deliveryTime,
                            PrintingTime = printingTime,
                            BuyerMessage = dtSource.Rows[i][20].ToString(),
                            SellerMessage = dtSource.Rows[i][21].ToString(),
                            MakeNote = dtSource.Rows[i][22].ToString(),
                            MergeOrder = dtSource.Rows[i][23].ToString(),
                            SendPerson = dtSource.Rows[i][24].ToString(),
                            SendTelphone = dtSource.Rows[i][25].ToString(),
                            SendProvince = dtSource.Rows[i][26].ToString(),
                            SendCity = dtSource.Rows[i][27].ToString(),
                            SendCounty = dtSource.Rows[i][28].ToString(),
                            SendAddress = dtSource.Rows[i][29].ToString(),
                        };
                        logEntity.Create();
                        db.Insert(logEntity);


                    }
                }
                db.Commit();
                return "����ɹ�";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private Decimal ChangeDataToD(string strData)
        {
            Decimal dData = 0.0M;
            if (strData.Contains("E"))
            {
                dData = Decimal.Parse(strData.ToString(), System.Globalization.NumberStyles.Float);
            }
            return dData;
        }
        #endregion
    }
}
