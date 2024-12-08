using Common;
using Common.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Repository.BCC01_EF;
using Repository.CustomModel;
using Repository.EF;
using Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class TenantsRepository : BaseRepositorySql<BCC01_Tenants>, ITenantsRepository
    {
        public async Task<ResponseService<TenantCustomResponseModel>> CreateTenant(BCC01_RegisterInformation register_information_entity, BCC01_DbContextSql dbContext, string phone_cucm)
        {
            try
            {
                var tenant_entity = CommonFuncMain.ToObject<BCC01_Tenants>(register_information_entity);
                
                // Create tenant's default setting (module, permission,user ...)
                var result = await CreateDefaultSetting(tenant_entity, register_information_entity, dbContext, phone_cucm);

                return new ResponseService<TenantCustomResponseModel>(result);
            }
            catch (Exception ex)
            {
                return new ResponseService<TenantCustomResponseModel>(ex);
            }
        }

        private async Task<TenantCustomResponseModel> CreateDefaultSetting(BCC01_Tenants tenant, BCC01_RegisterInformation registerInfo, BCC01_DbContextSql dbContext, string phone_cucm)
        {
            BaseInfoRegister baseInfo = new BaseInfoRegister("auto", DateTime.Now, DateTime.Now, tenant.email, tenant.email, tenant.id, tenant.phone, tenant.email, registerInfo.password);

            await dbContext.BCC01_RegisterInformation.AddAsync(registerInfo);
            await dbContext.BCC01_Tenants.AddAsync(tenant);           

            // Create default tenant's role 
            var roleDirectors = CommonFuncMain.ToObject<BCC01_RoleHierarchy>(baseInfo);
            roleDirectors.id = Guid.NewGuid();
            roleDirectors.role_name = Constants.DEFAULT_ROLE_DIRECTORS;
            roleDirectors.role_parent_id = Guid.Parse(Constants.ROOT_ROLE);
            roleDirectors.is_editdata_all_level = true;
            roleDirectors.is_removedata_all_level = true;
            roleDirectors.is_viewdata_all_level = true;
            var roleDirect = await dbContext.BCC01_RoleHierarchy.AddAsync(roleDirectors);

            var roleSuper = CommonFuncMain.ToObject<BCC01_RoleHierarchy>(baseInfo);
            roleSuper.id = Guid.NewGuid();
            roleSuper.role_name = Constants.DEFAULT_ROLE_SUPERVISOR;
            roleSuper.role_parent_id = roleDirect.Entity.id;
            roleSuper.is_editdata_all_level = true;
            roleSuper.is_removedata_all_level = true;
            roleSuper.is_viewdata_all_level = true;
            var roleSp = await dbContext.BCC01_RoleHierarchy.AddAsync(roleSuper);

            var roleAgent = CommonFuncMain.ToObject<BCC01_RoleHierarchy>(baseInfo);
            roleAgent.id = Guid.NewGuid();
            roleAgent.role_name = Constants.DEFAULT_ROLE_AGENT;
            roleAgent.role_parent_id = roleSp.Entity.id;
            await dbContext.BCC01_RoleHierarchy.AddAsync(roleAgent);

            // Create module 
            var defaultModule = await dbContext.BCC01_DefaultModule.Where(x => x.is_active == true).OrderBy(x => x.position).ToListAsync();
            var listmodule = CommonFuncMain.ToObject<List<ModuleModel>>(defaultModule);

            // Create default permission object
            var defaultPermissionObject = await dbContext.BCC01_DefaultPermissionObject.Where(x => x.is_active == true).ToListAsync();
            var listpermissionObject = CommonFuncMain.ToObject<List<PermissionObjectModel>>(defaultPermissionObject);

            List<BCC01_Module> listModule = new List<BCC01_Module>();
            listmodule.ForEach(x =>
            {
                var currentModuleID = x.id;
                BCC01_Module tempModule = new BCC01_Module();
                BCC01_Module module = new BCC01_Module();
                module = CommonFuncMain.ToObject<BCC01_Module>(baseInfo);
                module.id = Guid.NewGuid();
                module.module_name = x.module_name;
                module.display_name = x.display_name;
                module.position = x.position;
                module.description = x.description;
                module.is_active = true;
                tempModule = dbContext.Set<BCC01_Module>().Add(module).Entity;
                listpermissionObject.ForEach(y =>
                {
                    if (currentModuleID.Equals(y.module_id))
                    {
                        y.module_id = tempModule.id;
                    }
                });
                listModule.Add(tempModule);
            });

            // Create permission object
            List<BCC01_PermissionObject> listPermissionObject = new List<BCC01_PermissionObject>();
            List<BCC01_PermissionObject> listPermissionObjectAgent = new List<BCC01_PermissionObject>();
            listpermissionObject.ForEach(async x =>
            {
                BCC01_PermissionObject tempPO = new BCC01_PermissionObject();
                BCC01_PermissionObject BCC01_PermissionObject = new BCC01_PermissionObject();
                BCC01_PermissionObject = CommonFuncMain.ToObject<BCC01_PermissionObject>(baseInfo);
                BCC01_PermissionObject.id = Guid.NewGuid();
                BCC01_PermissionObject.object_name = x.object_name;
                BCC01_PermissionObject.module_id = x.module_id;
                BCC01_PermissionObject.description = x.description;
                BCC01_PermissionObject.is_active = true;
                await dbContext.BCC01_PermissionObject.AddAsync(BCC01_PermissionObject);
                if (!Constants.ROOT_PERMISSIONS.Contains(x.object_name))
                {
                    listPermissionObject.Add(BCC01_PermissionObject);
                }
                if (Constants.AGENT_PERMISSIONS.Contains(x.object_name))
                {
                    listPermissionObjectAgent.Add(BCC01_PermissionObject);
                }
            });
            
            // Create default common setting
            var defaultCommonSetting = await dbContext.BCC01_DefaultCommonSetting.Where(x => x.only_root == false).ToListAsync();
            foreach (var item in defaultCommonSetting)
            {
                BCC01_CommonSetting defaultCommonSettingByTenant = new BCC01_CommonSetting();
                defaultCommonSettingByTenant.id = Guid.NewGuid();
                defaultCommonSettingByTenant.setting_key = item.setting_key;
                defaultCommonSettingByTenant.value = item.value;
                defaultCommonSettingByTenant.description = item.description;
                defaultCommonSettingByTenant.common_type = item.common_type;
                defaultCommonSettingByTenant.setting_for = item.setting_for;
                defaultCommonSettingByTenant.create_by = baseInfo.create_by;
                defaultCommonSettingByTenant.create_time = baseInfo.create_time;
                defaultCommonSettingByTenant.modify_by = baseInfo.modify_by;
                defaultCommonSettingByTenant.modify_time = baseInfo.modify_time;
                defaultCommonSettingByTenant.tenant_id = baseInfo.tenant_id;

                await dbContext.BCC01_CommonSetting.AddAsync(defaultCommonSettingByTenant);
            }
                        
            // Create user
            //var generateDefaultAvatar = GenerateAvatar(tenant.email, tenant.id);
            var extensionPassword = Guid.NewGuid().ToString();
            BCC01_User user = CommonFuncMain.ToObject<BCC01_User>(baseInfo);
            user.fullname = tenant.tenant_name;
            user.role_id = roleDirectors.id;
            user.username = tenant.email;
            user.is_active = true;
            user.is_rootuser = false;
            user.is_administrator = true;
            user.report_to = null;
            user.is_agent = false;
            user.is_supervisor = false;
            user.extension_number = phone_cucm;//extensionRange.start_range.ToString();
            //user.extension_password = extensionPassword;
            user.avatar = null;
            await dbContext.BCC01_User.AddAsync(user);

            // Start progress default create profile 
            var adminProfile = CommonFuncMain.ToObject<BCC01_Profile>(baseInfo);
            adminProfile.id = Guid.NewGuid();
            adminProfile.profile_name = Constants.DEFAULT_PROFILE_ADMIN;
            adminProfile.is_active = true;
            adminProfile.create_time = DateTime.Now;
            adminProfile.modify_time = adminProfile.create_time;
            await dbContext.BCC01_Profile.AddAsync(adminProfile);

            var supervisorProfile = CommonFuncMain.ToObject<BCC01_Profile>(baseInfo);
            supervisorProfile.id = Guid.NewGuid();
            supervisorProfile.profile_name = Constants.DEFAULT_PROFILE_SUPERVISOR;
            supervisorProfile.is_active = true;
            supervisorProfile.create_time = DateTime.Now;
            supervisorProfile.modify_time = adminProfile.create_time;
            await dbContext.BCC01_Profile.AddAsync(supervisorProfile);

            var agentProfile = CommonFuncMain.ToObject<BCC01_Profile>(baseInfo);
            agentProfile.id = Guid.NewGuid();
            agentProfile.profile_name = Constants.DEFAULT_PROFILE_AGENT;
            agentProfile.is_active = true;
            agentProfile.create_time = DateTime.Now;
            agentProfile.modify_time = adminProfile.create_time;
            await dbContext.BCC01_Profile.AddAsync(agentProfile);

            // Create permission 
            foreach (BCC01_PermissionObject permissionObject in listPermissionObject)
            {
                BCC01_Permission permission = new BCC01_Permission();
                permission = CommonFuncMain.ToObject<BCC01_Permission>(baseInfo);
                permission.id = Guid.NewGuid();
                permission.profile_id = adminProfile.id;
                permission.permissionobject_id = permissionObject.id;
                permission.object_name = permissionObject.object_name;
                permission.is_active = true;

                var lstPermAdmin = defaultPermissionObject.Where(x => x.is_allow_admin == true).ToList();

                if (lstPermAdmin.Any(x => x.object_name == permissionObject.object_name))
                {
                    permission.is_allow_access = true;
                    permission.is_allow_create = true;
                    permission.is_allow_delete = true;
                    permission.is_allow_edit = true;
                    permission.is_show = true;
                }
                else
                {
                    switch (permissionObject.object_name)
                    {
                        case "User":
                            permission.is_allow_access = true;
                            permission.is_allow_create = false;
                            permission.is_allow_delete = false;
                            permission.is_allow_edit = false;
                            permission.is_show = false;
                            break;
                        case "Interaction Tag":
                            permission.is_allow_access = true;
                            permission.is_allow_create = true;
                            permission.is_allow_delete = false;
                            permission.is_allow_edit = false;
                            permission.is_show = false;
                            break;
                        default:
                            permission.is_allow_access = false;
                            permission.is_allow_create = false;
                            permission.is_allow_delete = false;
                            permission.is_allow_edit = false;
                            permission.is_show = false;
                            break;
                    }
                }

                //permission.is_allow_access = true;
                //permission.is_allow_create = true;
                //permission.is_allow_delete = true;
                //permission.is_allow_edit = true;
                //permission.is_show = true;
                await dbContext.BCC01_Permission.AddAsync(permission);
            }

            var lstDefaultAccess = new List<string>() { "Common Setting", "Queue", "State List", "User Management" };

            foreach (BCC01_PermissionObject permissionObject in listPermissionObject)
            {
                BCC01_Permission permission = new BCC01_Permission();
                permission = CommonFuncMain.ToObject<BCC01_Permission>(baseInfo);
                permission.id = Guid.NewGuid();
                permission.profile_id = supervisorProfile.id;
                permission.permissionobject_id = permissionObject.id;
                permission.object_name = permissionObject.object_name;
                permission.is_active = true;

                var lstPermSup = defaultPermissionObject.Where(x => x.is_allow_sup == true).ToList();

                if (lstPermSup.Any(x => x.object_name == permissionObject.object_name))
                {
                    permission.is_allow_access = true;
                    permission.is_allow_create = true;
                    permission.is_allow_delete = true;
                    permission.is_allow_edit = true;
                    permission.is_show = true;
                }
                else
                {
                    switch (permissionObject.object_name)
                    {
                        case "User":
                            permission.is_allow_access = true;
                            permission.is_allow_create = false;
                            permission.is_allow_delete = false;
                            permission.is_allow_edit = false;
                            permission.is_show = false;
                            break;
                        case "Agent Group":
                            permission.is_allow_access = true;
                            permission.is_allow_create = false;
                            permission.is_allow_delete = false;
                            permission.is_allow_edit = false;
                            permission.is_show = false;
                            break;
                        case "Interaction Tag":
                            permission.is_allow_access = true;
                            permission.is_allow_create = true;
                            permission.is_allow_delete = false;
                            permission.is_allow_edit = false;
                            permission.is_show = false;
                            break;
                        //case "Common Setting":
                        //    permission.is_allow_access = true;
                        //    permission.is_allow_create = false;
                        //    permission.is_allow_delete = false;
                        //    permission.is_allow_edit = false;
                        //    permission.is_show = false;
                        //    break;
                        default:
                            permission.is_allow_access = false;
                            permission.is_allow_create = false;
                            permission.is_allow_delete = false;
                            permission.is_allow_edit = false;
                            permission.is_show = false;
                            break;
                    }
                    if (lstDefaultAccess.Contains(permissionObject.object_name))
                    {
                        permission.is_allow_access = true;
                        permission.is_allow_create = false;
                        permission.is_allow_delete = false;
                        permission.is_allow_edit = false;
                        permission.is_show = false;
                    }
                }

                //permission.is_allow_access = true;
                //permission.is_allow_create = true;
                //permission.is_allow_delete = true;
                //permission.is_allow_edit = true;
                //permission.is_show = true;

                await dbContext.BCC01_Permission.AddAsync(permission);
            }

            foreach (BCC01_PermissionObject permissionObject in listPermissionObject)
            {
                BCC01_Permission permission = new BCC01_Permission();
                permission = CommonFuncMain.ToObject<BCC01_Permission>(baseInfo);
                permission.id = Guid.NewGuid();
                permission.profile_id = agentProfile.id;
                permission.permissionobject_id = permissionObject.id;
                permission.object_name = permissionObject.object_name;
                permission.is_active = true;

                var lstPermAgent = defaultPermissionObject.Where(x => x.is_allow_agent == true).ToList();

                if (lstPermAgent.Any(x => x.object_name == permissionObject.object_name))
                {
                    permission.is_allow_access = true;
                    permission.is_allow_create = true;
                    permission.is_allow_delete = true;
                    permission.is_allow_edit = true;
                    permission.is_show = true;
                }
                else
                {
                    switch (permissionObject.object_name)
                    {
                        case "User":
                            permission.is_allow_access = true;
                            permission.is_allow_create = false;
                            permission.is_allow_delete = false;
                            permission.is_allow_edit = false;
                            permission.is_show = false;
                            break;
                        case "Interaction Tag":
                            permission.is_allow_access = true;
                            permission.is_allow_create = true;
                            permission.is_allow_delete = false;
                            permission.is_allow_edit = false;
                            permission.is_show = false;
                            break;

                        default:
                            permission.is_allow_access = false;
                            permission.is_allow_create = false;
                            permission.is_allow_delete = false;
                            permission.is_allow_edit = false;
                            permission.is_show = false;
                            break;
                    }

                    if (lstDefaultAccess.Contains(permissionObject.object_name))
                    {
                        permission.is_allow_access = true;
                        permission.is_allow_create = false;
                        permission.is_allow_delete = false;
                        permission.is_allow_edit = false;
                        permission.is_show = false;
                    }
                }
                await dbContext.BCC01_Permission.AddAsync(permission);
            }

            // Add user profile
            var mapUserProfile = CommonFuncMain.ToObject<BCC01_MapProfileUser>(baseInfo);
            mapUserProfile.id = Guid.NewGuid();
            mapUserProfile.profile_id = adminProfile.id;
            mapUserProfile.username = user.username;
            mapUserProfile.is_active = true;
            await dbContext.BCC01_MapProfileUser.AddAsync(mapUserProfile);

            // End create profile 

            // Remove register info
            dbContext.BCC01_RegisterInformation.Remove(registerInfo);

            // Response
            var response = new TenantCustomResponseModel();
            response.id = tenant.id;
            response.tenant_name = tenant.tenant_name;
            response.province_id = tenant.province_id;
            response.address = tenant.address;
            response.phone = tenant.phone;
            response.email = tenant.email;
            response.business_type_id = tenant.business_type_id;
            response.num_employees = tenant.num_employees;
            response.license = tenant.license;
            response.is_trial = tenant.is_trial;
            response.expire_time = tenant.expire_time;
            response.is_active = tenant.is_active;
            response.customer_type = tenant.customer_type;
            response.asterisk_id = tenant.asterisk_id;
            response.extension_number = user.extension_number;
            response.extension_password = extensionPassword;
            response.create_time = tenant.create_time;
            response.create_by = tenant.create_by;
            response.modify_time = tenant.modify_time;
            response.modify_by = tenant.modify_by;
            response.role_id = user.role_id.ToString();
            response.tenant_id_vgw = tenant.tenant_id_vgw;
            response.file_saving_time = tenant.file_saving_time;
            response.type_saving_time = tenant.type_saving_time;
            return response;
        }

        public async Task<bool> CheckExistsEmail(Guid id, string email, BCC01_DbContextSql dbContext)
        {
            var checkEmailUser = await dbContext.BCC01_User.Where(x => x.email == email).FirstOrDefaultAsync();
            var checkEmailRegister = await dbContext.BCC01_RegisterInformation.Where(x => x.id != id && x.email == email).FirstOrDefaultAsync();
            if (checkEmailUser != null || checkEmailRegister != null)
            {
                return true;
            }
            return false;
        }
        public async Task<ResponseService<bool>> DeleteAll(Object id, string token)
        {
            var result = false;
            using (IDbContextTransaction transaction = await _db.Database.BeginTransactionAsync())
            {
                string error = "";
                try
                {
                    var tenant = await _db.BCC01_Tenants.FindAsync(id);
                    _db.BCC01_Tenants.Remove(tenant);

                    // Remove user
                    var listUser = from t in _db.BCC01_User
                                   where t.tenant_id.Equals(tenant.id)
                                   select t;

                    foreach (var item in listUser)
                    {
                        _db.BCC01_User.Remove(item);
                    }
                    // Remove role
                    var listRole = from t in _db.BCC01_RoleHierarchy
                                   where t.tenant_id.Equals(tenant.id)
                                   select t;

                    foreach (var item in listRole)
                    {
                        _db.BCC01_RoleHierarchy.Remove(item);
                    }


                    // Remove map profile user
                    var listMapProfileUser = from t in _db.BCC01_MapProfileUser
                                             where t.tenant_id.Equals(tenant.id)
                                             select t;

                    foreach (var item in listMapProfileUser)
                    {
                        _db.BCC01_MapProfileUser.Remove(item);
                    }

                    // Remove profile 
                    var listProfile = from t in _db.BCC01_Profile
                                      where t.tenant_id.Equals(tenant.id)
                                      select t;

                    foreach (var item in listProfile)
                    {
                        _db.BCC01_Profile.Remove(item);
                    }

                    // Remove permission 
                    var listPermission = from t in _db.BCC01_Permission
                                         where t.tenant_id.Equals(tenant.id)
                                         select t;

                    foreach (var item in listPermission)
                    {
                        _db.BCC01_Permission.Remove(item);
                    }

                    // Remove module 
                    var listModule = from t in _db.BCC01_Module
                                     where t.tenant_id.Equals(tenant.id)
                                     select t;

                    foreach (var item in listModule)
                    {
                        _db.BCC01_Module.Remove(item);
                    }

                    // Remove permission object
                    var listPermissionObject = from t in _db.BCC01_PermissionObject
                                               where t.tenant_id.Equals(tenant.id)
                                               select t;

                    foreach (var item in listPermissionObject)
                    {
                        _db.BCC01_PermissionObject.Remove(item);
                    }                   
                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                    result = true;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return new ResponseService<bool>(ex, error);
                }
            }
            return new ResponseService<bool>(result);
        }
        public async Task<ResponseService<bool>> UpdateIsActiveTenant(bool isActive, Object id)
        {
            var result = false;
            using (IDbContextTransaction transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    var tenant = await _db.BCC01_Tenants.FindAsync(id);
                    tenant.is_active = isActive;
                    var listUser = from t in _db.BCC01_User
                                   where t.tenant_id.Equals(tenant.id)
                                   select t;
                    foreach (var user in listUser)
                    {
                        user.is_active = isActive;
                    }
                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                    result = true;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return new ResponseService<bool>(ex);
                }
            }
            return new ResponseService<bool>(result);
        }

        public async Task<BCC01_Tenants> CheckExistByEmail(string email)
        {
            return await _db.BCC01_Tenants.FirstOrDefaultAsync(x => x.email == email);
        }
    }
}
