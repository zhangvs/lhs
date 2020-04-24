using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2018-05-10 09:36
    /// 描 述：拼多多导入记录
    /// </summary>
    public class ZZT_PDDLogEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        public string Id { get; set; }
        /// <summary>
        /// 客户编号
        /// </summary>
        /// <returns></returns>
        public string CustNo { get; set; }
        /// <summary>
        /// 包裹号
        /// </summary>
        /// <returns></returns>
        public string ParcelNo { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        /// <returns></returns>
        public string OrderNo { get; set; }
        /// <summary>
        /// 购买人昵称
        /// </summary>
        /// <returns></returns>
        public string NickName { get; set; }
        /// <summary>
        /// 收件人姓名
        /// </summary>
        /// <returns></returns>
        public string FullName { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        /// <returns></returns>
        public string Mobile { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        /// <returns></returns>
        public string Telphone { get; set; }
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
        /// 区
        /// </summary>
        /// <returns></returns>
        public string County { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        /// <returns></returns>
        public string Address { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        /// <returns></returns>
        public string ZipCode { get; set; }
        /// <summary>
        /// 快递公司
        /// </summary>
        /// <returns></returns>
        public string Express { get; set; }
        /// <summary>
        /// 快递单号
        /// </summary>
        /// <returns></returns>
        public string ExpressNo { get; set; }
        /// <summary>
        /// 重量(克)
        /// </summary>
        /// <returns></returns>
        public string Weight { get; set; }
        /// <summary>
        /// 运费
        /// </summary>
        /// <returns></returns>
        public string Freight { get; set; }
        /// <summary>
        /// 商品信息
        /// </summary>
        /// <returns></returns>
        public string GoodsName { get; set; }
        /// <summary>
        /// 下单时间
        /// </summary>
        /// <returns></returns>
        public DateTime? OrderTime { get; set; }
        /// <summary>
        /// 付款时间
        /// </summary>
        /// <returns></returns>
        public DateTime? PayTime { get; set; }
        /// <summary>
        /// 发货时间
        /// </summary>
        /// <returns></returns>
        public DateTime? DeliveryTime { get; set; }
        /// <summary>
        /// 打印时间
        /// </summary>
        /// <returns></returns>
        public DateTime? PrintingTime { get; set; }
        /// <summary>
        /// 买家留言
        /// </summary>
        /// <returns></returns>
        public string BuyerMessage { get; set; }
        /// <summary>
        /// 卖家备注
        /// </summary>
        /// <returns></returns>
        public string SellerMessage { get; set; }
        /// <summary>
        /// 我打备注
        /// </summary>
        /// <returns></returns>
        public string MakeNote { get; set; }
        /// <summary>
        /// 合并订单
        /// </summary>
        /// <returns></returns>
        public string MergeOrder { get; set; }
        /// <summary>
        /// 发货人
        /// </summary>
        /// <returns></returns>
        public string SendPerson { get; set; }
        /// <summary>
        /// 发货人电话
        /// </summary>
        /// <returns></returns>
        public string SendTelphone { get; set; }
        /// <summary>
        /// 发货省
        /// </summary>
        /// <returns></returns>
        public string SendProvince { get; set; }
        /// <summary>
        /// 发货市
        /// </summary>
        /// <returns></returns>
        public string SendCity { get; set; }
        /// <summary>
        /// 发货区/县
        /// </summary>
        /// <returns></returns>
        public string SendCounty { get; set; }
        /// <summary>
        /// 发货地址
        /// </summary>
        /// <returns></returns>
        public string SendAddress { get; set; }
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
            this.Id = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;
            this.DeleteMark = 0;
            this.EnabledMark = 1;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.Id = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}