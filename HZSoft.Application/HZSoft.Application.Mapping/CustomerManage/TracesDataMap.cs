using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2018-07-19 17:40
    /// 描 述：推送单号数据
    /// </summary>
    public class TracesDataMap : EntityTypeConfiguration<TracesDataEntity>
    {
        public TracesDataMap()
        {
            #region 表、主键
            //表
            this.ToTable("TracesData");
            //主键
            this.HasKey(t => t.LogisticCode);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
