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
    /// �� �ڣ�2018-03-23 16:49
    /// �� ������˾��ϵ�˿�
    /// </summary>
    public class Ku_CompanyContactService : RepositoryFactory<Ku_CompanyContactEntity>, Ku_CompanyContactIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<Ku_CompanyContactEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<Ku_CompanyContactEntity>();
            var queryParam = queryJson.ToJObject();
            //�ͻ�Id
            if (!queryParam["companyId"].IsEmpty())
            {
                int? CompanyId = Convert.ToInt32(queryParam["companyId"]);
                expression = expression.And(t => t.CompanyId == CompanyId);
            }
            //��ѯ����
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "Contact":         //��ϵ��
                        expression = expression.And(t => t.Contact.Contains(keyword));
                        break;
                    case "Mobile":          //�ֻ�
                        expression = expression.And(t => t.Mobile.Contains(keyword));
                        break;
                    case "Tel":             //�绰
                        expression = expression.And(t => t.Tel.Contains(keyword));
                        break;
                    case "QQ":              //QQ
                        expression = expression.And(t => t.QQ.Contains(keyword));
                        break;
                    default:
                        break;
                }
            }
            return this.BaseRepository().IQueryable(expression).OrderByDescending(t => t.CreateDate).ToList();
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<Ku_CompanyContactEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public Ku_CompanyContactEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="CompanyId">����ֵ</param>
        /// <returns></returns>
        public IEnumerable<Ku_CompanyContactEntity> GetCompanyContactList(int? CompanyId)
        {
            return this.BaseRepository().FindList("select * from Ku_CompanyContact where CompanyId=" + CompanyId);
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
        public void SaveForm(string keyValue, Ku_CompanyContactEntity entity)
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
