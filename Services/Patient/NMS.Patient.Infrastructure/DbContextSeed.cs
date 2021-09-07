using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace NMS.Patient.Infrastructure
{
    public class DbContextSeed
    {
        /// <summary>
        /// 初始化数据 
        /// </summary>
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var context = (ApplicationDbContext)serviceProvider.GetService(typeof(ApplicationDbContext));
            if (await context.Patient.AnyAsync())
            {
                return;
            }
        }
    }
}
