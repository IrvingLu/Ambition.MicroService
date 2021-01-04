using MediatR;
using Pet.Web.Application.Pet.Queries.Dto;
using Shared.Infrastructure.Core.BaseDto;

namespace Pet.Web.Application.Pet.Queries.Command
{
    public class GetPetInfoCommand:EntityDto,IRequest<PetInfoDto>
    {
    }
}
