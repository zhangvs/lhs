using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2018-06-01 16:52
    /// �� ����������
    /// </summary>
    public class Buys_OrderItemMap : EntityTypeConfiguration<Buys_OrderItemEntity>
    {
        public Buys_OrderItemMap()
        {
            #region ������
            //��
            this.ToTable("Buys_OrderItem");
            //����
            this.HasKey(t => t.OrderEntryId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
