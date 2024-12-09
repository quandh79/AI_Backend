using AutoMapper;
using Common;
using Common.Commons;
using Common.Models;
using System;
using System.Diagnostics;
using Management_AI.Config;
using Management_AI.Common;
using ILogger = Common.Commons.ILogger;


namespace Management_AI.Services.Implement
{
    public class BaseService
    {
        protected readonly ILogger _logger;
        protected readonly IMapper _mapper;

        protected BaseService(ILogger logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        public static string GetMethodName(StackTrace stackTrace)
        {
            return CommonFunc.GetMethodName(stackTrace);
        }
    }
}
