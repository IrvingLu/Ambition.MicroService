using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Primitives;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GateWay.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        // 服务注入
        public void ConfigureServices(IServiceCollection services)
        {
            //资源服务器配置
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddIdentityServerAuthentication("NmsKey", options =>
            {
                options.Authority = Configuration["ApplicationConfiguration:IdentityAddress"];
                options.RequireHttpsMetadata = false;
                options.ApiName = "api";
                options.ApiSecret = "secret";
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if (context.Request.Query.TryGetValue("token", out StringValues token))
                        {
                            context.Token = token;
                        }
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        var te = context.Exception;
                        return Task.CompletedTask;
                    }
                };
            });
            services.AddControllers();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("ApiGateway", new OpenApiInfo { Title = "网关服务", Version = "v1" });
            });
            services.AddOcelot().AddConsul();
        }
        // 中间件
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            };
            var apis = new List<string> { "Patientv1" };
            app.UseSwagger();
               app.UseSwaggerUI(options =>
               {
                   apis.ForEach(m =>
                   {
                       options.SwaggerEndpoint($"/{m}/swagger.json", m);
                   });
               });
            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });//api
            app.UseOcelot().Wait();
        }
    }
}
