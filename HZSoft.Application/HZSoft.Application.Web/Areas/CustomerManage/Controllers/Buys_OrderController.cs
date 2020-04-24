using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.Busines.CustomerManage;
using HZSoft.Util;
using HZSoft.Util.WebControl;
using System.Collections.Generic;
using System.Web.Mvc;
using HZSoft.Application.Busines.SystemManage;
using HZSoft.Application.Code;

namespace HZSoft.Application.Web.Areas.CustomerManage.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创 建：超级管理员
    /// 日 期：2018-06-01 16:52
    /// 描 述：进货单
    /// </summary>
    public class Buys_OrderController : MvcControllerBase
    {
        private Buys_OrderBLL buys_orderbll = new Buys_OrderBLL();
        private CodeRuleBLL codeRuleBLL = new CodeRuleBLL();

        #region 视图功能
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Buys_OrderIndex()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Buys_OrderForm()
        {
            if (Request["keyValue"] == null)
            {
                ViewBag.OrderCode = codeRuleBLL.GetBillCode(SystemInfo.CurrentModuleId);
                //ViewBag.OrderCode = codeRuleBLL.GetBillCode(SystemInfo.CurrentUserId, SystemInfo.CurrentModuleId);
            }
            return View();
        }
        /// <summary>
        /// 订单详细页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Detail()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = buys_orderbll.GetPageList(pagination, queryJson);
            var jsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return ToJsonResult(jsonData);
        }
        /// <summary>
        /// 获取实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = buys_orderbll.GetEntity(keyValue);
            var childData = buys_orderbll.GetDetails(keyValue);
            var jsonData = new
            {
                entity = data,
                childEntity = childData
            };
            return ToJsonResult(jsonData);
        }
        /// <summary>
        /// 获取子表详细信息 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetDetailsJson(string keyValue)
        {
            var data = buys_orderbll.GetDetails(keyValue);
            return ToJsonResult(data);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveForm(string keyValue)
        {
            buys_orderbll.RemoveForm(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="orderEntryJson">明细对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, Buys_OrderEntity entity, string orderEntryJson)
        {
            var orderEntryList = orderEntryJson.ToList<Buys_OrderItemEntity>();
            buys_orderbll.SaveForm(keyValue, entity, orderEntryList);
            return Success("操作成功。");
        }
        /// <summary>
        /// 仓管查看数量进行入库，实际操作库存
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="orderEntryJson">明细对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveKeeperForm(string keyValue, Buys_OrderEntity entity, string orderEntryJson)
        {
            var orderEntryList = orderEntryJson.ToList<Buys_OrderItemEntity>();
            buys_orderbll.SaveForm(keyValue, entity, orderEntryList);
            return Success("操作成功。");
        }
        #endregion
    }
}
