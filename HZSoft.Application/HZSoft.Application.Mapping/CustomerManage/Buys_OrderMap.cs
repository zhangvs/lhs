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
    public class Buys_OrderMap : EntityTypeConfiguration<Buys_OrderEntity>
    {
        public Buys_OrderMap()
        {
            #region ������
            //��
            this.ToTable("Buys_Order");
            //����
            this.HasKey(t => t.OrderId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
