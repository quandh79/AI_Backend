using Common.Params.Base;
using Repository.BCC01_EF;
using Repository.CustomModel;
using System;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public interface IProfileRepository : IBaseRepositorySql<BCC01_Profile>
    {
        Task<bool> AutoRemoveRecordsWhenRemoveProfile(Guid id);
        Task<BCC01_Profile> Create(BCC01_Profile obj);
        Task<bool> DeleteUserInProfile(DeleteUserInProfile request);
        Task<ListResult<UserModel>> GetUsersByProfile(PagingParam param);
    }
}