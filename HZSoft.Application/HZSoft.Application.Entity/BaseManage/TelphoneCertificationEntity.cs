using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.BaseManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2017-10-12 17:30
    /// �� ����TelphoneCertification
    /// </summary>
    public class TelphoneCertificationEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// Ψһ��ʶ
        /// </summary>
        /// <returns></returns>
        public string ID { get; set; }
        /// <summary>
        /// ��֤�ֻ���
        /// </summary>
        /// <returns></returns>
        public string mobileNumber { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string custName { get; set; }
        /// <summary>
        /// ���֤
        /// </summary>
        /// <returns></returns>
        public string custCertCode { get; set; }
        /// <summary>
        /// ���֤��Ч��
        /// </summary>
        /// <returns></returns>
        public string custCertDate { get; set; }
        /// <summary>
        /// ��ַ
        /// </summary>
        /// <returns></returns>
        public string custCertAddress { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        public string photo_z { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        public string photo_b { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        public string photo_s { get; set; }
        /// <summary>
        /// �ύ��ʶ
        /// </summary>
        /// <returns></returns>
        public int? loadMark { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        /// <returns></returns>
        public DateTime? createTime { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.ID = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.ID = keyValue;
                                            }
        #endregion
    }
}