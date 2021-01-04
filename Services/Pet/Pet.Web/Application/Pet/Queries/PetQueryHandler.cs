using MediatR;
using Microsoft.Extensions.Configuration;
using Pet.Web.Application.Pet.Queries.Command;
using Pet.Web.Application.Pet.Queries.Dto;
using Shared.Infrastructure.Core.Dapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pet.Web.Application.Pet.Queries
{
    public class PetQueryHandler : IRequestHandler<GetPetsCommand, IEnumerable<PetsDto>>,
                                   IRequestHandler<GetPetInfoCommand, PetInfoDto>
    {
        private readonly IConfiguration _configuration;
        private readonly IDapperQuery _dapper;

        public PetQueryHandler(IConfiguration configuration, IDapperQuery dapper)
        {
            _configuration = configuration;
            _dapper = dapper;
        }
        /// <summary>
        /// 用户的宠物列表
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<PetsDto>> Handle(GetPetsCommand request, CancellationToken cancellationToken)
        {
            const string Sql = "SELECT * FROM Pet WHERE ApplicationUserId = @UserId";
            var data = await _dapper.QueryAsync<PetsDto>(Sql, new { request.UserId });
            return data;
        }
        /// <summary>
        /// 宠物信息详情
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<PetInfoDto> Handle(GetPetInfoCommand request, CancellationToken cancellationToken)
        {
            const string Sql = "SELECT * FROM Pet WHERE Id=@Id";
            var data = await _dapper.QueryFirstAsync<PetInfoDto>(Sql, new { request.Id });
            return data;
        }
    }
}
