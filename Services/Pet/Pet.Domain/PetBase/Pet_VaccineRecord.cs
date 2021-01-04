using Shared.Domain.Abstractions;
using System;

namespace Pet.Domain.PetBase
{
    /// <summary>
    /// 功能描述    ：宠物疫苗记录  
    /// 创 建 者    ：鲁岩奇
    /// 创建日期    ：2020/12/29 16:05:04 
    /// 最后修改者  ：Administrator
    /// 最后修改日期：2020/12/29 16:05:04 
    /// </summary>
    public class Pet_VaccineRecord:Entity
    {
        /// <summary>
        /// 疫苗名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 疫苗图片
        /// </summary>
        public string PicPath { get; set; }
        /// <summary>
        /// 疫苗时间
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// 宠物id
        /// </summary>
        public Guid PetId { get; set; }
    }
}
