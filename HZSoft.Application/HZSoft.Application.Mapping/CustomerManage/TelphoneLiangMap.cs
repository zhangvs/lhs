using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2017-10-23 14:11
    /// �� �������ſ�
    /// </summary>
    public class TelphoneLiangMap : EntityTypeConfiguration<TelphoneLiangEntity>
    {
        public TelphoneLiangMap()
        {
            #region ������
            //��
            this.ToTable("TelphoneLiang");
            //����
            this.HasKey(t => t.TelphoneID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
