using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2018-07-19 17:40
    /// 描 述：推送单号数据
    /// </summary>
    public class TracesDataEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 快递单号
        /// </summary>
        /// <returns></returns>
        public string LogisticCode { get; set; }
        /// <summary>
        /// 推送Id
        /// </summary>
        /// <returns></returns>
        public string PushId { get; set; }
        /// <summary>
        /// 商户ID
        /// </summary>
        /// <returns></returns>
        public string EBusinessID { get; set; }
        /// <summary>
        /// OrderCode
        /// </summary>
        /// <returns></returns>
        public string OrderCode { get; set; }
        /// <summary>
        /// 快递公司编码
        /// </summary>
        /// <returns></returns>
        public string ShipperCode { get; set; }
        /// <summary>
        /// 成功与否
        /// </summary>
        /// <returns></returns>
        public string Success { get; set; }
        /// <summary>
        /// 失败原因
        /// </summary>
        /// <returns></returns>
        public string Reason { get; set; }
        /// <summary>
        /// 物流状态: 0-无轨迹，1-已揽收，2-在途中，3-签收,4-问题件
        /// </summary>
        /// <returns></returns>
        public int? State { get; set; }
        /// <summary>
        /// 订阅接口的Bk值
        /// </summary>
        /// <returns></returns>
        public string CallBack { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.LogisticCode = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.LogisticCode = keyValue;
                                            }
        #endregion
    }
}