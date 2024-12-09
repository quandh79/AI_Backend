using Common.Commons;
using Common.Params.Base;
using Management_AI.Common;
using Management_AI.CustomAttributes;
using Management_AI.Models.Common;
using Management_AI.Models.Main;
using Management_AI.Services.Implement.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Repository.CustomModel;

namespace Management_AI.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService usertService)
        {
            _userService = usertService;
        }
        [HttpPost]
        [Authorized]
        [Route("get-all")]
        [PermissionAttributeFilter("User Management", "access")]
        public async Task<IActionResult> GetAll([FromBody] PagingParam param)
        {
            ResponseService<ListResult<UserCustomResponse>> response = await _userService.GetAll(param);
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
        [PermissionAttributeFilter("User Management", "create")]
        public async Task<IActionResult> Create([FromBody] UserRequest obj)
        {
            ResponseService<UserCustomResponse> response = await _userService.Create(obj);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return new ResponseFail<UserCustomResponse>().Error(response);
            }
        }
        [HttpPost]
        [Authorized]
        [Route("update")]
        [PermissionAttributeFilter("User Management", "edit")]
        public async Task<IActionResult> Update([FromBody] UserUpdateRequest obj)
        {
            ResponseService<UserCustomResponse> response = await _userService.Update(obj);
            if (response.status)
            {
                if (response.data == null)
                    return BadRequest("Username does not exists");
                return Ok(response);
            }
            else
            {
                return new ResponseFail<UserCustomResponse>().Error(response);
            }
        }
        [HttpPost]
        [Authorized]
        [Route("delete")]
        [PermissionAttributeFilter("User Management", "delete")]
        public async Task<IActionResult> Delete([FromBody] ItemModel<string> obj)
        {
            ResponseService<bool> response = await _userService.Delete(obj.item);
            if (response.status)
            {
                if (!response.data)
                    return BadRequest("Username does not exists");
                return Ok(response);
            }
            else
            {
                return new ResponseFail<bool>().Error(response);
            }
        }
        [HttpPost]
        [Authorized]
        [Route("get-by-id")]
        [PermissionAttributeFilter("User Management", "access")]
        public async Task<IActionResult> GetById([FromBody] ItemModel<string> obj)
        {
            ResponseService<UserCustomResponse> response = await _userService.GetById(obj.item);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return new ResponseFail<UserCustomResponse>().Error(response);
            }
        }
        [HttpPost]
        [Authorized]
        [Route("get-user-state")]
        public async Task<IActionResult> GetUserState([FromBody] UsernameRequest param)
        {
            ResponseService<UserState> response = await _userService.GetUserState(param);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return new ResponseFail<UserState>().Error(response);
            }
        }
        [HttpPost]
        [Authorized]
        [Route("get-list-user-role")]
        public async Task<IActionResult> GetListUserRole([FromBody] UsernameRequest request)
        {
            ResponseService<ListResult<UserCustomResponse>> response = await _userService.GetListUserRole(request);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {

                return new ResponseFail<ListResult<UserCustomResponse>>().Error(response);
            }
        }
        [HttpPost]
        [Authorized]
        [Route("check-exist-by-email")]
        public async Task<IActionResult> CheckExistByEmail([FromBody] ItemModel<string> items)
        {
            var response = await _userService.CheckExistByEmail(items);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {

                return new ResponseFail<UserModel>().Error(response);
            }
        }
    }
}
