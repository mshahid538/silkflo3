using SilkFlo.Data.Core.Domain.Business;

namespace SilkFlo.Data.Core.Repositories.Business
{
  public interface IApplicationRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Domain.Business.Application> GetAsync(string id);

    Task<Domain.Business.Application> SingleOrDefaultAsync(Func<Domain.Business.Application, bool> predicate);

    Task<bool> AddAsync(Domain.Business.Application entity);

    Task<bool> AddRangeAsync(IEnumerable<Domain.Business.Application> entities);

    Task<IEnumerable<Domain.Business.Application>> GetAllAsync();

    Task<IEnumerable<Domain.Business.Application>> FindAsync(Func<Domain.Business.Application, bool> predicate);

    Task<Domain.Business.Application> GetUsingNameAsync(string name);

    Task GetForClientAsync(Client client);

    Task GetForClientAsync(IEnumerable<Client> clients);

    Task GetApplicationForAsync(SilkFlo.Data.Core.Domain.Business.Version version);

    Task GetApplicationForAsync(IEnumerable<SilkFlo.Data.Core.Domain.Business.Version> versions);

    Task<Domain.Business.Application> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Domain.Business.Application entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Domain.Business.Application> entities);
  }
}
