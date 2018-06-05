//=====================================================================================
// All Rights Reserved , Copyright @ LeeFuns 2014
// Software Developers @ LeeFuns 2014
//=====================================================================================

using LeeFuns.DataAccess;
using LeeFuns.Entity;
using LeeFuns.Repository;
using LeeFuns.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace LeeFuns.Business
{
    /// <summary>
    /// ���Ź���
    /// <author>
    ///		<name>she</name>
    ///		<date>2014.08.07 12:34</date>
    /// </author>
    /// </summary>
    public class Base_DepartmentBll : RepositoryFactory<Base_Department>
    {
        /// <summary>
        /// ��ȡ ��˾������ �б�
        /// </summary>
        /// <returns></returns>
        public DataTable GetTree()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  *
                            FROM    ( SELECT    CompanyId,				--��˾ID
												CompanyId AS DepartmentId ,--����ID
                                                ModuleCode ,			--����
                                                FullName ,				--����
                                                ParentId ,				--�ڵ�ID
                                                SortCode,				--�������
                                                'Company' AS Sort		--����
                                      FROM      Base_Company			--��˾��
                                      UNION
                                      SELECT    CompanyId,				--��˾ID
												DepartmentId,			--����ID
                                                Code ,					--����
                                                FullName ,				--����
                                                CompanyId AS ParentId ,	--�ڵ�ID
                                                SortCode,				--�������
                                                'Department' AS Sort	--����
                                      FROM      Base_Department			--���ű�ParentId=0
                                    ) T WHERE 1=1 ");
            if (!ManageProvider.Provider.Current().IsSystem)
            {
                strSql.Append(" AND ( DepartmentId IN ( SELECT ResourceId FROM Base_DataScopePermission WHERE");
                strSql.Append(" ObjectId IN ('" + ManageProvider.Provider.Current().ObjectId.Replace(",", "','") + "') ");
                strSql.Append(" ) )");
            }
            strSql.Append(" ORDER BY SortCode ASC");
            return Repository().FindTableBySql(strSql.ToString());
        }
        /// <summary>
        /// ���ݹ�˾id��ȡ���� �б�
        /// </summary>
        /// <param name="CompanyId">��˾ID</param>
        /// <returns></returns>
        public DataTable GetList( ref JqGridParam jqgridparam)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  *
                            FROM    ( SELECT    d.DepartmentId ,			--����
                                                c.FullName AS CompanyName ,	--������˾
                                                d.CompanyId ,				--������˾Id
                                                d.Code ,					--����
                                                d.FullName ,				--��������
                                                d.ShortName ,				--���ż��
                                                d.Nature ,					--��������
                                                d.Manager ,					--������
                                                d.Phone ,					--�绰
                                                d.Fax ,						--����
                                                d.Enabled ,					--��Ч
                                                d.SortCode,                 --������
                                                d.Remark					--˵��
                                      FROM      Base_Department d
                                                LEFT JOIN Base_Company c ON c.CompanyId = d.CompanyId
                                    ) T WHERE 1=1 ");
            List<DbParameter> parameter = new List<DbParameter>();
            return Repository().FindTablePageBySql(strSql.ToString(), parameter.ToArray(), ref jqgridparam);
            //return Repository().FindTableBySql(strSql.ToString(), parameter.ToArray());
        }
    }
}