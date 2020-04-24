using HZSoft.Application.Entity.ReportManage;
using HZSoft.Application.IService.ReportManage;
using HZSoft.Data;
using HZSoft.Data.Repository;
using HZSoft.Util.Extension;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using HZSoft.Util;
using HZSoft.Application.Entity.AuthorizeManage;
using HZSoft.Application.Code;

namespace HZSoft.Application.Service.ReportManage
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创建人：佘赐雄
    /// 日 期：2016.1.14 14:27
    /// 描 述：报表模板管理
    /// </summary>
    public class DmsService : RepositoryFactory, IDmsService
    {
        #region 获取数据
        /// <summary>
        /// 员工报表
        /// </summary>
        /// <param name="queryJson">起始，结束日期json</param>
        /// <returns></returns>
        public DataTable GetDateOrder_emp(string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            string strSql = "SELECT SellerName AS name, SUM(Amount) AS emp_amount, count(1) AS emp_count FROM TelphoneOrder where 1=1";
            if (!OperatorProvider.Provider.Current().IsSystem)
            {
                string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                strSql += " and SellerId in (" + dataAutor + ")";
            }
            //单据日期
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and CreateDate BETWEEN '" + startTime + "' and '" + endTime + "'";
            }
            strSql += " group by SellerName order by emp_amount desc";

            return this.BaseRepository().FindTable(strSql.ToString());
        }
        /// <summary>
        /// 销售号码分析报表
        /// </summary>
        /// <returns></returns>
        public DataTable GetAnalysis(string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            string whereSql = "";
            //单据日期
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                whereSql += " and TelphoneOrder.CreateDate BETWEEN '" + startTime + "' and '" + endTime + "'";
            }

            string strSql = @"SELECT fx,hd,count,zb FROM (
SELECT 'b2' fx,substring(contact,1,2) hd,count(1) count,CONVERT( DECIMAL(18,2),cast(count(1) as float)/CAST((SELECT count(1) total FROM TelphoneOrder WHERE Contact IS not NULL ) AS FLOAT)*100) zb FROM TelphoneOrder 
WHERE Contact IS not NULL " + whereSql+
@" GROUP BY substring(contact,1, 2) 
UNION
SELECT 'b3' fx,substring(contact,1,3) hd,count(1) count,CONVERT( DECIMAL(18,2),cast(count(1) as float)/CAST((SELECT count(1) total FROM TelphoneOrder WHERE Contact IS not NULL ) AS FLOAT)*100) zb  FROM TelphoneOrder 
WHERE Contact IS not NULL " + whereSql +
@" GROUP BY substring(contact,1, 3) 
UNION
SELECT 'operate' fx,TelphoneData.operate hd,count(1) count,CONVERT( DECIMAL(18,2),cast(count(1) as float)/CAST((SELECT count(1) total FROM TelphoneOrder WHERE Contact IS not NULL ) AS FLOAT)*100) zb  FROM TelphoneOrder 
LEFT JOIN TelphoneData  ON substring(TelphoneOrder.contact,1,7)=TelphoneData.Number7 
WHERE Contact IS not NULL  " + whereSql +
@" GROUP BY TelphoneData.Operate 
UNION
SELECT 'w1' fx, substring(contact,8,1) hd,count(1) count,CONVERT( DECIMAL(18,2),cast(count(1) as float)/CAST((SELECT count(1) total FROM TelphoneOrder WHERE Contact IS not NULL ) AS FLOAT)*100) zb  FROM TelphoneOrder 
WHERE Contact IS not NULL " + whereSql +
@" GROUP BY substring(contact,8,1) 
UNION
SELECT 'w2' fx,substring(contact,9,1) hd,count(1) count,CONVERT( DECIMAL(18,2),cast(count(1) as float)/CAST((SELECT count(1) total FROM TelphoneOrder WHERE Contact IS not NULL ) AS FLOAT)*100) zb  FROM TelphoneOrder 
WHERE Contact IS not NULL " + whereSql +
@" GROUP BY substring(contact,9,1) 
UNION
SELECT 'w3' fx,substring(contact,10,1) hd,count(1) count,CONVERT( DECIMAL(18,2),cast(count(1) as float)/CAST((SELECT count(1) total FROM TelphoneOrder WHERE Contact IS not NULL ) AS FLOAT)*100) zb  FROM TelphoneOrder 
WHERE Contact IS not NULL " + whereSql +
@" GROUP BY substring(contact,10,1) 
UNION
SELECT 'w4' fx,substring(contact,11,1) hd,count(1) count,CONVERT( DECIMAL(18,2),cast(count(1) as float)/CAST((SELECT count(1) total FROM TelphoneOrder WHERE Contact IS not NULL ) AS FLOAT)*100) zb  FROM TelphoneOrder 
WHERE Contact IS not NULL " + whereSql +
@" GROUP BY substring(contact,11,1) 
) t
ORDER BY fx,count desc";
            return this.BaseRepository().FindTable(strSql.ToString());
        }

        /// <summary>
        /// 员工报表
        /// </summary>
        /// <param name="queryJson">起始，结束日期json</param>
        /// <returns></returns>
        public DataTable GetCallLog(string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            string whereTime = "";
            //单据日期
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                whereTime += " and CreateDate BETWEEN '" + startTime + "' and '" + endTime + "'";
            }

            string strSql =
@"SELECT gg.workerusername,gg.workername,
ISNULL(minute, 0) minute,
ISNULL(callcount, 0) callcount,
ISNULL(yescallcount, 0) yescallcount,
ISNULL(nocallcount, 0) nocallcount,
CONVERT(DECIMAL(18, 2), yescallcount / CAST(ISNULL(NULLIF(callcount, 0), 1) AS FLOAT) * 100) jtl,
ISNULL(getcount, 0) getcount,
ISNULL(surpluscount, 0) surpluscount FROM(
    SELECT workerusername, workername, sum(CallDuration) / 60 minute, count(1) callcount FROM CRM_CallLog
    WHERE 1=1 " + whereTime + @"
    GROUP BY workerusername, workername)gg
LEFT JOIN (
    SELECT workerusername, workername, count(1) yescallcount FROM CRM_CallLog 
    WHERE CallDuration > 0 " + whereTime + @"
    GROUP BY workerusername, workername) bb ON gg.workerusername = bb.workerusername
LEFT JOIN (
    SELECT workerusername, workername, count(1) nocallcount FROM CRM_CallLog 
    WHERE CallDuration <= 0  " + whereTime + @"
    GROUP BY workerusername, workername) cc ON gg.workerusername = cc.workerusername
LEFT JOIN (
    SELECT TraceUserName, count(1) getcount FROM ZZT_400Customer
    WHERE DeleteMark=0 " + whereTime + @" GROUP BY TraceUserName
    UNION SELECT TraceUserName, count(1) getcount FROM ZZT_PDDCustomer
    WHERE DeleteMark=0 " + whereTime + @" GROUP BY TraceUserName
) dd ON gg.workername = dd.TraceUserName
LEFT JOIN (
    SELECT TraceUserName, count(1) surpluscount FROM ZZT_400Customer
    WHERE DeleteMark=0 " + whereTime + @" AND CallOutTime IS NULL  GROUP BY TraceUserName
    UNION SELECT TraceUserName, count(1) surpluscount FROM ZZT_PDDCustomer
    WHERE DeleteMark=0 " + whereTime + @" AND CallOutTime IS NULL  GROUP BY TraceUserName
) ee ON gg.workername = ee.TraceUserName
ORDER BY minute desc";

            return this.BaseRepository().FindTable(strSql.ToString());
        }
        #endregion
    }
}
