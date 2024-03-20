using MediatR;
using Silkflo.API.Services.Ideas.Queries;
using Silkflo.API.Services.ImportProcessState.Queries;
using Silkflo.Persistence.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silkflo.API.Services.ImportProcessState.QueryHandler
{
	public class GetImportProcessStateOfUserQueryHandler : IRequestHandler<GetImportProcessStateOfUserQuery, GetImportProcessStateOfUserQueryResult>
	{
		private readonly IDatabaseConnection _databaseConnection;
		public GetImportProcessStateOfUserQueryHandler(IDatabaseConnection databaseConnection)
		{
			_databaseConnection = databaseConnection;
		}

		public async Task<GetImportProcessStateOfUserQueryResult> Handle(GetImportProcessStateOfUserQuery query, CancellationToken cancellationToken)
		{
			var queryResult = new GetImportProcessStateOfUserQueryResult();

			using (var db = await _databaseConnection.GetDbConnectionAsync())
			{
				var result = await db.FirstOrDefaultAsync<State>("WHERE ClientId = @0 AND UserId = @1", query.ClientId, query.UserId);
				queryResult.Result = result;
			}

			queryResult.IsSucceed = true;
			return queryResult;
		}
	}
}
