using Common.Commons;
using Common.Params.Base;
using Repository.CustomModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using Management_AI.Models.Main;

namespace Management_AI.Services.Implement.Abstracts
{
    public interface IProfileService
    {
        Task<ResponseService<ProfileResponse>> Create(ProfileRequest obj);
        Task<ResponseService<bool>> DeleteTransaction(object obj);
        Task<ResponseService<bool>> DeleteUserInProfile(DeleteUserInProfile request);
        Task<ResponseService<ListResult<ProfileResponse>>> GetAll(PagingParam param);
        Task<ResponseService<ProfileResponse>> GetById(object id);
        Task<ResponseService<ListResult<UserModel>>> GetListUserByProfile(PagingParam param);
        Task<ResponseService<ProfileResponse>> Update(ProfileRequest obj);
        Task<ResponseService<bool>> UpdateIsActive(bool isActive, object id);
        Task<ResponseService<bool>> UpdatePermissionInProfile(List<UpdatePermissonRequest> listrequest);
    }
}