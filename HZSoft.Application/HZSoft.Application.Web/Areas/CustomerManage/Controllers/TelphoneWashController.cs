using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.Busines.CustomerManage;
using HZSoft.Util;
using HZSoft.Util.WebControl;
using System.Web.Mvc;
using System.Web;
using System.IO;
using System;
using System.Data.OleDb;
using System.Data;
using HZSoft.Util.Offices;
using System.Configuration;
using System.Data.SqlClient;

namespace HZSoft.Application.Web.Areas.CustomerManage.Controllers
{
    /// <summary>
    /// �� �� 6.1
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2017-09-16 16:41
    /// �� ���������
    /// </summary>
    public class TelphoneWashController : MvcControllerBase
    {
        private TelphoneWashBLL telphonesourcebll = new TelphoneWashBLL();

        #region ��ͼ����
        /// <summary>
        /// �б�ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult TelphoneWashIndex()
        {
            return View();
        }
        /// <summary>
        /// ��ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult TelphoneWashForm()
        {
            return View();
        }
        #endregion

        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ����</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = telphonesourcebll.GetPageList(pagination, queryJson);
            var jsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return ToJsonResult(jsonData);
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = telphonesourcebll.GetList(queryJson);
            return ToJsonResult(data);
        }
        /// <summary>
        /// ��ȡʵ�� 
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns>���ض���Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(int keyValue)
        {
            var data = telphonesourcebll.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveForm(int? keyValue)
        {
            telphonesourcebll.RemoveForm(keyValue);
            return Success("ɾ���ɹ���");
        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(int? keyValue, TelphoneWashEntity entity)
        {
            telphonesourcebll.SaveForm(keyValue, entity);
            return Success("�����ɹ���");
        }
        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <returns></returns>
        public ActionResult GetTelphone(string getCountStr)
        {
            int getCount = Convert.ToInt32(getCountStr);
            int state= telphonesourcebll.GetTelphone(getCount);
            if (state==0)
            {
                return Success("ϴ�ų����ݲ��㡣");
            }
            else if (state == 1)
            {
                return Success("��ȡ�ɹ���");
            }
            else
            {
                return Success("�����Ѿ���������ȡ����");
            }
        }
        #endregion


        #region ���������������

        public FileResult GetFile()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "Resource/ExcelTemplate/";
            string fileName = "����������ģ��.xlsx";
            return File(path + fileName, "application/ms-excel", fileName);
        }

        public ActionResult WashImport()
        {
            return View();
        }

        [HttpPost]
        public ActionResult WashImport(HttpPostedFileBase filebase)
        {
            HttpPostedFileBase file = Request.Files["files"];
            string FileName;
            string savePath;
            if (file == null || file.ContentLength <= 0)
            {
                ViewBag.error = "�ļ�����Ϊ��";
                return View();
            }
            else
            {
                string filename = Path.GetFileName(file.FileName);
                int filesize = file.ContentLength;//��ȡ�ϴ��ļ��Ĵ�С��λΪ�ֽ�byte
                string fileEx = System.IO.Path.GetExtension(filename);//��ȡ�ϴ��ļ�����չ��
                string NoFileName = System.IO.Path.GetFileNameWithoutExtension(filename);//��ȡ����չ�����ļ���
                int Maxsize = 4000 * 1024;//�����ϴ��ļ������ռ��СΪ4M
                string FileType = ".xls,.xlsx";//�����ϴ��ļ��������ַ���

                FileName = NoFileName + DateTime.Now.ToString("yyyyMMddhhmmss") + fileEx;
                if (!FileType.Contains(fileEx))
                {
                    ViewBag.error = "�ļ����Ͳ��ԣ�ֻ�ܵ���xls��xlsx��ʽ���ļ�";
                    return View();
                }
                if (filesize >= Maxsize)
                {
                    ViewBag.error = "�ϴ��ļ�����4M�������ϴ�";
                    return View();
                }
                string path = AppDomain.CurrentDomain.BaseDirectory + "Resource/ExcelData/";
                savePath = Path.Combine(path, FileName);
                file.SaveAs(savePath);
            }

            #region ��ͳ
            ////string result = string.Empty;
            //string strConn;
            //strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + savePath + ";" + "Extended Properties=Excel 8.0";
            //OleDbConnection conn = new OleDbConnection(strConn);
            //conn.Open();
            //OleDbDataAdapter myCommand = new OleDbDataAdapter("select * from [Sheet1$]", strConn);
            //DataSet myDataSet = new DataSet();
            //try
            //{
            //    myCommand.Fill(myDataSet, "ExcelInfo");
            //}
            //catch (Exception ex)
            //{
            //    ViewBag.error = ex.Message;
            //    return View();
            //}
            //DataTable table = myDataSet.Tables["ExcelInfo"].DefaultView.ToTable();

            ////����������ƣ�����ʱ������ع�
            //using (TransactionScope transaction = new TransactionScope())
            //{
            //    for (int i = 0; i < table.Rows.Count; i++)
            //    {
            //        //��ȡ��������
            //        string _areaName = table.Rows[i][0].ToString();
            //        //�жϵ����Ƿ����
            //        if (!_areaRepository.CheckAreaExist(_areaName))
            //        {
            //            ViewBag.error = "������ļ��У�" + _areaName + "���������ڣ�������Ӹõ���";
            //            return View();
            //        }
            //        else
            //        {
            //            Wash station = new Wash();
            //            station.AreaID = _areaRepository.GetIdByAreaName(_areaName).AreaID;
            //            station.WashName = table.Rows[i][1].ToString();
            //            station.TerminaAddress = table.Rows[i][2].ToString();
            //            station.CapacityGrade = table.Rows[i][3].ToString();
            //            station.OilEngineCapacity = decimal.Parse(table.Rows[i][4].ToString());
            //            _stationRepository.AddWash(station);
            //        }
            //    }
            //    transaction.Complete();
            //} 
            #endregion

            //excelתDataTable
            DataTable dtSource = ExcelHelper.ExcelImport(savePath);
            //��������TelphoneWash��
            //SqlBulkCopyByDatatable("TelphoneWash", dtSource);
            //һ���в���

            //����������ƣ�����ʱ������ع�
            ViewBag.error = telphonesourcebll.BatchAddEntity(dtSource);
            
            System.Threading.Thread.Sleep(2000);
            return View();
            //return RedirectToAction("TelphoneWashIndex");
        }

        /// <summary>
        /// ��datatable����Ĵ��������������Ƶ����ݿ��У������õ�����������
        /// </summary>
        /// <param name="TableName">Ŀ���</param>
        /// <param name="dt">Դ����</param>
        private void SqlBulkCopyByDatatable(string TableName, DataTable dt)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["BaseDb"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlBulkCopy sqlbulkcopy = new SqlBulkCopy(connectionString, SqlBulkCopyOptions.UseInternalTransaction))
                {
                    try
                    {
                        sqlbulkcopy.DestinationTableName = TableName;
                        sqlbulkcopy.ColumnMappings.Add("Telphone", "Telphone");//�Զ����DataTable�����ݿ����ֶ����Ӧ
                        //for (int i = 0; i < dt.Columns.Count; i++)
                        //{
                        //    sqlbulkcopy.ColumnMappings.Add(dt.Columns[i].ColumnName, dt.Columns[i].ColumnName);
                        //}
                        sqlbulkcopy.WriteToServer(dt);
                    }
                    catch (System.Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }
        #endregion
    }
}
