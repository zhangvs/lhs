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
    /// �� �ڣ�2018-05-04 13:50
    /// �� ����400�ͻ�
    /// </summary>
    public class ZZT_400CustomerService : RepositoryFactory<ZZT_400CustomerEntity>, ZZT_400CustomerIService
    {
        private ICodeRuleService coderuleService = new CodeRuleService();

        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<ZZT_400CustomerEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<ZZT_400CustomerEntity>();
            var queryParam = queryJson.ToJObject();

            string strSql = "select * from ZZT_400Customer where 1=1 ";          

            //��������
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and CallInTime BETWEEN '" + startTime + "' and '" + endTime + "'";
            }
            //�ͻ����
            if (!queryParam["CustNo"].IsEmpty())
            {
                string CustNo = queryParam["CustNo"].ToString();
                strSql += " and CustNo like '%" + CustNo + "%'";
            }
            //����
            if (!queryParam["FullName"].IsEmpty())
            {
                string FullName = queryParam["FullName"].ToString();
                strSql += " and FullName like '%" + FullName + "%'";
            }
            //�ֻ���
            if (!queryParam["Mobile"].IsEmpty())
            {
                string Mobile = queryParam["Mobile"].ToString();
                strSql += " and Mobile like '%" + Mobile + "%'";
            }

            //�ص�״̬
            if (!queryParam["CallState"].IsEmpty())
            {
                int CallState = queryParam["CallState"].ToInt();
                strSql += " and CallState = " + CallState;
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
        public IEnumerable<ZZT_400CustomerEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public ZZT_400CustomerEntity GetEntity(string keyValue)
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
            var expression = LinqExtensions.True<ZZT_400CustomerEntity>();
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
                    ZZT_400CustomerEntity entity = this.BaseRepository().FindEntity(custIds[i]);
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
        public void SaveForm(string keyValue, ZZT_400CustomerEntity entity)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            if (!string.IsNullOrEmpty(keyValue))
            {
                var old_Data = db.FindEntity<ZZT_400CustomerEntity>(t => t.Id == keyValue);
                //�޸ı�ע������µĸ�����¼
                if (old_Data.Description!=entity.Description && entity.Description != "&nbsp;")
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
                //�ֶ�����
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
                    ZZT_400CustomerEntity entity= this.BaseRepository().FindEntity(custIds[i]);
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
                    ZZT_400CustomerEntity entity = this.BaseRepository().FindEntity(custIds[i]);
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
                    ZZT_400CustomerEntity entity = this.BaseRepository().FindEntity(custIds[i]);
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

        #region 400����
        /// <summary>
        /// ������������
        /// </summary>
        /// <param name="dtSource">ʵ�����</param>
        /// <returns></returns>
        public string BatchAdd400(DataTable dtSource)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                for (int i = 0; i < dtSource.Rows.Count; i++)
                {                    
                    string mobile = dtSource.Rows[i][0].ToString();
                    if (!string.IsNullOrWhiteSpace(mobile))
                    {
                        string CustNo = "";
                        DateTime? NullTime = null;
                        string callInTimeStr = dtSource.Rows[i][5].ToString();
                        DateTime? callInTime = string.IsNullOrEmpty(callInTimeStr) ? NullTime : DateTime.ParseExact(callInTimeStr, "yyyy/M/d H:m:s", System.Globalization.CultureInfo.CurrentCulture);


                        string callStateStr = dtSource.Rows[i][4].ToString();
                        int callState = 0;
                        if (callStateStr == "�ѽ�����")
                        {
                            callState = 1;
                        }

                        var old_Data = db.FindEntity<ZZT_400CustomerEntity>(t => t.Mobile == mobile);
                        //�Ƿ��ǰ�����
                        if (old_Data != null)
                        {
                            CustNo = old_Data.CustNo;
                            //�޸ĵ�ǰ���ݵ�������ʱ�䣬����״̬                            
                            old_Data.CallInTime = callInTime;
                            old_Data.CallState = callState; 
                            old_Data.Modify(old_Data.Id);
                            db.Update(old_Data);                          
                        }
                        else
                        {
                            //�½�400�ͻ���
                            CustNo = coderuleService.SetBillCode(OperatorProvider.Provider.Current().UserId, SystemInfo.CurrentModuleId, "", db);//���ָ��ģ����߱�ŵĵ��ݺ�
                            ZZT_400CustomerEntity entity = new ZZT_400CustomerEntity()
                            {
                                CustNo = CustNo,
                                Mobile = mobile,
                                Province= dtSource.Rows[i][1].ToString(),
                                City= dtSource.Rows[i][2].ToString(),
                                CallInNumber = dtSource.Rows[i][3].ToString(),
                                CallState = callState,
                                CallInTime = callInTime,
                            };                            
                            entity.Create();

                            db.Insert(entity);
                        }

                        //����400��־
                        ZZT_400LogEntity logEntity = new ZZT_400LogEntity
                        {
                            CustNo = CustNo,
                            Mobile = mobile,
                            Province = dtSource.Rows[i][1].ToString(),
                            City = dtSource.Rows[i][2].ToString(),
                            CallInNumber = dtSource.Rows[i][3].ToString(),
                            CallState = dtSource.Rows[i][4].ToString(),
                            CallInTime = callInTime,
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
        #endregion
    }
}
