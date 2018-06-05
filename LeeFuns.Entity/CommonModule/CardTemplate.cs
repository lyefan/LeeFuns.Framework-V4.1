using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using LeeFuns.DataAccess.Attributes;

namespace LeeFuns.Entity
{
    /// <summary>
    /// 名片模板管理
    /// <author>
    ///     <name>Nyee</name>
    ///     <date>2015.06.15 15:04</date>
    /// </author>
    /// </summary>
    [Description("名片模板管理")]
    [PrimaryKey("TemplateId")]
    public class CardTemplate : BaseEntity
    {
        #region 获取/设置  字段值
        /// <summary>
        /// 名片模板主键
        /// </summary>
        [DisplayName("名片模板主键")]
        public int TemplateId { get; set; }

        /// <summary>
        /// 内容文本
        /// </summary>
        [DisplayName("内容文本")]
        public string Text { get; set; }

        /// <summary>
        /// 内容水平位置
        /// </summary>
        [DisplayName("内容水平位置")]
        public int TextAlign { get; set; }

        /// <summary>
        /// 字体颜色
        /// </summary>
        [DisplayName("字体颜色")]
        public string TextFontColor { get; set; }

        /// <summary>
        /// 字体大小
        /// </summary>
        [DisplayName("字体大小")]
        public float TextFontSize { get; set; }

        /// <summary>
        /// 字体类型
        /// </summary>
        [DisplayName("字体类型")]
        public string TextFontType { get; set; }

        /// <summary>
        /// 内容图片
        /// </summary>
        [DisplayName("内容图片")]
        public int TextImage { get; set; }

        /// <summary>
        /// 字体样式
        /// </summary>
        [DisplayName("字体样式")]
        public int TextStyle { get; set; }

        /// <summary>
        /// 字体宽度
        /// </summary>
        [DisplayName("字体宽度")]
        public int Width { get; set; }

        /// <summary>
        /// 字体高度
        /// </summary>
        [DisplayName("字体高度")]
        public int Height { get; set; }

        /// <summary>
        /// 字体X轴边距
        /// </summary>
        [DisplayName("字体X轴边距")]
        public float TextX { get; set; }

        /// <summary>
        /// 字体Y轴边距
        /// </summary>
        [DisplayName("字体Y轴边距")]
        public float TextY { get; set; }

        /// <summary>
        /// 内容属性文本
        /// </summary>
        [DisplayName("内容属性文本")]
        public string PropertyText { get; set; }

        /// <summary>
        /// 内容属性文本
        /// </summary>
        [DisplayName("是否显示")]
        public int IsShow { get; set; }

        /// <summary>
        /// CMYK值
        /// </summary>
        [DisplayName("CMYK值")]
        public string CMYK { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [DisplayName("类型")]
        public int Type { get; set; }

        /// <summary>
        /// 模板正面图片
        /// </summary>
        [DisplayName("模板正面图片")]
        public string FrontImg { get; set; }

        /// <summary>
        /// 模板反面图片
        /// </summary>
        [DisplayName("模板反面图片")]
        public string BackImg { get; set; }

        /// <summary>
        /// 模板效果图片
        /// </summary>
        [DisplayName("模板效果图片")]
        public string TemplateImg { get; set; }

        #endregion

    }
}
