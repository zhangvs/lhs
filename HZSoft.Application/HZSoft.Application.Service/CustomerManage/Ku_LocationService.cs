using HZSoft.Application.Code;
using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.IService.CustomerManage;
using HZSoft.Data.Repository;
using HZSoft.Util;
using HZSoft.Util.Extension;
using HZSoft.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace HZSoft.Application.Service.CustomerManage
{
    /// <summary>
    /// �� �� 6.1
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2018-03-26 11:54
    /// �� ���������
    /// </summary>
    public class Ku_LocationService : RepositoryFactory<Ku_LocationEntity>, Ku_LocationIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<Ku_LocationEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<Ku_LocationEntity>();
            var queryParam = queryJson.ToJObject();
            string strSql = $"select * from Ku_Location where 1=1 ";
            //��������
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and ModifyDate >= '" + startTime + "' and ModifyDate < '" + endTime + "'";
            }
            //��Ȧ
            if (!queryParam["RegeoName"].IsEmpty())
            {
                string RegeoName = queryParam["RegeoName"].ToString();
                strSql += " and RegeoName  like '%" + RegeoName + "%' ";
            }
            //����
            if (!queryParam["District"].IsEmpty())
            {
                string District = queryParam["District"].ToString();
                District = District.Replace("[", "").Replace("]", "").Replace("\"", "'");
                strSql += " and District in(" + District + ")";
            }
            //POI�ֲ�
            if (!queryParam["TypeName"].IsEmpty())
            {
                string TypeName = queryParam["TypeName"].ToString();
                TypeName = TypeName.Replace("[", "").Replace("]", "").Replace("\"", "'");
                strSql += " and TypeName in(" + TypeName + ")";
            }
            return this.BaseRepository().FindList(strSql.ToString(), pagination);
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<Ku_LocationEntity> GetList(string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            //�������
            string strOrder = "";
            string strSql = "";
            string des = OperatorProvider.Provider.Current().Description;
            //string des = "��ɽ��";
            string locationSql = LocationHelper.GetLocationSql(des);
            //������΢������Ļ�
            if (!queryParam["wxLon"].IsEmpty() && !queryParam["wxLat"].IsEmpty())
            {
                string wxLon = queryParam["wxLon"].ToString();
                string wxLat = queryParam["wxLat"].ToString();
                strSql = "select top 100 id,wxlon,wxlat,regeoname,address,count,dbo.f_GetDistance(" + wxLon + "," + wxLat + ",wxlon,wxlat) as description,picture FROM " + locationSql + " t where 1=1";
                strOrder = " order by dbo.f_GetDistance(" + wxLon + "," + wxLat + ",wxlon,wxlat) asc";
            }
            else
            {
                strSql = "select top 100 id,wxlon,wxlat,regeoname,address,count from " + locationSql + " where 1 = 1 ";
                strOrder = " ORDER BY CreateDate desc";
            }
            //������
            if (!queryParam["SearchName"].IsEmpty())
            {
                string OfficeName = queryParam["SearchName"].ToString();
                strSql += " and regeoname = '" + OfficeName + "'";
            }
            strSql += strOrder;

            return this.BaseRepository().FindList(strSql.ToString());
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public Ku_LocationEntity GetEntity(int? keyValue)
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
        public void SaveForm(int? keyValue, Ku_LocationEntity entity)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            if (keyValue !=null)
            {
                var location_Data = db.FindEntity<Ku_LocationEntity>(t => t.Id == keyValue);//��˾�ظ��ж�                                                                                           
                if (location_Data != null)
                {
                    if (entity.RegeoName!= location_Data.RegeoName)
                    {
                        //var companyList = db.FindList<Ku_CompanyEntity>(t => t.RegeoId == location_Data.RegeoId);
                        //foreach (var item in companyList)
                        //{
                        //    item.RegeoName = entity.RegeoName;
                        //}
                        db.ExecuteBySql("UPDATE dbo.Ku_Company SET RegeoName = '"+ entity.RegeoName + "' WHERE RegeoId = '"+ location_Data.RegeoId + "'");
                        db.Commit();
                    }
                }


                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                entity.Create();
                this.BaseRepository().Insert(entity);
            }
        }
        #endregion



        #region ���ݵ�ַͨ���ߵµ�ͼapiת�����꣬������ӵ�������Ϣ
        /// <summary>
        /// ���ݵ�ַͨ���ߵµ�ͼapiת�����꣬������ӵ�������Ϣ
        /// </summary>
        /// <param name="address">��ַ</param>
        public Ku_LocationEntity AddressToLocation(string address)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                int isRegeo = 0;
                Ku_LocationEntity locationData = null;
                //���ݵ�ַת������ȡ����
                GeocodesItem geo = GDAPI.AddressToGeo(address);
                //�����ӿڣ�ͨ�������Ƴ���Ȧ
                Regeocode regeo = GDAPI.GetRegeoByGeo(geo.location);
                //1.��pois,distance=0��  2.��aoi=0�ģ�
                if (regeo != null)
                {
                    List<PoisList> pois = regeo.pois;
                    List<Aois> aois = regeo.aois;

                    //1.��pois,distance=0��
                    if (pois != null)
                    {
                        PoisList poi = pois[0];
                        if (poi.distance == "0")
                        {
                            isRegeo = 1;
                            locationData = GetLocationByPOI(db, locationData, geo, regeo, poi, isRegeo);
                        }
                    }
                    //2.��aoi,distance=0��
                    if (isRegeo == 0)
                    {
                        if (aois != null)
                        {
                            Aois aoi = aois[0];//aoi���ݾ��������
                            if (aoi.distance == "0")
                            {
                                isRegeo = 2;
                                locationData = GetLocationByAOI(db, locationData, geo, regeo, aoi, isRegeo);
                            }
                        }
                    }

                    if (isRegeo == 0)
                    {
                        //3.����formatted_address��pois��
                        GeocodesItem geo2 = GDAPI.AddressToGeo(geo.formatted_address);
                        //�������ת��Ϊ���أ��У�δ֪�������һ��ת��������һ�£������ת��
                        if (geo2 != null && geo2.level != "����" && geo2.level != "��" && geo2.level != "δ֪" && geo2.location != geo.location)
                        {
                            //�����ӿڣ�ͨ�������Ƴ���Ȧ
                            Regeocode regeo2 = GDAPI.GetRegeoByGeo(geo2.location);
                            if (regeo2 != null)
                            {
                                List<PoisList> pois2 = regeo2.pois;
                                List<Aois> aois2 = regeo2.aois;
                                if (pois2 != null)
                                {
                                    PoisList poi2 = pois2[0];
                                    if (poi2.distance == "0")
                                    {
                                        isRegeo = 3;
                                        locationData = GetLocationByPOI(db, locationData, geo2, regeo2, poi2, isRegeo);
                                    }
                                }
                                //4.��aois
                                if (isRegeo == 0)
                                {
                                    if (aois2 != null)
                                    {
                                        Aois aoi2 = aois2[0];
                                        if (aoi2.distance == "0")
                                        {
                                            isRegeo = 4;
                                            locationData = GetLocationByAOI(db, locationData, geo2, regeo2, aoi2, isRegeo);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                //0.�������geo 
                if (isRegeo == 0)
                {
                    locationData = GetLocationByGeo(db, locationData, geo, 0);
                }
                return locationData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region ���λ����Ϣ���5�����

        //ͨ��poi���λ����Ϣ��(1,3)
        public Ku_LocationEntity GetLocationByPOI(IRepository db, Ku_LocationEntity locationData, GeocodesItem geo, Regeocode regeo, PoisList item, int isRegeo)
        {
            locationData = db.FindList<Ku_LocationEntity>(t => t.RegeoId == item.id).FirstOrDefault();
            if (locationData != null)
            {
                //�д����꣬����+1
                locationData.Count += 1;
                locationData.Modify(locationData.Id);
                db.Update<Ku_LocationEntity>(locationData);
            }
            else
            {
                AddressComponent addressComponent = regeo.addressComponent;
                string formatted_address = regeo.formatted_address;

                double wxLon = 0, wxLat = 0, bdLon = 0, bdLat = 0;
                GDAPI.getCoordinate(item.location, out wxLat, out wxLon, out bdLat, out bdLon);

                string typeCode, typeName;
                GetTypeCodeByName(item.type, out typeCode, out typeName);
                //�޴�����,����pos_location��
                locationData = new Ku_LocationEntity()
                {
                    Address = item.address,
                    Province = addressComponent.province,
                    City = addressComponent.city,
                    CityCode = addressComponent.citycode,
                    District = addressComponent.district,
                    AdCode = addressComponent.adcode,
                    Township = addressComponent.township,
                    Location = item.location,
                    wxLon = Convert.ToDecimal(wxLon),
                    wxLat = Convert.ToDecimal(wxLat),
                    bdLon = Convert.ToDecimal(bdLon),
                    bdLat = Convert.ToDecimal(bdLat),
                    _Level = geo.level,
                    IsRegeo = isRegeo,
                    RegeoId = item.id,
                    RegeoName = item.name,
                    TypeCode = typeCode,
                    TypeName = typeName,
                    Count = 1
                };
                locationData.Create();
                db.Insert<Ku_LocationEntity>(locationData);
            }
            return locationData;
        }

        //ͨ��aoi���λ����Ϣ��(2,4)
        public Ku_LocationEntity GetLocationByAOI(IRepository db, Ku_LocationEntity locationData, GeocodesItem geo, Regeocode regeo, Aois aoi, int isRegeo)
        {
            locationData = db.FindList<Ku_LocationEntity>(t => t.RegeoId == aoi.id).FirstOrDefault();
            if (locationData != null)
            {
                //�д����꣬����+1
                locationData.Count += 1;
                locationData.Modify(locationData.Id);
                db.Update<Ku_LocationEntity>(locationData);
            }
            else
            {
                AddressComponent addressComponent = regeo.addressComponent;

                double wxLon = 0, wxLat = 0, bdLon = 0, bdLat = 0;
                GDAPI.getCoordinate(aoi.location, out wxLat, out wxLon, out bdLat, out bdLon);

                string typeCode, typeName;
                GetTypeNameByCode(aoi.type, out typeCode, out typeName);
                //�޴�����,����pos_location��
                locationData = new Ku_LocationEntity()
                {
                    Address = regeo.formatted_address,
                    Province = addressComponent.province,
                    City = addressComponent.city,
                    CityCode = addressComponent.citycode,
                    District = addressComponent.district,
                    AdCode = addressComponent.adcode,
                    Township = addressComponent.township,
                    Location = aoi.location,
                    wxLon = Convert.ToDecimal(wxLon),
                    wxLat = Convert.ToDecimal(wxLat),
                    bdLon = Convert.ToDecimal(bdLon),
                    bdLat = Convert.ToDecimal(bdLat),
                    _Level = geo.level,
                    IsRegeo = isRegeo,
                    RegeoId = aoi.id,
                    RegeoName = aoi.name,
                    Area = aoi.area,//������ĸת��ʧ��decimal��ɽ��ʡ������ƽ���صط���ż��
                    TypeCode = typeCode,
                    TypeName = typeName,
                    Count = 1
                };
                locationData.Create();
                db.Update<Ku_LocationEntity>(locationData);

            }
            return locationData;
        }

        //ͨ��geo���λ����Ϣ��(5)
        public Ku_LocationEntity GetLocationByGeo(IRepository db, Ku_LocationEntity locationData, GeocodesItem geo, int isRegeo)
        {
            //���aoiȡ��
            locationData = db.FindList<Ku_LocationEntity>(t => t.Location == geo.location).FirstOrDefault();
            if (locationData != null)
            {
                locationData.Count += 1;
                locationData.Modify(locationData.Id);
                db.Update<Ku_LocationEntity>(locationData);
            }
            else
            {
                //���POI
                double wxLon = 0, wxLat = 0, bdLon = 0, bdLat = 0;
                GDAPI.getCoordinate(geo.location, out wxLat, out wxLon, out bdLat, out bdLon);
                //�޴�����,����pos_location��
                locationData = new Ku_LocationEntity()
                {
                    RegeoName = geo.formatted_address.Replace(geo.province, "").Replace(geo.city, "").Replace(geo.district, ""),
                    Address = geo.formatted_address,
                    Province = geo.province,
                    City = geo.city,
                    CityCode = geo.citycode,
                    District = geo.district,
                    AdCode = geo.adcode,
                    Township = geo.township,
                    Location = geo.location,
                    wxLon = Convert.ToDecimal(wxLon),
                    wxLat = Convert.ToDecimal(wxLat),
                    bdLon = Convert.ToDecimal(bdLon),
                    bdLat = Convert.ToDecimal(bdLat),
                    _Level = geo.level,
                    IsRegeo = 0,
                    Count = 1
                };
                locationData.Create();
                db.Insert<Ku_LocationEntity>(locationData);
            }

            return locationData;
        }
        #endregion

        #region �ߵ�POI����ת��
        //����code��ȡ��������
        public void GetTypeNameByCode(string typeCodes, out string typeCode, out string typeName)
        {
            typeName = ""; typeCode = typeCodes;
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            {
                //ֻƥ���һ��
                string[] codes = typeCode.Split('|');
                string typeCode1 = codes[0];
                var poiTypeData = db.FindList<POS_PoiTypeCode>(t => t.TypeCode == typeCode1).FirstOrDefault();
                if (poiTypeData != null)
                {
                    typeName = poiTypeData.SubCategory;
                    typeCode = typeCode1;
                }
            }

        }

        //���������ȡ����code
        public void GetTypeCodeByName(string typeNames, out string typeCode, out string typeName)
        {
            typeCode = ""; typeName = typeNames;
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            {
                string[] names = typeNames.Split('|');
                string typeName1 = names[0];
                if (typeName1.LastIndexOf(";") > 0)
                {
                    int sub = typeName1.LastIndexOf(";");
                    typeName1 = typeName1.Substring(sub + 1, typeName1.Length - sub - 1);
                }
                var poiTypeData = db.FindList<POS_PoiTypeCode>(t => t.SubCategory == typeName1).FirstOrDefault();
                if (poiTypeData != null)
                {
                    typeCode = poiTypeData.TypeCode;
                    typeName = typeName1;
                }
            }
        }
        #endregion

    }
}
