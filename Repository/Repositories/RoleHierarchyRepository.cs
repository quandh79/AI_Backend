using Common;
using Common.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Repository.BCC01_EF;
using Repository.CustomModel;
using Repository.Repositories.Abtracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class RoleHierarchyRepository : BaseRepositorySql<BCC01_RoleHierarchy>, IRoleHierarchyRepository
    {
        public RoleHierarchyRepository() : base() { }
        public async Task<List<RoleModel>> RoleRecursive(Guid tenant_id)
        {
            List<RoleModel> role = new List<RoleModel>();
            var listRole = await _db.BCC01_RoleHierarchy.Where(x => x.tenant_id == tenant_id).ToListAsync();
            List<RoleModel> roleModels = CommonFuncMain.ToObjectList<RoleModel>(listRole);
            var roleRecursive = GenerateRole(roleModels);
            role = roleRecursive;
            return role;
        }
        public List<RoleModel> GenerateRole(List<RoleModel> roleModels)
        {
            List<RoleModel> parentItems = (from a in roleModels where a.role_parent_id.ToString() == Constants.ROOT_ROLE.ToLower() select a).ToList();
            foreach (var item in parentItems)
            {
                List<RoleModel> childItems = (from a in roleModels where a.role_parent_id == item.id select a).ToList();
                if (childItems.Count > 0)
                    AddChildItem(item, roleModels);
            }
            return parentItems;
        }
        private void AddChildItem(RoleModel childItem, List<RoleModel> roleModels)
        {
            List<RoleModel> childItems = (from a in roleModels where a.role_parent_id == childItem.id select a).OrderBy(x => x.create_time).ToList();
            foreach (RoleModel cItem in childItems)
            {
                childItem.childRoles.Add(cItem);
                List<RoleModel> subChilds = (from a in roleModels where a.role_parent_id == cItem.id select a).OrderBy(x => x.create_time).ToList();
                if (subChilds.Count > 0)
                {
                    AddChildItem(cItem, roleModels);
                }
            }
        }
        // Lấy role cấp trên của user truyền vào.
        public virtual async Task<RoleHierarchyResponse> GetRoleByUser(string username)
        {
            RoleHierarchyResponse result = null;
            var role = await (from r in _db.BCC01_RoleHierarchy
                              join u in _db.BCC01_User on r.id equals u.role_id
                              where u.username.Equals(username)
                              select r).FirstOrDefaultAsync();
            result = CommonFuncMain.ToObject<RoleHierarchyResponse>(role);
            return result;
        }
        // Lấy danh sách các role dưới cấp của user truyền vào.
        public virtual async Task<List<RoleModel>> GetListRoleAgent(string username)
        {
            List<RoleModel> result = new List<RoleModel>();
            try
            {
                var userEntity = await _db.BCC01_User.AsNoTracking().Where(x => x.username == username).Select(x => new { role_id = x.role_id }).FirstOrDefaultAsync();
                var roles = await _db.BCC01_RoleHierarchy.AsNoTracking().ToListAsync();
                List<RoleModel> roleModels = CommonFuncMain.ToObjectList<RoleModel>(roles);
                var roleRecursive = RecursiveOrther(roleModels, (Guid)userEntity.role_id);
                // lấy danh sách các role dưới cấp .
                var listRolechild = Recursivelist(roleRecursive, new List<RoleModel>());
                listRolechild.ForEach(x => result.Add(x));
            }
            catch (Exception ex)
            {
                return result;
            }
            return result;
        }
        public RoleModel RecursiveOrther(List<RoleModel> listroles, Guid id)
        {
            RoleModel roleLv0 = new RoleModel();
            foreach (RoleModel temp in listroles)
            {
                if (temp.id == id)
                {
                    roleLv0 = temp;
                }
                foreach (RoleModel temp1 in listroles)
                {
                    if (temp1.role_parent_id == temp.id)
                    {
                        temp.childRoles.Add(temp1);
                    }
                }
            }
            return roleLv0;
        }
        public List<RoleModel> Recursivelist(RoleModel role, List<RoleModel> roles)
        {
            if (role.childRoles.Count > 0)
            {
                foreach (RoleModel temp in role.childRoles)
                {
                    roles.Add(temp);
                    Recursivelist(temp, roles);
                }
            }
            return roles;
        }
        public virtual async Task<bool> RemoveAndAutoConnectRoleLevel(Guid id)
        {
            using (IDbContextTransaction transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    var role_entity = await _db.BCC01_RoleHierarchy.FindAsync(id);
                    _db.BCC01_RoleHierarchy.Remove(role_entity);
                    var roles = await _db.BCC01_RoleHierarchy.Where(x => x.tenant_id == role_entity.tenant_id).ToListAsync();
                    List<RoleModel> roleModels = CommonFuncMain.ToObjectList<RoleModel>(roles);
                    var roleRecursive = RecursiveOrther(roleModels, id);
                    if (roleRecursive.childRoles.Any())
                    {
                        foreach (var item in roleRecursive.childRoles)
                        {
                            var entity = await _db.BCC01_RoleHierarchy.FindAsync(item.id);
                            entity.role_parent_id = role_entity.role_parent_id;
                            // Get the list of users owner the deleted role
                            var listUserDeleteRole = from u in _db.BCC01_User
                                                     where u.role_id.Equals(role_entity.id)
                                                     select u;
                            foreach (var user in listUserDeleteRole)
                            {
                                user.role_id = item.id;
                                _db.BCC01_User.Update(user);
                            }
                            // Get the list of users owner the deleted role
                            var listUserAgent = from u in _db.BCC01_User
                                                where u.role_id.Equals(item.id)
                                                select u;
                            // Get user report_to
                            var userReportTo = (from u in _db.BCC01_User
                                                where u.role_id.Equals(role_entity.role_parent_id)
                                                select u).FirstOrDefault();
                            foreach (var user in listUserAgent)
                            {
                                user.report_to = userReportTo.username;
                                _db.BCC01_User.Update(user);
                            }
                        }
                    }

                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();

                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    return false;
                }
            }
            return true;
        }
        public virtual async Task<bool> DeleteAndTransferRole(DeleteRoleRequest request)
        {
            using (IDbContextTransaction transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    var role_detele = await _db.BCC01_RoleHierarchy.FindAsync(request.role_delete_id);
                    var role_transfer = await _db.BCC01_RoleHierarchy.FindAsync(request.role_transfer_id);
                    var list_role_parrent_transfer = await _db.BCC01_RoleHierarchy.Where(x => x.role_parent_id.Equals(request.role_delete_id)).ToListAsync();

                    _db.BCC01_RoleHierarchy.Remove(role_detele);

                    foreach (var item in list_role_parrent_transfer)
                    {
                        item.role_parent_id = request.role_transfer_id;
                    }
                    // Get user report to delete
                    var userReportToRoleDetele = await _db.BCC01_User.Where(x => x.role_id.Equals(request.role_transfer_id) && x.tenant_id.Equals(role_detele.tenant_id)).FirstOrDefaultAsync();
                    // Get user report to transfer
                    var userReportToRoleTransfer = await _db.BCC01_User.Where(x => x.role_id.Equals(role_transfer.role_parent_id) && x.tenant_id.Equals(role_detele.tenant_id)).FirstOrDefaultAsync();
                    // Get list user by role delete and transfer 
                    var listUsers = await _db.BCC01_User.Where(x => x.role_id.Equals(request.role_delete_id) && x.tenant_id.Equals(role_detele.tenant_id)).ToListAsync();
                    foreach (var user in listUsers)
                    {
                        user.role_id = request.role_transfer_id;
                        user.report_to = userReportToRoleTransfer == null ? null : userReportToRoleTransfer.username;
                    }
                    // Auto connect role user
                    var roles = await _db.BCC01_RoleHierarchy.Where(x => x.tenant_id == role_detele.tenant_id).ToListAsync();
                    List<RoleModel> roleModels = CommonFuncMain.ToObjectList<RoleModel>(roles);
                    var roleRecursive = RecursiveOrther(roleModels, role_detele.id);
                    if (roleRecursive.childRoles.Any())
                    {
                        foreach (var item in roleRecursive.childRoles)
                        {
                            var entity = await _db.BCC01_RoleHierarchy.FindAsync(item.id);
                            entity.role_parent_id = role_detele.role_parent_id;
                            // get the list of users owner the deleted role
                            var listUserAgent = from u in _db.BCC01_User
                                                where u.role_id.Equals(item.id)
                                                select u;
                            foreach (var user in listUserAgent)
                            {
                                user.report_to = userReportToRoleDetele.username;
                            }
                        }
                    }

                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    return false;
                }
            }
            return true;
        }
        // Api lấy danh sách các username dưới cấp của user truyền vào. 
        public virtual async Task<List<UserRoleResponse>> GetListUserAgent(string username, Guid tenant_id)
        {
            List<UserRoleResponse> result = new List<UserRoleResponse>();
            try
            {
                var userRoleId = await _db.BCC01_User.Where(x => x.username == username)
                                            .Select(x => new { role_id = x.role_id }).FirstOrDefaultAsync();
                var roles = await _db.BCC01_RoleHierarchy.Where(x => x.tenant_id == tenant_id).ToListAsync();
                List<RoleModel> roleModels = CommonFuncMain.ToObjectList<RoleModel>(roles);
                var roleRecursive = RecursiveOrther(roleModels, userRoleId.role_id);
                // Lấy danh sách các role dưới cấp .
                var listRolechild = Recursivelist(roleRecursive, new List<RoleModel>());
                foreach (var item in listRolechild)
                {
                    var uEntity = await _db.BCC01_User.Where(x => x.role_id == item.id && x.tenant_id == tenant_id).Select(x => new UserRoleResponse { fullname = x.fullname, username = x.username, role_id = x.role_id }).ToListAsync();
                    if (uEntity != null)
                        result.AddRange(uEntity);
                }
            }
            catch (Exception)
            {
                return result;
            }
            return result;
        }
        // Lấy user cấp trên của user truyền vào.
        public virtual async Task<List<UserRoleResponse>> GetUserReportTo(Guid role_parent_id)
        {
            List<UserRoleResponse> result = new List<UserRoleResponse>();
            try
            {
                if (role_parent_id.Equals(Guid.Parse(Constants.ROOT_ROLE)))
                {
                    return result;
                }
                result = await _db.BCC01_User.Where(x => x.role_id.Equals(role_parent_id)).Select(s => new UserRoleResponse { fullname = s.fullname, username = s.username, role_id = s.role_id }).ToListAsync();
            }
            catch (Exception)
            {
                return result;
            }
            return result;
        }
        // Lấy list user ngang cấp của user truyền vào.
        public virtual async Task<List<UserRoleResponse>> GetListUserEqualByRoleId(Guid role_id, Guid tenant_id)
        {
            List<UserRoleResponse> result = new List<UserRoleResponse>();
            try
            {
                result = await _db.BCC01_User.Where(x => x.role_id.Equals(role_id) && x.tenant_id.Equals(tenant_id)).Select(s => new UserRoleResponse { fullname = s.fullname, username = s.username, phone = s.phone, role_id = s.role_id }).ToListAsync();
            }
            catch (Exception)
            {
                return result;
            }
            return result;
        }

        public async Task<List<BCC01_RoleHierarchy>> GetAllRoleByTenantId(Guid tenant_id)
        {
            return await _db.BCC01_RoleHierarchy.Where(x => x.tenant_id == tenant_id).ToListAsync();

        }
    }
}
