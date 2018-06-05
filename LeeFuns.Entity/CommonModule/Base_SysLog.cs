using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeeFuns.Utilities;

namespace LeeFuns.Entity
{    
    /// <summary>
    /// 系统日志管理
    /// <author>
    ///		<name>Nyee</name>
    ///		<date>2015.06.25 17:05</date>
    /// </author>
    /// </summary>
    public class Base_SysLog : BaseEntity
    {
        /// <summary>
        /// 日志主键
        /// </summary>
        [DisplayName("日志主键")]
        public string SysLogId { get; set; }
        /// <summary>
        /// 对象主键
        /// </summary>
        [DisplayName("对象主键")]
        public string ObjectId { get; set; }
        /// <summary>
        /// 日志类型
        /// </summary>
        [DisplayName("日志类型")]
        public string LogType { get; set; }
        /// <summary>
        /// 操作IP
        /// </summary>
        [DisplayName("操作IP")]
        public string IPAddress { get; set; }
        /// <summary>
        /// IP地址所在地址
        /// </summary>
        [DisplayName("IP地址所在地址")]
        public string IPAddressName { get; set; }
        /// <summary>
        /// 公司主键
        /// </summary>
        [DisplayName("公司主键")]
        public string CompanyId { get; set; }
        /// <summary>
        /// 部门主键
        /// </summary>
        [DisplayName("部门主键")]
        public string DepartmentId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>
        [DisplayName("创建用户主键")]
        public string CreateUserId { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        [DisplayName("创建用户")]
        public string CreateUserName { get; set; }
        /// <summary>
        /// 模块主键
        /// </summary>
        [DisplayName("模块主键")]
        public string ModuleId { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [DisplayName("描述")]
        public string Remark { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [DisplayName("状态")]
        public string Status { get; set; }

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.SysLogId = CommonHelper.GetGuid;
            this.CreateDate = DateTime.Now;
            this.CreateUserId = ManageProvider.Provider.Current().UserId;
            this.CreateUserName = ManageProvider.Provider.Current().UserName;
            //待定
            //this.IPAddress = 
            //this.IPAddressName = 
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="KeyValue"></param>
        public override void Modify(string KeyValue)
        {
            this.SysLogId = KeyValue;
            //待定
            //this.IPAddress = 
            //this.IPAddressName = 
        }
        #endregion
    }
}
