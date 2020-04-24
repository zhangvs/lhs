using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� �����ܴ���
    /// �� �ڣ�2016-03-14 09:47
    /// �� �����ͻ���Ϣ
    /// </summary>
    public class LOHAS_CustomerEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// �ͻ�����
        /// </summary>
        /// <returns></returns>
        public string CustomerId { get; set; }
        /// <summary>
        /// �ͻ����
        /// </summary>
        /// <returns></returns>
        public string EnCode { get; set; }
        /// <summary>
        /// �ͻ�����
        /// </summary>
        /// <returns></returns>
        public string FullName { get; set; }
        /// <summary>
        /// �ͻ����
        /// </summary>
        /// <returns></returns>
        public string ShortName { get; set; }
        /// <summary>
        /// �ͻ���ҵ
        /// </summary>
        /// <returns></returns>
        public string CustIndustryId { get; set; }
        /// <summary>
        /// �ͻ�����
        /// </summary>
        /// <returns></returns>
        public string CustTypeId { get; set; }
        /// <summary>
        /// �ͻ�����
        /// </summary>
        /// <returns></returns>
        public string CustLevelId { get; set; }
        /// <summary>
        /// �ͻ��̶�
        /// </summary>
        /// <returns></returns>
        public string CustDegreeId { get; set; }
        /// <summary>
        /// ����ʡ��
        /// </summary>
        /// <returns></returns>
        public string Province { get; set; }
        /// <summary>
        /// ���ڳ���
        /// </summary>
        /// <returns></returns>
        public string City { get; set; }
        /// <summary>
        /// ��ϵ��
        /// </summary>
        /// <returns></returns>
        public string Contact { get; set; }
        /// <summary>
        /// �ֻ�
        /// </summary>
        /// <returns></returns>
        public string Mobile { get; set; }
        /// <summary>
        /// �绰
        /// </summary>
        /// <returns></returns>
        public string Tel { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string Fax { get; set; }
        /// <summary>
        /// QQ
        /// </summary>
        /// <returns></returns>
        public string QQ { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        /// <returns></returns>
        public string Email { get; set; }
        /// <summary>
        /// ΢��
        /// </summary>
        /// <returns></returns>
        public string Wechat { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string Hobby { get; set; }
        /// <summary>
        /// ���˴���
        /// </summary>
        /// <returns></returns>
        public string LegalPerson { get; set; }
        /// <summary>
        /// ������Id
        /// </summary>
        /// <returns></returns>
        public string CompanyId { get; set; }
        /// <summary>
        /// ����������
        /// </summary>
        /// <returns></returns>
        public string CompanyName { get; set; }
        /// <summary>
        /// �ջ���ַ
        /// </summary>
        /// <returns></returns>
        public string ShippingAddress { get; set; }
        /// <summary>
        /// ��˾��վ
        /// </summary>
        /// <returns></returns>
        public string CompanySite { get; set; }
        /// <summary>
        /// ��˾���
        /// </summary>
        /// <returns></returns>
        public string CompanyDesc { get; set; }
        /// <summary>
        /// ������ԱId
        /// </summary>
        /// <returns></returns>
        public string TraceUserId { get; set; }
        /// <summary>
        /// ������Ա
        /// </summary>
        /// <returns></returns>
        public string TraceUserName { get; set; }
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
        /// ��ע
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
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


        /// <summary>
        /// �´θ���ʱ��
        /// </summary>
        /// <returns></returns>
        public DateTime? AlertDateTime { get; set; }
        /// <summary>
        /// �ͻ���Դ
        /// </summary>
        /// <returns></returns>
        public string Source { get; set; }
        /// <summary>
        /// �´θ���ʱ��
        /// </summary>
        /// <returns></returns>
        public DateTime? JiaoDateTime { get; set; }
        /// <summary>
        /// �´θ���ʱ��
        /// </summary>
        /// <returns></returns>
        public DateTime? LiangDateTime { get; set; }
        /// <summary>
        /// �´θ���ʱ��
        /// </summary>
        /// <returns></returns>
        public DateTime? JinDateTime { get; set; }
        /// <summary>
        /// ���
        /// </summary>
        /// <returns></returns>
        public string Style { get; set; }
        /// <summary>
        /// ���
        /// </summary>
        /// <returns></returns>
        public string Area { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string Age { get; set; }
        /// <summary>
        /// �Ա�
        /// </summary>		
        public int? Gender { get; set; }
        /// <summary>
        /// ְҵ
        /// </summary>
        /// <returns></returns>
        public string Profession { get; set; }
        /// <summary>
        /// ��ͥ
        /// </summary>
        /// <returns></returns>
        public string Family { get; set; }
        /// <summary>
        /// ��ͥ
        /// </summary>
        /// <returns></returns>
        public string Budget { get; set; }
        /// <summary>
        /// ǩԼ
        /// </summary>
        /// <returns></returns>
        public int? SignMark { get; set; }
        /// <summary>
        /// ǩԼ���
        /// </summary>
        /// <returns></returns>
        public decimal? SignAmount { get; set; }
        /// <summary>
        /// ǩԼʱ��
        /// </summary>
        /// <returns></returns>
        public DateTime? SignDateTime { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public int? DieMark { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        /// <returns></returns>
        public DateTime? DieDateTime { get; set; }

        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.CustomerId = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;
            this.ModifyDate = DateTime.Now;
        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.CustomerId = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}