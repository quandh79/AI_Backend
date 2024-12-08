using Common.Commons;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Repository.BCC01_EF;
using Repository.CustomModel;
using Repository.EF;
using Repository.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class ModuleRepository : BaseRepositorySql<BCC01_Module>, IModuleRepository
    {
        private readonly IStoreProcedureExcute _storeProcedureExcute;
        public ModuleRepository(
            IStoreProcedureExcute storeProcedureExcute
            ) : base()
        {
            _storeProcedureExcute = storeProcedureExcute;
        }

        public override async Task<BCC01_Module> Create(BCC01_Module obj)
        {
            BCC01_Module result = null;
            using (var dbcontext = new BCC01_DbContextSql())
            {
                var entity = dbcontext.BCC01_Module.Where(x => x.module_name.ToLower().Equals(obj.module_name.ToLower()) && x.tenant_id.Equals(obj.tenant_id)).FirstOrDefault();
                if (entity == null)
                {
                    result = dbcontext.BCC01_Module.Add(obj).Entity;
                    await dbcontext.SaveChangesAsync();
                }
            }
            return result;
        }

        public async Task<List<ModuleCustomResponse>> GetAllModule(string username,bool is_admin, Guid tenant_id)
        {
            var response = new List<ModuleCustomResponse>();
            var parameters = new DynamicParameters(
                new
                {
                    username = username,
                    is_admin = is_admin,
                    tenant_id = tenant_id
                });

            response = (await _storeProcedureExcute.ExecuteReturnList<ModuleCustomResponse>("usp_Module_Get_All", parameters,1)).ToList();
            return response;
        }
        public async Task<ResponseService<bool>> AsyncDefaultModule()
        {
            using (var _db = new BCC01_DbContextSql())
            {

                var tenants = await _db.BCC01_Tenants.Where(x => x.is_active && x.email != "root@recording.com").ToListAsync();
                var defauldModules = await _db.BCC01_DefaultModule.ToListAsync();
                foreach (var tenant in tenants)
                {

                    var modules = await _db.BCC01_Module.Where(x => x.tenant_id == tenant.id).ToListAsync();
                    var defauld = new List<BCC01_DefaultModule>();

                    foreach (var d in defauldModules)
                    {
                        var ischeck = modules.FirstOrDefault(x => x.module_name.ToLower().Equals(d.module_name.ToLower())) == null;
                        if (ischeck)
                        {
                            defauld.Add(d);
                        }
                    }


                    foreach (var d in defauld)
                    {
                        //adddModule
                        var module = new BCC01_Module
                        {
                            id = Guid.NewGuid(),
                            module_name = d.module_name,
                            create_by = d.create_by,
                            create_time = d.create_time,
                            description = d.description ?? "",
                            display_name = d.display_name,
                            is_active = d.is_active,
                            modify_by = d.modify_by ?? "system",
                            modify_time = d.modify_time,
                            position = d.position,
                            tenant_id = tenant.id,

                        };
                        var add = await _db.Set<BCC01_Module>().AddAsync(module);
                        await _db.SaveChangesAsync();
                        // add PermissionObject
                        var permisionObject = new BCC01_PermissionObject
                        {
                            id = Guid.NewGuid(),
                            create_by = d.create_by,
                            create_time = d.create_time,
                            description = module.module_name,
                            is_active = d.is_active,
                            modify_by = d.modify_by ?? "system",
                            modify_time = d.modify_time,
                            module_id = module.id,
                            object_name = module.module_name,
                            tenant_id = tenant.id
                        };
                        var permissionObj = await _db.Set<BCC01_PermissionObject>().AddAsync(permisionObject);
                        await _db.SaveChangesAsync();

                        //get profile aadmin
                        var profiles = await _db.BCC01_Profile.Where(x => x.tenant_id == tenant.id).ToListAsync();
                        if (profiles == null)
                        {
                            continue;
                        }
                        foreach (var profile in profiles)
                        {

                            var permision = new BCC01_Permission
                            {
                                tenant_id = tenant.id,
                                create_by = d.create_by,
                                create_time = d.create_time,
                                description = module.module_name,
                                id = Guid.NewGuid(),
                                is_active = d.is_active,
                                is_allow_access = true,
                                is_allow_create = true,
                                is_allow_delete = true,
                                is_allow_edit = true,
                                is_show = true,
                                modify_by = d.modify_by ?? "system",
                                modify_time = d.modify_time,
                                object_name = module.module_name,
                                permissionobject_id = permisionObject.id,
                                profile_id = profile.id,
                            };
                            var permission = await _db.Set<BCC01_Permission>().AddAsync(permision);
                            await _db.SaveChangesAsync();
                        }
                    }
                }
            }
            return new ResponseService<bool>(true);
        }
    }
    
}
