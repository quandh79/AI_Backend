using Common.Commons;
using Common.Params.Base;
using Management_AI.Models.Common;
using Management_AI.Models.Main;
using Management_AI.Services.Implement.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Management_AI.Common;
using Management_AI.Common.ResponAPI3rd;
using Management_AI.CustomAttributes;
using Management_AI.Services.Implement;
using Repository.BCC01_EF;
using Repository.CustomModel;
using Repository.EF;
using System;
using System.Threading.Tasks;

namespace Management_AI.Controllers
{
    [Route("api/team")]
    [ApiController]
    [Authorized]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;
        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }
        [HttpPost]
        [Route("get-all")]

        public async Task<IActionResult> GetAll([FromBody] PagingParam param)
        {
            ResponseService<ListResult<BCC01_Teams>> response = await _teamService.GetAll(param);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response.message);
            }
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] TeamModel obj)
        {
            ResponseService<TeamModel> response = await _teamService.Create(obj);
            if (response != null && response.status)
            {
                return Ok(response);
            }
            else
            {
                return new ResponseFail<TeamModel>().Error(response);
            }
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] TeamModel obj)
        {
            ResponseService<TeamModel> response = await _teamService.Update(obj);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response.message);
            }
        }

        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromBody] ItemModel<Guid> obj)
        {
            ResponseService<bool> response = await _teamService.Delete(obj.item);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return new ResponseFail<bool>().Error(response);
            }
        }
        [HttpPost]
        [Route("add-user-in-team")]
        public async Task<IActionResult> AddUserInTeam(AddMapTeamUserModel model)
        {
            var res = await _teamService.AddUserNameInTeam(model);
            if (res.status)
            {
                return Ok(res);
            }
            else
            {
                return new ResponseFail<bool>().Error(res);
            }
        }
        [HttpPost]
        [Route("delete-user-in-team")]
        public async Task<IActionResult> DeleteUserInTeam(DeleteMapTeamUserModel model)
        {
            var res = await _teamService.DeleteUserNameInTeam(model);
            if (res.status)
            {
                return Ok(res);
            }
            else
            {
                return new ResponseFail<bool>().Error(res);
            }
        }
        [HttpPost]
        [Route("get-all-user-not-in-team")]
        [Authorized]
        public async Task<IActionResult> GetAllUserNotInTeam(ItemModel<Guid> item)
        {
            var res = await _teamService.GetAllUserNotInTeam(item.item);
            if (res.status)
            {
                return Ok(res);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, res.message);
            }
        }
        [HttpPost]
        [Route("get-all-user-in-team")]
        [Authorized]
        public async Task<IActionResult> GetAllUserInTeam(ItemModel<Guid> item)
        {
            var res = await _teamService.GetAllUserInTeam(item.item);
            if (res.status)
            {
                return Ok(res);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, res.message);
            }
        }
    }
}
