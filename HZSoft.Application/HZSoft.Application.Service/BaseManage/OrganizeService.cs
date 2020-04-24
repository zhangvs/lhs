using HZSoft.Application.Code;
using HZSoft.Application.Entity.AuthorizeManage;
using HZSoft.Application.Entity.BaseManage;
using HZSoft.Application.IService.BaseManage;
using HZSoft.Data.Repository;
using HZSoft.Util;
using HZSoft.Util.Extension;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HZSoft.Application.Service.BaseManage
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创建人：佘赐雄
    /// 日 期：2015.11.02 14:27
    /// 描 述：机构管理
    /// </summary>
    public class OrganizeService : RepositoryFactory<OrganizeEntity>, IOrganizeService
    {
        #region 获取数据
        /// <summary>
        /// 机构列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OrganizeEntity> GetList()
        {
            return this.BaseRepository().IQueryable().OrderByDescending(t => t.SortCode).ToList();
            
            //string strSql = "select * from Base_Organize where DeleteMark <> 1 ";
            //if (!OperatorProvider.Provider.Current().IsSystem && OperatorProvider.Provider.Current().CompanyId != "207fa1a9-160c-4943-a89b-8fa4db0547ce")
            //{
            //    string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
            //    strSql += " and OrganizeId IN( select OrganizeId from Base_User where 1=1";
            //    strSql += " and UserId in (" + dataAutor + ") GROUP BY OrganizeId )";
            //}
            //return this.BaseRepository().FindList(strSql.ToString());
        }

        /// <summary>
        /// 机构列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OrganizeEntity> GetListByIds()
        {
            string strSql = "SELECT * FROM Base_Organize  where DeleteMark <> 1 ";
            if (!OperatorProvider.Provider.Current().IsSystem)
            {
                //string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                //strSql +=" and OrganizeId IN( select OrganizeId from Base_User where 1=1";
                //strSql += " and UserId in (" + dataAutor + ") GROUP BY OrganizeId )";

                strSql += " and OrganizeId in(SELECT OrganizeId FROM Base_Organize WHERE OrganizeId='" + OperatorProvider.Provider.Current().CompanyId + "' OR ParentId ='" + OperatorProvider.Provider.Current().CompanyId + "')";
            }
            strSql += " ORDER BY SortCode ";
            return this.BaseRepository().FindList(strSql.ToString());
        }
        /// <summary>
        /// 机构实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public OrganizeEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 公司名称不能重复
        /// </summary>
        /// <param name="organizeName">公司名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistFullName(string fullName, string keyValue)
        {
            var expression = LinqExtensions.True<OrganizeEntity>();
            expression = expression.And(t => t.FullName == fullName);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.OrganizeId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        /// <summary>
        /// 外文名称不能重复
        /// </summary>
        /// <param name="enCode">外文名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistEnCode(string enCode, string keyValue)
        {
            var expression = LinqExtensions.True<OrganizeEntity>();
            expression = expression.And(t => t.EnCode == enCode);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.OrganizeId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        /// <summary>
        /// 中文名称不能重复
        /// </summary>
        /// <param name="shortName">中文名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistShortName(string shortName, string keyValue)
        {
            var expression = LinqExtensions.True<OrganizeEntity>();
            expression = expression.And(t => t.ShortName == shortName);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.OrganizeId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除机构
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            int count = this.BaseRepository().IQueryable(t => t.ParentId == keyValue).Count();
            if (count > 0)
            {
                throw new Exception("当前所选数据有子节点数据！");
            }
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存机构表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="organizeEntity">机构实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, OrganizeEntity organizeEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                organizeEntity.Modify(keyValue);
                this.BaseRepository().Update(organizeEntity);
            }
            else
            {
                organizeEntity.Create();

                organizeEntity.EnCode = Str.PinYin(organizeEntity.FullName);//登录名为机构名拼音首字母
                this.BaseRepository().Insert(organizeEntity);

                IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
                try
                {
                    //新增默认管理部门
                    DepartmentEntity department = new DepartmentEntity();
                    department.OrganizeId = organizeEntity.OrganizeId;
                    department.ParentId = "0";
                    department.EnCode = organizeEntity.EnCode + "01";
                    department.FullName = organizeEntity.FullName + "管理部";
                    department.Create();
                    db.Insert(department);

                    //新增默认靓号角色
                    RoleEntity role = new RoleEntity();
                    role.OrganizeId = organizeEntity.OrganizeId;
                    role.Category = 1;//分类1 - 角色2 - 岗位3 - 职位4 - 工作组
                    role.EnCode = organizeEntity.EnCode + "01";
                    role.FullName = organizeEntity.FullName + "管理";
                    role.Create();
                    db.Insert(role);


                    #region 授权功能
                    var AuthorizeList = db.FindList<AuthorizeEntity>(t => t.ObjectId == "48c566d6-3dfb-4b08-8f62-a662642db300");
                    foreach (AuthorizeEntity item in AuthorizeList)
                    {
                        AuthorizeEntity authorizeEntity = new AuthorizeEntity();
                        authorizeEntity.Create();
                        authorizeEntity.Category = 2;  //1 - 部门2 - 角色3 - 岗位4 - 职位5 - 工作组
                        authorizeEntity.ObjectId = role.RoleId;
                        authorizeEntity.ItemType = item.ItemType;
                        authorizeEntity.ItemId = item.ItemId;
                        authorizeEntity.SortCode = item.SortCode;
                        db.Insert(authorizeEntity);
                    }
                    #endregion

                    #region 数据权限
                    var authorizeDataList = db.FindList<AuthorizeDataEntity>(t => t.ObjectId == "48c566d6-3dfb-4b08-8f62-a662642db300");
                    foreach (AuthorizeDataEntity item in authorizeDataList)
                    {
                        AuthorizeDataEntity authorizeDataEntity = new AuthorizeDataEntity();
                        authorizeDataEntity.Create();
                        authorizeDataEntity.AuthorizeType = item.AuthorizeType; //授权类型: 1 - 仅限本人2 - 仅限本人及下属3 - 所在部门4 - 所在公司5 - 按明细设置
                        authorizeDataEntity.Category = 2; //对象分类: 1 - 部门2 - 角色3 - 岗位4 - 职位5 - 工作组
                        authorizeDataEntity.ObjectId = role.RoleId;
                        authorizeDataEntity.IsRead = item.IsRead;
                        authorizeDataEntity.SortCode = item.SortCode;
                        db.Insert(authorizeDataEntity);
                    }
                    #endregion




                    //新增默认用户
                    UserEntity userEntity = new UserEntity();
                    userEntity.Create();
                    userEntity.Account= organizeEntity.EnCode;//登录名为机构名拼音首字母
                    userEntity.RealName = organizeEntity.FullName;
                    userEntity.OrganizeId = organizeEntity.OrganizeId;
                    userEntity.DepartmentId = department.DepartmentId;
                    userEntity.RoleId = role.RoleId;
                    userEntity.Secretkey = Md5Helper.MD5(CommonHelper.CreateNo(), 16).ToLower();
                    userEntity.Password = Md5Helper.MD5(DESEncrypt.Encrypt(Md5Helper.MD5("0000", 32).ToLower(), userEntity.Secretkey).ToLower(), 32).ToLower();
                    db.Insert(userEntity);

                    //新增默认用户关系
                    UserRelationEntity userRelationEntity = new UserRelationEntity();
                    userRelationEntity.Create();
                    userRelationEntity.Category = 2;//登录名为机构名拼音首字母
                    userRelationEntity.UserId = userEntity.UserId;
                    userRelationEntity.ObjectId = userEntity.RoleId;
                    db.Insert(userRelationEntity);

                    db.Commit();
                }
                catch (Exception)
                {
                    db.Rollback();
                    throw;
                }



            }
        }
        #endregion
    }
}
