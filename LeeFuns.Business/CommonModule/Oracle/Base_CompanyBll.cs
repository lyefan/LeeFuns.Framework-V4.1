using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LeeFuns.Entity;
using LeeFuns.Repository;
using LeeFuns.Utilities;

namespace LeeFuns.Business.CommonModule.Oracle
{
    /// <summary>
    /// 公司管理
    /// <author>
    ///     <name>Nyee</name>
    ///     <date>2015.06.16 12:02</date>
    /// </author>
    /// </summary>
    public class Base_CompanyBll : RepositoryFactory<Base_Company>
    {
        public List<Base_Company> GetList()
        {
            StringBuilder WhereSql = new StringBuilder();
            if (!ManageProvider.Provider.Current().IsSystem)
            {
                WhereSql.Append(" AND ( CompanyId IN ( SELECT ResourceId FROM Base_DataScopePermission WHERE");
                WhereSql.Append(" ObjectId IN ('" + ManageProvider.Provider.Current().ObjectId.Replace(",", "','") + "') ");
                WhereSql.Append(" ) )");
            }
            return Repository().FindList(WhereSql.ToString());
        }
             
    }
}
