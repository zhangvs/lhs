using HZSoft.Application.Code;
using HZSoft.Application.Entity.BaseManage;
using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.IService.CustomerManage;
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
    /// �� �ڣ�2017-11-04 16:22
    /// �� ��������
    /// </summary>
    public class CRM_CallLogService : RepositoryFactory<CRM_CallLogEntity>, CRM_CallLogIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<CRM_CallLogEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<CRM_CallLogEntity>();
            var queryParam = queryJson.ToJObject();
            string strSql = @" SELECT * FROM CRM_CallLog where 1=1 ";

            //��������
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and CallTime BETWEEN '" + startTime + "' and '" + endTime + "'";
            }

            //��������
            if (!queryParam["CallNumber"].IsEmpty())
            {
                string CallNumber = queryParam["CallNumber"].ToString();
                strSql += " and CallNumber = '" + CallNumber + "'";
            }

            //Ա��
            if (!queryParam["CreateUserId"].IsEmpty())
            {
                string CreateUserId = queryParam["CreateUserId"].ToString();
                strSql += " and CreateUserId = '" + CreateUserId + "'";
            }
            else
            {
                if (!OperatorProvider.Provider.Current().IsSystem)
                {
                    string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                    strSql += " and CreateUserId in (" + dataAutor + ")";
                }
            }
            return this.BaseRepository().FindList(strSql.ToString(), pagination);
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<CRM_CallLogEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public CRM_CallLogEntity GetEntity(string keyValue)
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
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, CRM_CallLogEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                entity.Create();
                this.BaseRepository().Insert(entity);
            }
        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        public void SaveForm( CRM_CallLogEntity entity)
        {
            //WriteInLog log = new WriteInLog();

            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();

            //log.writeInLog("���в��뻰����");
            entity.Create();
            UserEntity user= db.FindEntity<UserEntity>(t => t.RealName == entity.WorkerName);
            if (user !=null)
            {
                entity.CreateUserId = user.UserId;
                entity.CreateUserName = user.RealName;
                //log.writeInLog("�޸��û�id");
            }
            db.Insert(entity);
            //log.writeInLog("���뻰����ɹ�");

            var _400_Data = db.FindEntity<ZZT_400CustomerEntity>(t => t.Mobile == entity.CallNumber);
            if (_400_Data != null)
            {
                _400_Data.CallOutTime = entity.CallTime;
                _400_Data.CallState = 1;
                db.Update(_400_Data);
                //log.writeInLog("�����400�ͻ��ص�״̬ʱ��ɹ�");
            }
            else
            {
                var pdd_Data = db.FindEntity<ZZT_PDDCustomerEntity>(t => t.Mobile == entity.CallNumber);
                if (pdd_Data != null)
                {
                    pdd_Data.CallOutTime = entity.CallTime;
                    db.Update(pdd_Data);
                    //log.writeInLog("�����ƴ���ͻ��ص�ʱ��ɹ�");
                }
            }
            db.Commit();
            //log.writeInLog("�ύ���ݿ�");
        }
        #endregion
    }
}
