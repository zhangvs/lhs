using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2018-07-19 17:24
    /// �� �����������ͼ�¼
    /// </summary>
    public class TracesPushRecordMap : EntityTypeConfiguration<TracesPushRecordEntity>
    {
        public TracesPushRecordMap()
        {
            #region ������
            //��
            this.ToTable("TracesPushRecord");
            //����
            this.HasKey(t => t.Id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
