using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2020-04-11 21:25
    /// �� �����ͻ���Ϣ����
    /// </summary>
    public class LOHAS_CustomerMap : EntityTypeConfiguration<LOHAS_CustomerEntity>
    {
        public LOHAS_CustomerMap()
        {
            #region ������
            //��
            this.ToTable("LOHAS_Customer");
            //����
            this.HasKey(t => t.CustomerId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
