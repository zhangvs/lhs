using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2018-03-23 16:28
    /// �� ������˾��
    /// </summary>
    public class Ku_CompanyEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// Ψһ��ʶ
        /// </summary>
        /// <returns></returns>
        public int? Id { get; set; }
        /// <summary>
        /// ��˾��
        /// </summary>
        /// <returns></returns>
        public string CompanyName { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        public string CompanyOldName { get; set; }
        /// <summary>
        /// ע���
        /// </summary>
        /// <returns></returns>
        public string CreditCode { get; set; }
        /// <summary>
        /// ע���
        /// </summary>
        /// <returns></returns>
        public string CompanyCode { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        /// <returns></returns>
        public DateTime? BuildTime { get; set; }
        /// <summary>
        /// ���˴���
        /// </summary>
        /// <returns></returns>
        public string LegalPerson { get; set; }
        /// <summary>
        /// ��Ӫ״̬
        /// </summary>
        /// <returns></returns>
        public int? ManageState { get; set; }
        /// <summary>
        /// ע���ʱ�
        /// </summary>
        /// <returns></returns>
        public decimal? Capital { get; set; }
        /// <summary>
        /// ע�����
        /// </summary>
        /// <returns></returns>
        public string CapitalCurrency { get; set; }
        /// <summary>
        /// ��˾��ַ
        /// </summary>
        /// <returns></returns>
        public string CompanyAddress { get; set; }
        /// <summary>
        /// ��˾����
        /// </summary>
        /// <returns></returns>
        public string CompanyType { get; set; }
        /// <summary>
        /// ������ҵ
        /// </summary>
        /// <returns></returns>
        public string Sector { get; set; }
        /// <summary>
        /// ��Ӫ��Χ
        /// </summary>
        /// <returns></returns>
        public string Scope { get; set; }
        /// <summary>
        /// �Ǽǻ���
        /// </summary>
        /// <returns></returns>
        public string RegistrationAgency { get; set; }
        /// <summary>
        /// ��Ӫ������
        /// </summary>
        /// <returns></returns>
        public DateTime? ManageTermStart { get; set; }
        /// <summary>
        /// ��Ӫ����ֹ
        /// </summary>
        /// <returns></returns>
        public DateTime? ManageTermEnd { get; set; }
        /// <summary>
        /// ��׼����
        /// </summary>
        /// <returns></returns>
        public DateTime? ApprovalDate { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public DateTime? DeathDate { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public DateTime? RevocationlDate { get; set; }
        /// <summary>
        /// ע������
        /// </summary>
        /// <returns></returns>
        public DateTime? LogoutDate { get; set; }
        /// <summary>
        /// �ֻ�
        /// </summary>
        /// <returns></returns>
        public string Mobilephone { get; set; }
        /// <summary>
        /// �̻�
        /// </summary>
        /// <returns></returns>
        public string Telphone { get; set; }
        /// <summary>
        /// ʵ�ʵ�ַ
        /// </summary>
        /// <returns></returns>
        public string RealAddress { get; set; }
        /// <summary>
        /// ���õ�ַ
        /// </summary>
        /// <returns></returns>
        public string SpareAddress { get; set; }
        /// <summary>
        /// ʵ����ҵ����
        /// </summary>
        /// <returns></returns>
        public string RealIndustryCategory { get; set; }
        /// <summary>
        /// ʵ�ʷ���
        /// </summary>
        /// <returns></returns>
        public string RealIndustry { get; set; }
        /// <summary>
        /// ʡ
        /// </summary>
        /// <returns></returns>
        public string Province { get; set; }
        /// <summary>
        /// ��
        /// </summary>
        /// <returns></returns>
        public string City { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string District { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string Lon { get; set; }
        /// <summary>
        /// ά��
        /// </summary>
        /// <returns></returns>
        public string Lat { get; set; }
        /// <summary>
        /// λ��Id
        /// </summary>
        /// <returns></returns>
        public int? LocationId { get; set; }
        /// <summary>
        /// ��ȦId
        /// </summary>
        /// <returns></returns>
        public string RegeoId { get; set; }
        /// <summary>
        /// ��Ȧ
        /// </summary>
        /// <returns></returns>
        public string RegeoName { get; set; }
        /// <summary>
        /// �Ƿ��ϰ�
        /// </summary>
        /// <returns></returns>
        public int? IsBoss { get; set; }
        /// <summary>
        /// ���
        /// </summary>
        /// <returns></returns>
        public string Area { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string Employee { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string Mailbox { get; set; }
        /// <summary>
        /// ��ַ
        /// </summary>
        /// <returns></returns>
        public string Website { get; set; }
        /// <summary>
        /// ¥��
        /// </summary>
        /// <returns></returns>
        public string Building { get; set; }
        /// <summary>
        /// ¥��
        /// </summary>
        /// <returns></returns>
        public int? Floor { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public int? Room { get; set; }
        /// <summary>
        /// ��˾��Ƭ
        /// </summary>
        /// <returns></returns>
        public string CompanyPicture { get; set; }
        /// <summary>
        /// �鿴����
        /// </summary>
        /// <returns></returns>
        public int? SeeTimes { get; set; }
        /// <summary>
        /// ������˾����
        /// </summary>
        /// <returns></returns>
        public int? RelationCount { get; set; }
        /// <summary>
        /// רҵ���̶�
        /// </summary>
        /// <returns></returns>
        public int? Specialized { get; set; }
        /// <summary>
        /// ����״̬
        /// </summary>
        /// <returns></returns>
        public int? FollowState { get; set; }
        /// <summary>
        /// ȷ��״̬
        /// </summary>
        /// <returns></returns>
        public int? ConfirmState { get; set; }
        /// <summary>
        /// ע�����
        /// </summary>
        /// <returns></returns>
        public int? DeleteMark { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// ��ȡ״̬
        /// </summary>
        /// <returns></returns>
        public int? ObtainState { get; set; }
        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <returns></returns>
        public DateTime? ObtainDate { get; set; }
        /// <summary>
        /// ��ȡ�û�Id
        /// </summary>
        /// <returns></returns>
        public string ObtainUserId { get; set; }
        /// <summary>
        /// ��ȡ�û�
        /// </summary>
        /// <returns></returns>
        public string ObtainUserName { get; set; }
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
        /// ���������������
        /// </summary>
        public override void Create()
        {
            //this.Id = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;

            this.ObtainDate = DateTime.Now;
            this.ObtainUserId = OperatorProvider.Provider.Current().UserId;
            this.ObtainUserName = OperatorProvider.Provider.Current().UserName;
            this.ManageState = 1;//��Ӫ
            this.ObtainState = 1;
            this.DeleteMark = 0;
            this.SeeTimes = 0;
        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(int? keyValue)
        {
            this.Id = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}