using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Util.WebControl;
using System.Collections.Generic;
using System.Data;

namespace HZSoft.Application.IService.CustomerManage
{
    /// <summary>
    /// �� �� 6.1
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2018-05-04 13:50
    /// �� ����400�ͻ�
    /// </summary>
    public interface ZZT_400CustomerIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        IEnumerable<ZZT_400CustomerEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        IEnumerable<ZZT_400CustomerEntity> GetList(string queryJson);
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        ZZT_400CustomerEntity GetEntity(string keyValue);
        #endregion

        #region ��֤����
        /// <summary>
        /// �˻������ظ�
        /// </summary>
        /// <param name="account">�˻�ֵ</param>
        /// <param name="keyValue">����</param>
        /// <returns></returns>
        bool ExistMobile(string Mobile, string keyValue);
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValues">����</param>
        void RemoveForm(string keyValues);
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        void SaveForm(string keyValue, ZZT_400CustomerEntity entity);
        /// <summary>
        /// ���������
        /// </summary>
        /// <param name="keyValues">�ͻ�ids</param>
        /// <param name="TraceUserId">Ա��id</param>
        /// <param name="TraceUserName">Ա������</param>
        void SaveAssign(string keyValues, string TraceUserId, string TraceUserName);
        /// <summary>
        /// ���ϸ�����
        /// </summary>
        /// <param name="keyValues">�ͻ�ids</param>
        void SaveThrow(string keyValues);
        /// <summary>
        /// ��ȡ
        /// </summary>
        /// <param name="keyValues">�ͻ�ids</param>
        void SaveObtain(string keyValues);
        #endregion

        /// <summary>
        /// ������������400
        /// </summary>
        /// <param name="dtSource">ʵ�����</param>
        /// <returns></returns>
        string BatchAdd400(DataTable dtSource);
    }
}
