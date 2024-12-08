using Dapper;
using Repository.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Utility
{
    public interface IStoreProcedureExcute
    {
        string _connectionString { get; set; }
        //DbContextSql _dbContextSql { get; set; }
        //DbContextSql DbContext { get; }


        void ExecuteNotReturn(string storeProcedureName, DynamicParameters parameters = null, int dbOption = 0);
        Task<IEnumerable<T>> ExecuteReturnList<T>(string storeProcedureName, DynamicParameters parameters = null, int dbOption = 0);
        Task<IEnumerable<T>> ExecuteReturnListBySql<T>(string query, int dbOption = 0);
        Task<IEnumerable<T>> ExecuteReturnListBySql<T>(string query, DynamicParameters parameters = null,int dbOption = 0);
        T ExecuteReturnScaler<T>(string storeProcedureName, DynamicParameters parameters = null);
        Task<T> ExecuteReturnSingle<T>(string storeProcedureName, DynamicParameters parameters = null, int dbOption = 0);
    }
}