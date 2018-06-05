using LeeFuns.Utilities;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace LeeFuns.Entity
{
    /// <summary>
    /// 创建一个EF实体框架 上下文
    /// </summary>
    public partial class DataContext : DbContext
    {
        public DataContext()
            : base("LeeFunsFramework_SqlServer")
        {
        }
        public DbSet<Base_Employee> Base_Employee { get; set; }
        public DbSet<Base_Module> Base_Module { get; set; }
        public DbSet<Base_ModulePermission> Base_ModulePermission { get; set; }
        public DbSet<Base_User> Base_User { get; set; }

        public DbSet<CardTemplate> CardTemplate { get; set; }
    }
}
