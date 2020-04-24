using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：佘赐雄
    /// 日 期：2016-03-16 13:54
    /// 描 述：订单管理
    /// </summary>
    public class OrderEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 订单主键
        /// </summary>
        /// <returns></returns>
        public string OrderId { get; set; }
        /// <summary>
        /// 单据编号
        /// </summary>
        /// <returns></returns>
        public string OrderCode { get; set; }
        /// <summary>
        /// 单据日期
        /// </summary>
        /// <returns></returns>
        public DateTime? OrderDate { get; set; }
        /// <summary>
        /// 销售人员Id
        /// </summary>
        /// <returns></returns>
        public string SellerId { get; set; }
        /// <summary>
        /// 销售人员
        /// </summary>
        /// <returns></returns>
        public string SellerName { get; set; }
        /// <summary>
        /// 客户主键
        /// </summary>
        /// <returns></returns>
        public string CustomerId { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        /// <returns></returns>
        public string CustomerName { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        /// <returns></returns>
        public string Mobile { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        /// <returns></returns>
        public string Province { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        /// <returns></returns>
        public string City { get; set; }
        /// <summary>
        /// 县/区
        /// </summary>
        /// <returns></returns>
        public string County { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        /// <returns></returns>
        public string Address { get; set; }
        /// <summary>
        /// 服用到期日
        /// </summary>
        /// <returns></returns>
        public DateTime? DueDate { get; set; }
        /// <summary>
        /// 优惠金额
        /// </summary>
        /// <returns></returns>
        public decimal? DiscountSum { get; set; }
        /// <summary>
        /// 应收金额
        /// </summary>
        /// <returns></returns>
        public decimal? Accounts { get; set; }
        /// <summary>
        /// 已收金额
        /// </summary>
        /// <returns></returns>
        public decimal? ReceivedAmount { get; set; }
        /// <summary>
        /// 收款日期
        /// </summary>
        /// <returns></returns>
        public DateTime? PaymentDate { get; set; }
        /// <summary>
        /// 收款方式
        /// </summary>
        /// <returns></returns>
        public string PaymentMode { get; set; }
        /// <summary>
        /// 收款状态（1-未收款2-部分收款3-全部收款）
        /// </summary>
        /// <returns></returns>
        public int? PaymentState { get; set; }
        /// <summary>
        /// 销售费用
        /// </summary>
        /// <returns></returns>
        public decimal? SaleCost { get; set; }
        /// <summary>
        /// 配送方式
        /// </summary>
        /// <returns></returns>
        public string Express { get; set; }
        /// <summary>
        /// 单号
        /// </summary>
        /// <returns></returns>
        public string Numbers { get; set; }
        /// <summary>
        /// 物流状态
        /// 0：在途，即货物处于运输过程中；
        /// 1：揽件，货物已由快递公司揽收并且产生了第一条跟踪信息；
        /// 2：疑难，货物寄送过程出了问题；
        /// 3：签收，收件人已签收；
        /// 4：退签，即货物由于用户拒签、超区等原因退回，而且发件人已经签收；
        /// 5：派件，即快递正在进行同城派件；
        /// 6：退回，货物正处于退回发件人的途中；
        /// </summary>
        /// <returns></returns>
        public int? ExpressState { get; set; }
        /// <summary>
        /// 订单状态
        /// 核单1，-1
        /// 会计核单2，-2
        /// 发货3，退货-3
        /// </summary>
        /// <returns></returns>
        public int? OrderState { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        /// <returns></returns>
        public int? DeleteMark { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>
        /// <returns></returns>
        public int? EnabledMark { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>
        /// <returns></returns>
        public string CreateUserId { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        public string CreateUserName { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        /// <returns></returns>
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 修改用户主键
        /// </summary>
        /// <returns></returns>
        public string ModifyUserId { get; set; }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <returns></returns>
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
            this.ReceivedAmount = 0;
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