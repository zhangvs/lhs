using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2018-06-15 14:39
    /// �� ����������˾
    /// </summary>
    public class Ku_RelationCompanyMap : EntityTypeConfiguration<Ku_RelationCompanyEntity>
    {
        public Ku_RelationCompanyMap()
        {
            #region ������
            //��
            this.ToTable("Ku_RelationCompany");
            //����
            this.HasKey(t => t.Id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
