using MediatR;

namespace Silkflo.API.Services.ClientApplicationInterfaceSession.Command
{
    public class CreateClientApplicationInterfaceSessionCommand : IRequest<CreateClientApplicationInterfaceSessionCommandResult>
    {
        public string ClientId { get; set; }
        public bool IsActive { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SessionKey { get; set; }

        internal (bool, string) Validate()
        {
            var message = "";

            if (String.IsNullOrEmpty(ClientId))
                message += "ClientId is empty or Invalid";
            if (String.IsNullOrEmpty(Name))
                message += "Name is empty or Invalid";            
            if (String.IsNullOrEmpty(SessionKey))
                message += "SessionKey is empty or Invalid";            
            if (ExpirationDate == DateTime.MinValue)
                message += "ExpirationDate is empty or Invalid";

            return String.IsNullOrEmpty(message) ? (false, "") : (true, message);
        }

    }
}
