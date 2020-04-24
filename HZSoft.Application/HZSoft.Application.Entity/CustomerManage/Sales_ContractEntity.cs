using System;
using System.ComponentModel.DataAnnotations.Schema;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2017-12-15 16:52
    /// �� �������ۺ�ͬ��
    /// </summary>
    public class Sales_ContractEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// ��ͬId
        /// </summary>
        /// <returns></returns>
        [Column("ID")]
        public string Id { get; set; }
        /// <summary>
        /// ��ͬ���
        /// </summary>
        /// <returns></returns>
        [Column("CONTRACTNUM")]
        public string ContractNum { get; set; }
        /// <summary>
        /// �ͻ�Id
        /// </summary>
        /// <returns></returns>
        [Column("CUSTOMERID")]
        public string CustomerId { get; set; }
        /// <summary>
        /// �ͻ�����
        /// </summary>
        /// <returns></returns>
        [Column("CUSTOMERNAME")]
        public string CustomerName { get; set; }
        /// <summary>
        /// �ͻ���˾
        /// </summary>
        /// <returns></returns>
        [Column("CUSTOMERCOMPANY")]
        public string CustomerCompany { get; set; }
        /// <summary>
        /// ��ͬ����
        /// </summary>
        /// <returns></returns>
        [Column("CONTRACTDATE")]
        public DateTime? ContractDate { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        [Column("TOTALCOUNT")]
        public decimal? TotalCount { get; set; }
        /// <summary>
        /// �ܽ��
        /// </summary>
        /// <returns></returns>
        [Column("TotalAmount")]
        public decimal? TotalAmount { get; set; }
        /// <summary>
        /// ���ʱ��
        /// </summary>
        /// <returns></returns>
        [Column("CREATEDATE")]
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// �����Id
        /// </summary>
        /// <returns></returns>
        [Column("USERID")]
        public string UserId { get; set; }
        /// <summary>
        /// �����
        /// </summary>
        /// <returns></returns>
        [Column("USERNAME")]
        public string UserName { get; set; }
        /// <summary>
        /// ״̬
        /// </summary>
        /// <returns></returns>
        [Column("STATUS")]
        public int? Status { get; set; }
        /// <summary>
        /// �Ƿ�ɾ��
        /// </summary>
        /// <returns></returns>
        [Column("ISDEL")]
        public int? IsDel { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [Column("SPAREFIELD")]
        public string SpareField { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        /// <returns></returns>
        [Column("REMARK")]
        public string Remark { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
                                }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.Id = keyValue;
                                            }
        #endregion
    }
}