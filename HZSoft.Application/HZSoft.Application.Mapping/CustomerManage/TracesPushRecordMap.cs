using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2018-07-19 17:24
    /// 描 述：物流推送记录
    /// </summary>
    public class TracesPushRecordMap : EntityTypeConfiguration<TracesPushRecordEntity>
    {
        public TracesPushRecordMap()
        {
            #region 表、主键
            //表
            this.ToTable("TracesPushRecord");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
