using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2018-04-24 10:20
    /// �� ������˾���¸���
    /// </summary>
    public class Ku_CompanyCountEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        public int? Id { get; set; }
        /// <summary>
        /// ����Χ
        /// </summary>
        /// <returns></returns>
        public string AreaRange { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        public string WeekStartDate { get; set; }
        /// <summary>
        /// ����ֹ
        /// </summary>
        /// <returns></returns>
        public string WeekEndDate { get; set; }
        /// <summary>
        /// ���ܸ���
        /// </summary>
        /// <returns></returns>
        public string WeekCount { get; set; }
        /// <summary>
        /// ���¸���
        /// </summary>
        /// <returns></returns>
        public string MonthCount { get; set; }
        /// <summary>
        /// �������
        /// </summary>
        /// <returns></returns>
        public string YearCount { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string AllCount { get; set; }
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