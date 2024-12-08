using Common.Commons;
using Common.Params.Base;
using Microsoft.EntityFrameworkCore;
using Repository.CustomModel;
using Repository.Queries;
using Repository.BCC01_EF;

namespace Repository.Repositories
{
    public class PermissionRepository : BaseRepositorySql<BCC01_Permission>, IPermissionRepository
    {
        private Query<BCC01_Permission> _query;
        public PermissionRepository() : base()
        {
            _query = new Query<BCC01_Permission>(_db);
        }
        public virtual async Task<List<ContainerModel>> GetListPermissionIdByProfileId(Guid profile_id)
        {
            List<ContainerModel> response = new List<ContainerModel>();
            using (var db = new BCC01_DbContextSql())
            {
                var result = await db.BCC01_Permission
                              .Where(x => x.profile_id == profile_id)
                              .Select(x => new
                              {
                                  id = x.id
                              }).ToListAsync();
                response = CommonFuncMain.ToObjectList<ContainerModel>(result);
            }
            return response;
        }
        public virtual async Task<ListResult<BCC01_Permission>> GetPermissionByProfile(PagingParam param)
        {
            ListResult<BCC01_Permission> listResult = new ListResult<BCC01_Permission>();
            List<BCC01_Permission> result = new List<BCC01_Permission>();
            using (var db = new BCC01_DbContextSql())
            {
                var profile_id = Guid.Parse(param.search_list[0].value_search.ToString());

                var sqlstring = @"SELECT 
                    [Limit1].[id] AS [id], 
                    [Limit1].[profile_id] AS [profile_id], 
                    [Limit1].[permissionobject_id] AS [permissionobject_id], 
                    [Limit1].[is_allow_access] AS [is_allow_access], 
                    [Limit1].[is_allow_create] AS [is_allow_create], 
                    [Limit1].[is_allow_edit] AS [is_allow_edit], 
                    [Limit1].[is_allow_delete] AS [is_allow_delete], 
                    [Limit1].[object_name] AS [object_name], 
                    [Limit1].[description] AS [description], 
                    [Limit1].[is_active] AS [is_active], 
                    [Limit1].[create_time] AS [create_time], 
                    [Limit1].[create_by] AS [create_by], 
                    [Limit1].[modify_time] AS [modify_time], 
                    [Limit1].[modify_by] AS [modify_by], 
                    [Limit1].[tenant_id] AS [tenant_id], 
                    [Limit1].[is_show] AS [is_show]
                    FROM    (SELECT 
                        @p__linq__0 AS [p__linq__0], 
                        @p__linq__1 AS [p__linq__1], 
                        [Distinct1].[object_name] AS [object_name]
                        FROM ( SELECT DISTINCT 
                            [m].[object_name] AS [object_name]
                            FROM [dbo].[BCC01_Permission] AS [m]
                            WHERE ([m].[tenant_id] = @p__linq__0) AND ([m].[profile_id] = @p__linq__1)
                        )  AS [Distinct1] ) AS [Project2]
                    OUTER APPLY  (SELECT TOP (1) 
                        [m0].[id] AS [id], 
                        [m0].[profile_id] AS [profile_id], 
                        [m0].[permissionobject_id] AS [permissionobject_id], 
                        [m0].[is_allow_access] AS [is_allow_access], 
                        [m0].[is_allow_create] AS [is_allow_create], 
                        [m0].[is_allow_edit] AS [is_allow_edit], 
                        [m0].[is_allow_delete] AS [is_allow_delete], 
                        [m0].[object_name] AS [object_name], 
                        [m0].[description] AS [description], 
                        [m0].[is_active] AS [is_active], 
                        [m0].[create_time] AS [create_time], 
                        [m0].[create_by] AS [create_by], 
                        [m0].[modify_time] AS [modify_time], 
                        [m0].[modify_by] AS [modify_by], 
                        [m0].[tenant_id] AS [tenant_id], 
                        [m0].[is_show] AS [is_show]
                        FROM [dbo].[BCC01_Permission] AS [m0]
                        WHERE ([m0].[tenant_id] = @p__linq__0) AND ([m0].[profile_id] = @p__linq__1) 
		                AND ([Project2].[object_name] = [m0].[object_name])) AS [Limit1]";
                sqlstring = sqlstring.Replace("@p__linq__0", $"'{param.tenant_id}'");
                sqlstring = sqlstring.Replace("@p__linq__1", $"'{profile_id}'");
                param.flag = false;
                param.search_list.ForEach(x =>
                {
                    x.name_field = $"[Limit1].{x.name_field}";
                });
                listResult = await _query.SearchJoin(param, sqlstring);
            }
            return listResult;
        }
        // lấy danh sách quyền truy cập của một user.
        public virtual async Task<List<PermissionResShort>> GetListPermissionByUser(string username, string is_root, string is_admin)
        {
            bool flag = true;

            List<PermissionResShort> response = new List<PermissionResShort>();
            List<PermissionModel> permissionsResult = new List<PermissionModel>();
            using (var db = new BCC01_DbContextSql())
            {
                var listProfileid = await (from p in db.BCC01_Profile
                                           join m in db.BCC01_MapProfileUser on p.id equals m.profile_id
                                           where m.username.Equals(username) && p.is_active
                                           select new
                                           {
                                               profile_id = p.id
                                           }).ToListAsync();
                if (is_admin.Equals("true") || is_root.Equals("true"))
                {
                    foreach (var item in listProfileid)
                    {
                        var listPermisson = db.Set<BCC01_Permission>().Include(po => po.BCC01_PermissionObject)
                                                                    .Include(mp => mp.BCC01_Profile.BCC01_MapProfileUser)
                                                                    .Include(mo => mo.BCC01_PermissionObject.BCC01_Module)
                                                                        .Where(x => x.profile_id == item.profile_id)
                                                                        .Select(x => new PermissionModel()
                                                                        {
                                                                            id = x.id,
                                                                            permissionobject_id = x.permissionobject_id,
                                                                            object_name = x.BCC01_PermissionObject.object_name,
                                                                            module_name = x.BCC01_PermissionObject.BCC01_Module.module_name,
                                                                            is_show = x.is_show,
                                                                            is_allow_access = x.is_allow_access,
                                                                            is_allow_create = x.is_allow_create,
                                                                            is_allow_delete = x.is_allow_delete,
                                                                            is_allow_edit = x.is_allow_edit,
                                                                            is_active = x.is_active
                                                                        })
                                                                        .ToList();
                        if (flag)
                        {
                            permissionsResult = listPermisson;
                            flag = false;
                        }
                        else
                        {
                            foreach (var permissionR in permissionsResult)
                            {
                                foreach (var permission in listPermisson)
                                {
                                    if (permissionR.permissionobject_id.Equals(permission.permissionobject_id))
                                    {
                                        if (!permissionR.is_show)
                                            permissionR.is_show = permission.is_show;
                                        if (!permissionR.is_allow_access)
                                            permissionR.is_allow_access = permission.is_allow_access;
                                        if (!permissionR.is_allow_create)
                                            permissionR.is_allow_create = permission.is_allow_create;
                                        if (!permissionR.is_allow_delete)
                                            permissionR.is_allow_delete = permission.is_allow_delete;
                                        if (!permissionR.is_allow_edit)
                                            permissionR.is_allow_edit = permission.is_allow_edit;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    foreach (var item in listProfileid)
                    {
                        var listPermisson = db.Set<BCC01_Permission>().Include(po => po.BCC01_PermissionObject)
                                                                    .Include(mp => mp.BCC01_Profile.BCC01_MapProfileUser)
                                                                    .Include(mo => mo.BCC01_PermissionObject.BCC01_Module)
                                                                        .Where(x => x.profile_id == item.profile_id && x.BCC01_PermissionObject.BCC01_Module.is_active)
                                                                        .Select(x => new PermissionModel()
                                                                        {
                                                                            id = x.id,
                                                                            permissionobject_id = x.permissionobject_id,
                                                                            object_name = x.BCC01_PermissionObject.object_name,
                                                                            module_name = x.BCC01_PermissionObject.BCC01_Module.module_name,
                                                                            is_show = x.is_show,
                                                                            is_allow_access = x.is_allow_access,
                                                                            is_allow_create = x.is_allow_create,
                                                                            is_allow_delete = x.is_allow_delete,
                                                                            is_allow_edit = x.is_allow_edit,
                                                                            is_active = x.is_active
                                                                        })
                                                                        .ToList();
                        if (flag)
                        {
                            permissionsResult = listPermisson;
                            flag = false;
                        }
                        else
                        {
                            foreach (var permissionR in permissionsResult)
                            {
                                foreach (var permission in listPermisson)
                                {
                                    if (permissionR.permissionobject_id.Equals(permission.permissionobject_id))
                                    {
                                        if (!permissionR.is_show)
                                            permissionR.is_show = permission.is_show;
                                        if (!permissionR.is_allow_access)
                                            permissionR.is_allow_access = permission.is_allow_access;
                                        if (!permissionR.is_allow_create)
                                            permissionR.is_allow_create = permission.is_allow_create;
                                        if (!permissionR.is_allow_delete)
                                            permissionR.is_allow_delete = permission.is_allow_delete;
                                        if (!permissionR.is_allow_edit)
                                            permissionR.is_allow_edit = permission.is_allow_edit;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            response.AddRange(CommonFuncMain.ToObjectList<PermissionResShort>(permissionsResult));
            return response;
        }

        // lấy quyền của người dùng ở một đối tượng phân quyền.
        public virtual async Task<PermissionResShort> GetPermissionAObjectByUser(PermissionAObjectRequest request)
        {
            PermissionResShort response = null;
            using (var db = new BCC01_DbContextSql())
            {
                var listProfileid = await db.Set<BCC01_MapProfileUser>().AsNoTracking()
                                .Where(x => x.username == request.username)
                                .Select(x => new { profile_id = x.profile_id }).ToListAsync();
                foreach (var item in listProfileid)
                {
                    var permisson = db.Set<BCC01_Permission>().Include(po => po.BCC01_PermissionObject)
                                                                .Include(mp => mp.BCC01_Profile.BCC01_MapProfileUser)
                                                                    .Where(x => x.profile_id == item.profile_id && x.permissionobject_id == request.permissionobject_id)
                                                                    .Select(x => new PermissionResShort()
                                                                    {
                                                                        id = x.id,
                                                                        object_name = x.BCC01_PermissionObject.object_name,
                                                                        is_show = x.is_show,
                                                                        is_allow_access = x.is_allow_access,
                                                                        is_allow_create = x.is_allow_create,
                                                                        is_allow_delete = x.is_allow_delete,
                                                                        is_allow_edit = x.is_allow_edit,
                                                                        is_active = x.is_active
                                                                    }).FirstOrDefault();
                    if (permisson != null) response = permisson;
                }
            }
            return response;
        }
        // lấy quyền của người dùng ở 1 loại quyền cụ thể của 1 đối tượng phân quyền.
        public virtual async Task<Object> GetStatusPermissionTypeAObjectByUser(PermissionAObjectByTypeRequest request)
        {
            Object response = null;
            using (var db = new BCC01_DbContextSql())
            {
                var listProfileid = await db.Set<BCC01_MapProfileUser>().AsNoTracking()
                                .Where(x => x.username == request.username)
                                .Select(x => new { profile_id = x.profile_id }).ToListAsync();
                foreach (var item in listProfileid)
                {
                    var permisson = db.Set<BCC01_Permission>().Include(po => po.BCC01_PermissionObject)
                                                                .Include(mp => mp.BCC01_Profile.BCC01_MapProfileUser)
                                                                    .Where(x => x.profile_id == item.profile_id && x.permissionobject_id == request.permissionobject_id)
                                                                    .Select(x => new PermissionResShort()
                                                                    {
                                                                        id = x.id,
                                                                        object_name = x.BCC01_PermissionObject.object_name,
                                                                        is_show = x.is_show,
                                                                        is_allow_access = x.is_allow_access,
                                                                        is_allow_create = x.is_allow_create,
                                                                        is_allow_delete = x.is_allow_delete,
                                                                        is_allow_edit = x.is_allow_edit,
                                                                        is_active = x.is_active
                                                                    })
                                                                    .FirstOrDefault();
                    if (permisson != null)
                    {
                        switch (request.permission_type.ToLower())
                        {
                            case "create":
                                response = permisson.is_allow_create;
                                break;
                            case "edit":
                                response = permisson.is_allow_edit;
                                break;
                            case "access":
                                response = permisson.is_allow_access;
                                break;
                            case "delete":
                                response = permisson.is_allow_delete;
                                break;
                        }
                    }
                }
            }
            return response;
        }
        // lấy quyền của người dùng ở 1 loại quyền cụ thể của 1 đối tượng phân quyền.
        public virtual async Task<Object> GetStatusPermissionByTypeAndName(GetPermissionByTypeAndName request)
        {
            bool response = false;
            using (var db = new BCC01_DbContextSql())
            {
                var listProfileid = await db.Set<BCC01_MapProfileUser>().AsNoTracking()
                                .Where(x => x.username == request.username && x.BCC01_Profile.is_active)
                                .Select(x => new { profile_id = x.profile_id }).ToListAsync();

                foreach (var item in listProfileid)
                {
                    var permisson = await db.Set<BCC01_Permission>().Include(po => po.BCC01_PermissionObject)
                                                                .Include(mp => mp.BCC01_Profile.BCC01_MapProfileUser)
                                                                    .Where(x => x.profile_id == item.profile_id && x.object_name == request.permission_name)
                                                                    .Select(x => new PermissionResShort()
                                                                    {
                                                                        id = x.id,
                                                                        object_name = x.BCC01_PermissionObject.object_name,
                                                                        is_allow_access = x.is_allow_access,
                                                                        is_allow_create = x.is_allow_create,
                                                                        is_allow_delete = x.is_allow_delete,
                                                                        is_allow_edit = x.is_allow_edit,
                                                                        is_active = x.is_active
                                                                    })
                                                                    .FirstOrDefaultAsync();
                    if (permisson != null)
                    {
                        if (!response)
                        {
                            switch (request.permission_type.ToLower())
                            {
                                case "create":
                                    response = permisson.is_allow_create;
                                    break;
                                case "edit":
                                    response = permisson.is_allow_edit;
                                    break;
                                case "access":
                                    response = permisson.is_allow_access;
                                    break;
                                case "delete":
                                    response = permisson.is_allow_delete;
                                    break;
                            }
                        }
                    }
                }
            }
            return response;
        }
    }
}
