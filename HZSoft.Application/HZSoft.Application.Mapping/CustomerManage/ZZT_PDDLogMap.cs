using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2018-05-10 09:36
    /// �� ����ƴ��ർ���¼
    /// </summary>
    public class ZZT_PDDLogMap : EntityTypeConfiguration<ZZT_PDDLogEntity>
    {
        public ZZT_PDDLogMap()
        {
            #region ������
            //��
            this.ToTable("ZZT_PDDLog");
            //����
            this.HasKey(t => t.Id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
