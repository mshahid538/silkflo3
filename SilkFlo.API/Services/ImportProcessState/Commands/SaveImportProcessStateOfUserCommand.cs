using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silkflo.API.Services.ImportProcessState.Commands
{
	public class SaveImportProcessStateOfUserCommand : IRequest<SaveImportProcessStateOfUserCommandResult>
	{
		public string UserId { get; set; }
		public string ClientId { get; set; }
		public string State { get; set; }
		public DateTime Time { get; set; }
		public int SuccessCount { get; set; }
		public int FailedCount { get; set; }
	}

	public class SaveImportProcessStateOfUserCommandResult
	{
		public bool IsSucceed { get; set; }
	}
}
