using Shared.Domain.Abstractions;
using System;

namespace Pet.Domain.PetBase
{
    /// <summary>
    /// 功能描述    ：宠物病史  
    /// 创 建 者    ：鲁岩奇
    /// 创建日期    ：2020/12/29 16:08:35 
    /// 最后修改者  ：Administrator
    /// 最后修改日期：2020/12/29 16:08:35 
    /// </summary>
    public class Pet_MedicalHistory:Entity
    {
        /// <summary>
        /// 病名称
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// 宠物id
        /// </summary>
        public Guid PetId { get; set; }
    }
}
