using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silkflo.API.Services.AccessKey.Queries
{
	public class GetAllClientsAccessKeyQueryResult
	{
		public bool IsSucceed { get; set; }
		public string Message { get; set; }
		public List<SilkFlo.Data.Core.Domain.ClientApplicationInterfaceSession> Result { get; set; }
	}
}
