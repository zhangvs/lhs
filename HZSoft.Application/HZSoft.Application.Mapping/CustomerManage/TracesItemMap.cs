using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2018-07-19 17:42
    /// �� �����켣����
    /// </summary>
    public class TracesItemMap : EntityTypeConfiguration<TracesItemEntity>
    {
        public TracesItemMap()
        {
            #region ������
            //��
            this.ToTable("TracesItem");
            //����
            this.HasKey(t => t.Id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
