using Common;
using Common.Commons;
using Common.Params.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repository.CustomModel;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Management_AI.Common;
using Management_AI.CustomAttributes;
using Management_AI.Models.Common;
using Management_AI.Services.Implement.Abstracts;
using Management_AI.Models.Main;

namespace Management_AI.Controllers
{
    [Route("api/module")]
    [ApiController]
    public class ModuleController : ControllerBase
    {
        private readonly IModuleService _moduleService;
        public ModuleController(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }

        [HttpPost]
        [Authorized]
        [Route("get-all")]
        public async Task<IActionResult> GetAll([FromBody] PagingParam param)
        {
            ResponseService<ListResult<ModuleResponse>> response = await _moduleService.GetAll(param);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response.exception);
            }
        }

        [HttpGet]
        [Authorized]
        [Route("get-all-is-show")]
        public async Task<IActionResult> GetAllWithIsShow()
        {
            if (ModelState.IsValid)
            {
                var response = await _moduleService.GetAllWithIsShow();
                if (response.status)
                {
                    return Ok(response);
                }
                else
                {
                    return new ResponseFail<List<ModuleCustomResponse>>().Error(response);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        [HttpPost]
        [Authorized]
        [Route("get-by-id")]
        public async Task<IActionResult> GetById([FromBody] ItemModel<Guid> obj)
        {
            ResponseService<ModuleResponse> response = await _moduleService.GetById(obj.item);
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
        public async Task<IActionResult> Create([FromBody] ModuleRequest obj)
        {
            ResponseService<ModuleResponse> response = await _moduleService.Create(obj);
            if (response.status)
            {
                return Ok(response);

            }
            else
            {
                return new ResponseFail<ModuleResponse>().Error(response);
            }
        }
        [HttpPost]
        [Authorized]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] ModuleRequest obj)
        {
            ResponseService<ModuleResponse> response = await _moduleService.Update(obj);
            if (response.status)
            {
                if (response.data == null)
                    return BadRequest("Module does not exists");
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
            ResponseService<bool> response = await _moduleService.Delete(obj.item);
            if (response.status)
            {
                if (response.data == false)
                    return BadRequest("Module does not exists");
                return Ok(response);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response.exception);
            }
        }
        [HttpPost]
        [Route("asyncModul")]
        public async Task<IActionResult> asyncModul()
        {
            ResponseService<bool> response = await _moduleService.AsyncDefaultModule();
            return Ok(response);
        }
    }
}
