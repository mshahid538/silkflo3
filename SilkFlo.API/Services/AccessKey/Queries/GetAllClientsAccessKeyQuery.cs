using MediatR;
using Silkflo.API.Services.Ideas.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silkflo.API.Services.AccessKey.Queries
{
	public class GetAllClientsAccessKeyQuery : IRequest<GetAllClientsAccessKeyQueryResult>
	{
		public string ClientId {  get; set; }
	}
}
