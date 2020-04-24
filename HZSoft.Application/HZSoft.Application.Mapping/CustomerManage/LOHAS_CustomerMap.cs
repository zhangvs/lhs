using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2020-04-11 21:25
    /// 描 述：客户信息管理
    /// </summary>
    public class LOHAS_CustomerMap : EntityTypeConfiguration<LOHAS_CustomerEntity>
    {
        public LOHAS_CustomerMap()
        {
            #region 表、主键
            //表
            this.ToTable("LOHAS_Customer");
            //主键
            this.HasKey(t => t.CustomerId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
