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
using System.Data.Common;
using System.Linq;

namespace HZSoft.Application.Service.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创 建：超级管理员
    /// 日 期：2017-09-16 16:41
    /// 描 述：号码库
    /// </summary>
    public class TelphoneSourceService : RepositoryFactory<TelphoneSourceEntity>, TelphoneSourceIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<TelphoneSourceEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<TelphoneSourceEntity>();
            var queryParam = queryJson.ToJObject();
            string strSql = "select * from TelphoneSource where DeleteMark <> 1 and EnabledMark <> 1";
            //单据日期
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                //expression = expression.And(t => t.ModifyDate >= startTime && t.ModifyDate <= endTime);
                strSql += " and ModifyDate BETWEEN '"+ startTime + "' and '"+ endTime+"'";
            }
            //单据编号
            if (!queryParam["Telphone"].IsEmpty())
            {
                string Telphone = queryParam["Telphone"].ToString();
                //expression = expression.And(t => t.Telphone.Contains(Telphone));
                strSql += " and Telphone = '"+Telphone+"'";
            }
            //分配人
            if (!queryParam["SellerId"].IsEmpty())
            {
                string SellerId = queryParam["SellerId"].ToString();
                //expression = expression.And(t => t.SellerId.Contains(SellerId));
                strSql += " and SellerId = '" + SellerId+"'";
            }

            //售出状态
            if (!queryParam["SellMark"].IsEmpty())
            {
                int SellMark = queryParam["SellMark"].ToInt();
                //expression = expression.And(t => t.SellMark==SellMark);
                strSql += " and SellMark = " + SellMark;
            }
            if (!OperatorProvider.Provider.Current().IsSystem)
            {
                string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                strSql += " and OrganizeId IN( select OrganizeId from Base_User where 1=1";
                strSql += " and UserId in (" + dataAutor + ") GROUP BY OrganizeId )";
            }
            return this.BaseRepository().FindList(strSql.ToString(), pagination);
            //return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>返回列表</returns>
        public IEnumerable<TelphoneSourceEntity> GetList0539()
        {
            string strSql = "SELECT Telphone FROM TelphoneSource WHERE SellMark<>1 AND DeleteMark<>1 and EnabledMark <> 1 AND OrganizeId='207fa1a9-160c-4943-a89b-8fa4db0547ce' AND Number=0539 ";
            return this.BaseRepository().FindList(strSql.ToString());
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="telphone">自动匹配的手机号</param>
        /// <returns>返回列表</returns>
        public IEnumerable<TelphoneSourceEntity> GetList(string telphone)
        {
            string strSql = "SELECT TOP(10) Telphone FROM TelphoneSource WHERE SellMark<>1 AND DeleteMark<>1 and EnabledMark <> 1 and Telphone like '%" + telphone + "%' AND OrganizeId='207fa1a9-160c-4943-a89b-8fa4db0547ce' ";
            return this.BaseRepository().FindList(strSql.ToString());
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public TelphoneSourceEntity GetEntity(int? keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public TelphoneSourceEntity GetEntity(string telphone)
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
        public void SaveForm(int? keyValue, TelphoneSourceEntity entity)
        {
            if (keyValue!=null)
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
        /// 获取号码
        /// </summary>
        /// <returns></returns>
        public int GetTelphone()
        {
            //1.先查看该员工今天是否已经获取过(是否大于10)
            var expression = LinqExtensions.True<TelphoneSourceEntity>();
            string userid = OperatorProvider.Provider.Current().UserId;
            string username= OperatorProvider.Provider.Current().UserName;
            string strSql = "SELECT COUNT(*) FROM TelphoneSource WHERE 1=1 AND SellerId ='"+ userid + "' AND   datediff(day,[ModifyDate],getdate())=0";
            DataTable dt= this.BaseRepository().FindTable(strSql.ToString());
            int ges = int.Parse(dt.Rows[0][0].ToString());
            if (ges<10)
            {
                //2.没获取过，随机获取10个号码分配给当前员工
                //获取一遍数据库中没有分配的号码，
                string strSql1 = "SELECT * FROM TelphoneSource WHERE 1=1 AND SellerId IS NULL ";
                DataTable dts = this.BaseRepository().FindTable(strSql1.ToString());
                Random rd = new Random();
                List<int> gint=new List<int>();
                if (dts.Rows.Count<20)
                {
                    for (int i = 0; i < 20; i++)
                    {
                        //随机获取一个数
                        int dd = rd.Next(dts.Rows.Count);
                        //判断当前行是否被用过
                        if (gint.Contains(dd))
                        {
                            i--;
                            continue;
                        }
                        else
                        {
                            //不重复，修改当前实体，进行分配
                            gint.Add(dd);
                            DataRow dtr = dts.Rows[dd];
                            int keyValue = int.Parse(dtr["TelphoneID"].ToString());
                            TelphoneSourceEntity oldEntity = this.BaseRepository().FindEntity(keyValue);
                            oldEntity.SellerId = userid;
                            oldEntity.SellerName = username;
                            oldEntity.SellMark = 1;
                            oldEntity.Modify(keyValue);
                            this.BaseRepository().Update(oldEntity);
                        }

                    }
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            else
            {
                return 2;
            }
        }
        #endregion
    }
}
