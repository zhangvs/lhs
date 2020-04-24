using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.BaseManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2017-12-15 15:02
    /// �� ������Ʒ��
    /// </summary>
    public class POS_ProductEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// ��ƷID
        /// </summary>
        /// <returns></returns>
        public string Id { get; set; }
        /// <summary>
        /// ��Ʒ���
        /// </summary>
        /// <returns></returns>
        public string ProductCode { get; set; }
        /// <summary>
        /// ��Ʒ����
        /// </summary>
        /// <returns></returns>
        public string ProductName { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        public string TypeId { get; set; }
        /// <summary>
        /// ���ุ������
        /// </summary>
        /// <returns></returns>
        public string ParentId { get; set; }
        /// <summary>
        /// ���
        /// </summary>
        /// <returns></returns>
        public decimal? Stock { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
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
        /// ����ʱ��
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// �����û�����
        /// </summary>
        /// <returns></returns>
        public string CreateUserId { get; set; }
        /// <summary>
        /// �����û�
        /// </summary>
        /// <returns></returns>
        public string CreateUserName { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;
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