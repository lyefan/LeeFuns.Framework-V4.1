using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Collections;

namespace LeeFuns.DataAccess
{
    public interface IDatabase: IDisposable
    {
        /// <summary>
        /// 开始事务
        /// DbTransaction类为事务的基类
        /// </summary>
        /// <returns>DbTransaction</returns>
        DbTransaction BeginTrans();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt">DataTable对象</param>
        /// <returns>bool值，true/false</returns>
        bool BulkInsert(DataTable dt);

        /// <summary>
        /// 
        /// </summary>
        void Close();

        /// <summary>
        /// 
        /// </summary>
        void Commit();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Delete<T>(T entity);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyValue"></param>
        /// <returns></returns>
        int Delete<T>(object propertyValue);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyValue"></param>
        /// <returns></returns>
        int Delete<T>(object[] propertyValue);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyValue"></param>
        /// <param name="isOpenTrans"></param>
        /// <returns></returns>
        int Delete<T>(object propertyValue, DbTransaction isOpenTrans);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="ht"></param>
        /// <returns></returns>
        int Delete(string tableName, Hashtable ht);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="isOpenTrans"></param>
        /// <returns></returns>
        int Delete<T>(T entity, DbTransaction isOpenTrans);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <returns></returns>
        int Delete<T>(string propertyName, string propertyValue);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <returns></returns>
        int Delete<T>(string propertyName, object[] propertyValue);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="ht"></param>
        /// <param name="isOpenTrans"></param>
        /// <returns></returns>
        int Delete(string tableName, Hashtable ht, DbTransaction isOpenTrans);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <param name="isOpenTrans"></param>
        /// <returns></returns>
        int Delete<T>(string propertyName, string propertyValue, DbTransaction isOpenTrans);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <param name="isOpenTrans"></param>
        /// <returns></returns>
        int Delete<T>(string propertyName, object[] propertyValue, DbTransaction isOpenTrans);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <returns></returns>
        int Delete(string tableName, string propertyName, string propertyValue);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <returns></returns>
        int Delete(string tableName, string propertyName, object[] propertyValue);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <param name="isOpenTrans"></param>
        /// <returns></returns>
        int Delete(string tableName, string propertyName, string propertyValue, DbTransaction isOpenTrans);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <param name="isOpenTrans"></param>
        /// <returns></returns>
        int Delete(string tableName, string propertyName, object[] propertyValue, DbTransaction isOpenTrans);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="procName"></param>
        /// <returns></returns>
        int ExecuteByProc(string procName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="isOpenTrans"></param>
        /// <returns></returns>
        int ExecuteByProc(string procName, DbTransaction isOpenTrans);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int ExecuteByProc(string procName, DbParameter[] parameters);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="parameters"></param>
        /// <param name="isOpenTrans"></param>
        /// <returns></returns>
        int ExecuteByProc(string procName, DbParameter[] parameters, DbTransaction isOpenTrans);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        int ExecuteBySql(StringBuilder strSql);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="isOpenTrans"></param>
        /// <returns></returns>
        int ExecuteBySql(StringBuilder strSql, DbTransaction isOpenTrans);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int ExecuteBySql(StringBuilder strSql, DbParameter[] parameters);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="parameters"></param>
        /// <param name="isOpenTrans"></param>
        /// <returns></returns>
        int ExecuteBySql(StringBuilder strSql, DbParameter[] parameters, DbTransaction isOpenTrans);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        int FindCount<T>() where T : new();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="WhereSql"></param>
        /// <returns></returns>
        int FindCount<T>(string WhereSql) where T : new();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <returns></returns>
        int FindCount<T>(string propertyName, string propertyValue) where T : new();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="WhereSql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int FindCount<T>(string WhereSql, DbParameter[] parameters) where T : new();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        int FindCountBySql(string strSql);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int FindCountBySql(string strSql, DbParameter[] parameters);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="procName"></param>
        /// <returns></returns>
        DataSet FindDataSetByProc(string procName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        DataSet FindDataSetByProc(string procName, DbParameter[] parameters);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        DataSet FindDataSetBySql(string strSql);
        DataSet FindDataSetBySql(string strSql, DbParameter[] parameters);
        T FindEntity<T>(object propertyValue) where T : new();
        T FindEntity<T>(string propertyName, object propertyValue) where T : new();
        T FindEntityBySql<T>(string strSql);
        T FindEntityBySql<T>(string strSql, DbParameter[] parameters);
        T FindEntityByWhere<T>(string WhereSql) where T : new();
        T FindEntityByWhere<T>(string WhereSql, DbParameter[] parameters) where T : new();
        Hashtable FindHashtable(string tableName, StringBuilder WhereSql);
        Hashtable FindHashtable(string tableName, string propertyName, object propertyValue);
        Hashtable FindHashtable(string tableName, StringBuilder WhereSql, DbParameter[] parameters);
        Hashtable FindHashtableBySql(string strSql);
        Hashtable FindHashtableBySql(string strSql, DbParameter[] parameters);
        List<T> FindList<T>() where T : new();
        List<T> FindList<T>(string WhereSql) where T : new();
        List<T> FindList<T>(string propertyName, string propertyValue) where T : new();
        List<T> FindList<T>(string WhereSql, DbParameter[] parameters) where T : new();
        List<T> FindListBySql<T>(string strSql);
        List<T> FindListBySql<T>(string strSql, DbParameter[] parameters);
        List<T> FindListPage<T>(string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount) where T : new();
        List<T> FindListPage<T>(string WhereSql, string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount) where T : new();
        List<T> FindListPage<T>(string WhereSql, DbParameter[] parameters, string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount) where T : new();
        List<T> FindListPageBySql<T>(string strSql, string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount);
        List<T> FindListPageBySql<T>(string strSql, DbParameter[] parameters, string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount);
        List<T> FindListTop<T>(int Top) where T : new();
        List<T> FindListTop<T>(int Top, string WhereSql) where T : new();
        List<T> FindListTop<T>(int Top, string propertyName, string propertyValue) where T : new();
        List<T> FindListTop<T>(int Top, string WhereSql, DbParameter[] parameters) where T : new();
        object FindMax<T>(string propertyName) where T : new();
        object FindMax<T>(string propertyName, string WhereSql) where T : new();
        object FindMax<T>(string propertyName, string WhereSql, DbParameter[] parameters) where T : new();
        object FindMaxBySql(string strSql);
        object FindMaxBySql(string strSql, DbParameter[] parameters);
        DataTable FindTable<T>() where T : new();
        DataTable FindTable<T>(string WhereSql) where T : new();
        DataTable FindTable<T>(string WhereSql, DbParameter[] parameters) where T : new();
        DataTable FindTableByProc(string procName);
        DataTable FindTableByProc(string procName, DbParameter[] parameters);
        DataTable FindTableBySql(string strSql);
        DataTable FindTableBySql(string strSql, DbParameter[] parameters);
        DataTable FindTablePage<T>(string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount) where T : new();
        DataTable FindTablePage<T>(string WhereSql, string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount) where T : new();
        DataTable FindTablePage<T>(string WhereSql, DbParameter[] parameters, string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount) where T : new();
        DataTable FindTablePageBySql(string strSql, string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount);
        DataTable FindTablePageBySql(string strSql, DbParameter[] parameters, string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount);
        DataTable FindTableTop<T>(int Top) where T : new();
        DataTable FindTableTop<T>(int Top, string WhereSql) where T : new();
        DataTable FindTableTop<T>(int Top, string WhereSql, DbParameter[] parameters) where T : new();
        int Insert<T>(T entity);
        int Insert<T>(List<T> entity);
        int Insert(string tableName, Hashtable ht);
        int Insert<T>(T entity, DbTransaction isOpenTrans);
        int Insert<T>(List<T> entity, DbTransaction isOpenTrans);
        int Insert(string tableName, Hashtable ht, DbTransaction isOpenTrans);
        void Rollback();
        int Update<T>(T entity);
        int Update<T>(List<T> entity);
        int Update<T>(T entity, DbTransaction isOpenTrans);
        int Update<T>(string propertyName, string propertyValue);
        int Update<T>(List<T> entity, DbTransaction isOpenTrans);
        int Update(string tableName, Hashtable ht, string propertyName);
        int Update<T>(string propertyName, string propertyValue, DbTransaction isOpenTrans);
        int Update(string tableName, Hashtable ht, string propertyName, DbTransaction isOpenTrans);

        bool InTransaction { get; set; }
    }
}
