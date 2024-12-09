using Common;
using Common.Params.Base;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repository.CustomModel;
using Repository.EF;
using System;
using System.Threading.Tasks;
using Management_AI.Common;
using Management_AI.CustomAttributes;
using Repository.BCC01_EF;
using Management_AI.Models.Common;
using Management_AI.Services.Implement.Abstracts;
using Management_AI.Models.Main;

namespace Management_AI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultCommonSettingController : ControllerBase
    {
        public readonly IDefaultCommonSettingService _defaultCommonSettingService;

        public DefaultCommonSettingController(IDefaultCommonSettingService defaultCommonSettingService)
        {
            _defaultCommonSettingService = defaultCommonSettingService;
        }

        [HttpPost]
        [Authorized]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] DefaultCommonSettingAddRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = await _defaultCommonSettingService.Create(request);
                if (response.status)
                {
                    return Ok(response);
                }
                else
                {
                    return new ResponseFail<BCC01_DefaultCommonSetting>().Error(response);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost]
        [Authorized]
        [Route("get-all")]
        public async Task<IActionResult> GetAll([FromBody] PagingParam param)
        {
            if (ModelState.IsValid)
            {
                var response = await _defaultCommonSettingService.GetAll(param);
                if (response.status)
                {
                    return Ok(response);
                }
                else
                {
                    return new ResponseFail<ListResult<BCC01_DefaultCommonSetting>>().Error(response);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost]
        [Authorized]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromBody] ItemModel<Guid> obj)
        {
            if (ModelState.IsValid)
            {
                var response = await _defaultCommonSettingService.Delete(obj.item);
                if (response.status)
                {
                    return Ok(response);
                }
                else
                {
                    return new ResponseFail<bool>().Error(response);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost]
        [Authorized]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] DefaultCommonSettingAddRequest obj)
        {
            if (ModelState.IsValid)
            {
                var response = await _defaultCommonSettingService.Update(obj);
                if (response.status)
                {
                    return Ok(response);
                }
                else
                {
                    return new ResponseFail<BCC01_DefaultCommonSetting>().Error(response);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
