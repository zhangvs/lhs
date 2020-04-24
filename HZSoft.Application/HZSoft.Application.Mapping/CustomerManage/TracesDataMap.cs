using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2018-07-19 17:40
    /// �� �������͵�������
    /// </summary>
    public class TracesDataMap : EntityTypeConfiguration<TracesDataEntity>
    {
        public TracesDataMap()
        {
            #region ������
            //��
            this.ToTable("TracesData");
            //����
            this.HasKey(t => t.LogisticCode);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
