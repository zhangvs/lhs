using HZSoft.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.BaseManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2017-09-19 17:45
    /// �� ���������
    /// </summary>
    public class TelphoneDataMap : EntityTypeConfiguration<TelphoneDataEntity>
    {
        public TelphoneDataMap()
        {
            #region ������
            //��
            this.ToTable("TelphoneData");
            //����
            this.HasKey(t => t.ID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
