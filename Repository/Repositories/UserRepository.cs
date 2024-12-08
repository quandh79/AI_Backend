using Common.Commons;
using Common.Params.Base;
using Dapper;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Repository.BCC01_EF;
using Repository.CustomModel;
using Repository.Queries;
using Repository.Utility;
using System.Linq.Dynamic.Core;

namespace Repository.Repositories
{
    public class UserRepository : BaseRepositorySql<BCC01_User>, IUserRepository
    {
        private Query<UserCustomResponse> _query;
        private readonly IStoreProcedureExcute _storeProcedure;
        public UserRepository(IStoreProcedureExcute storeProcedure) : base()
        {
            _query = new Query<UserCustomResponse>(_db);
            _storeProcedure = storeProcedure;
        }
        public async Task<List<BCC01_User>> GetAllUser()
        {
            using (var _dbContextSql = new BCC01_DbContextSql())
            {
                return await _dbContextSql.BCC01_User.ToListAsync();
            }
        }
        public async Task<bool> CheckNumberEmployeeCreate(Guid tenant_id)
        {
            bool result = true;
            var num_employees = await _db.BCC01_Tenants.Where(x => x.id == tenant_id).Select(x => x.num_employees).FirstOrDefaultAsync();
            var current_employees = await _db.BCC01_User.Where(x => x.tenant_id == tenant_id).CountAsync();
            if (current_employees >= num_employees)
            {
                result = false;
            }
            return result;
        }
        public async Task<ResponseService<bool>> CreateAndMapUserToProfile(BCC01_User user_entity)
        {
            using (IDbContextTransaction transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    // Get profile with at least permission
                    var profileToMap = await GetProfileAtLeastPermission(user_entity.tenant_id);
                    var map_profile_entity = new BCC01_MapProfileUser();
                    map_profile_entity.id = Guid.NewGuid();
                    map_profile_entity.username = user_entity.username;
                    map_profile_entity.profile_id = profileToMap;
                    map_profile_entity.description = "auto";
                    map_profile_entity.is_active = true;
                    map_profile_entity.create_time = user_entity.create_time;
                    map_profile_entity.create_by = user_entity.create_by;
                    map_profile_entity.modify_time = user_entity.modify_time;
                    map_profile_entity.modify_by = user_entity.modify_by;
                    map_profile_entity.tenant_id = user_entity.tenant_id;

                    await _db.BCC01_MapProfileUser.AddAsync(map_profile_entity);
                    await _db.BCC01_User.AddAsync(user_entity);

                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return new ResponseService<bool>();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return new ResponseService<bool>(ex);
                }
            }
        }
        // Lấy profile có ít permission nhất
        private async Task<Guid> GetProfileAtLeastPermission(Guid tenant_id)
        {
            Dictionary<Guid, int> profileAtLeasePermission = new Dictionary<Guid, int>();
            var profileByTenant = await _db.BCC01_Profile.Where(x => x.tenant_id == tenant_id).Select(x => x.id).Distinct().ToListAsync();
            var permissionByTenant = await _db.BCC01_Permission.Where(x => x.tenant_id == tenant_id).ToListAsync();
            foreach (var item in profileByTenant)
            {
                var countPermissionInProfile = permissionByTenant.Where(x => x.profile_id == item)
                                               .Sum(x =>
                                               Convert.ToInt32(x.is_allow_access) +
                                               Convert.ToInt32(x.is_allow_create) +
                                               Convert.ToInt32(x.is_allow_delete) +
                                               Convert.ToInt32(x.is_allow_edit) +
                                               Convert.ToInt32(x.is_show));

                profileAtLeasePermission.Add(item, countPermissionInProfile);
            }

            var result = profileAtLeasePermission.Aggregate((x, y) => x.Value < y.Value ? x : y).Key;
            return result;
        }
        public async Task<BCC01_RoleHierarchy> GetRoleByUser(string role_id, Guid tenant_id)
        {
            var result = await _db.BCC01_RoleHierarchy.Where(x => x.id == Guid.Parse(role_id) && x.tenant_id == tenant_id).FirstOrDefaultAsync();
            return result;
        }
        public virtual async Task<ResponseService<bool>> RemoveAgentDataAndAssignReportTo(BCC01_User current_user)
        {
            using (IDbContextTransaction transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    var profileMapByUser = await _db.BCC01_MapProfileUser.Where(x => x.username == current_user.username).ToListAsync();
                    var skillMapByUser = await _db.BCC01_MapAgentSkill.Where(x => x.username == current_user.username).ToListAsync();
                    var agentGroupContainsAgent = await _db.BCC01_MapAgentGroup.Where(x => x.username == current_user.username).ToListAsync();
                    // Remove data by user 
                    profileMapByUser.ForEach(x => _db.BCC01_MapProfileUser.Remove(x));
                    skillMapByUser.ForEach(x => _db.BCC01_MapAgentSkill.Remove(x));
                    agentGroupContainsAgent.ForEach(x => _db.BCC01_MapAgentGroup.Remove(x));
                    _db.BCC01_User.Remove(current_user);
                    // Assign report to
                    if (!string.IsNullOrEmpty(current_user.report_to))
                    {
                        var listUserReportTo = await _db.BCC01_User.Where(x => x.report_to == current_user.username).ToListAsync();
                        foreach (var item in listUserReportTo)
                        {
                            item.report_to = current_user.report_to;
                            _db.BCC01_User.Update(item);
                        }
                    }
                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return new ResponseService<bool>();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return new ResponseService<bool>(ex);
                }
            }
        }
        public async Task<ListResult<UserCustomResponse>> GetListUser(PagingParam param, string current_user)
        {
            ListResult<UserCustomResponse> listresult = new ListResult<UserCustomResponse>();
            List<UserCustomResponse> result = new List<UserCustomResponse>();
            // get list user report_to
            var list_user = await GetListUserRole(new UsernameRequest() { username = current_user, tenant_id = param.tenant_id });
            var list_user_string = list_user.data.items.Select(x => x.username).ToList();
            var query = (from u in _db.BCC01_User
                         join r in _db.BCC01_RoleHierarchy on u.role_id equals r.id
                            //join m in _db.BCC01_MapUserReportTo on u.username equals m.username into map
                            //join r_p in _db.BCC01_RoleHierarchy on r.role_parent_id equals r_p.id into r_p
                            //from r_parent in r_p.DefaultIfEmpty()

                         where list_user_string.Contains(u.username)
                         select new UserCustomResponse
                         {
                             username = u.username,
                             fullname = u.fullname,
                             phone = u.phone,
                             email = u.email,
                             description = u.description,
                             extension_number = u.extension_number,
                             role_name = r.role_name,
                             role_id = u.role_id,
                             role_parent_id = r.role_parent_id,
                             is_administrator = u.is_administrator,
                             is_rootuser = u.is_rootuser,
                             is_active = u.is_active,
                             create_time = u.create_time,
                             create_by = u.create_by,
                             modify_time = u.modify_time,
                             modify_by = u.modify_by,
                             tenant_id = u.tenant_id,
                             report_to = u.report_to,
                             avatar = u.avatar,
                             reason_deactive = u.reason_deactive,
                             block_time = u.block_time,
                             wrong_password_number = u.wrong_password_number,
                             language = u.language,
                             is_agent = u.is_agent,
                             is_supervisor = u.is_supervisor,
                             type_extension = u.type_extension,
                             is_user_ldap = u.is_user_ldap,
                             user_window = u.user_window,
                             dtv = u.dtv
                            // lstReportTo = map.ToList(),
                         }).AsQueryable();
            var sqlstring = query.ToParametrizedSql().Item1;
            param.search_list.ForEach(x =>
            {
                if (!x.name_field.Equals("role_name"))
                {
                    x.name_field = $"[b].{x.name_field}";
                }
            });
            _query.conditions = " ";
            listresult = await _query.SearchJoin(param, sqlstring);

            //using linq add lstReportTo
            foreach(var data in listresult.items)
            {
                var lstReportTo = await _db.BCC01_MapUserReportTo.Where(x => x.username == data.username && x.tenant_id == param.tenant_id).Select(x => x.username_sup).ToListAsync();
                if(lstReportTo == null)
                {
                    data.lstReportTo = new List<string>();
                }
                data.lstReportTo = lstReportTo;
            }    

            return listresult;
        }

        public async Task<ResponseService<ListResult<UserCustomResponse>>> GetListUserRole(UsernameRequest request)
        {
            ListResult<UserCustomResponse> result = new ListResult<UserCustomResponse>();
            //string cteQuery = @"
            //        WITH userlist AS
            //        (
            //            SELECT BCC01_User.*  FROM BCC01_User WHERE username = " + $"'{request.username}' " +
            //        @"UNION ALL
            //         SELECT BCC01_User.* FROM BCC01_User INNER JOIN userlist 
            //          ON BCC01_User.report_to  = userlist.username 
            //        )
            //        SELECT * FROM userlist 
            //    ";
            var parameter = new DynamicParameters(new
            {
                username = request.username,
                tenant_id = request.tenant_id
            });
            result.items = (await _storeProcedure.ExecuteReturnList<UserCustomResponse>("usp_GetAllUserReportToByUserName", parameter,1)).ToList();
            result.total = result.items.Count;
            return new ResponseService<ListResult<UserCustomResponse>>(result);
        }
        public async Task<UserCustomResponse> GetFullById(string username, Guid tenant_id)
        {
            var response = new UserCustomResponse();
            var parameters = new DynamicParameters(
                new
                {
                    username = username,
                    tenant_id = tenant_id
                });

            response = await _storeProcedure.ExecuteReturnSingle<UserCustomResponse>("usp_User_Get_User_Information", parameters,1);
            return response;
        }

        public async Task<BCC01_User> GetFullByExtension(string extension, Guid tenant_id)
        {
            using (var _dbContextSql = new BCC01_DbContextSql())
            {
                return await _dbContextSql.BCC01_User.Where(x => x.extension_number == extension && x.tenant_id ==  tenant_id).FirstOrDefaultAsync();
            }
        }
    }
}
