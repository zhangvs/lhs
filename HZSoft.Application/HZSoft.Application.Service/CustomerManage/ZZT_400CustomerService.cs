using HZSoft.Application.Code;
using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.IService.CustomerManage;
using HZSoft.Application.IService.SystemManage;
using HZSoft.Application.Service.SystemManage;
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
    /// 日 期：2018-05-04 13:50
    /// 描 述：400客户
    /// </summary>
    public class ZZT_400CustomerService : RepositoryFactory<ZZT_400CustomerEntity>, ZZT_400CustomerIService
    {
        private ICodeRuleService coderuleService = new CodeRuleService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<ZZT_400CustomerEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<ZZT_400CustomerEntity>();
            var queryParam = queryJson.ToJObject();

            string strSql = "select * from ZZT_400Customer where 1=1 ";          

            //来电日期
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and CallInTime BETWEEN '" + startTime + "' and '" + endTime + "'";
            }
            //客户编号
            if (!queryParam["CustNo"].IsEmpty())
            {
                string CustNo = queryParam["CustNo"].ToString();
                strSql += " and CustNo like '%" + CustNo + "%'";
            }
            //姓名
            if (!queryParam["FullName"].IsEmpty())
            {
                string FullName = queryParam["FullName"].ToString();
                strSql += " and FullName like '%" + FullName + "%'";
            }
            //手机号
            if (!queryParam["Mobile"].IsEmpty())
            {
                string Mobile = queryParam["Mobile"].ToString();
                strSql += " and Mobile like '%" + Mobile + "%'";
            }

            //回电状态
            if (!queryParam["CallState"].IsEmpty())
            {
                int CallState = queryParam["CallState"].ToInt();
                strSql += " and CallState = " + CallState;
            }
            //分配状态
            if (!queryParam["AssignMark"].IsEmpty())
            {
                int AssignMark = queryParam["AssignMark"].ToInt();
                strSql += " and AssignMark = " + AssignMark;
            }
            //意向客户
            if (!queryParam["IntentionMark"].IsEmpty())
            {
                int IntentionMark = queryParam["IntentionMark"].ToInt();
                strSql += " and IntentionMark = " + IntentionMark;
            }
            //售出状态
            if (!queryParam["SellMark"].IsEmpty())
            {
                int SellMark = queryParam["SellMark"].ToInt();
                strSql += " and SellMark = " + SellMark;
            }

            //垃圾站
            if (!queryParam["DeleteMark"].IsEmpty())
            {
                strSql += " and DeleteMark = 1";//垃圾站无需判断公海可用标识，也无需判断权限
            }
            else
            {
                strSql += " and DeleteMark = 0";//不为垃圾站，判断是否是公海

                //公海
                if (!queryParam["EnabledMark"].IsEmpty())
                {
                    strSql += " and EnabledMark = 0";//不为垃圾站，为公海,也无需判断权限
                }
                else
                {
                    strSql += " and EnabledMark = 1";//不为垃圾站，不公海，判断权限
                    //分配人
                    if (!queryParam["TraceUserId"].IsEmpty())
                    {
                        string TraceUserId = queryParam["TraceUserId"].ToString();
                        strSql += " and TraceUserId = '" + TraceUserId + "'";
                    }
                    else
                    {
                        if (!OperatorProvider.Provider.Current().IsSystem && OperatorProvider.Provider.Current().Account != "admin")
                        {
                            string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                            strSql += " and TraceUserId in (" + dataAutor + ")";
                        }
                    }
                }
            }
            return this.BaseRepository().FindList(strSql.ToString(), pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<ZZT_400CustomerEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ZZT_400CustomerEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 账户不能重复
        /// </summary>
        /// <param name="Mobile">账户值</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistMobile(string Mobile, string keyValue)
        {
            var expression = LinqExtensions.True<ZZT_400CustomerEntity>();
            expression = expression.And(t => t.Mobile == Mobile);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.Id != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValues">主键</param>
        public void RemoveForm(string keyValues)
        {
            //this.BaseRepository().Delete(keyValues);
            if (!string.IsNullOrEmpty(keyValues))
            {
                string[] custIds = keyValues.Split(',');
                for (int i = 0; i < custIds.Length; i++)
                {
                    ZZT_400CustomerEntity entity = this.BaseRepository().FindEntity(custIds[i]);
                    entity.Modify(custIds[i]);
                    entity.DeleteMark = 1;//软删除
                    this.BaseRepository().Update(entity);
                }
            }
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, ZZT_400CustomerEntity entity)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            if (!string.IsNullOrEmpty(keyValue))
            {
                var old_Data = db.FindEntity<ZZT_400CustomerEntity>(t => t.Id == keyValue);
                //修改备注后，添加新的跟进记录
                if (old_Data.Description!=entity.Description && entity.Description != "&nbsp;")
                {
                    //插入跟进记录
                    TrailRecordEntity trailRecordEntity = new TrailRecordEntity
                    {
                        ObjectSort = 4,
                        ObjectId = keyValue,
                        TrackContent = entity.Description
                    };
                    trailRecordEntity.Create();
                    db.Insert(trailRecordEntity);
                }

                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
                db.Commit();
            }
            else
            {   
                //手动新增
                entity.Create();
                entity.AssignMark = 1;
                entity.TraceUserId = OperatorProvider.Provider.Current().UserId;//分配人为自己
                entity.TraceUserName = OperatorProvider.Provider.Current().UserName;
                entity.CustNo = coderuleService.SetBillCode(entity.CreateUserId, SystemInfo.CurrentModuleId, "", db);//获得指定模块或者编号的单据号
                db.Insert(entity);


                //插入跟进记录
                TrailRecordEntity trailRecordEntity = new TrailRecordEntity
                {
                    ObjectSort = 4,
                    ObjectId = entity.Id,
                    TrackContent = entity.Description
                };
                trailRecordEntity.Create();
                db.Insert(trailRecordEntity);
                db.Commit();
            }

        }
        /// <summary>
        /// 分配
        /// </summary>
        /// <param name="keyValues">客户ids</param>
        /// <param name="TraceUserId">员工id</param>
        /// <param name="TraceUserName">员工名称</param>
        public void SaveAssign(string keyValues, string TraceUserId, string TraceUserName)
        {
            if (!string.IsNullOrEmpty(keyValues))
            {
                string[] custIds = keyValues.Split(',');
                for (int i = 0; i < custIds.Length; i++)
                {
                    ZZT_400CustomerEntity entity= this.BaseRepository().FindEntity(custIds[i]);
                    entity.Modify(custIds[i]);
                    entity.AssignMark = 1;
                    entity.TraceUserId = TraceUserId;
                    entity.TraceUserName = TraceUserName;
                    entity.EnabledMark = 1;//可用，从公海变成可用客户
                    entity.DeleteMark = 0;//垃圾站还原需改成未删除
                    this.BaseRepository().Update(entity);
                }
            }
        }
        /// <summary>
        /// 放入公海
        /// </summary>
        /// <param name="keyValues">客户ids</param>
        public void SaveThrow(string keyValues)
        {
            if (!string.IsNullOrEmpty(keyValues))
            {
                string[] custIds = keyValues.Split(',');
                for (int i = 0; i < custIds.Length; i++)
                {
                    ZZT_400CustomerEntity entity = this.BaseRepository().FindEntity(custIds[i]);
                    entity.Modify(custIds[i]);
                    entity.AssignMark = 0;//不分配
                    //entity.TraceUserId = "";
                    //entity.TraceUserName = "";
                    entity.EnabledMark = 0;//不可用，公海根据此查询
                    entity.DeleteMark = 0;//垃圾站还原需改成未删除
                    this.BaseRepository().Update(entity);
                }
            }
        }

        /// <summary>
        /// 领取
        /// </summary>
        /// <param name="keyValues">客户ids</param>
        public void SaveObtain(string keyValues)
        {
            if (!string.IsNullOrEmpty(keyValues))
            {
                string[] custIds = keyValues.Split(',');
                for (int i = 0; i < custIds.Length; i++)
                {
                    ZZT_400CustomerEntity entity = this.BaseRepository().FindEntity(custIds[i]);
                    entity.Modify(custIds[i]);
                    entity.AssignMark = 1;//不分配
                    entity.TraceUserId = OperatorProvider.Provider.Current().UserId;
                    entity.TraceUserName = OperatorProvider.Provider.Current().UserName;
                    entity.EnabledMark = 1;//不可用，公海根据此查询
                    this.BaseRepository().Update(entity);
                }
            }
        }
        #endregion

        #region 400导入
        /// <summary>
        /// 批量（新增）
        /// </summary>
        /// <param name="dtSource">实体对象</param>
        /// <returns></returns>
        public string BatchAdd400(DataTable dtSource)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                for (int i = 0; i < dtSource.Rows.Count; i++)
                {                    
                    string mobile = dtSource.Rows[i][0].ToString();
                    if (!string.IsNullOrWhiteSpace(mobile))
                    {
                        string CustNo = "";
                        DateTime? NullTime = null;
                        string callInTimeStr = dtSource.Rows[i][5].ToString();
                        DateTime? callInTime = string.IsNullOrEmpty(callInTimeStr) ? NullTime : DateTime.ParseExact(callInTimeStr, "yyyy/M/d H:m:s", System.Globalization.CultureInfo.CurrentCulture);


                        string callStateStr = dtSource.Rows[i][4].ToString();
                        int callState = 0;
                        if (callStateStr == "已接来电")
                        {
                            callState = 1;
                        }

                        var old_Data = db.FindEntity<ZZT_400CustomerEntity>(t => t.Mobile == mobile);
                        //是否此前拨打过
                        if (old_Data != null)
                        {
                            CustNo = old_Data.CustNo;
                            //修改当前数据的列来电时间，来电状态                            
                            old_Data.CallInTime = callInTime;
                            old_Data.CallState = callState; 
                            old_Data.Modify(old_Data.Id);
                            db.Update(old_Data);                          
                        }
                        else
                        {
                            //新建400客户表
                            CustNo = coderuleService.SetBillCode(OperatorProvider.Provider.Current().UserId, SystemInfo.CurrentModuleId, "", db);//获得指定模块或者编号的单据号
                            ZZT_400CustomerEntity entity = new ZZT_400CustomerEntity()
                            {
                                CustNo = CustNo,
                                Mobile = mobile,
                                Province= dtSource.Rows[i][1].ToString(),
                                City= dtSource.Rows[i][2].ToString(),
                                CallInNumber = dtSource.Rows[i][3].ToString(),
                                CallState = callState,
                                CallInTime = callInTime,
                            };                            
                            entity.Create();

                            db.Insert(entity);
                        }

                        //插入400日志
                        ZZT_400LogEntity logEntity = new ZZT_400LogEntity
                        {
                            CustNo = CustNo,
                            Mobile = mobile,
                            Province = dtSource.Rows[i][1].ToString(),
                            City = dtSource.Rows[i][2].ToString(),
                            CallInNumber = dtSource.Rows[i][3].ToString(),
                            CallState = dtSource.Rows[i][4].ToString(),
                            CallInTime = callInTime,
                        };
                        logEntity.Create();
                        db.Insert(logEntity);


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
        #endregion
    }
}
