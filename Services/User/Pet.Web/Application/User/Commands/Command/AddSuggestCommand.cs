using MediatR;

namespace Pet.User.Web.Application.User.Commands.Command
{
    public class AddSuggestCommand:IRequest
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
