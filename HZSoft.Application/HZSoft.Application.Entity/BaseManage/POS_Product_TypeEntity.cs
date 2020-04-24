using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.BaseManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2017-12-18 13:59
    /// �� ������Ʒ����
    /// </summary>
    public class POS_Product_TypeEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// ��������
        /// </summary>		
        public string ItemId { get; set; }
        /// <summary>
        /// ��������
        /// </summary>		
        public string ParentId { get; set; }
        /// <summary>
        /// �������
        /// </summary>		
        public string ItemCode { get; set; }
        /// <summary>
        /// ��������
        /// </summary>		
        public string ItemName { get; set; }
        /// <summary>
        /// ���ͽṹ
        /// </summary>		
        public int? IsTree { get; set; }
        /// <summary>
        /// �������
        /// </summary>		
        public int? IsNav { get; set; }
        /// <summary>
        /// ������
        /// </summary>		
        public int? SortCode { get; set; }
        /// <summary>
        /// ɾ�����
        /// </summary>		
        public int? DeleteMark { get; set; }
        /// <summary>
        /// ��Ч��־
        /// </summary>		
        public int? EnabledMark { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>		
        public string Description { get; set; }
        /// <summary>
        /// ��������
        /// </summary>		
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// �����û�����
        /// </summary>		
        public string CreateUserId { get; set; }
        /// <summary>
        /// �����û�
        /// </summary>		
        public string CreateUserName { get; set; }
        /// <summary>
        /// �޸�����
        /// </summary>		
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// �޸��û�����
        /// </summary>		
        public string ModifyUserId { get; set; }
        /// <summary>
        /// �޸��û�
        /// </summary>		
        public string ModifyUserName { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.ItemId = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;
            this.DeleteMark = 0;
        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.ItemId = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}