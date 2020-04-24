using System;
using System.ComponentModel.DataAnnotations.Schema;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2018-06-01 16:52
    /// 描 述：进货单
    /// </summary>
    public class Buys_OrderEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键Id
        /// </summary>
        /// <returns></returns>
        [Column("ORDERID")]
        public string OrderId { get; set; }
        /// <summary>
        /// 单据编号
        /// </summary>
        /// <returns></returns>
        [Column("ORDERCODE")]
        public string OrderCode { get; set; }
        /// <summary>
        /// 单据日期
        /// </summary>
        /// <returns></returns>
        [Column("ORDERDATE")]
        public DateTime? OrderDate { get; set; }
        /// <summary>
        /// 供应商Id
        /// </summary>
        /// <returns></returns>
        [Column("SUPPLIERID")]
        public string SupplierId { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        /// <returns></returns>
        [Column("SUPPLIERNAME")]
        public string SupplierName { get; set; }
        /// <summary>
        /// 优惠金额
        /// </summary>
        /// <returns></returns>
        [Column("DISCOUNTSUM")]
        public decimal? DiscountSum { get; set; }
        /// <summary>
        /// 应付金额
        /// </summary>
        /// <returns></returns>
        [Column("ACCOUNTS")]
        public decimal? Accounts { get; set; }
        /// <summary>
        /// 已付金额
        /// </summary>
        /// <returns></returns>
        [Column("PAIDAMOUNT")]
        public decimal? PaidAmount { get; set; }
        /// <summary>
        /// 付款日期
        /// </summary>
        /// <returns></returns>
        [Column("PAYMENTDATE")]
        public DateTime? PaymentDate { get; set; }
        /// <summary>
        /// 付款方式
        /// </summary>
        /// <returns></returns>
        [Column("PAYMENTMODE")]
        public string PaymentMode { get; set; }
        /// <summary>
        /// 付款状态（1-未付款2-部分付款3-全部付款）
        /// </summary>
        /// <returns></returns>
        [Column("PAYMENTSTATE")]
        public int? PaymentState { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        /// <returns></returns>
        [Column("ORDERSTATE")]
        public int? OrderState { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        [Column("DESCRIPTION")]
        public string Description { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        /// <returns></returns>
        [Column("DELETEMARK")]
        public int? DeleteMark { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>
        /// <returns></returns>
        [Column("ENABLEDMARK")]
        public int? EnabledMark { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        /// <returns></returns>
        [Column("CREATEDATE")]
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>
        /// <returns></returns>
        [Column("CREATEUSERID")]
        public string CreateUserId { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        [Column("CREATEUSERNAME")]
        public string CreateUserName { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        /// <returns></returns>
        [Column("MODIFYDATE")]
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 修改用户主键
        /// </summary>
        /// <returns></returns>
        [Column("MODIFYUSERID")]
        public string ModifyUserId { get; set; }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <returns></returns>
        [Column("MODIFYUSERNAME")]
        public string ModifyUserName { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.OrderId = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;
            this.PaymentState = 0;
            this.OrderState = 0;
            this.DeleteMark = 0;
            this.EnabledMark = 1;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.OrderId = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}