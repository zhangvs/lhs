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
    /// 版 本 6.1
    /// 
    /// 创 建：超级管理员
    /// 日 期：2017-11-04 16:22
    /// 描 述：话单
    /// </summary>
    public class CRM_CallLogService : RepositoryFactory<CRM_CallLogEntity>, CRM_CallLogIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<CRM_CallLogEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<CRM_CallLogEntity>();
            var queryParam = queryJson.ToJObject();
            string strSql = @" SELECT * FROM CRM_CallLog where 1=1 ";

            //呼叫日期
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and CallTime BETWEEN '" + startTime + "' and '" + endTime + "'";
            }

            //呼出号码
            if (!queryParam["CallNumber"].IsEmpty())
            {
                string CallNumber = queryParam["CallNumber"].ToString();
                strSql += " and CallNumber = '" + CallNumber + "'";
            }

            //员工
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
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<CRM_CallLogEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public CRM_CallLogEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
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
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm( CRM_CallLogEntity entity)
        {
            //WriteInLog log = new WriteInLog();

            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();

            //log.writeInLog("进行插入话单表");
            entity.Create();
            UserEntity user= db.FindEntity<UserEntity>(t => t.RealName == entity.WorkerName);
            if (user !=null)
            {
                entity.CreateUserId = user.UserId;
                entity.CreateUserName = user.RealName;
                //log.writeInLog("修改用户id");
            }
            db.Insert(entity);
            //log.writeInLog("插入话单表成功");

            var _400_Data = db.FindEntity<ZZT_400CustomerEntity>(t => t.Mobile == entity.CallNumber);
            if (_400_Data != null)
            {
                _400_Data.CallOutTime = entity.CallTime;
                _400_Data.CallState = 1;
                db.Update(_400_Data);
                //log.writeInLog("插入改400客户回电状态时间成功");
            }
            else
            {
                var pdd_Data = db.FindEntity<ZZT_PDDCustomerEntity>(t => t.Mobile == entity.CallNumber);
                if (pdd_Data != null)
                {
                    pdd_Data.CallOutTime = entity.CallTime;
                    db.Update(pdd_Data);
                    //log.writeInLog("插入改拼多多客户回电时间成功");
                }
            }
            db.Commit();
            //log.writeInLog("提交数据库");
        }
        #endregion
    }
}
