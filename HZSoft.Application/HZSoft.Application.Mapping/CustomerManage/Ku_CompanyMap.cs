using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2018-03-23 16:28
    /// �� ������˾��
    /// </summary>
    public class Ku_CompanyMap : EntityTypeConfiguration<Ku_CompanyEntity>
    {
        public Ku_CompanyMap()
        {
            #region ������
            //��
            this.ToTable("Ku_Company");
            //����
            this.HasKey(t => t.Id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
