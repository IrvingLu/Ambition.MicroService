using AutoMapper;
using MediatR;
using Pet.User.Infrastructure.Repositories;
using Pet.User.Web.Application.Queries.Command.Tenant;
using Pet.User.Web.Application.Tenant.Queries.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace Pet.User.Web.Application.Queries.Dto
{
    public class TenantQueryHandler : IRequestHandler<GetTenantInfoCommand, TenantInfoDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository<Domain.Tenant.Tenant> _tenantRepository;

        #region Ctor
        public TenantQueryHandler(IMapper mapper, IUserRepository<Domain.Tenant.Tenant> tenantRepository)
        {
            _mapper = mapper;
            _tenantRepository = tenantRepository;
        }
        #endregion

        #region methods
        /// <summary>
        /// 获取租户信息
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<TenantInfoDto> Handle(GetTenantInfoCommand request, CancellationToken cancellationToken)
        {
            var data = await _tenantRepository.GetByIdAsync(request.Id);
            var result = _mapper.Map<TenantInfoDto>(data);
            return result;
        }
        #endregion

    }
}
