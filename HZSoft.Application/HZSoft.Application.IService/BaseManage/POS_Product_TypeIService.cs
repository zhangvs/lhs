using HZSoft.Application.Entity.BaseManage;
using HZSoft.Util.WebControl;
using System.Collections.Generic;

namespace HZSoft.Application.IService.BaseManage
{
    /// <summary>
    /// �� �� 6.1
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2017-12-18 13:59
    /// �� ������Ʒ����
    /// </summary>
    public interface POS_Product_TypeIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        IEnumerable<POS_Product_TypeEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        IEnumerable<POS_Product_TypeEntity> GetList(string queryJson);
        IEnumerable<POS_Product_TypeEntity> GetList();
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        POS_Product_TypeEntity GetEntity(string keyValue);
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
        void RemoveForm(string keyValue);
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        void SaveForm(string keyValue, POS_Product_TypeEntity entity);
        #endregion

        #region ��֤����
        /// <summary>
        /// �����Ų����ظ�
        /// </summary>
        /// <param name="itemCode">���</param>
        /// <param name="keyValue">����</param>
        /// <returns></returns>
        bool ExistItemCode(string itemCode, string keyValue);
        /// <summary>
        /// �������Ʋ����ظ�
        /// </summary>
        /// <param name="itemName">����</param>
        /// <param name="keyValue">����</param>
        /// <returns></returns>
        bool ExistItemName(string itemName, string keyValue);
        #endregion
    }
}
