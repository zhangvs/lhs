using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2017-09-16 16:41
    /// �� ���������
    /// </summary>
    public class TelphoneSourceMap : EntityTypeConfiguration<TelphoneSourceEntity>
    {
        public TelphoneSourceMap()
        {
            #region ������
            //��
            this.ToTable("TelphoneSource");
            //����
            this.HasKey(t => t.TelphoneID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
