using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2018-03-23 16:49
    /// �� ������˾��ϵ�˿�
    /// </summary>
    public class Ku_CompanyContactMap : EntityTypeConfiguration<Ku_CompanyContactEntity>
    {
        public Ku_CompanyContactMap()
        {
            #region ������
            //��
            this.ToTable("Ku_CompanyContact");
            //����
            this.HasKey(t => t.Id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
