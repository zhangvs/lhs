using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2018-05-04 13:50
    /// �� ����400�ͻ�
    /// </summary>
    public class ZZT_400CustomerMap : EntityTypeConfiguration<ZZT_400CustomerEntity>
    {
        public ZZT_400CustomerMap()
        {
            #region ������
            //��
            this.ToTable("ZZT_400Customer");
            //����
            this.HasKey(t => t.Id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
