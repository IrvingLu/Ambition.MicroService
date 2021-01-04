using System;

namespace Pet.User.Web.Application.Tenant.Queries.Dto
{
    public class TenantInfoDto
    {
        /// <summary>
        /// 租户名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// logo图
        /// </summary>
        public string LogoPath { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public string Latitude { get; set; }
        /// <summary>
        /// 营业开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 营业结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 公告
        /// </summary>
        public string Announcement { get; set; }
        /// <summary>
        /// 店铺活动图
        /// </summary>
        public string ActivityPath { get; set; }

    }
}
