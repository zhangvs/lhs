using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2018-07-19 17:42
    /// 描 述：轨迹详情
    /// </summary>
    public class TracesItemEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 标识id
        /// </summary>
        /// <returns></returns>
        public string Id { get; set; }
        /// <summary>
        /// 物流单号
        /// </summary>
        /// <returns></returns>
        public string LogisticCode { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        /// <returns></returns>
        public string AcceptTime { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        /// <returns></returns>
        public string AcceptStation { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        public string Remark { get; set; }
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