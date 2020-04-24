using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2017-11-04 16:22
    /// �� ��������
    /// </summary>
    [Serializable]
    public class CRM_CallLogEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// ����id
        /// </summary>
        /// <returns></returns>
        public string Id { get; set; }
        /// <summary>
        /// ��ҵId
        /// </summary>
        /// <returns></returns>
        public string EnterpriseId { get; set; }
        /// <summary>
        /// ��ϯ
        /// </summary>
        /// <returns></returns>
        public string WorkerUserName { get; set; }
        /// <summary>
        /// ��ϯ����
        /// </summary>
        /// <returns></returns>
        public string WorkerName { get; set; }
        /// <summary>
        /// ��ϯ��
        /// </summary>
        /// <returns></returns>
        public string WorkerGroupId { get; set; }
        /// <summary>
        /// ��ϯ����
        /// </summary>
        /// <returns></returns>
        public string WorkerPhoneNumber { get; set; }
        /// <summary>
        /// �����ϴ�ʱ��
        /// </summary>
        /// <returns></returns>
        public DateTime? UploadTime { get; set; }
        /// <summary>
        /// ��ϯIP
        /// </summary>
        /// <returns></returns>
        public string WorkerIP { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public int? CallType { get; set; }
        /// <summary>
        /// �ͻ�����
        /// </summary>
        /// <returns></returns>
        public string CallNumber { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        /// <returns></returns>
        public DateTime? CallTime { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        /// <returns></returns>
        public DateTime? RingTime { get; set; }
        /// <summary>
        /// ��ͨʱ��
        /// </summary>
        /// <returns></returns>
        public DateTime? ConnectedTime { get; set; }
        /// <summary>
        /// ͨ������ʱ��
        /// </summary>
        /// <returns></returns>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 0 ʧ�ܣ�δ��ͨ��1 �ɹ�����ͨ
        /// </summary>
        /// <returns></returns>
        public int? CallResult { get; set; }
        /// <summary>
        /// �Ƿ���¼��
        /// </summary>
        /// <returns></returns>
        public bool? IsRecordingFile { get; set; }
        /// <summary>
        /// ¼���ļ���
        /// </summary>
        /// <returns></returns>
        public string RecordingFile { get; set; }
        /// <summary>
        /// �����ļ������ϴ�ǰ��
        /// </summary>
        /// <returns></returns>
        public string LocalFileName { get; set; }
        /// <summary>
        /// ¼���ļ���СK
        /// </summary>
        /// <returns></returns>
        public int? RecordingFileSize { get; set; }
        /// <summary>
        /// ¼����ʽ
        /// </summary>
        /// <returns></returns>
        public int? RecordingType { get; set; }
        /// <summary>
        /// ��ϯ�һ����ͻ��һ������� 
        /// δʹ�ñ���
        /// </summary>
        /// <returns></returns>
        public string HangReson { get; set; }
        /// <summary>
        /// ͨ��ʱ��
        /// </summary>
        /// <returns></returns>
        public int? CallDuration { get; set; }
        /// <summary>
        /// ¼���Ƿ����ϴ�
        /// </summary>
        /// <returns></returns>
        public bool? IsRecordingFileUploaded { get; set; }
        /// <summary>
        /// ¼���ϴ�ʱ��
        /// </summary>
        /// <returns></returns>
        public DateTime? RecordingUploadTime { get; set; }
        /// <summary>
        /// �����Ƿ����ϴ�
        /// </summary>
        /// <returns></returns>
        public bool? IsUploaded { get; set; }
        /// <summary>
        /// �Ƿ񻰵���¼�������ϴ����
        /// </summary>
        /// <returns></returns>
        public bool? IsDone { get; set; }
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
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            //this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            //this.CreateUserName = OperatorProvider.Provider.Current().UserName;
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