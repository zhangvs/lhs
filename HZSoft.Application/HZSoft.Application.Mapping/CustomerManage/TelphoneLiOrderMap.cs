using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2017-10-26 09:09
    /// �� �������Ŷ���
    /// </summary>
    public class TelphoneLiOrderMap : EntityTypeConfiguration<TelphoneLiOrderEntity>
    {
        public TelphoneLiOrderMap()
        {
            #region ������
            //��
            this.ToTable("TelphoneLiOrder");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
