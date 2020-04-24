using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.IService.CustomerManage;
using HZSoft.Application.Service.CustomerManage;
using HZSoft.Util.WebControl;
using System.Collections.Generic;
using System;
using System.Data;

namespace HZSoft.Application.Busines.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创 建：超级管理员
    /// 日 期：2017-10-23 14:11
    /// 描 述：靓号库
    /// </summary>
    public class TelphoneLiangBLL
    {
        private TelphoneLiangIService service = new TelphoneLiangService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<TelphoneLiangEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<TelphoneLiangEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<TelphoneLiangEntity> GetList(string telphone, string organizeId)
        {
            return service.GetList(telphone, organizeId);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<TelphoneLiangEntity> GetGrade(string organizeId, string Grade)
        {
            return service.GetGrade(organizeId,Grade);
        }
        
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public TelphoneLiangEntity GetEntity(int? keyValue)
        {
            return service.GetEntity(keyValue);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public TelphoneLiangEntity GetEntityByTelphone(string telphone)
        {
            return service.GetEntityByTelphone(telphone);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(int? keyValue)
        {
            try
            {
                service.RemoveForm(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(int? keyValue, TelphoneLiangEntity entity)
        {
            try
            {
                service.SaveForm(keyValue, entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <returns></returns>
        public string BatchAddEntity(DataTable dtSource)
        {
            try
            {
                string returnMsg = service.BatchAddEntity(dtSource);
                return returnMsg;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
