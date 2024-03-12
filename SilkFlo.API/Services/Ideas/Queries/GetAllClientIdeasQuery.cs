using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silkflo.API.Services.Ideas.Queries
{
    public class GetAllClientIdeasQuery : IRequest<GetAllClientIdeasQueryResult>
    {
        public string SecretToken { get; set; }
    }
}
