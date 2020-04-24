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
    /// 版 本 6.1
    /// 
    /// 创 建：超级管理员
    /// 日 期：2017-10-23 14:11
    /// 描 述：靓号库
    /// </summary>
    public class TelphoneLiangService : RepositoryFactory<TelphoneLiangEntity>, TelphoneLiangIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<TelphoneLiangEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            string strSql = "select * from TelphoneLiang where DeleteMark <> 1 and EnabledMark <> 1";
            //单据编号
            if (!queryParam["Telphone"].IsEmpty())
            {
                string Telphone = queryParam["Telphone"].ToString();
                strSql += " and Telphone = '" + Telphone + "'";
            }
            //机构
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
            //分类
            if (!queryParam["Grade"].IsEmpty())
            {
                string Grade = queryParam["Grade"].ToString();
                strSql += " and Grade = '" + Grade + "'";
            }

            return this.BaseRepository().FindList(strSql.ToString(), pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
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
        /// 获取列表
        /// </summary>
        /// <param name="organizeId">靓号公司</param>
        /// <param name="Grade">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<TelphoneLiangEntity> GetGrade(string organizeId, string Grade)
        {
            string strSql = "select * from TelphoneLiang where DeleteMark <> 1 and EnabledMark <> 1 and SellMark <> 1";
            //单据编号
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
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public TelphoneLiangEntity GetEntity(int? keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="telphone">手机号</param>
        /// <param name="organizeId">靓号机构</param>
        /// <returns></returns>
        public TelphoneLiangEntity GetEntityByTelphone(string telphone)
        {
            return this.BaseRepository().FindEntity(t => t.Telphone == telphone);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(int? keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
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
        /// 批量（新增）
        /// </summary>
        /// <param name="dtSource">实体对象</param>
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

                        //类别
                        string itemName = dtSource.Rows[i][2].ToString();
                        string itemNCode = "";
                        var DataItemDetail = db.FindEntity<DataItemDetailEntity>(t => t.ItemName == itemName);
                        if (DataItemDetail != null)
                        {
                            itemNCode = DataItemDetail.ItemValue;
                        }
                        else
                        {
                            itemNCode = "8";//其它
                        }

                        //跟进前7位确定城市和运营商
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
                            return "号段不存在" + Number7;
                        }
                        //套餐
                        string Package = dtSource.Rows[i][3].ToString();

                        //所属公司
                        string organize = dtSource.Rows[i][4].ToString();
                        string organizeId = "";
                        var organizeData = db.FindEntity<OrganizeEntity>(t => t.FullName == organize);
                        if (organizeData != null)
                        {
                            organizeId = organizeData.OrganizeId;
                        }
                        else
                        {
                            return "机构不存在" + organize;
                        }

                        //添加靓号
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
                return "导入成功";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
