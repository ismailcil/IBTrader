using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using IB.Core.Extensions;
using IB.Core.Model;
using IB.Core.SqlQueryBuilder.Clauses;
using IB.Core.SqlQueryBuilder;

namespace IB.Core.Db
{
    public static class DbClass
    {
        public static string CreateQuery(string tableName, List<WhereClause> lstWhereClause, int? topN, OrderByClause? orderBy, List<JoinClause> lstJoinClause, string[] selectedColums, params string[] groupBy)
        {
            var query = new SelectQueryBuilder();
            query.SelectFromTable(tableName);
            if (lstJoinClause != null && lstJoinClause.Count > 0)
            {
                foreach (var item in lstJoinClause)
                {
                    query.AddJoin(item);
                }
            }

            if (selectedColums.Length > 0)
                query.SelectColumns(selectedColums);
            else
                query.SelectAllColumns();
            if (topN.HasValue)
                query.TopRecords = topN.Value;

            if (lstWhereClause != null && lstWhereClause.Count > 0)
            {
                foreach (var item in lstWhereClause)
                {
                    query.AddWhere(item);
                }
            }
            if (orderBy.HasValue)
            {
                query.AddOrderBy(orderBy.Value);
            }
            if (groupBy.Length > 0)
                query.GroupBy(groupBy);


            string statement = query.BuildQuery();
            return statement;
        }
    
        private static MdlSql GetSqlModel(string dbName, string dbIp, string text, params IDbDataParameter[] param)
        {
            var dic = param.ToDictionary(itemParam => itemParam.ParameterName, itemParam => itemParam.Value);
            text = text.Replace(Environment.NewLine, string.Empty).Trim();
            return new MdlSql { SqlDatabaseName = dbName, SqlDatabaseIp = dbIp, SqlParam = dic, SqlQuery = text };
        }

        private static void HandleException(Exception ex, MdlSql logObj)
        {
            var stackTrace = new StackTrace();
            var stackFrames = stackTrace.GetFrames();
            var lsterrorStack = new List<string>();
            if (stackFrames != null)
            {
                lsterrorStack.AddRange(stackFrames.TakeWhile(itemFrame => itemFrame.GetMethod().Name != "InvokeMethod").Select(itemFrame => itemFrame.GetMethod().Name));
            }
            logObj.ErrorStackFrameMethodName = lsterrorStack;
            logObj.ErrorMessage = ex.Message;
            logObj.ErrorStackTrace = ex.StackTrace;

        }
        
        private static IDbCommand PrepareCommand(IDbConnection conn, string text, params IDbDataParameter[] param)
        {
            IDbCommand comm = new SqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = text;

            if (param != null)
            {
                foreach (var t in param)
                {
                    comm.Parameters.Add(t);
                }
            }
            return comm;
        }

        public static IDbDataParameter CreateParameter(string name, object value)
        {
            if (value == null)
            {
                value = DBNull.Value;
            }
            else if (value is DateTime)
            {
                value = ((DateTime)value).ToString("s");
            }

            else
            {
                var s = value as string;
                if (s != null)
                {
                    var param =
                        new SqlParameter(name, SqlDbType.VarChar, s.Length)
                        {
                            Value = value
                        };

                    return param;
                }
            }
            return new SqlParameter(name, value);
        }
        
        public static IDbDataParameter CreateParameter(string name, object value, ParameterDirection paramDirection)
        {
            var param = CreateParameter(name, value);
            param.Direction = paramDirection;
            return param;
        }
        
        public static bool RunSp(string text, string connectionString, params IDbDataParameter[] param)
        {
            var conn = new SqlConnection(connectionString);

            var logObj = GetSqlModel(conn.Database, conn.DataSource, text, param);
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                var comm = PrepareCommand(conn, text, param);
                var numberOfRecords = comm.ExecuteNonQuery();
                if (numberOfRecords == 0)
                    throw new Exception("NOTAFFECTED");

                return true;
            }
            catch (Exception ex)
            {
                HandleException(ex, logObj); throw new Exception("#-1"); // Unhandeled Exception;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }

        }
        
        public static bool Run(string text, string connectionString, params IDbDataParameter[] param)
        {
            var conn = new SqlConnection(connectionString);
            var logObj = GetSqlModel(conn.Database, conn.DataSource, text, param);
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                var comm = PrepareCommand(conn, text, param);
                comm.CommandType = CommandType.Text;
                var numberOfRecords = comm.ExecuteNonQuery();
                if (numberOfRecords == 0)
                    throw new Exception("NOTAFFECTED");

                return true;
            }
            catch (Exception ex)
            {
                HandleException(ex, logObj); throw new Exception("#-1"); // Unhandeled Exception;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }

        }
        
        public static bool Run(string text, string connectionString, bool ignoreFail = false, params IDbDataParameter[] param)
        {
            var conn = new SqlConnection(connectionString);
            var logObj = GetSqlModel(conn.Database, conn.DataSource, text, param);
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                var comm = PrepareCommand(conn, text, param);
                comm.CommandType = CommandType.Text;
                var numberOfRecords = comm.ExecuteNonQuery();
                if (!ignoreFail)
                {
                    if (numberOfRecords == 0)
                        throw new Exception("NOTAFFECTED");
                }

                return true;
            }
            catch (Exception ex)
            {

                HandleException(ex, logObj); throw new Exception("#-1"); // Unhandeled Exception;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }

        }
        
        public static object ReturnScalarSp(string text, string connectionString, params IDbDataParameter[] param)
        {
            var conn = new SqlConnection(connectionString);
            var logObj = GetSqlModel(conn.Database, conn.DataSource, text, param);
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                var comm = PrepareCommand(conn, text, param);
                var result = comm.ExecuteScalar();
                comm.Parameters.Clear();

                return result;
            }
            catch (Exception ex)
            {
                HandleException(ex, logObj); throw new Exception("#-1"); // Unhandeled Exception;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }
        
        public static object ReturnScalar(string text, string connectionString, params IDbDataParameter[] param)
        {
            var conn = new SqlConnection(connectionString);
            var logObj = GetSqlModel(conn.Database, conn.DataSource, text, param);
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                var comm = PrepareCommand(conn, text, param);
                comm.CommandType = CommandType.Text;
                var result = comm.ExecuteScalar();

                comm.Parameters.Clear();

                return result;
            }
            catch (Exception ex)
            {
                HandleException(ex, logObj); throw new Exception("#-1"); // Unhandeled Exception;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }

        }
        
        public static DataSet ReturnDataSet(string text, string connectionString, params IDbDataParameter[] param)
        {
            var conn = new SqlConnection(connectionString);

            var logObj = GetSqlModel(conn.Database, conn.DataSource, text, param);
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                var da = new SqlDataAdapter(text, conn)
                {
                    SelectCommand = { CommandType = CommandType.Text }
                };

                if (param != null)
                {
                    foreach (var t in param)
                    {
                        da.SelectCommand.Parameters.Add((SqlParameter)t);
                    }
                }
                var ds = new DataSet();
                da.Fill(ds);
                da.SelectCommand.Parameters.Clear();

                if (ds.Tables.Count == 0)
                    return null;

                return ds.Tables[0].Rows.Count == 0 ? null : ds;
            }
            catch (Exception ex)
            {
                HandleException(ex, logObj); throw new Exception("#-1"); // Unhandeled Exception;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }



        }
        
        public static DataSet ReturnDataSetSp(string text, string connectionString, params IDbDataParameter[] param)
        {
            var conn = new SqlConnection(connectionString);
            var logObj = GetSqlModel(conn.Database, conn.DataSource, text, param);
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                var da = new SqlDataAdapter(text, conn)
                {
                    SelectCommand = { CommandType = CommandType.StoredProcedure }
                };

                if (param != null)
                {
                    foreach (var t in param)
                    {
                        da.SelectCommand.Parameters.Add((SqlParameter)t);
                    }
                }
                var ds = new DataSet();
                da.Fill(ds);
                da.SelectCommand.Parameters.Clear();

                return ds.Tables[0].Rows.Count == 0 ? null : ds;
            }
            catch (Exception ex)
            {

                HandleException(ex, logObj); throw new Exception("#-1"); // Unhandeled Exception;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }

        }
        
        public static IDataReader ReturnExecuteReader(string text, string connectionString, params IDbDataParameter[] param)
        {
            var conn = new SqlConnection(connectionString);
            var logObj = GetSqlModel(conn.Database, conn.DataSource, text, param);
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                var comm = PrepareCommand(conn, text, param);
                comm.CommandType = CommandType.Text;
                return comm.ExecuteReader(CommandBehavior.CloseConnection);

            }
            catch (Exception ex)
            {
                HandleException(ex, logObj); throw new Exception("#-1"); // Unhandeled Exception;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }
        
        public static IDataReader ReturnExecuteReaderSp(string text, string connectionString, params IDbDataParameter[] param)
        {
            var conn = new SqlConnection(connectionString);
            var logObj = GetSqlModel(conn.Database, conn.DataSource, text, param);
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                var comm = PrepareCommand(conn, text, param);
                return comm.ExecuteReader(CommandBehavior.CloseConnection);

            }
            catch (Exception ex)
            {
                HandleException(ex, logObj); throw new Exception("#-1"); // Unhandeled Exception;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }
        
        public static bool RunBulkInsert<T>(string tableName, string connectionString, IEnumerable<T> lst, List<SqlBulkCopyColumnMapping> columnMappings = null)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var logObj = GetSqlModel(conn.Database, conn.DataSource, tableName);
                conn.Open();
                var transaction = conn.BeginTransaction();

                using (var bulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, transaction))
                {
                    //bulkCopy.BatchSize = 100;
                    bulkCopy.DestinationTableName = tableName;
                    if (columnMappings != null)
                        foreach (var itemMap in columnMappings)
                        {
                            bulkCopy.ColumnMappings.Add(itemMap);
                        }
                    try
                    {
                        bulkCopy.WriteToServer(lst.AsDataTable());
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        HandleException(ex, logObj); throw new Exception("#-1"); // Unhandeled Exception;
                    }
                    finally
                    {
                        if (conn.State != ConnectionState.Closed)
                            conn.Close();
                    }
                }


                return true;
            }
        }
        
        public static SqlTransaction GetTransaction(string connectionString)
        {
            SqlTransaction trans = null;

            var conn = new SqlConnection(connectionString);
            if (conn.State != ConnectionState.Open)
                conn.Open();
            trans = conn.BeginTransaction();

            return trans;
        }
        
        public static int RunTransaction(string text, SqlTransaction trans, bool isSp = false, params IDbDataParameter[] param)
        {
            var logObj = GetSqlModel(trans.Connection.Database, trans.Connection.DataSource, text, param);
            try
            {
                var comm = PrepareCommand(trans.Connection, text, param);
                comm.Transaction = trans;
                comm.CommandType = CommandType.Text;
                if (isSp)
                    comm.CommandType = CommandType.StoredProcedure;
                return comm.ExecuteScalar().ToInt();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                HandleException(ex, logObj);
                throw new Exception("#-1"); // Unhandeled Exception;
            }
        }
    }
}
