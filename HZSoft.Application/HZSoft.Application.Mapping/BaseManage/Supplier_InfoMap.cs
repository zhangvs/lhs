using HZSoft.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.BaseManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2017-06-30 10:28
    /// �� ������Ӧ��
    /// </summary>
    public class Supplier_InfoMap : EntityTypeConfiguration<Supplier_InfoEntity>
    {
        public Supplier_InfoMap()
        {
            #region ������
            //��
            this.ToTable("Supplier_Info");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
