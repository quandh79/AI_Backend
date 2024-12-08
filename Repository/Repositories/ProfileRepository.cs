using Common;
using Common.Params.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Repository.BCC01_EF;
using Repository.CustomModel;
using Repository.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class ProfileRepository : BaseRepositorySql<BCC01_Profile>, IProfileRepository
    {
        public ProfileRepository() : base() { }

        public virtual async Task<ListResult<UserModel>> GetUsersByProfile(PagingParam param)
        {
            ListResult<UserModel> response = new ListResult<UserModel>();
            using (var db = new BCC01_DbContextSql())
            {
                Guid profile_id = Guid.Empty;
                foreach (var item in param.search_list)
                {
                    if (item.name_field.Equals("profile_id"))
                        profile_id = Guid.Parse(item.value_search.ToString());
                }
                var query = (from mp in db.BCC01_MapProfileUser
                             join u in db.BCC01_User on mp.username equals u.username
                             select new UserModel
                             {
                                 username = u.username,
                                 fullname = u.fullname,
                                 email = u.email,
                                 phone = u.phone,
                                 description = u.description,
                                 role_id = u.role_id,
                                 is_administrator = u.is_administrator,
                                 is_rootuser = u.is_rootuser,
                                 is_active = u.is_active,
                                 report_to = u.report_to,
                                 create_time = u.create_time,
                                 create_by = u.create_by,
                                 modify_time = u.modify_time,
                                 modify_by = u.modify_by,
                                 tenant_id = u.tenant_id,
                                 profile_id = mp.profile_id
                             }).Where(x => x.tenant_id.Equals(param.tenant_id) && x.profile_id.Equals(profile_id));
                response.items = await query.ToListAsync();
                response.total = await query.CountAsync();
            }
            return response;
        }
        public virtual async Task<bool> AutoRemoveRecordsWhenRemoveProfile(Guid id)
        {
            using (var dbcontext = new BCC01_DbContextSql())
            {
                using (IDbContextTransaction transaction = dbcontext.Database.BeginTransaction())
                {
                    try
                    {
                        var proEntity = dbcontext.BCC01_Profile.Find(id);
                        dbcontext.BCC01_Profile.Remove(proEntity);
                        var mproEntitys = dbcontext.BCC01_MapProfileUser.Where(x => x.profile_id == id).ToList();
                        mproEntitys.ForEach(x => dbcontext.BCC01_MapProfileUser.Remove(x));
                        await dbcontext.SaveChangesAsync();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine(ex);
                        return false;
                    }
                }
            }
            return true;
        }
        public override async Task<BCC01_Profile> Create(BCC01_Profile obj)
        {
            BCC01_Profile result = null;
            using (var dbcontext = new BCC01_DbContextSql())
            {
                using (IDbContextTransaction transaction = dbcontext.Database.BeginTransaction())
                {
                    try
                    {
                        result = dbcontext.BCC01_Profile.Add(obj).Entity;
                        var listPermissionObj = dbcontext.BCC01_PermissionObject.Where(x => x.tenant_id == obj.tenant_id).ToList();
                        foreach (BCC01_PermissionObject permissionObject in listPermissionObj)
                        {
                            DateTime defaultDatetime = DateTime.Now;
                            BCC01_Permission permission = new BCC01_Permission();
                            permission.id = Guid.NewGuid();
                            permission.profile_id = result.id;
                            permission.permissionobject_id = permissionObject.id;
                            permission.object_name = permissionObject.object_name;
                            permission.description = permissionObject.object_name;
                            permission.is_allow_create = true;
                            permission.is_allow_edit = true;
                            permission.is_allow_delete = true;
                            permission.is_allow_access = true;
                            permission.is_active = true;
                            permission.create_time = defaultDatetime;
                            permission.create_by = obj.create_by;
                            permission.modify_time = defaultDatetime;
                            permission.modify_by = obj.create_by;
                            permission.tenant_id = obj.tenant_id;
                            if (!Constants.ROOT_PERMISSIONS.Contains(permissionObject.object_name))
                            {
                                dbcontext.BCC01_Permission.Add(permission);
                            }
                        }
                        await dbcontext.SaveChangesAsync();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine(ex);
                        return result;
                    }
                }
            }
            return result;
        }
        public async Task<bool> DeleteUserInProfile(DeleteUserInProfile request)
        {
            using (var dbcontext = new BCC01_DbContextSql())
            {
                try
                {
                    var entity = dbcontext.BCC01_MapProfileUser.Where(x => x.username == request.username && x.profile_id == request.profile_id).FirstOrDefault();
                    dbcontext.BCC01_MapProfileUser.Remove(entity);
                    await dbcontext.SaveChangesAsync();
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }
    }
}
