using HZSoft.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.BaseManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2017-10-12 17:30
    /// �� ����TelphoneCertification
    /// </summary>
    public class TelphoneCertificationMap : EntityTypeConfiguration<TelphoneCertificationEntity>
    {
        public TelphoneCertificationMap()
        {
            #region ������
            //��
            this.ToTable("TelphoneCertification");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
