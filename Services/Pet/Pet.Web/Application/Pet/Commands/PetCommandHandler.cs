using AutoMapper;
using MediatR;
using Pet.Infrastructure.Repositories;
using Pet.Web.Application.Pet.Commands.Command;
using System.Threading;
using System.Threading.Tasks;

namespace Pet.Web.Application.Pet.Commands
{
    public class PetCommandHandler : IRequestHandler<InsertPetCommand,Unit>
    {
        private readonly IPetRepository<Domain.PetBase.Pet> _petRepository;
        private readonly IMapper _mapper;

        public PetCommandHandler(IPetRepository<Domain.PetBase.Pet> petRepository, IMapper mapper)
        {
            _petRepository = petRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// 新增宠物信息
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Unit> Handle(InsertPetCommand request, CancellationToken cancellationToken)
        {
            var data = _mapper.Map<Domain.PetBase.Pet>(request);
            await _petRepository.InsertAsync(data);
            return new Unit();
        }

    }
}
