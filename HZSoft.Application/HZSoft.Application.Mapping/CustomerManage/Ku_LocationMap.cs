using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2018-03-26 11:54
    /// �� ���������
    /// </summary>
    public class Ku_LocationMap : EntityTypeConfiguration<Ku_LocationEntity>
    {
        public Ku_LocationMap()
        {
            #region ������
            //��
            this.ToTable("Ku_Location");
            //����
            this.HasKey(t => t.Id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
