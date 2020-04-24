using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2018-07-19 17:24
    /// 描 述：物流推送记录
    /// </summary>
    public class TracesPushRecordEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        public string Id { get; set; }
        /// <summary>
        /// 用户电商ID
        /// </summary>
        /// <returns></returns>
        public string EBusinessID { get; set; }
        /// <summary>
        /// 推送个数
        /// </summary>
        /// <returns></returns>
        public string Count { get; set; }
        /// <summary>
        /// 推送时间
        /// </summary>
        /// <returns></returns>
        public string PushTime { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.Id = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.Id = keyValue;
                                            }
        #endregion
    }
}