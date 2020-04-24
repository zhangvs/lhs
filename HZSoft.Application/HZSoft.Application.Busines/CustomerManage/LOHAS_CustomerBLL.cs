using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.IService.CustomerManage;
using HZSoft.Application.Service.CustomerManage;
using HZSoft.Util.WebControl;
using System.Collections.Generic;
using System;
using System.Data;

namespace HZSoft.Application.Busines.LOHAS_CustomerManage
{
    /// <summary>
    /// �� �� 6.1
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2020-04-11 21:25
    /// �� �����ͻ���Ϣ����
    /// </summary>
    public class LOHAS_CustomerBLL
    {

        private LOHAS_CustomerIService service = new LOHAS_CustomerService();

        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LOHAS_CustomerEntity> GetList()
        {
            return service.GetList();
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<LOHAS_CustomerEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public LOHAS_CustomerEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        #endregion

        #region ��֤����
        /// <summary>
        /// �ͻ����Ʋ����ظ�
        /// </summary>
        /// <param name="fullName">����</param>
        /// <param name="keyValue">����</param>
        /// <returns></returns>
        public bool ExistFullName(string fullName, string keyValue)
        {
            return service.ExistFullName(fullName, keyValue);
        }
        /// <summary>
        /// �ֻ������ظ�
        /// </summary>
        /// <param name="fullName">����</param>
        /// <param name="keyValue">����</param>
        /// <returns></returns>
        public bool ExistMobile(string Mobile, string keyValue)
        {
            return service.ExistMobile(Mobile, keyValue);
        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
        public void RemoveForm(string keyValue)
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
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, LOHAS_CustomerEntity entity)
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
        /// �����
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity">ʵ��</param>
        /// <param name="moduleId">ģ��</param>
        public void SaveForm(string keyValue, LOHAS_CustomerEntity entity, string moduleId)
        {
            try
            {
                service.SaveForm(keyValue, entity, moduleId);
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// �����400���������޸ģ�
        /// </summary>
        /// <returns></returns>
        public string BatchAdd(DataTable dtSource)
        {
            try
            {
                string returnMsg = service.BatchAdd(dtSource);
                return returnMsg;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion
    }
}
