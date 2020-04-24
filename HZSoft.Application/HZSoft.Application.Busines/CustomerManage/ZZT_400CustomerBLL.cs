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
    /// �� �� 6.1
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2018-05-04 13:50
    /// �� ����400�ͻ�
    /// </summary>
    public class ZZT_400CustomerBLL
    {
        private ZZT_400CustomerIService service = new ZZT_400CustomerService();

        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<ZZT_400CustomerEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<ZZT_400CustomerEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public ZZT_400CustomerEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        #endregion

        #region ��֤����
        /// <summary>
        /// �˻������ظ�
        /// </summary>
        /// <param name="Mobile">�˻�ֵ</param>
        /// <param name="keyValue">����</param>
        /// <returns></returns>
        public bool ExistMobile(string Mobile, string keyValue = "")
        {
            return service.ExistMobile(Mobile, keyValue);
        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValues">����</param>
        public void RemoveForm(string keyValues)
        {
            try
            {
                service.RemoveForm(keyValues);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, ZZT_400CustomerEntity entity)
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
        /// <summary>
        /// ���������
        /// </summary>
        /// <param name="keyValues">�ͻ�ids</param>
        /// <param name="TraceUserId">Ա��id</param>
        /// <param name="TraceUserName">Ա������</param>
        public void SaveAssign(string keyValues, string TraceUserId, string TraceUserName)
        {
            try
            {
                service.SaveAssign(keyValues, TraceUserId, TraceUserName);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// ���ϸ�����
        /// </summary>
        /// <param name="keyValues">�ͻ�ids</param>
        public void SaveThrow(string keyValues)
        {
            try
            {
                service.SaveThrow(keyValues);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// ��ȡ
        /// </summary>
        /// <param name="keyValues">�ͻ�ids</param>
        public void SaveObtain(string keyValues)
        {
            try
            {
                service.SaveObtain(keyValues);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        /// <summary>
        /// �����400���������޸ģ�
        /// </summary>
        /// <returns></returns>
        public string BatchAdd400(DataTable dtSource)
        {
            try
            {
                string returnMsg = service.BatchAdd400(dtSource);
                return returnMsg;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}
