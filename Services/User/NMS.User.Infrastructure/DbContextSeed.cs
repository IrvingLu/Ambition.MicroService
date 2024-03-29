﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NMS.User.Domain.Identity;
using System;
using System.Threading.Tasks;

namespace NMS.User.Infrastructure
{
    public class DbContextSeed
    {
        /// <summary>
        /// 默认权限
        /// </summary>
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var context = (ApplicationDbContext)serviceProvider.GetService(typeof(ApplicationDbContext));
            var roleManager = (RoleManager<ApplicationRole>)serviceProvider.GetService(typeof(RoleManager<ApplicationRole>));
            var userManager = (UserManager<ApplicationUser>)serviceProvider.GetService(typeof(UserManager<ApplicationUser>));
            string roleAdmin = "Administrator";
            if (await context.Users.AnyAsync())
            {
                return;
            }
            await CreateDefaultRole(roleManager, roleAdmin);
            var user = await CreateDefaultUser(userManager);
            await AddDefaultRoleToDefaultUser(userManager, roleAdmin, user);
        }
        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="roleManager"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        private static async Task CreateDefaultRole(RoleManager<ApplicationRole> roleManager, string role)
        {
            await roleManager.CreateAsync(new ApplicationRole { Name=role});
        }
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="roleManager"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        private static async Task<ApplicationUser> CreateDefaultUser(UserManager<ApplicationUser> userManager)
        {
            var user = new ApplicationUser { Email = "18504029848@163.com", UserName = "admin" };
            await userManager.CreateAsync(user, "123456");
            var createdUser = await userManager.FindByEmailAsync("18504029848@163.com");
            return createdUser;
        }
        /// <summary>
        /// 为用户添加权限
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="role"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private static async Task AddDefaultRoleToDefaultUser(UserManager<ApplicationUser> userManager, string role, ApplicationUser user)
        {
            await userManager.AddToRoleAsync(user, role);
        }
    }
}
