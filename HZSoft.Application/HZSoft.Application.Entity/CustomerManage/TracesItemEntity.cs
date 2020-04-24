using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2018-07-19 17:42
    /// �� �����켣����
    /// </summary>
    public class TracesItemEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// ��ʶid
        /// </summary>
        /// <returns></returns>
        public string Id { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public string LogisticCode { get; set; }
        /// <summary>
        /// ʱ��
        /// </summary>
        /// <returns></returns>
        public string AcceptTime { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string AcceptStation { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        /// <returns></returns>
        public string Remark { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.Id = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.Id = keyValue;
                                            }
        #endregion
    }
}