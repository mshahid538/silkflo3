using PetaPoco;

namespace Silkflo.Persistence.Abstractions
{
    public interface IDatabaseConnection
    {
        Task<IDatabase> GetDbConnectionAsync();
    }
}
