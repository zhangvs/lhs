using HZSoft.Application.Code;
using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.IService.CustomerManage;
using HZSoft.Application.IService.SystemManage;
using HZSoft.Application.Service.SystemManage;
using HZSoft.Data.Repository;
using HZSoft.Util.Extension;
using HZSoft.Util.WebControl;
using HZSoft.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using HZSoft.Application.Service.BaseManage;
using HZSoft.Application.IService.BaseManage;
using HZSoft.Application.Entity.BaseManage;

namespace HZSoft.Application.Service.CustomerManage
{
    /// <summary>
    /// �� �� 6.1
    /// 
    /// �� �����ܴ���
    /// �� �ڣ�2016-03-14 09:47
    /// �� �����ͻ���Ϣ
    /// </summary>
    public class LOHAS_CustomerService : RepositoryFactory<LOHAS_CustomerEntity>, LOHAS_CustomerIService
    {
        private ICodeRuleService coderuleService = new CodeRuleService();
        private IUserService userService = new UserService();
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LOHAS_CustomerEntity> GetList()
        {
            string strSql = "select * from LOHAS_Customer where (DeleteMark<>1 or DeleteMark IS NULL)  ";
            string strOrder = " ORDER BY CreateDate desc";
            if (!OperatorProvider.Provider.Current().IsSystem)
            {
                string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                strSql += " and TraceUserId in (" + dataAutor + ")";
            }

            strSql += strOrder;
            return this.BaseRepository().FindList(strSql.ToString());
        }

        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<LOHAS_CustomerEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            string strSql = $"select * from LOHAS_Customer where (DeleteMark<>1 or DeleteMark IS NULL) ";
            //��������
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and CreateDate >= '" + startTime + "' and CreateDate < '" + endTime + "'";
            }
            //�ͻ����
            if (!queryParam["EnCode"].IsEmpty())
            {
                string EnCode = queryParam["EnCode"].ToString();
                strSql += " and EnCode like '%" + EnCode + "%'";
            }

            //�ͻ�����
            if (!queryParam["FullName"].IsEmpty())
            {
                string FullName = queryParam["FullName"].ToString();
                strSql += " and FullName like '%" + FullName + "%'";
            }
            //���ʦ
            if (!queryParam["Contact"].IsEmpty())
            {
                string Contact = queryParam["Contact"].ToString();
                strSql += " and Contact like '%" + Contact + "%'";
            }

            //�ͻ�����
            if (!queryParam["CustLevelId"].IsEmpty())
            {
                int CustLevelId = queryParam["CustLevelId"].ToInt();
                strSql += " and CustLevelId  = " + CustLevelId;
            }
            //�ͻ��̶�
            if (!queryParam["CustDegreeId"].IsEmpty())
            {
                int CustDegreeId = queryParam["CustDegreeId"].ToInt();
                strSql += " and CustDegreeId  = " + CustDegreeId;
            }
            //ǩԼ
            if (!queryParam["SignMark"].IsEmpty())
            {
                int SignMark = queryParam["SignMark"].ToInt();
                strSql += " and SignMark  = " + SignMark;
            }
            //����
            if (!queryParam["DieMark"].IsEmpty())
            {
                int DieMark = queryParam["DieMark"].ToInt();
                strSql += " and DieMark  = " + DieMark;
            }
            //��ʱ����
            if (!queryParam["AlertState"].IsEmpty())
            {
                strSql += " and AlertDateTime < '" + DateTime.Now + "'";
            }
            //����
            if (!queryParam["JinMark"].IsEmpty())
            {
                strSql += " and JinDateTime is not null " ;
            }
            //����
            if (!queryParam["LiangMark"].IsEmpty())
            {
                strSql += " and LiangDateTime is not null ";
            }

            //������
            if (!queryParam["TraceUserName"].IsEmpty())
            {
                string TraceUserName = queryParam["TraceUserName"].ToString();
                strSql += " and TraceUserName = '" + TraceUserName + "'";
            }
            else
            {
                if (!OperatorProvider.Provider.Current().IsSystem)
                {
                    string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                    strSql += " and TraceUserId in (" + dataAutor + ")";
                }
            }
            return this.BaseRepository().FindList(strSql.ToString(), pagination);
        }

        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public LOHAS_CustomerEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        #endregion

        #region ��֤����
        /// <summary>
        /// �ͻ����Ʋ����ظ�
        /// </summary>
        /// <param name="fullName">����</param>
        /// <param name="keyValue">����</param>
        /// <returns></returns>
        public bool ExistFullName(string fullName, string keyValue)
        {
            var expression = LinqExtensions.True<LOHAS_CustomerEntity>();
            expression = expression.And(t => t.FullName == fullName && t.DeleteMark != 1);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.CustomerId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        /// <summary>
        /// �ֻ������ظ�
        /// </summary>
        /// <param name="Mobile">����</param>
        /// <param name="keyValue">����</param>
        /// <returns></returns>
        public bool ExistMobile(string Mobile, string keyValue)
        {
            var expression = LinqExtensions.True<LOHAS_CustomerEntity>();
            expression = expression.And(t => t.Mobile == Mobile && t.DeleteMark != 1);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.CompanyId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }

        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
        public void RemoveForm(string keyValue)
        {
            //IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            //try
            //{
            //    db.Delete<LOHAS_CustomerEntity>(keyValue);
            //    db.Delete<TrailRecordEntity>(t => t.ObjectId.Equals(keyValue));
            //    db.Delete<CustomerContactEntity>(t => t.CustomerId.Equals(keyValue));
            //    db.Commit();
            //}
            //catch (Exception)
            //{
            //    db.Rollback();
            //    throw;
            //}

            LOHAS_CustomerEntity entity = GetEntity(keyValue);
            entity.Modify(keyValue);
            entity.DeleteMark = 1;
            this.BaseRepository().Update(entity);

        }

        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, LOHAS_CustomerEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
                try
                {
                    entity.Create();
                    //���ָ��ģ����߱�ŵĵ��ݺ�
                    entity.EnCode = coderuleService.SetBillCode(entity.CreateUserId, "d7773d26-a762-41da-a138-b2a2090304b3", "", db);
                    db.Insert(entity);
                    db.Commit();
                }
                catch (Exception)
                {
                    db.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// �����
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        /// <param name="moduleId">ģ��</param>
        public void SaveForm(string keyValue, LOHAS_CustomerEntity entity, string moduleId)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
                try
                {
                    entity.Create();
                    //���ָ��ģ����߱�ŵĵ��ݺ�
                    entity.EnCode = coderuleService.SetBillCode(entity.CreateUserId, moduleId, "", db);
                    db.Insert(entity);
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


        #region 400����
        /// <summary>
        /// ������������
        /// </summary>
        /// <param name="dtSource">ʵ�����</param>
        /// <returns></returns>
        public string BatchAdd(DataTable dtSource)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            string errorMsg = "�����ظ����룺";
            try
            {
                if (dtSource==null)
                {
                    return "��������Ϊ�գ�Sheet1";
                }
                for (int i = 0; i < dtSource.Rows.Count; i++)
                {
                    string mobile = dtSource.Rows[i][1].ToString();

                    LOHAS_CustomerEntity hsf_CardEntity = db.IQueryable<LOHAS_CustomerEntity>(t => t.Mobile.Equals(mobile)).First();
                    if (hsf_CardEntity!=null)
                    {
                        errorMsg += mobile+",";
                        break;
                    }

                    DateTime? NullTime = null;
                    string jiaoDateTimeStr = dtSource.Rows[i][4].ToString();
                    DateTime? jiaoDateTime = string.IsNullOrEmpty(jiaoDateTimeStr) ? NullTime : DateTime.ParseExact(jiaoDateTimeStr, "yyyy/M/d H:m:s", System.Globalization.CultureInfo.CurrentCulture);


                    string liangDateTimeStr = dtSource.Rows[i][7].ToString();
                    DateTime? liangDateTime = string.IsNullOrEmpty(liangDateTimeStr) ? NullTime : DateTime.ParseExact(liangDateTimeStr, "yyyy/M/d H:m:s", System.Globalization.CultureInfo.CurrentCulture);


                    string jinDateTimeStr = dtSource.Rows[i][8].ToString();
                    DateTime? jinDateTime = string.IsNullOrEmpty(jinDateTimeStr) ? NullTime : DateTime.ParseExact(jinDateTimeStr, "yyyy/M/d H:m:s", System.Globalization.CultureInfo.CurrentCulture);


                    string alertDateTimeStr = dtSource.Rows[i][14].ToString();
                    DateTime? alertDateTime = string.IsNullOrEmpty(alertDateTimeStr) ? NullTime : DateTime.ParseExact(alertDateTimeStr, "yyyy/M/d H:m:s", System.Globalization.CultureInfo.CurrentCulture);

                    

                    string traceUserName = dtSource.Rows[i][13].ToString();
                    string traceUserId = "";
                    UserEntity userEntity = userService.CheckLogin(traceUserName);
                    if (userEntity != null)
                    {
                        traceUserId = userEntity.UserId;
                    }
                    else
                    {
                        return "�����˲����ڣ�" + i;
                    }


                    string qy = dtSource.Rows[i][16].ToString();
                    qy = qy == "��" ? "1" : "0";
                    string signDateTimeStr = dtSource.Rows[i][17].ToString();
                    DateTime? signDateTime = string.IsNullOrEmpty(signDateTimeStr) ? NullTime : DateTime.ParseExact(signDateTimeStr, "yyyy/M/d H:m:s", System.Globalization.CultureInfo.CurrentCulture);

                    //����ͻ�
                    LOHAS_CustomerEntity entity = new LOHAS_CustomerEntity
                    {
                        FullName = dtSource.Rows[i][0].ToString(),
                        Mobile = mobile,
                        ShippingAddress = dtSource.Rows[i][2].ToString(),
                        Area = dtSource.Rows[i][3].ToString(),
                        JiaoDateTime = jiaoDateTime,
                        Style = dtSource.Rows[i][5].ToString(),
                        Contact = dtSource.Rows[i][6].ToString(),
                        LiangDateTime = liangDateTime,
                        JinDateTime = jinDateTime,
                        Source = dtSource.Rows[i][9].ToString(),
                        Budget = dtSource.Rows[i][10].ToString(),
                        CustLevelId = dtSource.Rows[i][11].ToString(),
                        CustDegreeId = dtSource.Rows[i][12].ToString(),
                        TraceUserName = traceUserName,
                        TraceUserId = traceUserId,
                        AlertDateTime = alertDateTime,
                        Description = dtSource.Rows[i][15].ToString(),
                        SignMark = Convert.ToInt32(qy),
                        SignDateTime= signDateTime

                    };
                    entity.Create();
                    entity.EnCode = coderuleService.SetBillCode(entity.CreateUserId, "d7773d26-a762-41da-a138-b2a2090304b3", "", db);
                    db.Insert(entity);
                }
                db.Commit();
                return "����ɹ���"+ errorMsg.Trim(',');
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion
    }
}
