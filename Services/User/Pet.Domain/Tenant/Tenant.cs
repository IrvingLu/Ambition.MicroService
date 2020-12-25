using Microsoft.EntityFrameworkCore;
using Shared.Domain.Abstractions;
using System;
using System.Collections.Generic;

namespace Pet.User.Domain.Tenant
{
    /// <summary>
    /// 功能描述    ：租户领域(商家)  
    /// 创 建 者    ：Administrator
    /// 创建日期    ：2020/12/25 13:48:53 
    /// 最后修改者  ：Administrator
    /// 最后修改日期：2020/12/25 13:48:53 
    /// </summary>
    public class Tenant : Entity, IAggregateRoot
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
        /// <summary>
        /// 是否为官方店铺
        /// </summary>
        public bool IsOfficial { get; set; }
        /// <summary>
        /// 是否为官方推荐店铺
        /// </summary>
        public bool IsOfficialRecommend { get; set; }
        /// <summary>
        /// 是否锁定商铺
        /// </summary>
        public bool LockoutEnabled { get; set; }
        /// <summary>
        /// 营业许可信息
        /// </summary>
        public BusinessInfo BusinessInfo { get; set; }
    }
    [Owned]
    public class BusinessInfo : ValueObject
    {
        /// <summary>
        /// 营业执照
        /// </summary>
        public string LicensePath { get; private set; }
        /// <summary>
        /// 法人
        /// </summary>
        public string LegalPerson { get; private set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact { get; private set; }
        /// <summary>
        /// 联系人电话
        /// </summary>
        public string ContactPhone { get; private set; }

        public BusinessInfo() { }
        public BusinessInfo(string licensePath, string legalPerson, string countyContact, string contactPhone)
        {
            LicensePath = licensePath;
            LegalPerson = legalPerson;
            Contact = countyContact;
            ContactPhone = contactPhone;
        }
        protected override IEnumerable<object> GetAtomicValues()
        {
            throw new NotImplementedException();
        }
    }
}
