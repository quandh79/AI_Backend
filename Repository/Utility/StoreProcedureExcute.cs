using Common.Commons;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Ocsp;
using Repository.BCC01_EF;
using Repository.BCC03_EF;
using Repository.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Repository.Utility
{
    public class StoreProcedureExcute : IDisposable, IStoreProcedureExcute
    {
        public string _connectionString { get; set; }
        public IConfiguration _configuration;

        public StoreProcedureExcute()
        {
            _connectionString = ConfigHelper.Get("ConnectionStrings", "BCC03_Connection");
        }

        /// <summary>
        /// Execute store not return any values 
        /// </summary>
        /// <param name="storeProcedureName">name</param>
        /// <param name="parameters"> Array of parameters </param>
        public void ExecuteNotReturn(string storeProcedureName, DynamicParameters parameters = null,int dbOption = 0)
        {
            if (dbOption == 0) {
                using (var _dbContextSql = new BCC03_DbContextSql())
                {
                    SqlConnection sql = _dbContextSql.Database.GetDbConnection() as SqlConnection;
                    sql.Open();
                    sql.Execute(storeProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                }
            }
            else
            {
                using (var _dbContextSql = new BCC01_DbContextSql())
                {
                    SqlConnection sql = _dbContextSql.Database.GetDbConnection() as SqlConnection;
                    sql.Open();
                    sql.Execute(storeProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                }
            }
            
        }

        /// <summary>
        /// Execute store  return a type
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="storeProcedureName">name</param>
        /// <param name="parameters">Array of parameters </param>
        /// <returns></returns>
        public T ExecuteReturnScaler<T>(string storeProcedureName, DynamicParameters parameters = null)
        {
            using (var _dbContextSql = new BCC03_DbContextSql())
            {
                SqlConnection sql = _dbContextSql.Database.GetDbConnection() as SqlConnection;
                sql.Open();
                return (T)Convert.ChangeType(sql.ExecuteScalar<T>(storeProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure), typeof(T));
            }
        }

        /// <summary>
        /// Execute store return a list object
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="storeProcedureName">name</param>
        /// <param name="parameters">Array of parameters </param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> ExecuteReturnList<T>(string storeProcedureName, DynamicParameters parameters = null, int dbOption = 0)
        {
            if (dbOption == 0)
            {
                using (var _dbContextSql = new BCC03_DbContextSql())
                {
                    SqlConnection sql = _dbContextSql.Database.GetDbConnection() as SqlConnection;
                    sql.Open();
                    return await sql.QueryAsync<T>(storeProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                }
            }
            else
            {
                using (var _dbContextSql = new BCC01_DbContextSql())
                {
                    SqlConnection sql = _dbContextSql.Database.GetDbConnection() as SqlConnection;
                    sql.Open();
                    return await sql.QueryAsync<T>(storeProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                }
            }
            
        }

        /// <summary>
        /// Execute store return single object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storeProcedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<T> ExecuteReturnSingle<T>(string storeProcedureName, DynamicParameters parameters = null, int dbOption = 0)
        {
            if(dbOption == 0)
            {
                using (var _dbContextSql = new BCC03_DbContextSql())
                {
                    SqlConnection sql = _dbContextSql.Database.GetDbConnection() as SqlConnection;
                    sql.Open();
                    return await sql.QueryFirstOrDefaultAsync<T>(storeProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                }
            }
            else
            {
                using (var _dbContextSql = new BCC01_DbContextSql())
                {
                    SqlConnection sql = _dbContextSql.Database.GetDbConnection() as SqlConnection;
                    sql.Open();
                    return await sql.QueryFirstOrDefaultAsync<T>(storeProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                }
            }
        }

        /// <summary>
        /// Execute store  return a list object
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="storeProcedureName">name</param>
        /// <param name="parameters">Array of parameters </param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> ExecuteReturnListBySql<T>(string query, int dbOption = 0)
        {
            if (dbOption == 0)
            {
                using (var _dbContextSql = new BCC03_DbContextSql())
                {
                    SqlConnection sql = _dbContextSql.Database.GetDbConnection() as SqlConnection;
                    sql.Open();
                    return await sql.QueryAsync<T>(query, null, commandType: System.Data.CommandType.Text);
                }
            }
            else
            {
                using (var _dbContextSql = new BCC01_DbContextSql())
                {
                    SqlConnection sql = _dbContextSql.Database.GetDbConnection() as SqlConnection;
                    sql.Open();
                    return await sql.QueryAsync<T>(query, null, commandType: System.Data.CommandType.Text);
                }
            }
            
        }
        /// <summary>
        /// Execute store  return a list object
        /// </summary>
        /// <typeparam name="T">type of return</typeparam>
        /// <param name="storeProcedureName">name</param>
        /// <param name="parameters">Array of parameters </param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> ExecuteReturnListBySql<T>(string query, DynamicParameters parameters = null, int dbOption = 0)
        {
            if (dbOption == 0)
            {
                using (var _dbContextSql = new BCC03_DbContextSql())
                {
                    SqlConnection sql = _dbContextSql.Database.GetDbConnection() as SqlConnection;
                    sql.Open();
                    return await sql.QueryAsync<T>(query, parameters);
                }
            }
            else
            {
                using (var _dbContextSql = new BCC01_DbContextSql())
                {
                    SqlConnection sql = _dbContextSql.Database.GetDbConnection() as SqlConnection;
                    sql.Open();
                    return await sql.QueryAsync<T>(query, parameters);
                }
            }

        }
        public void Dispose()
        {

        }

    }

}
