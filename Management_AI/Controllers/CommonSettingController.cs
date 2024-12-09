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
using Management_AI.Models.Common;
using Repository.BCC01_EF;
using Management_AI.Models.Main;
using Management_AI.Services.Implement.Abstracts;

namespace Management_AI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonSettingController : ControllerBase
    {
        public readonly ICommonSettingService _commonSettingService;

        public CommonSettingController(ICommonSettingService commonSettingService)
        {
            _commonSettingService = commonSettingService;
        }

        [HttpPost]
        [Authorized]
        [Route("get-all")]
        [PermissionAttributeFilter("Common Setting", "access")]
        public async Task<IActionResult> GetAll([FromBody] PagingParam param)
        {
            if (ModelState.IsValid)
            {
                var response = await _commonSettingService.GetAll(param);
                if (response.status)
                {
                    return Ok(response);
                }
                else
                {
                    return new ResponseFail<ListResult<BCC01_CommonSetting>>().Error(response);
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
        [PermissionAttributeFilter("Common Setting", "edit")]
        public async Task<IActionResult> Update([FromBody] CommonSettingAddRequest obj)
        {
            if (ModelState.IsValid)
            {
                var response = await _commonSettingService.Update(obj);
                if (response.status)
                {
                    return Ok(response);
                }
                else
                {
                    return new ResponseFail<BCC01_CommonSetting>().Error(response);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
