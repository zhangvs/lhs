using HZSoft.Application.Code;
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
    /// �� �ڣ�2018-06-27 14:31
    /// �� ������˾�����¼
    /// </summary>
    public class Ku_CompanySeeService : RepositoryFactory<Ku_CompanySeeEntity>, Ku_CompanySeeIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<Ku_CompanySeeEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<Ku_CompanySeeEntity>();
            var queryParam = queryJson.ToJObject();
            string strSql = $"select * from Ku_CompanySee where 1=1 ";
            //��������
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and SeeDate >= '" + startTime + "' and SeeDate < '" + endTime + "'";
            }
            //��˾��
            if (!queryParam["CompanyName"].IsEmpty())
            {
                string CompanyName = queryParam["CompanyName"].ToString();
                strSql += " and CompanyName  like '%" + CompanyName + "%' ";
            }
            if (!OperatorProvider.Provider.Current().IsSystem)
            {
                string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                strSql += " and (SeeUserId in (" + dataAutor + "))";
            }

            //ָ������һ���ڵĻ�ȡ����
            if (!queryParam["SeeDate"].IsEmpty())
            {
                DateTime startTime = queryParam["SeeDate"].ToDate();
                DateTime endTime = queryParam["SeeDate"].ToDate().AddDays(1);
                strSql += " and SeeDate >= '" + startTime + "' and SeeDate < '" + endTime + "'";
            }

            //��˾��
            if (!queryParam["SeeUserName"].IsEmpty())
            {
                string SeeUserName = queryParam["SeeUserName"].ToString();
                strSql += " and SeeUserName  like '%" + SeeUserName + "%' ";
            }

            return this.BaseRepository().FindList(strSql.ToString(), pagination);
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<Ku_CompanySeeEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }

        /// <summary>
        /// �����¼
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Ku_CompanySeeEntity> GetListByUserId(string searchName)
        {
            string strSql = @"select * from Ku_CompanySee where SeeUserId = '"+OperatorProvider.Provider.Current().UserId+"'";

            //��˾��
            if (!searchName.IsEmpty())
            {
                strSql += " and CompanyName like '%" + searchName + "%' ";
            }
            strSql += " ORDER BY SeeDate desc";
            return this.BaseRepository().FindList(strSql.ToString());
        }

        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public Ku_CompanySeeEntity GetEntity(string keyValue)
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
        public void SaveForm(string keyValue, Ku_CompanySeeEntity entity)
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
        #endregion
    }
}
