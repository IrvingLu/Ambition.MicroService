using MediatR;
using Pet.Web.Application.Pet.Queries.Dto;
using System.Collections.Generic;

namespace Pet.Web.Application.Pet.Queries.Command
{
    public class GetPetsCommand:IRequest<IEnumerable<PetsDto>>
    {
        public string UserId { get; set; }


        public GetPetsCommand(string userid)
        {
            this.UserId = userid;
        }
    }
}
