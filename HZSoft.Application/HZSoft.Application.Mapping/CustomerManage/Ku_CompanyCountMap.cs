using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2018-04-24 10:20
    /// �� ������˾���¸���
    /// </summary>
    public class Ku_CompanyCountMap : EntityTypeConfiguration<Ku_CompanyCountEntity>
    {
        public Ku_CompanyCountMap()
        {
            #region ������
            //��
            this.ToTable("Ku_CompanyCount");
            //����
            this.HasKey(t => t.Id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
