using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2018-05-10 09:15
    /// �� ����ƴ���ͻ�
    /// </summary>
    public class ZZT_PDDCustomerMap : EntityTypeConfiguration<ZZT_PDDCustomerEntity>
    {
        public ZZT_PDDCustomerMap()
        {
            #region ������
            //��
            this.ToTable("ZZT_PDDCustomer");
            //����
            this.HasKey(t => t.Id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
