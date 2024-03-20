using MediatR;
using PetaPoco;
using Silkflo.API.Services.Ideas.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silkflo.API.Services.ImportProcessState.Queries
{
	public class GetImportProcessStateOfUserQuery : IRequest<GetImportProcessStateOfUserQueryResult>
	{
		public string UserId { get; set; }
		public string ClientId { get; set; }
	}

	public class GetImportProcessStateOfUserQueryResult
	{
		public bool IsSucceed { get; set; }
		public State Result { get; set; }
	}

	[TableName("ImportProcessStates")]
	public class State
	{
		public int SuccessCount { get; set; }
		public int FailedCount { get; set; }
		public string Status { get; set; }
	}
}
