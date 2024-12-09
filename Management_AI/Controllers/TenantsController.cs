using Common.Commons;
using Common.Params.Base;
using Repository.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.CustomModel;
using System;
using System.Net;
using System.Threading.Tasks;
using Management_AI.CustomAttributes;
using Management_AI.Services.Implement;
using Management_AI.Common;
using Common;
using Newtonsoft.Json;
using Repository.BCC01_EF;
using Management_AI.Common.ResponAPI3rd;
using Management_AI.Models.Common;
using Management_AI.Models.Main;
using Management_AI.Services.Implement.Abstracts;

namespace Management_AI.Controllers
{
    [Route("api/tenant")]
    [ApiController]
    [Authorized]
    public class TenantsController : ControllerBase
    {
        private readonly ITenantsService _tenantsService;
        public TenantsController(ITenantsService tenantsService)
        {
            _tenantsService = tenantsService;
        }

        [HttpPost]
        [Route("get-all")]
        public async Task<IActionResult> GetAll([FromBody] PagingParam param)
        {
            ResponseService<ListResult<TenantResponse>> response = await _tenantsService.GetAll(param);
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
        [Route("get-by-id")]
        public async Task<IActionResult> GetById([FromBody] ItemModel<Guid> obj)
        {
            ResponseService<BCC01_Tenants> response = await _tenantsService.GetById(obj.item);
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
        public async Task<IActionResult> Create([FromBody] RegisterRequest obj)
        {
            ResponseService<TenantResponse> response = await _tenantsService.Create(obj);
            if (response != null && response.status)
            {
                return Ok(response);
            }
            else
            {
                return new ResponseFail<TenantResponse>().Error(response);
            }
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] BCC01_Tenants obj)
        {
            ResponseService<BCC01_TenantsResponse> response = await _tenantsService.Update(obj);
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
            ResponseService<bool> response = await _tenantsService.Delete(obj.item);
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
        [Route("update-is-active")]
        public async Task<IActionResult> UpdateIsActive([FromBody] UpdateIsActiveModel obj)
        {
            ResponseService<bool> response = await _tenantsService.UpdateIsActive(obj.IsActive, obj.id);
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
        [Route("check-exist-tenant-by-email")]
        public async Task<IActionResult> CheckExitByEmail([FromBody] ItemModel<string> obj)
        {
            ResponseService<TenantResponse> response = await _tenantsService.CheckExitTenantByEmail(obj.item);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response.message);
            }
        }

    }
}
