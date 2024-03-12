using MediatR;
using Silkflo.API.Services.ClientApplicationInterfaceSession.Command;
using Silkflo.API.Utilities;
using Silkflo.Persistence.Abstractions;
using SilkFlo.Data.Core.Domain;

namespace Silkflo.API.Services.ClientApplicationInterfaceSession.CommandHandler
{
    public class CreateClientApplicationInterfaceSessionCommandHandler : IRequestHandler<CreateClientApplicationInterfaceSessionCommand, CreateClientApplicationInterfaceSessionCommandResult>
    {
        private readonly IDatabaseConnection _databaseConnection;
        private readonly KeyGeneratorService _keyGeneratorService;
        public CreateClientApplicationInterfaceSessionCommandHandler(IDatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
            _keyGeneratorService = new KeyGeneratorService();
        }

        public async Task<CreateClientApplicationInterfaceSessionCommandResult> Handle(CreateClientApplicationInterfaceSessionCommand command, CancellationToken cancellationToken)
        {
            try
            {
                SilkFlo.Data.Core.Domain.ClientApplicationInterfaceSession clientApplicationInterfaceSession = null;

				command.SessionKey = _keyGeneratorService.GenerateSecretKey();

                var validateCommand = command.Validate();
                if (validateCommand.Item1)
                    throw new ArgumentException(validateCommand.Item2);

                using (var db = await _databaseConnection.GetDbConnectionAsync())
                {
                    clientApplicationInterfaceSession = new SilkFlo.Data.Core.Domain.ClientApplicationInterfaceSession()
                    {
                        Name = command.Name,
                        Description = command.Description,
                        SessionKey = command.SessionKey,
                        ExpirationDate = command.ExpirationDate,
                        IsActive = command.IsActive,
                        ClientId = command.ClientId,
                    };

                    await db.InsertAsync(clientApplicationInterfaceSession);
                }

                return new CreateClientApplicationInterfaceSessionCommandResult()
                {
					IsSucceed = true,
                    Result = clientApplicationInterfaceSession.SessionKey
				};
            }
            catch (Exception ex)
            {
				return new CreateClientApplicationInterfaceSessionCommandResult()
				{
					IsSucceed = false
				};
			}
        }
    }
}
