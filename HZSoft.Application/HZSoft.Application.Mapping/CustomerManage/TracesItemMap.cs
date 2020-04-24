using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2018-07-19 17:42
    /// 描 述：轨迹详情
    /// </summary>
    public class TracesItemMap : EntityTypeConfiguration<TracesItemEntity>
    {
        public TracesItemMap()
        {
            #region 表、主键
            //表
            this.ToTable("TracesItem");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
