using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2018-07-19 17:40
    /// �� �������͵�������
    /// </summary>
    public class TracesDataEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// ��ݵ���
        /// </summary>
        /// <returns></returns>
        public string LogisticCode { get; set; }
        /// <summary>
        /// ����Id
        /// </summary>
        /// <returns></returns>
        public string PushId { get; set; }
        /// <summary>
        /// �̻�ID
        /// </summary>
        /// <returns></returns>
        public string EBusinessID { get; set; }
        /// <summary>
        /// OrderCode
        /// </summary>
        /// <returns></returns>
        public string OrderCode { get; set; }
        /// <summary>
        /// ��ݹ�˾����
        /// </summary>
        /// <returns></returns>
        public string ShipperCode { get; set; }
        /// <summary>
        /// �ɹ����
        /// </summary>
        /// <returns></returns>
        public string Success { get; set; }
        /// <summary>
        /// ʧ��ԭ��
        /// </summary>
        /// <returns></returns>
        public string Reason { get; set; }
        /// <summary>
        /// ����״̬: 0-�޹켣��1-�����գ�2-��;�У�3-ǩ��,4-�����
        /// </summary>
        /// <returns></returns>
        public int? State { get; set; }
        /// <summary>
        /// ���Ľӿڵ�Bkֵ
        /// </summary>
        /// <returns></returns>
        public string CallBack { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.LogisticCode = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.LogisticCode = keyValue;
                                            }
        #endregion
    }
}