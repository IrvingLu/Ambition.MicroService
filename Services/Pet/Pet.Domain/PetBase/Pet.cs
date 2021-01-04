using AutoMapper.Configuration.Annotations;
using Shared.Domain.Abstractions;
using Shared.Domain.Abstractions.Identity;
using System;
using System.ComponentModel;

namespace Pet.Domain.PetBase
{
    /// <summary>
    /// 功能描述    ：宠物基础信息  
    /// 创 建 者    ：鲁岩奇
    /// 创建日期    ：2020/12/29 15:23:19 
    /// 最后修改者  ：Administrator
    /// 最后修改日期：2020/12/29 15:23:19 
    /// </summary>
    public class Pet : Entity, IAggregateRoot
    {
        #region Base
        /// <summary>
        /// 宠物名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public Sex Sex { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birthday { get; set; }
        /// <summary>
        /// 品种
        /// </summary>
        public string Variety { get; set; }
        /// <summary>
        /// 宠物照片
        /// </summary>
        public string PicPath { get; set; }
        #endregion

        ///// <summary>
        ///// 关联用户id
        ///// </summary>
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }


    public enum Sex
    {
        [Description("公")]
        Male,
        [Description("母")]
        Female

    }
}
