using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2018-05-10 09:15
    /// 描 述：拼多多客户
    /// </summary>
    public class ZZT_PDDCustomerMap : EntityTypeConfiguration<ZZT_PDDCustomerEntity>
    {
        public ZZT_PDDCustomerMap()
        {
            #region 表、主键
            //表
            this.ToTable("ZZT_PDDCustomer");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
