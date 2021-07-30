using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using Shortener.Common.Extensions;

namespace Alpha.API.Configuration
{
    public static class ApiConfig
    {
        public static IServiceCollection WebApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddControllers();

            #region VERSIONING
            services.AddApiVersioning(opt =>
            {
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(opt =>
            {
                opt.GroupNameFormat = "'v'VVV";
                opt.SubstituteApiVersionInUrl = true;
            });
            #endregion

            #region SWAGGER
            services.AddSwaggerConfig();
            #endregion

            #region CORS
            services.AddCors(opt =>
            {
                opt.AddPolicy("Development",
                    builder => builder
                        .AllowAnyMethod()
                        .AllowAnyOrigin()
                        .AllowAnyHeader());

                opt.AddPolicy("Production",
                    builder => builder
                        .WithMethods("GET", "OPTION")
                        //.WithOrigins("url do front") // TODO:
                        .AllowAnyOrigin()
                        .AllowAnyHeader());
            });
            #endregion

            #region MassTransit RabbitMQ
            services.AddMassTransit(configuration);
            #endregion

            return services;
        }

        public static IApplicationBuilder UseApiConfiguration(this IApplicationBuilder app,
            IApiVersionDescriptionProvider provider,
            IConfiguration configuration)
        {

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwaggerConfig(provider);
            return app;
        }
    }
}
