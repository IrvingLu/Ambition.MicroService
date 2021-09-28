/************************************************************************
*本页作者    ：鲁岩奇
*创建日期    ：2020/11/10 9:51:36 
*功能描述    ：命令
*使用说明    ：命令
***********************************************************************/

using AutoMapper;
using DotNetCore.CAP;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NMS.Patient.Infrastructure.Repositories;
using NMS.Patient.Service.Patient.Command;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NMS.Patient.Service.Patient
{
    public class PatientCommandHandler : IRequestHandler<CreatePatientCommand, Unit>, IRequestHandler<UpdatePatientCommand, Unit>, IRequestHandler<DeletePatientCommand, Unit>
    {
        private readonly IPatientRepository<Domain.Patient.Patient> _patientRepository;
        private readonly IMapper _mapper;
        private readonly ICapPublisher _capBus;

        public PatientCommandHandler(IPatientRepository<Domain.Patient.Patient> patientRepository, IMapper mapper, ICapPublisher capBus)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
            _capBus = capBus;
        }
        /// <summary>
        /// 添加患者信息
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Unit> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            var data = new Domain.Patient.Patient(request.Name);
            await _capBus.PublishAsync("user.services.show.time", DateTime.Now);
            await _patientRepository.AddAsync(data);
            await _patientRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return new Unit();
        }
        /// <summary>
        /// 更新患者信息
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Unit> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            var result = _mapper.Map<Domain.Patient.Patient>(request);
            await _patientRepository.UpdateAsync(result);
            await _patientRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return new Unit();
        }
        /// <summary>
        /// 删除患者信息
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Unit> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
        {
            var data= await _patientRepository.FindByIdAsync(request.Id);
            await _patientRepository.RemoveAsync(data);
            await _patientRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return new Unit();
        }
    }
}
