using MediatR;
using Silkflo.API.Services.AccessKey.Queries;
using Silkflo.API.Services.Ideas.Queries;
using Silkflo.Persistence.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silkflo.API.Services.AccessKey.QueryHandlers
{
	public class GetAllClientsAccessKeyQueryHandler : IRequestHandler<GetAllClientsAccessKeyQuery, GetAllClientsAccessKeyQueryResult>
	{
		private readonly IDatabaseConnection _databaseConnection;
		public GetAllClientsAccessKeyQueryHandler(IDatabaseConnection databaseConnection)
		{
			_databaseConnection = databaseConnection;
		}

		public async Task<GetAllClientsAccessKeyQueryResult> Handle(GetAllClientsAccessKeyQuery query, CancellationToken cancellationToken)
		{
			var queryResult = new GetAllClientsAccessKeyQueryResult();

			using (var db = await _databaseConnection.GetDbConnectionAsync())
			{
				queryResult.Result = await db.FetchAsync<SilkFlo.Data.Core.Domain.ClientApplicationInterfaceSession>("WHERE ClientId = @0", query.ClientId);
			}

			queryResult.Result?.ForEach(result => 
			{
				result.SessionKey = $"*********{result.SessionKey.Substring(result.SessionKey.Length - 3)}";
				result.ExpirationDateString = result.ExpirationDate.ToString("dd/MM/yyyy h:mm tt");

            });

			queryResult.IsSucceed = true;
			return queryResult;
		}
	}
}
