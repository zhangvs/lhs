using HZSoft.Application.Code;
using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.IService.CustomerManage;
using HZSoft.Data.Repository;
using HZSoft.Util;
using HZSoft.Util.Extension;
using HZSoft.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace HZSoft.Application.Service.CustomerManage
{
    /// <summary>
    /// �� �� 6.1
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2018-03-23 16:28
    /// �� ������˾��
    /// </summary>
    public class Ku_CompanyService : RepositoryFactory<Ku_CompanyEntity>, Ku_CompanyIService
    {
        private Ku_LocationService locationService = new Ku_LocationService();
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<Ku_CompanyEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<Ku_CompanyEntity>();
            var queryParam = queryJson.ToJObject();
            string des = OperatorProvider.Provider.Current().Description;
            string locationSql = LocationHelper.GetLocationSql(des);
            string strSql = $"select c.* from Ku_Company c LEFT JOIN {locationSql} l ON l.Id=c.LocationId where ManageState=1 AND LocationId>0 AND c.District!='' ";//��Ӫ������ʾ����ȡ��˾ ObtainState=0 
            //����λ����ȦID���ڵĻ��������������Ȧ��������
            if (!queryParam["LocationId"].IsEmpty())
            {
                string LocationId = queryParam["LocationId"].ToString();
                strSql += " and LocationId = " + LocationId;
            }
            else if (!queryParam["RegeoName"].IsEmpty())
            {
                string RegeoName = queryParam["RegeoName"].ToString();
                strSql += " and c.RegeoName  like '%" + RegeoName + "%' ";
            }
            //λ��ID
            if (!queryParam["LocationIds"].IsEmpty())
            {
                string LocationIds = queryParam["LocationIds"].ToString();
                strSql += " and LocationId in (" + LocationIds + ")";
            }            
            //��������
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and BuildTime >= '" + startTime + "' and BuildTime < '" + endTime + "'";
            }
            //��˾��
            if (!queryParam["CompanyName"].IsEmpty())
            {
                string CompanyName = queryParam["CompanyName"].ToString();
                strSql += " and c.CompanyName  like '%" + CompanyName + "%' ";
            }
            //��Ӫ��Χ
            if (!queryParam["Scope"].IsEmpty())
            {
                string Scope = queryParam["Scope"].ToString();
                strSql += " and c.Scope  like '%" + Scope + "%' ";
            }
            //��ҵ
            if (!queryParam["Sector"].IsEmpty())
            {
                string Sector = queryParam["Sector"].ToString();
                Sector = Sector.Replace("[", "").Replace("]", "").Replace("\"", "'");
                strSql += " and c.Sector  in(" + Sector + ")";
            }
            //����
            if (!queryParam["District"].IsEmpty())
            {
                string District = queryParam["District"].ToString();
                District = District.Replace("[", "").Replace("]", "").Replace("\"", "'");
                strSql += " and c.District in(" + District + ")";
            }
            //POI�ֲ�
            if (!queryParam["TypeName"].IsEmpty())
            {
                string TypeName = queryParam["TypeName"].ToString();
                TypeName = TypeName.Replace("[", "").Replace("]", "").Replace("\"", "'");
                strSql += " and l.TypeName in(" + TypeName + ")";
            }
            //������
            if (!queryParam["ObtainUserId"].IsEmpty())
            {
                string ObtainUserId = queryParam["ObtainUserId"].ToString();
                strSql += " and c.ObtainUserId = '" + ObtainUserId + "'";
            }
            //״̬
            if (!queryParam["FollowState"].IsEmpty())
            {
                string FollowState = queryParam["FollowState"].ToString();
                strSql += " and c.FollowState = '" + FollowState + "'";
            }
            //����¥�㣬����
            if (pagination.sidx == "Floor")
            {
                pagination.sidx = "Floor " + pagination.sord + ",Room " + pagination.sord;
            }
            return this.BaseRepository().FindList(strSql.ToString(), pagination);
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<Ku_CompanyEntity> MyGetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<Ku_CompanyEntity>();
            var queryParam = queryJson.ToJObject();
            string des = OperatorProvider.Provider.Current().Description;
            string locationSql = LocationHelper.GetLocationSql(des);
            string strSql = $"select c.* from Ku_Company c LEFT JOIN {locationSql} l ON l.Id=c.LocationId where ObtainState=1 and ManageState=1 AND LocationId>0 AND c.District!='' ";//ObtainState=1��ȡ��
            //����λ����ȦID���ڵĻ��������������Ȧ��������
            if (!queryParam["LocationId"].IsEmpty())
            {
                string LocationId = queryParam["LocationId"].ToString();
                strSql += " and LocationId = " + LocationId;
            }
            else if (!queryParam["RegeoName"].IsEmpty())
            {
                string RegeoName = queryParam["RegeoName"].ToString();
                strSql += " and c.RegeoName  like '%" + RegeoName + "%' ";
            }
            //λ��ID
            if (!queryParam["LocationIds"].IsEmpty())
            {
                string LocationIds = queryParam["LocationIds"].ToString();
                strSql += " and LocationId in (" + LocationIds + ")";
            }
            //��������
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and BuildTime >= '" + startTime + "' and BuildTime < '" + endTime + "'";
            }
            //��˾��
            if (!queryParam["CompanyName"].IsEmpty())
            {
                string CompanyName = queryParam["CompanyName"].ToString();
                strSql += " and c.CompanyName  like '%" + CompanyName + "%' ";
            }
            //��Ӫ��Χ
            if (!queryParam["Scope"].IsEmpty())
            {
                string Scope = queryParam["Scope"].ToString();
                strSql += " and c.Scope  like '%" + Scope + "%' ";
            }
            //��ҵ
            if (!queryParam["Sector"].IsEmpty())
            {
                string Sector = queryParam["Sector"].ToString();
                Sector = Sector.Replace("[", "").Replace("]", "").Replace("\"", "'");
                strSql += " and c.Sector  in(" + Sector + ")";
            }
            //����
            if (!queryParam["District"].IsEmpty())
            {
                string District = queryParam["District"].ToString();
                District = District.Replace("[", "").Replace("]", "").Replace("\"", "'");
                strSql += " and c.District in(" + District + ")";
            }
            //POI�ֲ�
            if (!queryParam["TypeName"].IsEmpty())
            {
                string TypeName = queryParam["TypeName"].ToString();
                TypeName = TypeName.Replace("[", "").Replace("]", "").Replace("\"", "'");
                strSql += " and l.TypeName in(" + TypeName + ")";
            }
            //������
            if (!queryParam["ObtainUserId"].IsEmpty())
            {
                string ObtainUserId = queryParam["ObtainUserId"].ToString();
                strSql += " and c.ObtainUserId = '" + ObtainUserId + "'";
            }
            else
            {
                if (!OperatorProvider.Provider.Current().IsSystem)
                {
                    string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                    strSql += " and (c.ObtainUserId in (" + dataAutor + "))";
                }
            }

            //������
            if (!queryParam["ObtainUserName"].IsEmpty())
            {
                string ObtainUserName = queryParam["ObtainUserName"].ToString();
                strSql += " and c.ObtainUserName = '" + ObtainUserName + "'";
            }
            //��ȡ����
            if (!queryParam["ObtainStartTime"].IsEmpty() && !queryParam["ObtainEndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["ObtainStartTime"].ToDate();
                DateTime endTime = queryParam["ObtainEndTime"].ToDate().AddDays(1);
                strSql += " and ObtainDate >= '" + startTime + "' and ObtainDate < '" + endTime + "'";
            }
            //��ȡ����
            if (!queryParam["ModifyStartTime"].IsEmpty() && !queryParam["ModifyEndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["ModifyStartTime"].ToDate();
                DateTime endTime = queryParam["ModifyEndTime"].ToDate().AddDays(1);
                strSql += " and c.ModifyDate >= '" + startTime + "' and c.ModifyDate < '" + endTime + "'";
            }
            //ָ������һ���ڵĻ�ȡ����
            if (!queryParam["ObtainDate"].IsEmpty())
            {
                DateTime startTime = queryParam["ObtainDate"].ToDate();
                DateTime endTime = queryParam["ObtainDate"].ToDate().AddDays(1);
                strSql += " and ObtainDate >= '" + startTime + "' and ObtainDate < '" + endTime + "'";
            }
            //ָ������һ���ڵĻ�ȡ����
            if (!queryParam["ModifyDate"].IsEmpty())
            {
                DateTime startTime = queryParam["ModifyDate"].ToDate();
                DateTime endTime = queryParam["ModifyDate"].ToDate().AddDays(1);
                strSql += " and c.ModifyDate >= '" + startTime + "' and c.ModifyDate < '" + endTime + "'";
            }

            //״̬
            if (!queryParam["FollowState"].IsEmpty())
            {
                string FollowState = queryParam["FollowState"].ToString();
                strSql += " and c.FollowState = '" + FollowState + "'";
            }
            //����¥�㣬����
            if (pagination.sidx == "Floor")
            {
                pagination.sidx = "Floor " + pagination.sord + ",Room " + pagination.sord;
            }
            //��������
            if (!queryParam["syear"].IsEmpty())
            {
                string syear = queryParam["syear"].ToString();
                strSql += " and year(c.ModifyDate)=" + syear;
            }
            //��������
            if (!queryParam["smonth"].IsEmpty())
            {
                string smonth = queryParam["smonth"].ToString();
                strSql += " and month(c.ModifyDate)=" + smonth;
            }

            return this.BaseRepository().FindList(strSql.ToString(), pagination);
        }


        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<Ku_CompanyEntity> OkGetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<Ku_CompanyEntity>();
            var queryParam = queryJson.ToJObject();
            string des = OperatorProvider.Provider.Current().Description;
            string strSql = $"select * from Ku_Company c where ObtainState=1 and ManageState=1 and FollowState=3 ";//ObtainState=1��ȡ��
            //��������
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and BuildTime >= '" + startTime + "' and BuildTime < '" + endTime + "'";
            }
            //��˾��
            if (!queryParam["CompanyName"].IsEmpty())
            {
                string CompanyName = queryParam["CompanyName"].ToString();
                strSql += " and c.CompanyName  like '%" + CompanyName + "%' ";
            }
            //��Ӫ��Χ
            if (!queryParam["Scope"].IsEmpty())
            {
                string Scope = queryParam["Scope"].ToString();
                strSql += " and c.Scope  like '%" + Scope + "%' ";
            }
            //��ҵ
            if (!queryParam["Sector"].IsEmpty())
            {
                string Sector = queryParam["Sector"].ToString();
                Sector = Sector.Replace("[", "").Replace("]", "").Replace("\"", "'");
                strSql += " and c.Sector  in(" + Sector + ")";
            }
            //����
            if (!queryParam["District"].IsEmpty())
            {
                string District = queryParam["District"].ToString();
                District = District.Replace("[", "").Replace("]", "").Replace("\"", "'");
                strSql += " and c.District in(" + District + ")";
            }
            //������
            if (!queryParam["ObtainUserId"].IsEmpty())
            {
                string ObtainUserId = queryParam["ObtainUserId"].ToString();
                strSql += " and c.ObtainUserId = '" + ObtainUserId + "'";
            }
            else
            {
                if (!OperatorProvider.Provider.Current().IsSystem)
                {
                    string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                    strSql += " and (c.ObtainUserId in (" + dataAutor + "))";
                }
            }

            //������
            if (!queryParam["ObtainUserName"].IsEmpty())
            {
                string ObtainUserName = queryParam["ObtainUserName"].ToString();
                strSql += " and c.ObtainUserName = '" + ObtainUserName + "'";
            }
            //��ȡ����
            if (!queryParam["ObtainStartTime"].IsEmpty() && !queryParam["ObtainEndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["ObtainStartTime"].ToDate();
                DateTime endTime = queryParam["ObtainEndTime"].ToDate().AddDays(1);
                strSql += " and ObtainDate >= '" + startTime + "' and ObtainDate < '" + endTime + "'";
            }
            //��ȡ����
            if (!queryParam["ModifyStartTime"].IsEmpty() && !queryParam["ModifyEndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["ModifyStartTime"].ToDate();
                DateTime endTime = queryParam["ModifyEndTime"].ToDate().AddDays(1);
                strSql += " and c.ModifyDate >= '" + startTime + "' and c.ModifyDate < '" + endTime + "'";
            }
            //ָ������һ���ڵĻ�ȡ����
            if (!queryParam["ObtainDate"].IsEmpty())
            {
                DateTime startTime = queryParam["ObtainDate"].ToDate();
                DateTime endTime = queryParam["ObtainDate"].ToDate().AddDays(1);
                strSql += " and ObtainDate >= '" + startTime + "' and ObtainDate < '" + endTime + "'";
            }
            //ָ������һ���ڵĻ�ȡ����
            if (!queryParam["ModifyDate"].IsEmpty())
            {
                DateTime startTime = queryParam["ModifyDate"].ToDate();
                DateTime endTime = queryParam["ModifyDate"].ToDate().AddDays(1);
                strSql += " and c.ModifyDate >= '" + startTime + "' and c.ModifyDate < '" + endTime + "'";
            }

            //״̬
            if (!queryParam["FollowState"].IsEmpty())
            {
                string FollowState = queryParam["FollowState"].ToString();
                strSql += " and c.FollowState = '" + FollowState + "'";
            }
            //����¥�㣬����
            if (pagination.sidx == "Floor")
            {
                pagination.sidx = "Floor " + pagination.sord + ",Room " + pagination.sord;
            }
            //��������
            if (!queryParam["syear"].IsEmpty())
            {
                string syear = queryParam["syear"].ToString();
                strSql += " and year(c.ModifyDate)=" + syear;
            }
            //��������
            if (!queryParam["smonth"].IsEmpty())
            {
                string smonth = queryParam["smonth"].ToString();
                strSql += " and month(c.ModifyDate)=" + smonth;
            }

            return this.BaseRepository().FindList(strSql.ToString(), pagination);
        }
        ///// <summary>
        ///// ��ȡ�б�
        ///// </summary>
        ///// <param name="queryJson">��ѯ����</param>
        ///// <returns>�����б�</returns>
        //public IEnumerable<Ku_CompanyEntity> GetList(string queryJson)
        //{
        //    return this.BaseRepository().IQueryable().ToList();
        //}
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<Ku_CompanyEntity> GetList(string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            string strSql = "";
            string strOrder = "";

            strSql = "select * from Ku_Company where ManageState=1 ";
            strOrder = " ORDER BY floor desc,room desc,CreateDate desc";
            //д��¥ID
            if (!queryParam["LocationId"].IsEmpty())
            {
                string locationid = queryParam["LocationId"].ToString();
                strSql += " and locationid = '" + locationid + "'";
            }
            //��˾��
            if (!queryParam["SearchName"].IsEmpty())
            {
                string CompanyName = queryParam["SearchName"].ToString();
                strSql += " and (CompanyName  like '%" + CompanyName + "%'  or CompanyAddress  like '%" + CompanyName + "%') ";
            }

            strSql += strOrder;

            return this.BaseRepository().FindList(strSql.ToString());
        }

        /// <summary>
        /// ����˽��
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Ku_CompanyEntity> GetListByUserId(string searchName, string state)
        {
            string strSql = @"select * from Ku_Company where ManageState=1 and ObtainUserId = '" + OperatorProvider.Provider.Current().UserId + "'";

            //��˾��
            if (!searchName.IsEmpty())
            {
                strSql += " and (CompanyName  like '%" + searchName + "%'  or CompanyAddress  like '%" + searchName + "%') ";
            }
            //״̬
            if (!state.IsEmpty())
            {
                strSql += " and FollowState = '" + state + "'";
            }
            strSql += " ORDER BY ObtainDate desc";

            return this.BaseRepository().FindList(strSql.ToString());
        }


        /// <summary>
        /// ��������ҳ��-״̬����
        /// </summary>
        /// <returns>�����б�</returns>
        public DataTable GetFollowState()
        {
            string userId = OperatorProvider.Provider.Current().UserId;
            string strSql = $"SELECT count(1) FROM Ku_Company b WHERE ObtainUserId='{userId}' and FollowState=3 UNION all "
+ $"SELECT count(1) FROM Ku_Company b WHERE ObtainUserId = '{userId}' and FollowState = 2 UNION all "
+ $"SELECT count(1) FROM Ku_Company b WHERE ObtainUserId = '{userId}' and FollowState = 4 UNION all "
+ $"SELECT count(1) FROM Ku_Company b WHERE ObtainUserId = '{userId}' and FollowState = 0";
            return this.BaseRepository().FindTable(strSql);
        }

        /// <summary>
        /// ��ȡ�б��Զ���ȫ��˾����
        /// </summary>
        /// <param name="CompanyName">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<Ku_CompanyEntity> GetListByName(string CompanyName)
        {
            string des = OperatorProvider.Provider.Current().Description;
            string locationSql = LocationHelper.GetLocationSql(des);
            string strSql = $"select top 5 c.Id,c.CompanyName from Ku_Company c LEFT JOIN {locationSql} l ON l.Id=c.LocationId where ManageState=1 AND LocationId>0 AND c.District!='' ";

            //��˾��
            if (!CompanyName.IsEmpty())
            {
                strSql += " and c.CompanyName like '%" + CompanyName + "%'";
            }

            return this.BaseRepository().FindList(strSql.ToString());
        }

        /// <summary>
        /// ��ȡ�б��Զ���ȫ��˾����(˽��)
        /// </summary>
        /// <param name="CompanyName">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<Ku_CompanyEntity> GetListByNameSi(string CompanyName)
        {
            string des = OperatorProvider.Provider.Current().Description;
            string locationSql = LocationHelper.GetLocationSql(des);
            string strSql = $"select top 5 c.CompanyName from Ku_Company c LEFT JOIN {locationSql} l ON l.Id=c.LocationId where ObtainState=1 and ManageState=1 AND LocationId>0 AND c.District!='' ";

            //��˾��
            if (!CompanyName.IsEmpty())
            {
                strSql += " and c.CompanyName like '%" + CompanyName + "%'";
            }
            if (!OperatorProvider.Provider.Current().IsSystem)
            {
                string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                strSql += " and (c.ObtainUserId in (" + dataAutor + "))";
            }
            return this.BaseRepository().FindList(strSql.ToString());
        }

        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public Ku_CompanyEntity GetEntity(int? keyValue)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            var entity = db.FindEntity<Ku_CompanyEntity>(t => t.Id == keyValue);

            //�鿴����+1
            if (entity.SeeTimes == null)
            {
                entity.SeeTimes = 1;
            }
            else
            {
                entity.SeeTimes = entity.SeeTimes + 1;
            }
            this.BaseRepository().Update(entity);

            //ͬһ���������ͬһ����˾�Ļ���ֻ�޸����ʱ�䣬��������µ������¼
            string userid = OperatorProvider.Provider.Current().UserId;
            var oldSeeEntity = db.FindEntity<Ku_CompanySeeEntity>(t => t.CompanyId == entity.Id && t.SeeUserId == userid);
            if (oldSeeEntity != null)
            {
                oldSeeEntity.SeeDate = DateTime.Now;
                db.Update(oldSeeEntity);
            }
            else
            {
                //����һ�������¼
                Ku_CompanySeeEntity seeEntity = new Ku_CompanySeeEntity
                {
                    CompanyId = entity.Id,
                    CompanyName = entity.CompanyName
                };
                seeEntity.Create();
                db.Insert(seeEntity);
            }
            db.Commit();

            return entity;
        }
        #endregion

        #region ��֤����
        /// <summary>
        /// �ͻ����Ʋ����ظ�
        /// </summary>
        /// <param name="CompanyName">����</param>
        /// <param name="keyValue">����</param>
        /// <returns></returns>
        public bool ExistFullName(string CompanyName, string keyValue)
        {
            var expression = LinqExtensions.True<Ku_CompanyEntity>();
            expression = expression.And(t => t.CompanyName == CompanyName);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.Id != int.Parse(keyValue));
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        #endregion

        #region �ύ����


        /// <summary>
        /// ��ȡ��˾������˽��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public void ObtainForm(int? keyValue)
        {
            if (keyValue != null)
            {
                IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
                var entity = db.FindEntity<Ku_CompanyEntity>(t => t.Id == keyValue);
                entity.ObtainUserId = OperatorProvider.Provider.Current().UserId;
                entity.ObtainUserName = OperatorProvider.Provider.Current().UserName;
                entity.ObtainDate = DateTime.Now;
                entity.ObtainState = 1;
                this.BaseRepository().Update(entity);
            }
        }

        /// <summary>
        /// ������ȡ
        /// </summary>
        /// <param name="dtSource">ʵ�����</param>
        /// <returns></returns>
        public string BatchObtain(DataTable dtSource)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                string resultStr = "";
                for (int i = 0; i < dtSource.Rows.Count; i++)
                {
                    string companyName = dtSource.Rows[i][0].ToString();
                    if (!string.IsNullOrEmpty(companyName))
                    {
                        var entity = db.FindEntity<Ku_CompanyEntity>(t => t.CompanyName == companyName);
                        if (entity == null)
                        {
                            resultStr += companyName + "��δ���ִ˹�˾\n";
                        }
                        else if (entity.ObtainState==1)
                        {
                            resultStr += companyName + "���ѱ���ȡ\n";
                        }
                        else
                        {
                            entity.ObtainUserId = OperatorProvider.Provider.Current().UserId;
                            entity.ObtainUserName = OperatorProvider.Provider.Current().UserName;
                            entity.ObtainDate = DateTime.Now;
                            entity.ObtainState = 1;
                            this.BaseRepository().Update(entity);
                        }
                    }
                }
                db.Commit();
                return "������ϣ�\n"+ resultStr;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        /// <summary>
        /// ����
        /// </summary>
        /// <param name="keyValue">�ͻ�ids</param>
        /// <param name="ObtainUserId">Ա��id</param>
        /// <param name="ObtainUserName">Ա������</param>
        public void SaveAssign(int? keyValue, string ObtainUserId, string ObtainUserName)
        {
            if (keyValue != null)
            {
                IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
                var entity = db.FindEntity<Ku_CompanyEntity>(t => t.Id == keyValue);
                entity.ObtainUserId = ObtainUserId;
                entity.ObtainUserName = ObtainUserName;
                entity.ObtainDate = DateTime.Now;
                entity.ObtainState = 1;
                entity.DeleteMark = 0;//����վ��ԭ��ĳ�δɾ��
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }

        }

        /// <summary>
        /// ���빫��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public void ThrowForm(int? keyValue)
        {
            if (keyValue != null)
            {
                IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
                var entity = db.FindEntity<Ku_CompanyEntity>(t => t.Id == keyValue);
                entity.ObtainUserId = "";
                entity.ObtainUserName = "";//�ÿջ�ȡ��
                entity.ObtainState = 0;//δ����
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
        }

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
        public void SaveForm(int? keyValue, Ku_CompanyEntity entity)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            if (keyValue != null)
            {
                Ku_CompanyEntity oldEntity = db.FindEntity<Ku_CompanyEntity>(t => t.Id == keyValue);
                //��ʵ��ַ����ע���ַ
                if (oldEntity.CompanyAddress != entity.RealAddress && !string.IsNullOrEmpty(entity.RealAddress) && oldEntity.RealAddress != entity.RealAddress)
                {
                    //���������ַ
                    Ku_LocationEntity locationData = locationService.AddressToLocation(entity.RealAddress);
                    entity = GetCompany(entity, locationData);

                    //��ȥԭ������
                    Ku_LocationEntity oldLoactionEntity = db.FindEntity<Ku_LocationEntity>(t => t.Id == oldEntity.LocationId);
                    oldLoactionEntity.Count = oldLoactionEntity.Count - 1;
                    db.Update<Ku_LocationEntity>(oldLoactionEntity);
                }
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                var company_Data = db.FindEntity<Ku_CompanyEntity>(t => t.CompanyName == entity.CompanyName);//��˾�ظ��ж�                                                                                           
                if (company_Data != null)
                {
                    throw new Exception(entity.CompanyName + " �ظ���");
                }
                //��ַΪ���ж�                                                                                           
                if (string.IsNullOrEmpty(entity.CompanyAddress))
                {
                    throw new Exception(entity.CompanyName + " ��ַΪ�գ�");
                }
                //�����������id��ֱ�Ӵӵ�ǰ������ӹ�˾���������������㣬����Ҫ�ӵ�ַת������
                if (entity.LocationId==null)
                {
                    //1.���ݵ�ַ���������Ϣ
                    Ku_LocationEntity locationData = locationService.AddressToLocation(entity.CompanyAddress);
                    entity = GetCompany(entity, locationData);
                }
                else
                {
                    //��������
                    Ku_LocationEntity locationData = db.FindEntity<Ku_LocationEntity>(t => t.Id == entity.LocationId);
                    locationData.Count = locationData.Count + 1;
                    db.Update(locationData);

                    entity = GetCompany(entity, locationData);

                }
                entity.Create();
                this.BaseRepository().Insert(entity);
            }
        }

        #endregion

        #region ��ȫ��˾����
        /// <summary>
        /// ��ȫ��˾�����е�1.¥�㷿�䣬2.���������Ϣ
        /// </summary>
        /// <param name="entity">��˾ʵ��</param>
        /// <param name="locationData">�����ʵ��</param>
        /// <returns></returns>
        public Ku_CompanyEntity GetCompany(Ku_CompanyEntity entity, Ku_LocationEntity locationData)
        {
            //ͨ����ַ��ȡ¥��ͷ���
            if (entity.Floor ==0 && entity.Room ==0 && !string.IsNullOrEmpty(entity.RealAddress))
            {
                string building;
                int floor, room;
                getFloorRoom(entity.RealAddress, out building, out floor, out room);
                entity.Building = building;
                entity.Floor = floor;
                entity.Room = room;
            }

            //��ӿͻ���Ϣ��,�������λ��id����Ȧ���ƣ����ظ���˾
            entity.Province = locationData.Province;
            entity.City = locationData.City;
            entity.District = locationData.District;
            entity.LocationId = locationData.Id;
            entity.RegeoId = locationData.RegeoId;
            entity.RegeoName = locationData.RegeoName;
            return entity;
        }
        #endregion


        #region ͨ����ַƥ��¥�㷿���
        /// <summary>
        /// ͨ����ַƥ��¥�㷿���
        /// </summary>
        /// <param name="address">��ַ</param>
        /// <param name="building">¥��</param>
        /// <param name="floor">¥��</param>
        /// <param name="room">����</param>
        public void getFloorRoom(string address, out string building, out int floor, out int room)
        {
            //ͨ����ַƥ���ַ�д��ڵ�¥�㣬����
            building = "";
            floor = 0;
            room = 0;
            Regex r = new Regex(@"\d*��¥"); // ����һ��Regex����ʵ��
            address = address.Replace('��', '1').Replace('��', '1').Replace('��', '1').Replace('��', '1').Replace('��', '1').Replace('��', '1').Replace('��', '1').Replace('��', '1').Replace('��', '1');
            Match m = r.Match(address); // ���ַ�����ƥ��
            if (m.Success)
            {
                building = m.Value.Replace("��¥", "");
            }

            Regex rr = new Regex(@"\d*��$|\d*$"); // ����һ��Regex����ʵ��
            Match mm = rr.Match(address); // ���ַ�����ƥ��
            if (mm.Success)
            {
                string fr = mm.Value.Replace("��", "").Replace("��", "");
                if (fr.Length == 3)
                {
                    floor = Convert.ToInt32(fr.Substring(0, 1));
                    room = Convert.ToInt32(fr.Substring(1, 2));
                }
                else if (fr.Length == 4)
                {
                    floor = Convert.ToInt32(fr.Substring(0, 2));
                    room = Convert.ToInt32(fr.Substring(2, 2));
                }
                else if (fr.Length == 5)
                {
                    floor = Convert.ToInt32(fr.Substring(0, 2));
                    room = Convert.ToInt32(fr.Substring(2, 3));
                }
            }

        }
        #endregion


    }
}
