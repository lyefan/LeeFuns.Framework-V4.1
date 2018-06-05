using LeeFuns.DataAccess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Data.Common;

namespace LeeFuns.Utilities
{
    public class ConditionBuilder
    {
        public static string GetWhereSql(IList conditions, out List<DbParameter> parameter, string orderField = "", string orderType = "")
        {
            string str = DbFactory.CreateDbParmCharacter();
            List<DbParameter> list = new List<DbParameter>();
            StringBuilder builder = new StringBuilder();
            if (conditions.Count > 0)
            {
                builder.Append(" AND");
            }
            int num = 0;
            foreach (Condition condition in conditions)
            {
                DateTime dateTime;
                DateTime time2;
                if (condition.ParamValue == null)
                {
                    continue;
                }
                string str2 = "";
                if (string.IsNullOrEmpty(condition.Logic))
                {
                    str2 = "";
                }
                else
                {
                    str2 = (condition.Logic == "AND") ? "AND" : "OR";
                }
                if ((conditions.Count - 1) == num)
                {
                    str2 = "";
                }
                string paramName = condition.ParamName;
                string str4 = condition.ParamName + num;
                int operation = (int) condition.Operation;
                switch (condition.Operation)
                {
                    case ConditionOperate.Equal:
                        builder.Append(" " + condition.LeftBrace + paramName + " = " + str + str4 + condition.RightBrace + " " + str2);
                        list.Add(DbFactory.CreateDbParameter(str + str4, condition.ParamValue));
                        break;

                    case ConditionOperate.NotEqual:
                        builder.Append(" " + condition.LeftBrace + paramName + " <> " + str + str4 + condition.RightBrace + " " + str2);
                        list.Add(DbFactory.CreateDbParameter(str + str4, condition.ParamValue));
                        break;

                    case ConditionOperate.Greater:
                        builder.Append(" " + condition.LeftBrace + paramName + " > " + str + str4 + condition.RightBrace + " " + str2);
                        list.Add(DbFactory.CreateDbParameter(str + str4, condition.ParamValue));
                        break;

                    case ConditionOperate.GreaterThan:
                        builder.Append(" " + condition.LeftBrace + paramName + " >= " + str + str4 + condition.RightBrace + " " + str2);
                        list.Add(DbFactory.CreateDbParameter(str + str4, condition.ParamValue));
                        break;

                    case ConditionOperate.Less:
                        builder.Append(" " + condition.LeftBrace + paramName + " < " + str + str4 + condition.RightBrace + " " + str2);
                        list.Add(DbFactory.CreateDbParameter(str + str4, condition.ParamValue));
                        break;

                    case ConditionOperate.LessThan:
                        builder.Append(" " + condition.LeftBrace + paramName + " <= " + str + str4 + condition.RightBrace + " " + str2);
                        list.Add(DbFactory.CreateDbParameter(str + str4, condition.ParamValue));
                        break;

                    case ConditionOperate.Null:
                        builder.Append(string.Format(" " + condition.LeftBrace + "{0} is null ", paramName) + condition.RightBrace + " " + str2);
                        break;

                    case ConditionOperate.NotNull:
                        builder.Append(string.Format(" " + condition.LeftBrace + "{0} is not null ", paramName) + condition.RightBrace + " " + str2);
                        break;

                    case ConditionOperate.Like:
                        builder.Append(" " + condition.LeftBrace + paramName + " like " + str + str4 + condition.RightBrace + " " + str2);
                        list.Add(DbFactory.CreateDbParameter(str + str4, "%" + condition.ParamValue + "%"));
                        break;

                    case ConditionOperate.NotLike:
                        builder.Append(" " + condition.LeftBrace + paramName + " not like " + str + str4 + condition.RightBrace + " " + str2);
                        list.Add(DbFactory.CreateDbParameter(str + str4, "%" + condition.ParamValue + "%"));
                        break;

                    case ConditionOperate.LeftLike:
                        builder.Append(" " + condition.LeftBrace + paramName + " like " + str + str4 + condition.RightBrace + " " + str2);
                        list.Add(DbFactory.CreateDbParameter(str + str4, "%" + condition.ParamValue));
                        break;

                    case ConditionOperate.RightLike:
                        builder.Append(" " + condition.LeftBrace + paramName + " like " + str + str4 + condition.RightBrace + " " + str2);
                        list.Add(DbFactory.CreateDbParameter(str + str4, condition.ParamValue + "%"));
                        break;

                    case ConditionOperate.Yesterday:
                        dateTime = CommonHelper.GetDateTime(DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd") + " 00:00");
                        time2 = CommonHelper.GetDateTime(DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd") + " 23:59");
                        builder.Append(string.Format(" " + condition.LeftBrace + "{0} between " + str + "start{1}  AND " + str + "end_{2}" + condition.RightBrace, paramName, str4, str4) + " " + str2);
                        list.Add(DbFactory.CreateDbParameter(str + string.Format("start{0}", str4), dateTime));
                        list.Add(DbFactory.CreateDbParameter(str + string.Format("end_{0}", str4), time2));
                        break;

                    case ConditionOperate.Today:
                        dateTime = CommonHelper.GetDateTime(DateTimeHelper.GetToday() + " 00:00");
                        time2 = CommonHelper.GetDateTime(DateTimeHelper.GetToday() + " 23:59");
                        builder.Append(string.Format(" " + condition.LeftBrace + "{0} between " + str + "start{1}  AND " + str + "end_{2}" + condition.RightBrace, paramName, str4, str4) + " " + str2);
                        list.Add(DbFactory.CreateDbParameter(str + string.Format("start{0}", str4), dateTime));
                        list.Add(DbFactory.CreateDbParameter(str + string.Format("end_{0}", str4), time2));
                        break;

                    case ConditionOperate.Tomorrow:
                        dateTime = CommonHelper.GetDateTime(DateTime.Now.AddDays(1.0).ToString("yyyy-MM-dd") + " 00:00");
                        time2 = CommonHelper.GetDateTime(DateTime.Now.AddDays(1.0).ToString("yyyy-MM-dd") + " 23:59");
                        builder.Append(string.Format(" " + condition.LeftBrace + "{0} between " + str + "start{1}  AND " + str + "end_{2}" + condition.RightBrace, paramName, str4, str4) + " " + str2);
                        list.Add(DbFactory.CreateDbParameter(str + string.Format("start{0}", str4), dateTime));
                        list.Add(DbFactory.CreateDbParameter(str + string.Format("end_{0}", str4), time2));
                        break;

                    case ConditionOperate.LastWeek:
                        dateTime = CommonHelper.GetDateTime(DateTime.Now.AddDays((double) (Convert.ToInt32((int) (1 - Convert.ToInt32(DateTime.Now.DayOfWeek))) - 7)).ToString("yyyy-MM-dd") + " 00:00");
                        time2 = CommonHelper.GetDateTime(dateTime.AddDays(6.0).ToString("yyyy-MM-dd") + " 23:59");
                        builder.Append(string.Format(" " + condition.LeftBrace + "{0} between " + str + "start{1}  AND " + str + "end_{2}" + condition.RightBrace, paramName, str4, str4) + " " + str2);
                        list.Add(DbFactory.CreateDbParameter(str + string.Format("start{0}", str4), dateTime));
                        list.Add(DbFactory.CreateDbParameter(str + string.Format("end_{0}", str4), time2));
                        break;

                    case ConditionOperate.ThisWeek:
                        dateTime = CommonHelper.GetDateTime(DateTime.Now.AddDays((double) (1 - Convert.ToInt32(DateTime.Now.DayOfWeek))).ToString("yyyy-MM-dd") + " 00:00");
                        time2 = CommonHelper.GetDateTime(dateTime.AddDays(6.0).ToString("yyyy-MM-dd") + " 23:59");
                        builder.Append(string.Format(" " + condition.LeftBrace + "{0} between " + str + "start{1}  AND " + str + "end_{2}" + condition.RightBrace, paramName, str4, str4) + " " + str2);
                        list.Add(DbFactory.CreateDbParameter(str + string.Format("start{0}", str4), dateTime));
                        list.Add(DbFactory.CreateDbParameter(str + string.Format("end_{0}", str4), time2));
                        break;

                    case ConditionOperate.NextWeek:
                        dateTime = CommonHelper.GetDateTime(DateTime.Now.AddDays((double) (Convert.ToInt32((int) (1 - Convert.ToInt32(DateTime.Now.DayOfWeek))) + 7)).ToString("yyyy-MM-dd") + " 00:00");
                        time2 = CommonHelper.GetDateTime(dateTime.AddDays(6.0).ToString("yyyy-MM-dd") + " 23:59");
                        builder.Append(string.Format(" " + condition.LeftBrace + "{0} between " + str + "start{1}  AND " + str + "end_{2}" + condition.RightBrace, paramName, str4, str4) + " " + str2);
                        list.Add(DbFactory.CreateDbParameter(str + string.Format("start{0}", str4), dateTime));
                        list.Add(DbFactory.CreateDbParameter(str + string.Format("end_{0}", str4), time2));
                        break;

                    case ConditionOperate.LastMonth:
                        dateTime = CommonHelper.GetDateTime(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-01") + " 00:00");
                        time2 = CommonHelper.GetDateTime(DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddDays(-1.0).ToString("yyyy-MM-dd") + " 23:59");
                        builder.Append(string.Format(" " + condition.LeftBrace + "{0} between " + str + "start{1}  AND " + str + "end_{2}" + condition.RightBrace, paramName, str4, str4) + " " + str2);
                        list.Add(DbFactory.CreateDbParameter(str + string.Format("start{0}", str4), dateTime));
                        list.Add(DbFactory.CreateDbParameter(str + string.Format("end_{0}", str4), time2));
                        break;

                    case ConditionOperate.ThisMonth:
                        dateTime = CommonHelper.GetDateTime(DateTime.Now.ToString("yyyy-MM-01") + " 00:00");
                        time2 = CommonHelper.GetDateTime(DateTime.Now.AddMonths(1).AddDays(-1.0).ToString("yyyy-MM-dd") + " 23:59");
                        builder.Append(string.Format(" " + condition.LeftBrace + "{0} between " + str + "start{1}  AND " + str + "end_{2}" + condition.RightBrace, paramName, str4, str4) + " " + str2);
                        list.Add(DbFactory.CreateDbParameter(str + string.Format("start{0}", str4), dateTime));
                        list.Add(DbFactory.CreateDbParameter(str + string.Format("end_{0}", str4), time2));
                        break;

                    case ConditionOperate.NextMonth:
                        dateTime = CommonHelper.GetDateTime(DateTime.Now.AddMonths(1).ToString("yyyy-MM-01") + " 00:00");
                        time2 = CommonHelper.GetDateTime(DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddMonths(2).AddDays(-1.0).ToString("yyyy-MM-dd") + " 23:59");
                        builder.Append(string.Format(" " + condition.LeftBrace + "{0} between " + str + "start{1}  AND " + str + "end_{2}" + condition.RightBrace, paramName, str4, str4) + " " + str2);
                        list.Add(DbFactory.CreateDbParameter(str + string.Format("start{0}", str4), dateTime));
                        list.Add(DbFactory.CreateDbParameter(str + string.Format("end_{0}", str4), time2));
                        break;

                    case ConditionOperate.BeforeDay:
                        dateTime = CommonHelper.GetDateTime(DateTime.Now.AddDays(double.Parse("-" + condition.ParamValue.ToString())) + " 00:00");
                        time2 = CommonHelper.GetDateTime(DateTimeHelper.GetToday() + " 23:59");
                        builder.Append(string.Format(" " + condition.LeftBrace + "{0} between " + str + "start{1}  AND " + str + "end_{2}" + condition.RightBrace, paramName, str4, str4) + " " + str2);
                        list.Add(DbFactory.CreateDbParameter(str + string.Format("start{0}", str4), dateTime));
                        list.Add(DbFactory.CreateDbParameter(str + string.Format("end_{0}", str4), time2));
                        break;

                    case ConditionOperate.AfterDay:
                        dateTime = CommonHelper.GetDateTime(DateTime.Now.AddDays(double.Parse(condition.ParamValue.ToString())) + " 00:00");
                        time2 = CommonHelper.GetDateTime(DateTimeHelper.GetToday() + " 23:59");
                        builder.Append(string.Format(" " + condition.LeftBrace + "{0} between " + str + "start{1}  AND " + str + "end_{2}" + condition.RightBrace, paramName, str4, str4) + " " + str2);
                        list.Add(DbFactory.CreateDbParameter(str + string.Format("start{0}", str4), dateTime));
                        list.Add(DbFactory.CreateDbParameter(str + string.Format("end_{0}", str4), time2));
                        break;
                }
                num++;
            }
            if (!string.IsNullOrEmpty(orderField))
            {
                orderType = (orderType.ToLower() == "desc") ? "desc" : "asc";
                builder.Append(" Order By " + orderField + " " + orderType);
            }
            parameter = list;
            return builder.ToString();
        }
    }
}

