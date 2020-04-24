using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2018-05-09 11:28
    /// 描 述：400导入记录
    /// </summary>
    public class ZZT_400LogMap : EntityTypeConfiguration<ZZT_400LogEntity>
    {
        public ZZT_400LogMap()
        {
            #region 表、主键
            //表
            this.ToTable("ZZT_400Log");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
