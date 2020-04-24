using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2018-06-27 14:31
    /// �� ������˾�����¼
    /// </summary>
    public class Ku_CompanySeeEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string Id { get; set; }
        /// <summary>
        /// ��˾ID
        /// </summary>
        /// <returns></returns>
        public int? CompanyId { get; set; }
        /// <summary>
        /// ��˾����
        /// </summary>
        /// <returns></returns>
        public string CompanyName { get; set; }
        /// <summary>
        /// �鿴����
        /// </summary>
        /// <returns></returns>
        public DateTime? SeeDate { get; set; }
        /// <summary>
        /// �鿴�û�����
        /// </summary>
        /// <returns></returns>
        public string SeeUserId { get; set; }
        /// <summary>
        /// �鿴�û�
        /// </summary>
        /// <returns></returns>
        public string SeeUserName { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.Id = Guid.NewGuid().ToString();
            this.SeeDate = DateTime.Now;
            this.SeeUserId = OperatorProvider.Provider.Current().UserId;
            this.SeeUserName = OperatorProvider.Provider.Current().UserName;
        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.Id = keyValue;
            this.SeeDate = DateTime.Now;
            this.SeeUserId = OperatorProvider.Provider.Current().UserId;
            this.SeeUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}