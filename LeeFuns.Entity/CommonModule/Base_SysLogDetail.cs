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
    /// 系统日志明细管理
    /// <author>
    ///		<name>Nyee</name>
    ///		<date>2015.06.25 17:00</date>
    /// </author>
    /// </summary>
    public class Base_SysLogDetail : BaseEntity
    {
        /// <summary>
        /// 系统日志明细主键
        /// </summary>
        [DisplayName("系统日志明细主键")]
        public string SysLogDetailId { get; set; }
        /// <summary>
        /// 日志主键
        /// </summary>
        [DisplayName("日志主键")]
        public string SysLogId { get; set; }
        /// <summary>
        /// 属性名称
        /// </summary>
        [DisplayName("属性名称")]
        public string PropertyName { get; set; }
        /// <summary>
        /// 属性字段
        /// </summary>
        [DisplayName("属性字段")]
        public string PropertyField { get; set; }
        /// <summary>
        /// 属性新值
        /// </summary>
        [DisplayName("属性新值")]
        public string NewValue { get; set; }
        /// <summary>
        /// 属性旧值
        /// </summary>
        [DisplayName("属性旧值")]
        public string OldValue { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime? CreateDate { get; set; }

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.SysLogDetailId = CommonHelper.GetGuid;
            this.CreateDate = DateTime.Now;
        }        
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="KeyValue"></param>
        public override void Modify(string KeyValue)
        {
            this.SysLogDetailId = KeyValue;
        }
        #endregion
    }
}
