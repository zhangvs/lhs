using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2017-09-22 16:27
    /// �� ����ϴ�ų�
    /// </summary>
    public class TelphoneWashMap : EntityTypeConfiguration<TelphoneWashEntity>
    {
        public TelphoneWashMap()
        {
            #region ������
            //��
            this.ToTable("TelphoneWash");
            //����
            this.HasKey(t => t.TelphoneID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
