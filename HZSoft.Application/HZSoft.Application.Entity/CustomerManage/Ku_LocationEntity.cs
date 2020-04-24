using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2018-03-26 11:54
    /// �� ���������
    /// </summary>
    public class Ku_LocationEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public int? Id { get; set; }
        /// <summary>
        /// ƥ�伶��
        /// </summary>
        /// <returns></returns>
        public string _Level { get; set; }
        /// <summary>
        /// �Ƿ������
        /// </summary>
        /// <returns></returns>
        public int? IsRegeo { get; set; }
        /// <summary>
        /// �ȵ�ȦId
        /// </summary>
        /// <returns></returns>
        public string RegeoId { get; set; }
        /// <summary>
        /// �ȵ�Ȧ
        /// </summary>
        /// <returns></returns>
        public string RegeoName { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public int? Count { get; set; }
        /// <summary>
        /// ��ַ
        /// </summary>
        /// <returns></returns>
        public string Address { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string Poiweight { get; set; }
        /// <summary>
        /// ���
        /// </summary>
        /// <returns></returns>
        public string Area { get; set; }
        /// <summary>
        /// POI����
        /// </summary>
        /// <returns></returns>
        public string TypeCode { get; set; }
        /// <summary>
        /// POI��������
        /// </summary>
        /// <returns></returns>
        public string TypeName { get; set; }
        /// <summary>
        /// ʡ��
        /// </summary>
        /// <returns></returns>
        public string Province { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string City { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string CityCode { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string District { get; set; }
        /// <summary>
        /// �������
        /// </summary>
        /// <returns></returns>
        public string AdCode { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string Township { get; set; }
        /// <summary>
        /// POI����
        /// </summary>
        /// <returns></returns>
        public string Location { get; set; }
        /// <summary>
        /// �ߵ¾���
        /// </summary>
        /// <returns></returns>
        public decimal? wxLon { get; set; }
        /// <summary>
        /// �ߵ�γ��
        /// </summary>
        /// <returns></returns>
        public decimal? wxLat { get; set; }
        /// <summary>
        /// �ٶȾ���
        /// </summary>
        /// <returns></returns>
        public decimal? bdLon { get; set; }
        /// <summary>
        /// �ٶȾ���
        /// </summary>
        /// <returns></returns>
        public decimal? bdLat { get; set; }
        /// <summary>
        /// ������Ƭ
        /// </summary>
        /// <returns></returns>
        public string Picture { get; set; }
        /// <summary>
        /// ɾ�����
        /// </summary>
        /// <returns></returns>
        public int? DeleteMark { get; set; }
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
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            //this.Id = Guid.NewGuid().ToString();
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
            this.Id = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}