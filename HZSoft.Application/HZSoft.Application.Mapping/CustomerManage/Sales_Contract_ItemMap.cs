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
    public class Sales_Contract_ItemMap : EntityTypeConfiguration<Sales_Contract_ItemEntity>
    {
        public Sales_Contract_ItemMap()
        {
            #region ������
            //��
            this.ToTable("Sales_Contract_Item");
            //����
            this.HasKey(t => t.Id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
