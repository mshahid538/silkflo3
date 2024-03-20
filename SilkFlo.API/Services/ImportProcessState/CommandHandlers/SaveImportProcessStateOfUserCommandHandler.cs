using MediatR;
using PetaPoco;
using Silkflo.API.Services.ClientApplicationInterfaceSession.Command;
using Silkflo.API.Services.ImportProcessState.Commands;
using Silkflo.API.Utilities;
using Silkflo.Persistence.Abstractions;
using SilkFlo.Data.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silkflo.API.Services.ImportProcessState.CommandHandlers
{
	public class SaveImportProcessStateOfUserCommandHandler : IRequestHandler<SaveImportProcessStateOfUserCommand, SaveImportProcessStateOfUserCommandResult>
	{
		private readonly IDatabaseConnection _databaseConnection;
		private readonly KeyGeneratorService _keyGeneratorService;
		public SaveImportProcessStateOfUserCommandHandler(IDatabaseConnection databaseConnection)
		{
			_databaseConnection = databaseConnection;
			_keyGeneratorService = new KeyGeneratorService();
		}

		public async Task<SaveImportProcessStateOfUserCommandResult> Handle(SaveImportProcessStateOfUserCommand command, CancellationToken cancellationToken)
		{
			try
			{
				using (var db = await _databaseConnection.GetDbConnectionAsync())
				{
					var userState = await db.FirstOrDefaultAsync<ImportProcessState>("WHERE ClientId = @0 AND UserId = @1", command.ClientId, command.UserId);
					if(userState != null)
					{
						await db.ExecuteAsync($"UPDATE ImportProcessStates SET SuccessCount = @0, FailedCount = @1, Status = @2 WHERE ClientId = @3 AND UserId = @4", 
											command.SuccessCount, command.FailedCount, command.State, command.ClientId, command.UserId);

						return new SaveImportProcessStateOfUserCommandResult()
						{
							IsSucceed = true,
						};
					}

					var newState = new ImportProcessState()
					{
						ClientId = command.ClientId,
						UserId = command.UserId,
						FailedCount = command.FailedCount,
						Status = command.State,
						SuccessCount = command.SuccessCount,
						Time = command.Time
					};

					await db.InsertAsync(newState);
				}

				return new SaveImportProcessStateOfUserCommandResult()
				{
					IsSucceed = true,
				};
			}
			catch (Exception ex)
			{
				return new SaveImportProcessStateOfUserCommandResult()
				{
					IsSucceed = false
				};
			}
		}

		[TableName("ImportProcessStates")]
		public class ImportProcessState
		{
			public string UserId { get; set; }
			public string ClientId { get; set; }
			public string Status { get; set; }
			public DateTime Time { get; set; }
			public int SuccessCount { get; set; }
			public int FailedCount { get; set; }
		}
	}
}
