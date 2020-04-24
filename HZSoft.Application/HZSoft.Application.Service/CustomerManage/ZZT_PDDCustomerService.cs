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
    /// 日 期：2018-05-10 09:15
    /// 描 述：拼多多客户
    /// </summary>
    public class ZZT_PDDCustomerService : RepositoryFactory<ZZT_PDDCustomerEntity>, ZZT_PDDCustomerIService
    {
        private ICodeRuleService coderuleService = new CodeRuleService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<ZZT_PDDCustomerEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<ZZT_PDDCustomerEntity>();
            var queryParam = queryJson.ToJObject();

            string strSql = "select * from ZZT_PDDCustomer where 1=1 ";

            //订单
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and OrderTime BETWEEN '" + startTime + "' and '" + endTime + "'";
            }
            //客户编号
            if (!queryParam["CustNo"].IsEmpty())
            {
                string CustNo = queryParam["CustNo"].ToString();
                strSql += " and CustNo like '%" + CustNo + "%'";
            }
            //手机号
            if (!queryParam["Mobile"].IsEmpty())
            {
                string Mobile = queryParam["Mobile"].ToString();
                strSql += " and Mobile like '%" + Mobile + "%'";
            }
            //姓名
            if (!queryParam["FullName"].IsEmpty())
            {
                string FullName = queryParam["FullName"].ToString();
                strSql += " and FullName like '%" + FullName + "%'";
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
        public IEnumerable<ZZT_PDDCustomerEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ZZT_PDDCustomerEntity GetEntity(string keyValue)
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
            var expression = LinqExtensions.True<ZZT_PDDCustomerEntity>();
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
                    ZZT_PDDCustomerEntity entity = this.BaseRepository().FindEntity(custIds[i]);
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
        public void SaveForm(string keyValue, ZZT_PDDCustomerEntity entity)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            if (!string.IsNullOrEmpty(keyValue))
            {
                var old_Data = db.FindEntity<ZZT_PDDCustomerEntity>(t => t.Id == keyValue);
                //修改备注后，添加新的跟进记录
                if (old_Data.Description != entity.Description && entity.Description != "&nbsp;")
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
                    ZZT_PDDCustomerEntity entity = this.BaseRepository().FindEntity(custIds[i]);
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
                    ZZT_PDDCustomerEntity entity = this.BaseRepository().FindEntity(custIds[i]);
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
                    ZZT_PDDCustomerEntity entity = this.BaseRepository().FindEntity(custIds[i]);
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

        #region PDD导入
        /// <summary>
        /// 批量（新增）
        /// </summary>
        /// <param name="dtSource">实体对象</param>
        /// <returns></returns>
        public string BatchAddPDD(DataTable dtSource)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                for (int i = 0; i < dtSource.Rows.Count; i++)
                {
                    string mobile = dtSource.Rows[i][4].ToString();
                    string telphone = dtSource.Rows[i][5].ToString();

                    if (!string.IsNullOrWhiteSpace(mobile))
                    {
                        string CustNo = "";
                        DateTime? NullTime = null;
                        string orderTimeStr = dtSource.Rows[i][16].ToString();
                        string payTimeStr = dtSource.Rows[i][17].ToString();
                        string deliveryTimeStr = dtSource.Rows[i][18].ToString();
                        string printingTimeStr = dtSource.Rows[i][19].ToString();
                        DateTime? orderTime=string.IsNullOrEmpty(orderTimeStr)? NullTime : DateTime.ParseExact(orderTimeStr, "yyyy/M/d H:m:s", System.Globalization.CultureInfo.CurrentCulture);
                        DateTime? payTime = string.IsNullOrEmpty(payTimeStr) ? NullTime : DateTime.ParseExact(payTimeStr, "yyyy/M/d H:m:s", System.Globalization.CultureInfo.CurrentCulture);
                        DateTime? deliveryTime = string.IsNullOrEmpty(deliveryTimeStr) ? NullTime : DateTime.ParseExact(deliveryTimeStr, "yyyy/M/d H:m:s", System.Globalization.CultureInfo.CurrentCulture);
                        DateTime? printingTime = string.IsNullOrEmpty(printingTimeStr) ? NullTime : DateTime.ParseExact(printingTimeStr, "yyyy/M/d H:m:s", System.Globalization.CultureInfo.CurrentCulture);

                        var old_Data = db.FindEntity<ZZT_PDDCustomerEntity>(t => t.Mobile == mobile);
                        //是否此前购买过
                        if (old_Data != null)
                        {
                            CustNo = old_Data.CustNo;
                            old_Data.OrderNo = dtSource.Rows[i][1].ToString();
                            old_Data.OrderTime = orderTime;
                            old_Data.Province = dtSource.Rows[i][6].ToString();
                            old_Data.City = dtSource.Rows[i][7].ToString();
                            old_Data.County = dtSource.Rows[i][8].ToString();
                            old_Data.Address = dtSource.Rows[i][9].ToString();
                            old_Data.GoodsName = dtSource.Rows[i][15].ToString();
                            old_Data.Modify(old_Data.Id);
                            db.Update(old_Data);
                        }
                        else
                        {
                            //新建PDD客户表
                            CustNo = coderuleService.SetBillCode(OperatorProvider.Provider.Current().UserId, SystemInfo.CurrentModuleId, "", db);//获得指定模块或者编号的单据号
                            ZZT_PDDCustomerEntity entity = new ZZT_PDDCustomerEntity()
                            {
                                CustNo = CustNo,
                                OrderNo = dtSource.Rows[i][1].ToString(),
                                OrderTime = orderTime,
                                Mobile = mobile,
                                Telphone = telphone,
                                Province = dtSource.Rows[i][6].ToString(),
                                City = dtSource.Rows[i][7].ToString(),
                                County = dtSource.Rows[i][8].ToString(),
                                Address = dtSource.Rows[i][9].ToString(),
                                GoodsName = dtSource.Rows[i][15].ToString(),
                            };
                            entity.Create();
                            db.Insert(entity);
                        }

                        //插入PDD日志
                        string parceNo = ChangeDataToD(dtSource.Rows[i][0].ToString()).ToString();
                        ZZT_PDDLogEntity logEntity = new ZZT_PDDLogEntity
                        {
                            CustNo = CustNo,
                            ParcelNo = parceNo,
                            OrderNo = dtSource.Rows[i][1].ToString(),
                            NickName = dtSource.Rows[i][2].ToString(),
                            FullName = dtSource.Rows[i][3].ToString(),
                            Mobile = mobile,
                            Telphone = telphone,
                            Province = dtSource.Rows[i][6].ToString(),
                            City = dtSource.Rows[i][7].ToString(),
                            County = dtSource.Rows[i][8].ToString(),
                            Address = dtSource.Rows[i][9].ToString(),
                            ZipCode = dtSource.Rows[i][10].ToString(),
                            Express = dtSource.Rows[i][11].ToString(),
                            ExpressNo = dtSource.Rows[i][12].ToString(),
                            Weight = dtSource.Rows[i][13].ToString(),
                            Freight = dtSource.Rows[i][14].ToString(),
                            GoodsName = dtSource.Rows[i][15].ToString(),
                            OrderTime = orderTime,
                            PayTime = payTime,
                            DeliveryTime = deliveryTime,
                            PrintingTime = printingTime,
                            BuyerMessage = dtSource.Rows[i][20].ToString(),
                            SellerMessage = dtSource.Rows[i][21].ToString(),
                            MakeNote = dtSource.Rows[i][22].ToString(),
                            MergeOrder = dtSource.Rows[i][23].ToString(),
                            SendPerson = dtSource.Rows[i][24].ToString(),
                            SendTelphone = dtSource.Rows[i][25].ToString(),
                            SendProvince = dtSource.Rows[i][26].ToString(),
                            SendCity = dtSource.Rows[i][27].ToString(),
                            SendCounty = dtSource.Rows[i][28].ToString(),
                            SendAddress = dtSource.Rows[i][29].ToString(),
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

        private Decimal ChangeDataToD(string strData)
        {
            Decimal dData = 0.0M;
            if (strData.Contains("E"))
            {
                dData = Decimal.Parse(strData.ToString(), System.Globalization.NumberStyles.Float);
            }
            return dData;
        }
        #endregion
    }
}
