using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Shortener.Common.Extensions;

namespace Beta.API.Configuration
{
    public static class ApiConfig
    {
        public static IServiceCollection WebApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            #region SWAGGER
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Beta.API", Version = "v1" });
            });
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

            app.UseSwagger();

            return app;
        }
    }
}
