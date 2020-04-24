using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2018-05-10 09:36
    /// 描 述：拼多多导入记录
    /// </summary>
    public class ZZT_PDDLogMap : EntityTypeConfiguration<ZZT_PDDLogEntity>
    {
        public ZZT_PDDLogMap()
        {
            #region 表、主键
            //表
            this.ToTable("ZZT_PDDLog");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
