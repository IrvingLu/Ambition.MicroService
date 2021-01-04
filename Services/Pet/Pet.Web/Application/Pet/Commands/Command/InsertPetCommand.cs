using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Pet.Domain.PetBase;

namespace Pet.Web.Application.Pet.Commands.Command
{
    public class InsertPetCommand:IRequest
    {
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

        ///// <summary>
        ///// 关联用户id
        ///// </summary>
        public string ApplicationUserId { get; set; }
    }
}
