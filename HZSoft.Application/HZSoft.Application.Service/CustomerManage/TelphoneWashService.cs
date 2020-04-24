using HZSoft.Application.Code;
using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.IService.CustomerManage;
using HZSoft.Data.Repository;
using HZSoft.Util;
using HZSoft.Util.Extension;
using HZSoft.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Transactions;

namespace HZSoft.Application.Service.CustomerManage
{
    /// <summary>
    /// �� �� 6.1
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2017-09-16 16:41
    /// �� ���������
    /// </summary>
    public class TelphoneWashService : RepositoryFactory<TelphoneWashEntity>, TelphoneWashIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<TelphoneWashEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<TelphoneWashEntity>();
            var queryParam = queryJson.ToJObject();
            string strSql = "select * from TelphoneWash where 1=1";

            //��������
            if (!queryParam["CreateStartTime"].IsEmpty() && !queryParam["CreateEndTime"].IsEmpty())
            {
                DateTime createStartTime = DateTime.Now.Date;
                DateTime createEndTime = DateTime.Now.Date.AddDays(1);
                strSql += " and CreateDate BETWEEN '" + createStartTime + "' and '" + createEndTime + "'";
            }
            //����״̬
            if (!queryParam["AssignMark"].IsEmpty())
            {
                int AssignMark = queryParam["AssignMark"].ToInt();
                strSql += " and AssignMark = " + AssignMark;
            }

            if (queryParam["CreateStartTime"].IsEmpty() && queryParam["CreateEndTime"].IsEmpty() && queryParam["AssignMark"].IsEmpty())
            {
                //��������
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    DateTime startTime = queryParam["StartTime"].ToDate();
                    DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                    strSql += " and ObtainDate BETWEEN '" + startTime + "' and '" + endTime + "'";
                }
                else
                {
                    //Ĭ����ʾ��ǰ�����˵�δ��������
                    DateTime startTime = DateTime.Now.Date;
                    DateTime endTime = DateTime.Now.Date.AddDays(1);
                    strSql += " and CallTime IS NULL ";
                }
                //�ֻ���
                if (!queryParam["Telphone"].IsEmpty())
                {
                    string Telphone = queryParam["Telphone"].ToString();
                    strSql += " and Telphone like '%" + Telphone + "%'";
                }
                //�Ŷ�
                if (!queryParam["Number"].IsEmpty())
                {
                    string Telphone = queryParam["Number"].ToString();
                    strSql += " and Number = '" + Telphone + "'";
                }
                //���н��
                if (!queryParam["CallResult"].IsEmpty())
                {
                    int CallResult = queryParam["CallResult"].ToInt();
                    strSql += " and CallResult = " + CallResult;
                }

                //����ͻ�
                if (!queryParam["IntentionMark"].IsEmpty())
                {
                    int IntentionMark = queryParam["IntentionMark"].ToInt();
                    strSql += " and IntentionMark = " + IntentionMark;
                }
                //�۳�״̬
                if (!queryParam["SellMark"].IsEmpty())
                {
                    int SellMark = queryParam["SellMark"].ToInt();
                    strSql += " and SellMark = " + SellMark;
                }
                //δ��ͨ��ʽ
                if (!queryParam["NoConnectSelect"].IsEmpty())
                {
                    string NoConnectSelect = queryParam["NoConnectSelect"].ToString();
                    strSql += " and NoConnectMark = " + NoConnectSelect;
                }
                //���ɽ�����
                if (!queryParam["NoDealSelect"].IsEmpty())
                {
                    string NoDealSelect = queryParam["NoDealSelect"].ToString();
                    strSql += " and NoDealMark = " + NoDealSelect;
                }

                //������
                if (!queryParam["SellerId"].IsEmpty())
                {
                    string SellerId = queryParam["SellerId"].ToString();
                    strSql += " and SellerId = '" + SellerId + "'";
                }
                else
                {
                    if (!OperatorProvider.Provider.Current().IsSystem)
                    {
                        string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                        strSql += " and SellerId in (" + dataAutor + ")";

                    }
                }

            }



            return this.BaseRepository().FindList(strSql.ToString(), pagination);
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<TelphoneWashEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public TelphoneWashEntity GetEntity(int? keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
        public void RemoveForm(int? keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        public void SaveForm(int? keyValue, TelphoneWashEntity entity)
        {
            if (keyValue != null)
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                entity.Create();
                this.BaseRepository().Insert(entity);
            }
        }

        /// <summary>
        /// ������������
        /// </summary>
        /// <param name="dtSource">ʵ�����</param>
        /// <returns></returns>
        public string BatchAddEntity(DataTable dtSource)
        {

            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            
            try
            {
                for (int i = 0; i < dtSource.Rows.Count; i++)
                {
                    TelphoneWashEntity entity = new TelphoneWashEntity();
                    string telphone = dtSource.Rows[i][0].ToString();
                    if (telphone.Length == 11)
                    {
                        var wash_Data = db.FindEntity<TelphoneWashEntity>(t => t.Telphone == telphone);
                        if (wash_Data!=null)
                        {
                            return telphone+"�ظ����룡";
                        }
                        entity.Telphone = telphone.ToString();
                        entity.Grade = telphone.Substring(3, 4);
                        entity.Number = telphone.Substring(7);
                        entity.DeleteMark = 0;
                        entity.SellMark = 0;
                        entity.IntentionMark = 0;
                        entity.AssignMark = 0;
                        entity.Create();
                        db.Insert(entity);
                    }

                }
                db.Commit();
                return "����ɹ�";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <returns></returns>
        public int GetTelphone(int getCount)
        {
            //1.�Ȳ鿴��Ա�������Ƿ��Ѿ���ȡ��(�Ƿ����300)
            string userid = OperatorProvider.Provider.Current().UserId;
            string username = OperatorProvider.Provider.Current().UserName;
            string strSql = "SELECT COUNT(*) FROM TelphoneWash WHERE 1=1 AND SellerId ='" + userid + "' AND   datediff(day,[ObtainDate],getdate())=0";
            DataTable dt = this.BaseRepository().FindTable(strSql.ToString());
            //�Ѿ���ȡ�ĸ���
            int ges = int.Parse(dt.Rows[0][0].ToString());
            //ÿ������ȡ��
            int maxGet = int.Parse(Config.GetValue("maxGet"));
            //С�ڵ�������ȡ��300�ſ��Ի�ȡ
            if (ges + getCount <= maxGet)
            {


                //��ȡһ�����ݿ���û�з���ĺ��룬
                string strSql1 = "SELECT * FROM TelphoneWash WHERE 1=1 AND AssignMark <>1  AND DeleteMark<>1 ";
                DataTable dts = this.BaseRepository().FindTable(strSql1.ToString());
                //dts.Rows.Count�����ʣ��������
                if (dts.Rows.Count > getCount)
                {
                    #region ˳���ȡ
                    string strSql2 = " set rowcount " + getCount + " update TelphoneWash set sellerid='" + userid + "',sellername='" + username + "',obtaindate=getdate() ,AssignMark=1 WHERE AssignMark  <>1 set rowcount 0 ";
                    int dts2 = this.BaseRepository().ExecuteBySql(strSql2.ToString());

                    #endregion
                    #region �����ȡ
                    //2.û��ȡ���������ȡ100������������ǰԱ��
                    //Random rd = new Random();
                    //List<int> gint = new List<int>();
                    //for (int i = 0; i < maxGet; i++)
                    //{
                    //    //�����ȡһ����
                    //    int dd = rd.Next(dts.Rows.Count);
                    //    //�жϵ�ǰ���Ƿ��ù�
                    //    if (gint.Contains(dd))
                    //    {
                    //        i--;
                    //        continue;
                    //    }
                    //    else
                    //    {
                    //        //���ظ����޸ĵ�ǰʵ�壬���з���
                    //        gint.Add(dd);
                    //        DataRow dtr = dts.Rows[dd];
                    //        int keyValue = int.Parse(dtr["TelphoneID"].ToString());
                    //        TelphoneWashEntity oldEntity = this.BaseRepository().FindEntity(keyValue);
                    //        oldEntity.SellerId = userid;
                    //        oldEntity.SellerName = username;
                    //        oldEntity.AssignMark = 1;
                    //        oldEntity.ObtainDate = DateTime.Now;
                    //        oldEntity.Modify(keyValue);
                    //        this.BaseRepository().Update(oldEntity);
                    //    }
                    //}
                    #endregion
                    return 1;
                }
                else
                {
                    return 0;//ϴ�ų����ݲ���
                }
            }
            else
            {
                return 2;//�����Ѿ���ȡ��
            }
        }
        #endregion
    }
}
