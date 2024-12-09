using Common;
using Repository.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Repository.CustomModel;
using System.Linq;
using System.Threading.Tasks;
using Repository.BCC01_EF;
using Management_AI.Config;
using Management_AI.Common;

namespace Management_AI.CustomAttributes
{
    public class PermissionAttributeFilter : ActionFilterAttribute, IActionFilter
    {
        private string permisson_name = "";
        private string permission_type = "";
        public PermissionAttributeFilter(string permisson_name, string permission_type)
        {
            this.permisson_name = permisson_name;
            this.permission_type = permission_type;
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            StringValues headerValues;
            context.HttpContext.Request.Headers.TryGetValue("API_SECRET_KEY", out headerValues);
            string secret_key = null;
            if (headerValues.Any())
            {
                secret_key = headerValues.FirstOrDefault();
            }
            if (secret_key == null || secret_key != ConfigManager.Get("API_SECRET_KEY"))
            {
                var result = await GetStatusPermissionTypeAObjectByUser(SessionStore.Get<string>(Constants.KEY_SESSION_USER_ID), permisson_name, permission_type);
                if (!result)
                {
                    context.Result = new BadRequestObjectResult(Constants.ACCESS_DENIED);
                }
                else
                {
                    await next();
                }
            }
            else
            {
                await next();
            }
        }

        // lấy quyền của người dùng ở 1 loại quyền cụ thể của 1 đối tượng phân quyền.
        public virtual async Task<bool> GetStatusPermissionTypeAObjectByUser(string username, string permission_name, string permission_type)
        {
            bool response = false;
            using (var db = new BCC01_DbContextSql())
            {
                try
                {
                    var is_root = SessionStore.Get<string>(Constants.KEY_SESSION_IS_ROOT) != null ? SessionStore.Get<string>(Constants.KEY_SESSION_IS_ROOT).ToLower() : "false";
                    var is_admin = SessionStore.Get<string>(Constants.KEY_SESSION_IS_ADMIN) != null ? SessionStore.Get<string>(Constants.KEY_SESSION_IS_ADMIN).ToLower() : "false";
                    if (is_root.Equals("true"))
                    {
                        return true;
                    }
                    else
                    {
                        if (is_admin.Equals("true"))
                        {
                            if (Constants.ROOT_PERMISSIONS.Contains(permission_name))
                            {
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }
                        else
                        {
                            var listProfileid = await db.Set<BCC01_MapProfileUser>().AsNoTracking()
                                .Where(x => x.username == username && x.BCC01_Profile.is_active)
                                .Select(x => new { x.profile_id }).ToListAsync();
                            foreach (var item in listProfileid)
                            {
                                var permisson = db.Set<BCC01_Permission>().Include(po => po.BCC01_PermissionObject)
                                                                            .Include(mp => mp.BCC01_Profile.BCC01_MapProfileUser)
                                                                                .Where(x => x.profile_id == item.profile_id && x.object_name == permisson_name)
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
                                                                                .FirstOrDefault();
                                if (permisson != null)
                                {
                                    if (!response)
                                    {
                                        switch (permission_type.ToLower())
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
                    }
                }
                catch
                { }
            }
            return response;
        }


    }
}
