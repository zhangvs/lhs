using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2017-11-04 16:22
    /// �� ��������
    /// </summary>
    public class CRM_CallLogMap : EntityTypeConfiguration<CRM_CallLogEntity>
    {
        public CRM_CallLogMap()
        {
            #region ������
            //��
            this.ToTable("CRM_CallLog");
            //����
            this.HasKey(t => t.Id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
