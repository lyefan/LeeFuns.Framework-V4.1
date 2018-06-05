using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LeeFuns.Entity;
using LeeFuns.Repository;

namespace LeeFuns.Business.CommonModule.Oracle
{
    /// <summary>
    /// 模块设置
    /// <author>
    ///		<name>Nyee</name>
    ///		<date>2015.06.16 13:48</date>
    /// </author>
    /// </summary>
    public class Base_ModuleBll : RepositoryFactory<Base_Module>
    {
        public List<Base_Module> GetList()
        {
            return this.Repository().FindList("ORDER BY ParentId ASC,SortCode ASC");
        }

        //public List<Base_Module> GetListBySql(string sql)
        //{
        //    return this.Repository().FindListBySql(sql);
        //}

    }
}
