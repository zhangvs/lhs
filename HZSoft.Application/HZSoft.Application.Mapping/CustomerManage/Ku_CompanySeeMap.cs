using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2018-06-27 14:31
    /// �� ������˾�����¼
    /// </summary>
    public class Ku_CompanySeeMap : EntityTypeConfiguration<Ku_CompanySeeEntity>
    {
        public Ku_CompanySeeMap()
        {
            #region ������
            //��
            this.ToTable("Ku_CompanySee");
            //����
            this.HasKey(t => t.Id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
