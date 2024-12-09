using Common.Commons;
using Common.Params.Base;
using Management_AI.Models.Common;
using Management_AI.Models.Main;
using Management_AI.Services.Implement.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Management_AI.CustomAttributes;
using Repository.CustomModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Management_AI.Controllers
{
    [Route("api/permission")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;
        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }
        [HttpPost]
        [Authorized]
        [Route("get-all")]
        public async Task<IActionResult> GetAll([FromBody] PagingParam param)
        {
            ResponseService<ListResult<PermissionResponse>> response = await _permissionService.GetAll(param);
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
            ResponseService<PermissionResponse> response = await _permissionService.GetById(obj.item);
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
        [Route("get-list-permission-by-user")]
        public async Task<IActionResult> GetListPermissionByUser([FromBody] ItemModel<string> obj)
        {
            List<PermissionResShort> res = await _permissionService.GetListPermissionByUser(obj.item);
            ResponseService<List<PermissionResShort>> response = new ResponseService<List<PermissionResShort>>(res);
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
        [Route("get-permission-a-object-by-user")]
        public async Task<IActionResult> GetPermissionAObjectByUser([FromBody] PermissionAObjectRequest request)
        {
            PermissionResShort res = await _permissionService.GetPermissionAObjectByUser(request);
            ResponseService<PermissionResShort> response = new ResponseService<PermissionResShort>(res);
            if (response.status)
            {
                if (response.data == null) response.status = false;
                return Ok(response);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response.exception);
            }
        }
        [HttpPost]
        [Authorized]
        [Route("get-status-permission-type-a-object-by-user")]
        public async Task<IActionResult> GetStatusPermissionTypeAObjectByUser([FromBody] PermissionAObjectByTypeRequest request)
        {
            ResponseService<bool> response = null;
            var res = await _permissionService.GetStatusPermissionTypeAObjectByUser(request);
            if (res.GetType().Equals(typeof(bool)))
            {
                response = new ResponseService<bool>((bool)res);
            }
            else
            {
                response = new ResponseService<bool>((string)res);
            }
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
        [Route("get-status-permission-by-type-and-name")]
        public async Task<IActionResult> GetStatusPermissionByTypeAndName([FromBody] GetPermissionByTypeAndName request)
        {
            ResponseService<bool> response = null;

            var res = await _permissionService.GetStatusPermissionByTypeAndName(request);
            if (res.GetType().Equals(typeof(bool)))
            {
                response = new ResponseService<bool>((bool)res);
            }
            else
            {
                response = new ResponseService<bool>((string)res);
            }
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
        [Route("get-list-permission-by-profile")]
        public async Task<IActionResult> GetListPermissionByProfileId([FromBody] PagingParam param)
        {
            ResponseService<ListResult<PermissionResponse>> response = await _permissionService.GetListPermissionByProfileId(param);
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
