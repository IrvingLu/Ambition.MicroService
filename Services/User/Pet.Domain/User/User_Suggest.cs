using Shared.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pet.User.Domain.User
{
    /// <summary>
    /// 功能描述    ：用户反馈建议表  
    /// 创 建 者    ：鲁岩奇
    /// 创建日期    ：2020/12/30 16:01:01 
    /// 最后修改者  ：Administrator
    /// 最后修改日期：2020/12/30 16:01:01 
    /// </summary>
    public class User_Suggest : Entity
    {
        /// <summary>
        /// 反馈内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 图片路径
        /// </summary>
        public string PicPath { get; set; }
        /// <summary>
        /// 联系方式，方便联系
        /// </summary>
        public string ContactWay { get; set; }
    }
}
