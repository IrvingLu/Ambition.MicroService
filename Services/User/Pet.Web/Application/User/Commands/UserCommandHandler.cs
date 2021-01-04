using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Pet.User.Domain.User;
using Pet.User.Infrastructure.Repositories;
using Pet.User.Web.Application.User.Commands.Command;
using Pet.User.Web.Application.User.Commands.Dto;
using Shared.Domain.Abstractions.Identity;
using Shared.Infrastructure.Core.Dapper;
using Shared.Infrastructure.Core.Tools;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pet.User.Web.Application.User.Commands
{
    /// <summary>
    /// 功能描述    ：用户事件
    /// 创 建 者    ：Administrator
    /// 创建日期    ：2020/12/25 13:17:23 
    /// 最后修改者  ：Administrator
    /// 最后修改日期：2020/12/25 13:17:23 
    /// </summary>
    public class UserCommandHandler :
        IRequestHandler<WechatUserCommand, UserAuthDto>,
        IRequestHandler<AddSuggestCommand, Unit>,
         IRequestHandler<GetUserInfoCommand, UserInfoDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserRepository<User_Suggest> _suggestRepository;
        private readonly IMapper _mapper;
        private readonly IDapperQuery _dapper;

        public UserCommandHandler(UserManager<ApplicationUser> userManager, IUserRepository<User_Suggest> suggestRepository, IMapper mapper, IDapperQuery dapper)
        {
            _userManager = userManager;
            _suggestRepository = suggestRepository;
            _mapper = mapper;
            _dapper = dapper;
        }
        /// <summary>
        /// 授权
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<UserAuthDto> Handle(WechatUserCommand request, CancellationToken cancellationToken)
        {
            ///根据手机号判断用户是否存在
            var userInfo = await _userManager.FindByNameAsync(request.UserPhone);
            ///如果用户存在
            if (userInfo != null)
            {
                ///返回用户名，加密密码
                return new UserAuthDto()
                {
                    UserName = userInfo.UserName,
                    Password = RSA2Helper.Encrypt("wechatlogin", Encoding.Default, RSAConfig.PublicKey, RSAConfig.PrivateKey)
                };
            }
            else
            {
                ///创建用户
                var user = new ApplicationUser()
                {
                    NickName = request.NickName,
                    Avatar = request.Avatar,
                    UserName = request.UserPhone,
                    PhoneNumber = request.UserPhone,
                };
                await _userManager.CreateAsync(user, request.UserPhone);
                ///返回用户名，加密密码
                return new UserAuthDto()
                {
                    UserName = request.UserPhone,
                    Password = RSA2Helper.Encrypt("wechatlogin", Encoding.Default, RSAConfig.PublicKey, RSAConfig.PrivateKey)
                };
            }
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<UserInfoDto> Handle(GetUserInfoCommand request, CancellationToken cancellationToken)
        {
            const string sql = "SELECT a.NickName,a.Avatar FROM AspNetUsers AS a WHERE Id=@UserId";
            var data = await _dapper.QueryFirstAsync<UserInfoDto>(sql, new { request.UserId });
            return data;
        }
        #region 意见反馈
        /// <summary>
        /// 添加意见
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Unit> Handle(AddSuggestCommand request, CancellationToken cancellationToken)
        {
            var data = _mapper.Map<User_Suggest>(request);
            await _suggestRepository.InsertAsync(data);
            return new Unit();
        }


        #endregion

    }
}
