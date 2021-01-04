using AutoMapper;
using MediatR;
using Pet.User.Domain.Tenant;
using Pet.User.Infrastructure.Repositories;
using Pet.User.Web.Application.Tenant.Commands.Command;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pet.User.Web.Application.Tenant.Commands
{
    public class TenantCommandHandler : IRequestHandler<CreateServiceCategoryCommand, Unit>, IRequestHandler<UpdateTenantInfoCommand, Unit>
    {
        private readonly IUserRepository<Tenant_ServiceCategory> _serviceCategoryRepository;
        private readonly IUserRepository<Domain.Tenant.Tenant> _tenantRepository;
        private readonly IMapper _mapper;
        #region ctor
        public TenantCommandHandler(IUserRepository<Tenant_ServiceCategory> serviceCategoryRepository,
            IUserRepository<Domain.Tenant.Tenant> tenantRepository,
            IMapper mapper)
        {
            _serviceCategoryRepository = serviceCategoryRepository;
            _tenantRepository = tenantRepository;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        /// <summary>
        /// 创建租户的服务项目
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Unit> Handle(CreateServiceCategoryCommand request, CancellationToken cancellationToken)
        {
            var data = _mapper.Map<Tenant_ServiceCategory>(request);
            await _serviceCategoryRepository.InsertAsync(data);
            return new Unit();
        }
        /// <summary>
        /// 更新租户信息
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Unit> Handle(UpdateTenantInfoCommand request, CancellationToken cancellationToken)
        {
            var data = await _tenantRepository.GetByIdAsync(request.Id);
            data.UpdateTime = DateTime.Now;
            data.LogoPath = request.LogoPath;
            data.Name = request.Name;
            data.Announcement = request.Announcement;
            await _tenantRepository.UpdateAsync(data);
            return new Unit();
        }
        #endregion

    }
}
