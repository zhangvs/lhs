using HZSoft.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.BaseManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2017-12-18 13:59
    /// �� ������Ʒ����
    /// </summary>
    public class POS_Product_TypeMap : EntityTypeConfiguration<POS_Product_TypeEntity>
    {
        public POS_Product_TypeMap()
        {
            #region ������
            //��
            this.ToTable("POS_Product_Type");
            //����
            this.HasKey(t => t.ItemId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
