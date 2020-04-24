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
    public class Sales_Contract_ItemEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// ��ͬ�ӱ�Id
        /// </summary>
        /// <returns></returns>
        [Column("ID")]
        public string Id { get; set; }
        /// <summary>
        /// ��ͬ����Id
        /// </summary>
        /// <returns></returns>
        [Column("CONTRACTID")]
        public string ContractId { get; set; }
        /// <summary>
        /// ��ͬ���
        /// </summary>
        /// <returns></returns>
        [Column("CONTRACTNUM")]
        public string ContractNum { get; set; }
        /// <summary>
        /// ��ƷId
        /// </summary>
        /// <returns></returns>
        [Column("PRODUCTID")]
        public string ProductId { get; set; }
        /// <summary>
        /// ��Ʒ���
        /// </summary>
        /// <returns></returns>
        [Column("ProductCode")]
        public string ProductCode { get; set; }
        /// <summary>
        /// ��Ʒ����
        /// </summary>
        /// <returns></returns>
        [Column("PRODUCTNAME")]
        public string ProductName { get; set; }
        /// <summary>
        /// ��λ
        /// </summary>
        /// <returns></returns>
        [Column("PRODUCTUNIT")]
        public string ProductUnit { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [Column("UNITPRICE")]
        public decimal? UnitPrice { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [Column("COUNT")]
        public decimal? Count { get; set; }
        /// <summary>
        /// ���
        /// </summary>
        /// <returns></returns>
        [Column("AMOUNT")]
        public decimal? Amount { get; set; }
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
        [Column("SORT")]
        public int? Sort { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [Column("DESCRIPTION")]
        public string Description { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.Id = Guid.NewGuid().ToString();
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