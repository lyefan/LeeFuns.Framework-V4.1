using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace LeeFuns.Utilities
{
    public static class TreeJson
    {
        public static string TreeToJson(this IList list, string ParentId = "0")
        {
            StringBuilder builder = new StringBuilder();
            List<TreeJsonEntity> list2 = DataHelper.IListToList<TreeJsonEntity>(list).FindAll(t => t.parentId == ParentId);
            builder.Append("[");
            if (list2.Count > 0)
            {
                foreach (TreeJsonEntity entity in list2)
                {
                    builder.Append("{");
                    builder.Append("\"id\":\"" + entity.id + "\",");
                    builder.Append("\"text\":\"" + entity.text.Replace("&nbsp;", "") + "\",");
                    builder.Append("\"value\":\"" + entity.value + "\",");
                    if (!((entity.Attribute == null) || string.IsNullOrEmpty(entity.Attribute)))
                    {
                        builder.Append("\"" + entity.Attribute + "\":\"" + entity.AttributeValue + "\",");
                    }
                    if (!((entity.AttributeA == null) || string.IsNullOrEmpty(entity.AttributeA)))
                    {
                        builder.Append("\"" + entity.AttributeA + "\":\"" + entity.AttributeValueA + "\",");
                    }
                    if (!((entity.img == null) || string.IsNullOrEmpty(entity.img.Replace("&nbsp;", ""))))
                    {
                        builder.Append("\"img\":\"" + entity.img.Replace("&nbsp;", "") + "\",");
                    }
                    if (entity.checkstate.HasValue)
                    {
                        builder.Append("\"checkstate\":" + entity.checkstate + ",");
                    }
                    builder.Append("\"showcheck\":" + entity.showcheck.ToString().ToLower() + ",");
                    builder.Append("\"isexpand\":" + entity.isexpand.ToString().ToLower() + ",");
                    if (entity.complete)
                    {
                        builder.Append("\"complete\":" + entity.complete.ToString().ToLower() + ",");
                    }
                    builder.Append("\"hasChildren\":" + entity.hasChildren.ToString().ToLower() + ",");
                    builder.Append("\"ChildNodes\":" + list.TreeToJson(entity.id));
                    builder.Append("},");
                }
                builder = builder.Remove(builder.Length - 1, 1);
            }
            builder.Append("]");
            return builder.ToString();
        }
    }
}

