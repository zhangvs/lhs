using HZSoft.Application.Code;
using HZSoft.Application.Entity.BaseManage;
using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.Entity.SystemManage;
using HZSoft.Application.IService.CustomerManage;
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
    /// �� �ڣ�2017-10-23 14:11
    /// �� �������ſ�
    /// </summary>
    public class TelphoneLiangService : RepositoryFactory<TelphoneLiangEntity>, TelphoneLiangIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<TelphoneLiangEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            string strSql = "select * from TelphoneLiang where DeleteMark <> 1 and EnabledMark <> 1";
            //���ݱ��
            if (!queryParam["Telphone"].IsEmpty())
            {
                string Telphone = queryParam["Telphone"].ToString();
                strSql += " and Telphone = '" + Telphone + "'";
            }
            //����
            if (!queryParam["OrganizeId"].IsEmpty())
            {
                string OrganizeId = queryParam["OrganizeId"].ToString();
                strSql += " and OrganizeId = '" + OrganizeId + "'";
            }
            else
            {
                if (!OperatorProvider.Provider.Current().IsSystem && OperatorProvider.Provider.Current().CompanyId != "207fa1a9-160c-4943-a89b-8fa4db0547ce")
                {
                    //string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                    //strSql += " and OrganizeId IN( select OrganizeId from Base_User where 1=1";
                    //strSql += " and UserId in (" + dataAutor + ") GROUP BY OrganizeId )";
                    strSql += " and OrganizeId in(SELECT OrganizeId FROM Base_Organize WHERE OrganizeId='" + OperatorProvider.Provider.Current().CompanyId + "' OR ParentId ='" + OperatorProvider.Provider.Current().CompanyId + "')";
                }
            }
            //����
            if (!queryParam["Grade"].IsEmpty())
            {
                string Grade = queryParam["Grade"].ToString();
                strSql += " and Grade = '" + Grade + "'";
            }

            return this.BaseRepository().FindList(strSql.ToString(), pagination);
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<TelphoneLiangEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        public IEnumerable<TelphoneLiangEntity> GetList(string telphone, string organizeId)
        {
            string strSql = "SELECT TOP(20) Telphone FROM TelphoneLiang WHERE SellMark<>1 AND DeleteMark<>1 and EnabledMark <> 1 ";
            if (!string.IsNullOrEmpty(telphone))
            {
                strSql += " and Telphone like '%" + telphone + "%' ";
            }
            if (!string.IsNullOrEmpty(organizeId))
            {
                strSql += " and OrganizeId in(SELECT OrganizeId FROM Base_Organize WHERE OrganizeId='" + organizeId + "' OR ParentId ='" + organizeId + "')";
            }
            return this.BaseRepository().FindList(strSql.ToString());
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="organizeId">���Ź�˾</param>
        /// <param name="Grade">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<TelphoneLiangEntity> GetGrade(string organizeId, string Grade)
        {
            string strSql = "select * from TelphoneLiang where DeleteMark <> 1 and EnabledMark <> 1 and SellMark <> 1";
            //���ݱ��
            if (!Grade.IsEmpty())
            {
                strSql += " and Grade = '" + Grade + "'";
            }
            if (!organizeId.IsEmpty())
            {
                strSql += " and OrganizeId IN( select OrganizeId from Base_Organize where ParentId='" + organizeId + "' OR OrganizeId='" + organizeId + "')";
            }
            strSql += " ORDER BY Grade,price desc";
            return this.BaseRepository().FindList(strSql);
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public TelphoneLiangEntity GetEntity(int? keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="telphone">�ֻ���</param>
        /// <param name="organizeId">���Ż���</param>
        /// <returns></returns>
        public TelphoneLiangEntity GetEntityByTelphone(string telphone)
        {
            return this.BaseRepository().FindEntity(t => t.Telphone == telphone);
        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
        public void RemoveForm(int? keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        public void SaveForm(int? keyValue, TelphoneLiangEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue.ToString()))
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


        /// <summary>
        /// ������������
        /// </summary>
        /// <param name="dtSource">ʵ�����</param>
        /// <returns></returns>
        public string BatchAddEntity(DataTable dtSource)
        {

            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();

            try
            {
                for (int i = 0; i < dtSource.Rows.Count; i++)
                {
                    string telphone = dtSource.Rows[i][0].ToString();
                    if (telphone.Length == 11)
                    {
                        string Number7 = telphone.Substring(0, 7);
                        decimal Price = Convert.ToDecimal(dtSource.Rows[i][1].ToString());

                        //���
                        string itemName = dtSource.Rows[i][2].ToString();
                        string itemNCode = "";
                        var DataItemDetail = db.FindEntity<DataItemDetailEntity>(t => t.ItemName == itemName);
                        if (DataItemDetail != null)
                        {
                            itemNCode = DataItemDetail.ItemValue;
                        }
                        else
                        {
                            itemNCode = "8";//����
                        }

                        //����ǰ7λȷ�����к���Ӫ��
                        string City = "";
                        string Operator = "";
                        var TelphoneData = db.FindEntity<TelphoneDataEntity>(t => t.Number7 == Number7);
                        if (TelphoneData != null)
                        {
                            City = TelphoneData.City;
                            Operator = TelphoneData.Operate;
                        }
                        else
                        {
                            return "�Ŷβ�����" + Number7;
                        }
                        //�ײ�
                        string Package = dtSource.Rows[i][3].ToString();

                        //������˾
                        string organize = dtSource.Rows[i][4].ToString();
                        string organizeId = "";
                        var organizeData = db.FindEntity<OrganizeEntity>(t => t.FullName == organize);
                        if (organizeData != null)
                        {
                            organizeId = organizeData.OrganizeId;
                        }
                        else
                        {
                            return "����������" + organize;
                        }

                        //�������
                        TelphoneLiangEntity entity = new TelphoneLiangEntity();
                        entity.Telphone = telphone;
                        entity.City = City;
                        entity.Operator = Operator;
                        entity.Grade = itemNCode;
                        entity.Price = Price;
                        entity.Package = Package;
                        entity.SellMark = 0;
                        entity.DeleteMark = 0;
                        entity.OrganizeId = organizeId;
                        entity.Create();
                        db.Insert(entity);
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
    }
}
