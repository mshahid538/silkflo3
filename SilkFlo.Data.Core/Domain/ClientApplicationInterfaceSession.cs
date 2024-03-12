using PetaPoco;

namespace SilkFlo.Data.Core.Domain
{
    [TableName("ClientApplicationInterfaceSessions")]
    public class ClientApplicationInterfaceSession
    {
        public string ClientId { get; set; }
        public bool IsActive { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SessionKey { get; set; }

        [ResultColumn]
        public string ExpirationDateString { get; set; }
    }
}
