using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2018-05-04 13:50
    /// 描 述：400客户
    /// </summary>
    public class ZZT_400CustomerMap : EntityTypeConfiguration<ZZT_400CustomerEntity>
    {
        public ZZT_400CustomerMap()
        {
            #region 表、主键
            //表
            this.ToTable("ZZT_400Customer");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
