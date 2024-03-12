using MediatR;
using Silkflo.API.Services.ClientApplicationInterfaceSession.Command;
using Silkflo.API.Services.Ideas.Queries;
using Silkflo.API.Utilities;
using Silkflo.Persistence.Abstractions;
using SilkFlo.Data.Core.Domain.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silkflo.API.Services.Ideas.QueryHandlers
{
    public class GetAllClientIdeasQueryHandler : IRequestHandler<GetAllClientIdeasQuery, GetAllClientIdeasQueryResult>
    {
        private readonly IDatabaseConnection _databaseConnection;
        public GetAllClientIdeasQueryHandler(IDatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public async Task<GetAllClientIdeasQueryResult> Handle(GetAllClientIdeasQuery query, CancellationToken cancellationToken)
        {
            var queryResult = new GetAllClientIdeasQueryResult();

            using (var db = await _databaseConnection.GetDbConnectionAsync())
            {
                var session = await db.FirstOrDefaultAsync<SilkFlo.Data.Core.Domain.ClientApplicationInterfaceSession>("WHERE Lower(SessionKey) = @0", query.SecretToken.ToLower());

                if (session is null)
                    throw new ArgumentException("Session not found");

                if(session is not null && !session.IsActive)
                    throw new ArgumentException("Session is not Active");

                if (session is not null && session.ExpirationDate < DateTime.Now)
                    throw new ArgumentException("Session is not Active");

                queryResult.Result = await db.FetchAsync<IdeaResult>("WHERE ClientId = @0", session.ClientId);
            }

            queryResult.IsSucceed = true;
            return queryResult;
        }
    }
}
