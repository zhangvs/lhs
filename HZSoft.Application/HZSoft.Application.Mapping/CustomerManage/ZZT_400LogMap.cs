using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2018-05-09 11:28
    /// �� ����400�����¼
    /// </summary>
    public class ZZT_400LogMap : EntityTypeConfiguration<ZZT_400LogEntity>
    {
        public ZZT_400LogMap()
        {
            #region ������
            //��
            this.ToTable("ZZT_400Log");
            //����
            this.HasKey(t => t.Id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
