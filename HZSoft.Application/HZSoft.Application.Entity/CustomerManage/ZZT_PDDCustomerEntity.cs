using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2018-05-10 09:15
    /// 描 述：拼多多客户
    /// </summary>
    public class ZZT_PDDCustomerEntity : BaseEntity
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
        /// 分配
        /// </summary>
        /// <returns></returns>
        public int? AssignMark { get; set; }
        /// <summary>
        /// 分配人主键
        /// </summary>
        /// <returns></returns>
        public string TraceUserId { get; set; }
        /// <summary>
        /// 分配人
        /// </summary>
        /// <returns></returns>
        public string TraceUserName { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        /// <returns></returns>
        public string OrderNo { get; set; }        
        /// <summary>
        /// 商品名称
        /// </summary>
        /// <returns></returns>
        public string GoodsName { get; set; }
        /// <summary>
        /// 订单时间
        /// </summary>
        /// <returns></returns>
        public DateTime? OrderTime { get; set; }
        /// <summary>
        /// 回电时间
        /// </summary>
        /// <returns></returns>
        public DateTime? CallOutTime { get; set; }
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
        /// 姓名
        /// </summary>
        /// <returns></returns>
        public string FullName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        /// <returns></returns>
        public int? Gender { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        /// <returns></returns>
        public int? Age { get; set; }
        /// <summary>
        /// 语气
        /// </summary>
        /// <returns></returns>
        public string Tone { get; set; }
        /// <summary>
        /// 正在服用
        /// </summary>
        /// <returns></returns>
        public int? OrallyMark { get; set; }
        /// <summary>
        /// 病情
        /// </summary>
        /// <returns></returns>
        public string Disease { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        /// <returns></returns>
        public string Mobile { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        /// <returns></returns>
        public string Telphone { get; set; }
        /// <summary>
        /// 微信
        /// </summary>
        /// <returns></returns>
        public string Wechat { get; set; }
        /// <summary>
        /// QQ
        /// </summary>
        /// <returns></returns>
        public string QQ { get; set; }
        /// <summary>
        /// 职业
        /// </summary>
        /// <returns></returns>
        public string Profession { get; set; }
        /// <summary>
        /// 爱好
        /// </summary>
        /// <returns></returns>
        public string Hobby { get; set; }
        /// <summary>
        /// 客户级别
        /// </summary>
        /// <returns></returns>
        public string CustLevelId { get; set; }
        /// <summary>
        /// 意向
        /// </summary>
        /// <returns></returns>
        public int? IntentionMark { get; set; }
        /// <summary>
        /// 售出
        /// </summary>
        /// <returns></returns>
        public int? SellMark { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        /// <returns></returns>
        public int? SortCode { get; set; }
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
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
            //this.AssignMark = 0;
            //this.OrallyMark = 0;
            //this.IntentionMark = 0;
            //this.SellMark = 0;
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