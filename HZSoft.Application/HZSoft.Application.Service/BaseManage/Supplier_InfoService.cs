using HZSoft.Application.Entity.BaseManage;
using HZSoft.Application.IService.BaseManage;
using HZSoft.Data.Repository;
using HZSoft.Util.Extension;
using HZSoft.Util.WebControl;
using System.Collections.Generic;
using System.Linq;
using HZSoft.Util;
using HZSoft.Application.Code;
using HZSoft.Application.IService.SystemManage;
using HZSoft.Application.Service.SystemManage;
using System;

namespace HZSoft.Application.Service.BaseManage
{
    /// <summary>
    /// �� �� 6.1
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2017-06-30 10:28
    /// �� ������Ӧ��
    /// </summary>
    public class Supplier_InfoService : RepositoryFactory<Supplier_InfoEntity>, Supplier_InfoIService
    {
        private ICodeRuleService coderuleService = new CodeRuleService();
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<Supplier_InfoEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<Supplier_InfoEntity>();
            var queryParam = queryJson.ToJObject();
            //��ѯ����&& !queryParam["keyword"].IsEmpty()
            if (!queryParam["condition"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "Code":              //������
                        expression = expression.And(t => t.Code.Contains(keyword));
                        break;
                    case "FullName":            //�������
                        expression = expression.And(t => t.FullName.Contains(keyword));
                        break;
                    case "ShortName":            //���
                        expression = expression.And(t => t.ShortName.Contains(keyword));
                        break;
                    case "Namepy":              //������
                        expression = expression.And(t => t.Namepy.Contains(keyword));
                        break;
                    case "ParentId":                 //�ϼ����
                        expression = expression.And(t => t.ParentId.Contains(keyword));
                        break;
                    case "All":
                        expression = expression.And(t => t.FullName.Contains(keyword)
                            || t.Code.Contains(keyword)
                            || t.FullName.Contains(keyword)
                            || t.ShortName.Contains(keyword)
                            || t.Namepy.Contains(keyword)
                            || t.ParentId.Contains(keyword)
                            );
                        break;
                    default:
                        break;
                }
            }
            return this.BaseRepository().FindList(expression, pagination);
        }
        public IEnumerable<Supplier_InfoEntity> GetParentId()
        {
            return this.BaseRepository().FindList("SELECT * FROM Supplier_Info WHERE ParentId IS null ");
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<Supplier_InfoEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public Supplier_InfoEntity GetEntity(string keyValue)
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
        public void SaveForm(string keyValue, Supplier_InfoEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                //IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
                //entity.Create();
                ////���ָ��ģ����߱�ŵĵ��ݺ�
                //entity.Code = coderuleService.SetBillCode(entity.CreatorId, SystemInfo.CurrentModuleId, "", db);
                //this.BaseRepository().Insert(entity);

                IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
                try
                {
                    entity.Create();
                    db.Insert(entity);
                    //ռ�õ��ݺ�
                    coderuleService.UseRuleSeed(entity.CreatorId, SystemInfo.CurrentModuleId, "", db);
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
