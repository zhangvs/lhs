using HZSoft.Application.Code;
using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.IService.CustomerManage;
using HZSoft.Data.Repository;
using HZSoft.Util.Extension;
using HZSoft.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace HZSoft.Application.Service.CustomerManage
{
    /// <summary>
    /// �� �� 6.1
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2018-04-24 10:20
    /// �� ������˾���¸���
    /// </summary>
    public class Ku_CompanyCountService : RepositoryFactory<Ku_CompanyCountEntity>, Ku_CompanyCountIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<Ku_CompanyCountEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return this.BaseRepository().FindList(pagination);
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<Ku_CompanyCountEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }

        /// <summary>
        /// ���ܸ�������
        /// </summary>
        /// <returns></returns>
        public Ku_CompanyCountEntity GetThisWeekCountData(string StartDate, string EndDate)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            //�������أ������
            string des = OperatorProvider.Provider.Current().Description;
            Ku_CompanyCountEntity thisWeekData=null;
            if (!string.IsNullOrEmpty(des))
            {
                thisWeekData = db.FindEntity<Ku_CompanyCountEntity>(t => t.WeekStartDate == StartDate && t.WeekEndDate == EndDate && t.AreaRange == des);
            }
            else
            {
                thisWeekData = db.FindEntity<Ku_CompanyCountEntity>(t => t.WeekStartDate == StartDate && t.WeekEndDate == EndDate && (t.AreaRange == null || t.AreaRange == ""));
            }

            if (thisWeekData != null)
            {
                return thisWeekData;
            }
            else
            {
                string locationSql = "";
                if (!string.IsNullOrEmpty(des))
                {
                    Regex r = new Regex(@"[\u4e00-\u9fa5]+");//��������
                    if (!r.IsMatch(des))
                    {
                        //�뾶Ȧ
                        if (des.IndexOf('|') > 0)
                        {
                            string[] locations = des.Split('|');
                            for (int i = 0; i < locations.Length; i++)
                            {
                                locationSql += "SELECT * FROM Ku_Location where dbo.f_GetDistance(" + locations[i] + @",bdlon,bdlat)<=1000 UNION ";//���������|�ָ�
                            }
                            locationSql = "(" + locationSql.Substring(0, locationSql.Length - 6) + ")";
                        }
                        else
                        {
                            locationSql = "(SELECT * FROM Ku_Location where dbo.f_GetDistance(" + des + @",bdlon,bdlat)<=1000) ";//�����û��SellerId����������
                        }
                    }
                    else
                    {
                        //����
                        if (des.IndexOf('|') > 0)
                        {
                            des = des.Replace("|", "','");
                            locationSql = "(SELECT * FROM Ku_Location where district in (" + des + "))";//���������|�ָ�
                        }
                        else
                        {
                            locationSql = "(SELECT * FROM Ku_Location where district ='" + des + "')";//�����û��SellerId����������
                        }
                    }
                }
                else
                {
                    locationSql = "Ku_Location";
                }
                DateTime endTime = EndDate.ToDate().AddDays(1);
                //�������أ������
                string searchSql = @"SELECT count(*) FROM " + locationSql + @" l LEFT JOIN Ku_Company c ON l.Id=c.LocationId  
WHERE ManageState=1 AND LocationId>0 AND c.District!='' and BuildTime >= '" + StartDate + @"' and BuildTime<  '" + endTime + @"'
UNION ALL
SELECT count(*) FROM " + locationSql + @" l LEFT JOIN Ku_Company c ON l.Id=c.LocationId 
WHERE ManageState=1 AND LocationId>0 AND c.District!='' and BuildTime >= '" + StartDate.Substring(0, 7) + @"-01' and BuildTime<  '" + endTime + @"'
UNION ALL
SELECT count(*) FROM " + locationSql + @" l LEFT JOIN Ku_Company c ON l.Id=c.LocationId 
WHERE ManageState=1 AND LocationId>0 AND c.District!='' and BuildTime >= '" + StartDate.Substring(0, 4) + @"-01-01' and BuildTime<  '" + endTime + @"'
UNION ALL
SELECT count(*) FROM " + locationSql + @" l LEFT JOIN Ku_Company c ON l.Id=c.LocationId 
WHERE ManageState=1 AND LocationId>0 AND c.District!=''";

                var dtCount = BaseRepository().FindTable(searchSql.ToString());
                Ku_CompanyCountEntity entity = new Ku_CompanyCountEntity()
                {
                    AreaRange = des,
                    WeekStartDate = StartDate,
                    WeekEndDate = EndDate,
                    WeekCount = dtCount.Rows[0][0].ToString(),
                    MonthCount = dtCount.Rows[1][0].ToString(),
                    YearCount = dtCount.Rows[2][0].ToString(),
                    AllCount = dtCount.Rows[3][0].ToString()
                };
                entity.Create();
                this.BaseRepository().Insert(entity);
                return entity;
            }
        }

        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public Ku_CompanyCountEntity GetEntity(int? keyValue)
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
        public void SaveForm(string keyValue, Ku_CompanyCountEntity entity)
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
