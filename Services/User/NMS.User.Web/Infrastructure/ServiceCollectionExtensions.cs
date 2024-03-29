﻿using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NMS.User.Domain.Identity;
using NMS.User.Infrastructure;
using NMS.User.Service;
using Shared.Infrastructure.Core.Extensions;
using SkyApm.Utilities.DependencyInjection;
using System;

namespace NMS.User.Web.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
           // services.ConfigureStartupConfig<MongodbHostConfig>(configuration.GetSection("MongodbHostConfig"));
            RedisHelper.Initialization(new CSRedis.CSRedisClient(configuration.GetConnectionString("CsRedisCachingConnectionString")));//redis初始化
            services.AddSkyApmExtensions();//sky Apm监控
            services.AddCorsConfig();//跨域配置
            services.AddIdentityOptions();//身份认证配置
            services.AddAutoMapper(typeof(Startup));//automapper
            services.AddMediatR(typeof(ServiceStartup));//CQRS
            services.AddHealthChecks();//健康检查
            services.AddEventBus<ApplicationDbContext>(configuration);//事件总线
            services.AddConsulGrpc(configuration);//服务注册
            services.AddController();//api控制器
            services.AddIdentity<ApplicationUser, ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();//Identity  usermanager和rolemanager服务注入
            services.AddSwaggerInfo($"{typeof(Startup).Namespace}");
            return services;

        }
        /// <summary>
        /// 身份认证配置
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static void AddIdentityOptions(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                // 密码配置
                options.Password.RequireDigit = false;//是否需要数字(0-9).
                options.Password.RequiredLength = 6;//设置密码长度最小为6
                options.Password.RequireNonAlphanumeric = false;//是否包含非字母或数字字符。
                options.Password.RequireUppercase = false;//是否需要大写字母(A-Z).
                options.Password.RequireLowercase = false;//是否需要小写字母(a-z).
                // 锁定设置
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);//账户锁定时长30分钟
                options.Lockout.MaxFailedAccessAttempts = 10;//10次失败的尝试将账户锁定
                options.Lockout.AllowedForNewUsers = false;
                // 用户设置
                options.User.RequireUniqueEmail = false; //是否Email地址必须唯一
            });
        }
    }
}
