using Common.Commons;
using Common.Params.Base;
using Management_AI.CustomAttributes;
using Management_AI.Models.Common;
using Management_AI.Models.Main;
using Management_AI.Services.Implement.Abstracts;
using Management_AI.Services.Implement.Abtracts;
using Microsoft.AspNetCore.Mvc;
using Repository.CustomModel;

namespace Management_AI.Controllers
{
    [Route("api/profile")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;
        private readonly IMapProfileUserService _mapProfileUserService;

        public ProfileController(IProfileService profileService, IMapProfileUserService mapProfileUserService)
        {
            _profileService = profileService;
            _mapProfileUserService = mapProfileUserService;
        }
        [HttpPost]
        [Authorized]
        [Route("get-all")]
        public async Task<IActionResult> GetAll([FromBody] PagingParam param)
        {
            ResponseService<ListResult<ProfileResponse>> response = await _profileService.GetAll(param);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response.exception);
            }
        }
        [HttpPost]
        [Authorized]
        [Route("get-by-id")]
        public async Task<IActionResult> GetById([FromBody] ItemModel<Guid> obj)
        {
            ResponseService<ProfileResponse> response = await _profileService.GetById(obj.item);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response.exception);
            }
        }
        [HttpPost]
        [Authorized]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] ProfileRequest obj)
        {
            ResponseService<ProfileResponse> response = await _profileService.Create(obj);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response.exception);
            }
        }
        [HttpPost]
        [Authorized]
        [Route("get-list-user-by-profile")]
        public async Task<IActionResult> GetUsersByProfile([FromBody] PagingParam param)
        {
            var response = await _profileService.GetListUserByProfile(param);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response.exception);
            }
        }
        [HttpPost]
        [Authorized]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] ProfileRequest obj)
        {
            ResponseService<ProfileResponse> response = await _profileService.Update(obj);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response.exception);
            }
        }
        [HttpPost]
        [Authorized]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromBody] ItemModel<Guid> obj)
        {
            ResponseService<bool> response = await _profileService.DeleteTransaction(obj.item);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response.exception);
            }
        }
        [HttpPost]
        [Authorized]
        [Route("update-permission-in-profile")]
        public async Task<IActionResult> UpdatePermissionInProfile([FromBody] List<UpdatePermissonRequest> listrequest)
        {
            ResponseService<bool> response = await _profileService.UpdatePermissionInProfile(listrequest);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response.exception);
            }
        }
        [HttpPost]
        [Authorized]
        [Route("delete-user-in-profile")]
        public async Task<IActionResult> DeleteUserInProfile([FromBody] DeleteUserInProfile request)
        {
            ResponseService<bool> response = await _profileService.DeleteUserInProfile(request);
            if (response.status)
            {
                if (response.data)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest("Delete Failed");
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response.exception);
            }
        }
        [HttpPost]
        [Authorized]
        [Route("add-user-to-profile")]
        public async Task<IActionResult> AddUserToProfile([FromBody] AddUserToProfile obj)
        {

            var response = await _mapProfileUserService.Create(obj);
            if (response.status)
            { 
                return Ok(response);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response.exception);
            }
        }
    }
}
