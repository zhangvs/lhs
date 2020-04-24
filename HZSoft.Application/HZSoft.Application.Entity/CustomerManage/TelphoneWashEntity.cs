using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2017-09-22 16:27
    /// �� ����ϴ�ų�
    /// </summary>
    public class TelphoneWashEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// ���
        /// </summary>
        /// <returns></returns>
        public int? TelphoneID { get; set; }
        /// <summary>
        /// �ֻ���
        /// </summary>
        /// <returns></returns>
        public string Telphone { get; set; }
        /// <summary>
        /// �Ŷ�
        /// </summary>
        /// <returns></returns>
        public string Number { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string Grade { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public int? AssignMark { get; set; }
        /// <summary>
        /// ������Id
        /// </summary>
        /// <returns></returns>
        public string SellerId { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        public string SellerName { get; set; }
        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <returns></returns>
        public DateTime? ObtainDate { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        /// <returns></returns>
        public DateTime? CallTime { get; set; }
        /// <summary>
        /// δ��ͨ��ʽ
        /// </summary>
        /// <returns></returns>
        public int? NoConnectMark { get; set; }
        /// <summary>
        /// ���ɽ�����
        /// </summary>
        /// <returns></returns>
        public int? NoDealMark { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string Surname { get; set; }
        /// <summary>
        /// �Ա��ʶ
        /// </summary>
        /// <returns></returns>
        public int? SexMark { get; set; }
        /// <summary>
        /// ���䷶Χ��ʶ
        /// </summary>
        /// <returns></returns>
        public int? AgeMark { get; set; }
        /// <summary>
        /// �������ر�ʶ
        /// </summary>
        /// <returns></returns>
        public int? AreaMark { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public int? ToneMark { get; set; }
        /// <summary>
        /// ����ͻ�
        /// </summary>
        /// <returns></returns>
        public int? IntentionMark { get; set; }
        /// <summary>
        /// �۳�
        /// </summary>
        /// <returns></returns>
        public int? SellMark { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        /// <returns></returns>
        public string CallDescription { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        public int? SortCode { get; set; }
        /// <summary>
        /// ɾ�����
        /// </summary>
        /// <returns></returns>
        public int? DeleteMark { get; set; }
        /// <summary>
        /// ��Ч��־
        /// </summary>
        /// <returns></returns>
        public int? EnabledMark { get; set; }
        /// <summary>
        /// ��������
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
        /// <summary>
        /// �޸�����
        /// </summary>
        /// <returns></returns>
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// �޸��û�����
        /// </summary>
        /// <returns></returns>
        public string ModifyUserId { get; set; }
        /// <summary>
        /// �޸��û�
        /// </summary>
        /// <returns></returns>
        public string ModifyUserName { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.CreateDate = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;
        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(int? keyValue)
        {
            this.TelphoneID = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}