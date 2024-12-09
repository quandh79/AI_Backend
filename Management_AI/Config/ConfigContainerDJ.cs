using Microsoft.Extensions.DependencyInjection;
using System;

namespace Management_AI.Config
{
    public class ConfigContainerDJ
    {
        public static IServiceProvider _serviceProvider { get; set; }

        public static T CreateInstance<T>()
        {
            return _serviceProvider.GetService<T>();
        }
    }
}
