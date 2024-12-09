using Common.Commons;
using Common.Params.Base;
using Management_AI.Models.Common;
using Management_AI.Models.Main;
using Management_AI.Services.Implement.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Management_AI.CustomAttributes;
using Repository.BCC01_EF;
using Repository.CustomModel;
using Repository.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Management_AI.Controllers
{
    [Route("api/role-hierarchy")]
    [ApiController]
    public class RoleHierarchyController : ControllerBase
    {
        private readonly IRoleHierarchyService _roleHierarchyService;
        public RoleHierarchyController(IRoleHierarchyService roleHierarchyService)
        {
            _roleHierarchyService = roleHierarchyService;

        }
        [HttpPost]
        [Authorized]
        [Route("get-role-recursive")]
        public async Task<IActionResult> RoleRecursive()
        {
            ResponseService<List<RoleModel>> response = await _roleHierarchyService.RoleRecursive();
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
        [Route("get-all-role-by-tenantId")]
        public async Task<IActionResult> RoleRecursive([FromBody] ItemModel<Guid> obj)
        {
            ResponseService<ListResult<RoleHierarchyResponse>> response = await _roleHierarchyService.GetAllByTenantId(obj.item);
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
        [Route("get-all")]
        public async Task<IActionResult> GetAll([FromBody] PagingParam param)
        {
            ResponseService<ListResult<RoleHierarchyResponse>> response = await _roleHierarchyService.GetAll(param);
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
            ResponseService<BCC01_RoleHierarchy> response = await _roleHierarchyService.GetById(obj.item);
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
        public async Task<IActionResult> Create([FromBody] RoleHierarchyRequest obj)
        {
            ResponseService<BCC01_RoleHierarchy> response = await _roleHierarchyService.Create(obj);
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
        public async Task<IActionResult> Update([FromBody] RoleHierarchyRequest obj)
        {
            ResponseService<BCC01_RoleHierarchy> response = await _roleHierarchyService.Update(obj);
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
            ResponseService<bool> response = await _roleHierarchyService.Delete(obj.item);
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
        [Route("delete-transfer")]
        public async Task<IActionResult> DeleteAndTransferRole([FromBody] DeleteRoleRequest request)
        {
            ResponseService<bool> response = await _roleHierarchyService.DeleteAndTransferRole(request);
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
        [Route("get-user-report-to")]
        public async Task<IActionResult> GetListUserReportTo([FromBody] ItemModel<Guid> obj)
        {
            ResponseService<List<UserRoleResponse>> response = await _roleHierarchyService.GetListUserReportTo(obj.item);
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
        [PermissionAttributeFilter("User Management", "access")]
        [Route("get-user-equal-by-roleid")]
        //[SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<List<UserRoleResponse>>))]
        //[SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<List<UserRoleResponse>>))]
        public async Task<IActionResult> GetListUserEqualByRoleId([FromBody] ItemModel<Guid> obj)
        {
            ResponseService<List<UserRoleResponse>> response = await _roleHierarchyService.GetListUserEqualByRoleId(obj.item);
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
