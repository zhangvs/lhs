using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2018-05-10 09:36
    /// �� ����ƴ��ർ���¼
    /// </summary>
    public class ZZT_PDDLogEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string Id { get; set; }
        /// <summary>
        /// �ͻ����
        /// </summary>
        /// <returns></returns>
        public string CustNo { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        public string ParcelNo { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        public string OrderNo { get; set; }
        /// <summary>
        /// �������ǳ�
        /// </summary>
        /// <returns></returns>
        public string NickName { get; set; }
        /// <summary>
        /// �ռ�������
        /// </summary>
        /// <returns></returns>
        public string FullName { get; set; }
        /// <summary>
        /// �ֻ�
        /// </summary>
        /// <returns></returns>
        public string Mobile { get; set; }
        /// <summary>
        /// �绰
        /// </summary>
        /// <returns></returns>
        public string Telphone { get; set; }
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
        public string County { get; set; }
        /// <summary>
        /// ��ַ
        /// </summary>
        /// <returns></returns>
        public string Address { get; set; }
        /// <summary>
        /// �ʱ�
        /// </summary>
        /// <returns></returns>
        public string ZipCode { get; set; }
        /// <summary>
        /// ��ݹ�˾
        /// </summary>
        /// <returns></returns>
        public string Express { get; set; }
        /// <summary>
        /// ��ݵ���
        /// </summary>
        /// <returns></returns>
        public string ExpressNo { get; set; }
        /// <summary>
        /// ����(��)
        /// </summary>
        /// <returns></returns>
        public string Weight { get; set; }
        /// <summary>
        /// �˷�
        /// </summary>
        /// <returns></returns>
        public string Freight { get; set; }
        /// <summary>
        /// ��Ʒ��Ϣ
        /// </summary>
        /// <returns></returns>
        public string GoodsName { get; set; }
        /// <summary>
        /// �µ�ʱ��
        /// </summary>
        /// <returns></returns>
        public DateTime? OrderTime { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        /// <returns></returns>
        public DateTime? PayTime { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        /// <returns></returns>
        public DateTime? DeliveryTime { get; set; }
        /// <summary>
        /// ��ӡʱ��
        /// </summary>
        /// <returns></returns>
        public DateTime? PrintingTime { get; set; }
        /// <summary>
        /// �������
        /// </summary>
        /// <returns></returns>
        public string BuyerMessage { get; set; }
        /// <summary>
        /// ���ұ�ע
        /// </summary>
        /// <returns></returns>
        public string SellerMessage { get; set; }
        /// <summary>
        /// �Ҵ�ע
        /// </summary>
        /// <returns></returns>
        public string MakeNote { get; set; }
        /// <summary>
        /// �ϲ�����
        /// </summary>
        /// <returns></returns>
        public string MergeOrder { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        public string SendPerson { get; set; }
        /// <summary>
        /// �����˵绰
        /// </summary>
        /// <returns></returns>
        public string SendTelphone { get; set; }
        /// <summary>
        /// ����ʡ
        /// </summary>
        /// <returns></returns>
        public string SendProvince { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        public string SendCity { get; set; }
        /// <summary>
        /// ������/��
        /// </summary>
        /// <returns></returns>
        public string SendCounty { get; set; }
        /// <summary>
        /// ������ַ
        /// </summary>
        /// <returns></returns>
        public string SendAddress { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
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
            this.Id = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;
            this.DeleteMark = 0;
            this.EnabledMark = 1;
        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.Id = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}