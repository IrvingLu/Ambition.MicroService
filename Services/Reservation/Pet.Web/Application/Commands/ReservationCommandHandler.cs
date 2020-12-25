using AutoMapper;
using MediatR;
using Pet.Reservation.Infrastructure.Repositories;
using Pet.Reservation.Web.Application.Commands.Command;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pet.Reservation.Web.Application.Commands
{
    public class ReservationCommandHandler : IRequestHandler<CreateReservationCommand>
    {
        private readonly IMapper _mapper;
        private readonly IReservationRepository<Domain.Reservation.Reservation> _resRepository;

        public ReservationCommandHandler(IMapper mapper, IReservationRepository<Domain.Reservation.Reservation> resRepository)
        {
            _mapper = mapper;
            _resRepository = resRepository;
        }
        /// <summary>
        /// 创建预约
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Unit> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            var data = _mapper.Map<Domain.Reservation.Reservation>(request);
            await _resRepository.InsertAsync(data);
            return new Unit();
        }
    }
}
