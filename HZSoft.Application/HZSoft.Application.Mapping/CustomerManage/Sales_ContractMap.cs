using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2017-12-15 16:52
    /// �� �������ۺ�ͬ��
    /// </summary>
    public class Sales_ContractMap : EntityTypeConfiguration<Sales_ContractEntity>
    {
        public Sales_ContractMap()
        {
            #region ������
            //��
            this.ToTable("Sales_Contract");
            //����
            this.HasKey(t => t.Id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
