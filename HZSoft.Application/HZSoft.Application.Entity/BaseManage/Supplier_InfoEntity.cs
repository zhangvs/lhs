using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.BaseManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2017-06-30 10:28
    /// �� ������Ӧ��
    /// </summary>
    public class Supplier_InfoEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// Ψһ��ʶ��
        /// </summary>
        /// <returns></returns>
        public string ID { get; set; }
        /// <summary>
        /// ���
        /// </summary>
        /// <returns></returns>
        public string Code { get; set; }
        /// <summary>
        /// ȫ��
        /// </summary>
        /// <returns></returns>
        public string FullName { get; set; }
        /// <summary>
        /// ���
        /// </summary>
        /// <returns></returns>
        public string ShortName { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        public string Namepy { get; set; }
        /// <summary>
        /// ��λ��ַ
        /// </summary>
        /// <returns></returns>
        public string Address { get; set; }
        /// <summary>
        /// ��ϵ��
        /// </summary>
        /// <returns></returns>
        public string Contract { get; set; }
        /// <summary>
        /// ֤����
        /// </summary>
        /// <returns></returns>
        public string IDNo { get; set; }
        /// <summary>
        /// �ֻ���
        /// </summary>
        /// <returns></returns>
        public string Mobile { get; set; }
        /// <summary>
        /// �绰
        /// </summary>
        /// <returns></returns>
        public string Telephone { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string Fax { get; set; }
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
        /// ��
        /// </summary>
        /// <returns></returns>
        public string Country { get; set; }
        /// <summary>
        /// ˰��
        /// </summary>
        /// <returns></returns>
        public string TaxNo { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public string Email { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        public string Opeingbank { get; set; }
        /// <summary>
        /// �������˺�
        /// </summary>
        /// <returns></returns>
        public string Account { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        /// <returns></returns>
        public string Remark { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// ������Id
        /// </summary>
        /// <returns></returns>
        public string CreatorId { get; set; }
        /// <summary>
        /// ����������
        /// </summary>
        /// <returns></returns>
        public string CreatorName { get; set; }
        /// <summary>
        /// �ϼ��ͻ�Id
        /// </summary>
        /// <returns></returns>
        public string ParentId { get; set; }
        /// <summary>
        /// �Ƿ���Ҫ�ʼ�
        /// </summary>
        /// <returns></returns>
        public int? IsNeedCheck { get; set; }
        /// <summary>
        /// ״̬
        /// </summary>
        /// <returns></returns>
        public int? Status { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.ID = Guid.NewGuid().ToString();
            this.CreateTime = DateTime.Now;
            this.CreatorId = OperatorProvider.Provider.Current().UserId;
            this.CreatorName = OperatorProvider.Provider.Current().UserName;
                                            }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.ID = keyValue;
            this.CreateTime = DateTime.Now;
            this.CreatorId = OperatorProvider.Provider.Current().UserId;
            this.CreatorName = OperatorProvider.Provider.Current().UserName;
                                            }
        #endregion
    }
}