using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HZSoft.Util
{
    /// <summary>
    /// 返回消息
    /// </summary>
    public class ReturnMessage
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 结果消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 结果编码
        /// </summary>
        public int? ErrorCode { get; set; }

        public override string ToString()
        {
            return Json.ToJson(this);
        }
    }

    /// <summary>
    /// 物流追踪返回消息
    /// </summary>
    public class PushReturnMessage
    {
        /// <summary>
        /// 结果消息
        /// </summary>
        public string EBusinessID { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool? Success { get; set; }
        /// <summary>
        /// 结果消息
        /// </summary>
        public string Reason { get; set; }

        public override string ToString()
        {
            return Json.ToJson(this);
        }
    }
}

