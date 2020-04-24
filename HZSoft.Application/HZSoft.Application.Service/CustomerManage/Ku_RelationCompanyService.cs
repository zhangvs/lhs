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
    /// �� �ڣ�2018-06-15 14:39
    /// �� ����������˾
    /// </summary>
    public class Ku_RelationCompanyService : RepositoryFactory<Ku_RelationCompanyEntity>, Ku_RelationCompanyIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<Ku_RelationCompanyEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<Ku_RelationCompanyEntity>();
            var queryParam = queryJson.ToJObject();
            //�ͻ�Id
            if (!queryParam["companyId"].IsEmpty())
            {
                int? CompanyId = Convert.ToInt32(queryParam["companyId"]);
                expression = expression.And(t => t.CompanyId == CompanyId);
            }
            //�ͻ�Id
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();
                expression = expression.And(t => t.CompanyName.Contains(keyword) || t.RelationCompanyName.Contains(keyword));
            }

            return this.BaseRepository().IQueryable(expression).OrderByDescending(t => t.CreateDate).ToList();
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<Ku_RelationCompanyEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public Ku_RelationCompanyEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region ��֤����
        /// <summary>
        /// �ͻ����Ʋ����ظ�
        /// </summary>
        /// <param name="RelationCompanyName">����</param>
        /// <param name="keyValue">����</param>
        /// <returns></returns>
        public bool ExistFullName(string RelationCompanyName, string keyValue)
        {
            var expression = LinqExtensions.True<Ku_RelationCompanyEntity>();
            expression = expression.And(t => t.RelationCompanyName == RelationCompanyName);
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
        /// <param name="keyValue">����</param>
        public void RemoveForm(string keyValue)
        {
            var entity = this.BaseRepository().FindEntity(keyValue);
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            var company = db.FindEntity<Ku_CompanyEntity>(t => t.Id == entity.CompanyId);
            //��������-1
            company.RelationCount = --company.RelationCount;
            db.Update<Ku_CompanyEntity>(company);
            db.Commit();

            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, Ku_RelationCompanyEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {

                IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();

                //var relationData = db.FindEntity<Ku_RelationCompanyEntity>(t => t.CompanyId == entity.CompanyId && t.RelationCompanyId== entity.RelationCompanyId);//������˾���ظ��ж�  
                //if (relationData!=null)
                //{
                //    throw new Exception($"�Ѵ�����ù�˾[{entity.RelationCompanyName}]�Ĺ�����");
                //}

                var company = db.FindEntity<Ku_CompanyEntity>(t => t.Id == entity.CompanyId);

                //��������+1
                if (company.RelationCount == null)
                {
                    company.RelationCount = 1;
                }
                else
                {
                    company.RelationCount = ++company.RelationCount;
                }
                db.Update<Ku_CompanyEntity>(company);
                db.Commit();

                entity.Create();
                this.BaseRepository().Insert(entity);
            }
        }
        #endregion
    }
}
