using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using Management_AI.Config;

namespace Management_AI.Extensions
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Management_AI API", Version = "v1" });
                c.OperationFilter<AuthorizationOperationFiltercs>();
            });
            return services;
        }

        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger().UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Management_AI API");
                options.DocumentTitle = "Management_AI";
                options.RoutePrefix = string.Empty;  // Set Swagger UI at apps root
            });
            return app;
        }
    }
}
