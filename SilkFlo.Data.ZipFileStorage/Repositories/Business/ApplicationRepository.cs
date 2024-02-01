using SilkFlo.Data.Core;
using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Repositories.Business;

namespace SilkFlo.Data.Persistence.Repositories.Business
{
  public class ApplicationRepository : IApplicationRepository
  {
    private readonly UnitOfWork _unitOfWork;

    public ApplicationRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public Core.Domain.Business.Application Get(string id) => this.GetAsync(id).Result;

    public async Task<Core.Domain.Business.Application> GetAsync(string id)
    {
      if (id == null)
        return (Core.Domain.Business.Application) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessApplications.SingleOrDefault<Core.Domain.Business.Application>((Func<Core.Domain.Business.Application, bool>) (x => x.Id == id));
    }

    public Core.Domain.Business.Application SingleOrDefault(Func<Core.Domain.Business.Application, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<Core.Domain.Business.Application> SingleOrDefaultAsync(Func<Core.Domain.Business.Application, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessApplications.Where<Core.Domain.Business.Application>(predicate).FirstOrDefault<Core.Domain.Business.Application>();
    }

    public bool Add(Core.Domain.Business.Application entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(Core.Domain.Business.Application entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<Core.Domain.Business.Application> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<Core.Domain.Business.Application> entities)
    {
      if (entities == null)
        return false;
      foreach (Core.Domain.Business.Application entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<Core.Domain.Business.Application> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<Core.Domain.Business.Application>> GetAllAsync()
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<Core.Domain.Business.Application>) (await dataSetAsync.BusinessApplications.ToListAsync<Core.Domain.Business.Application>()).OrderBy<Core.Domain.Business.Application, string>((Func<Core.Domain.Business.Application, string>) (m => m.Name));
    }

    public IEnumerable<Core.Domain.Business.Application> Find(Func<Core.Domain.Business.Application, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<Core.Domain.Business.Application>> FindAsync(Func<Core.Domain.Business.Application, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<Core.Domain.Business.Application>) dataSetAsync.BusinessApplications.Where<Core.Domain.Business.Application>(predicate).OrderBy<Core.Domain.Business.Application, string>((Func<Core.Domain.Business.Application, string>) (m => m.Name));
    }

    public Core.Domain.Business.Application GetUsingName(string name) => this.GetUsingNameAsync(name).Result;

    public async Task<Core.Domain.Business.Application> GetUsingNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (Core.Domain.Business.Application) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessApplications.SingleOrDefault<Core.Domain.Business.Application>((Func<Core.Domain.Business.Application, bool>) (x => x.Name == name));
    }

    public void GetForClient(Client client) => this.GetForClientAsync(client).RunSynchronously();

    public async Task GetForClientAsync(Client client)
    {
      List<Core.Domain.Business.Application> lst;
      if (client == null)
        lst = (List<Core.Domain.Business.Application>) null;
      else if (string.IsNullOrWhiteSpace(client.Id))
      {
        lst = (List<Core.Domain.Business.Application>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessApplications.Where<Core.Domain.Business.Application>((Func<Core.Domain.Business.Application, bool>) (x => x.ClientId == client.Id)).OrderBy<Core.Domain.Business.Application, string>((Func<Core.Domain.Business.Application, string>) (x => x.Name)).ToList<Core.Domain.Business.Application>();
        dataSet = (DataSet) null;
        foreach (Core.Domain.Business.Application item in lst)
        {
          item.ClientId = client.Id;
          item.Client = client;
        }
        client.Applications = lst;
        lst = (List<Core.Domain.Business.Application>) null;
      }
    }

    public void GetForClient(IEnumerable<Client> clients) => this.GetForClientAsync(clients).RunSynchronously();

    public async Task GetForClientAsync(IEnumerable<Client> clients)
    {
      if (clients == null)
        return;
      foreach (Client client in clients)
        await this.GetForClientAsync(client);
    }

    public void GetApplicationFor(IEnumerable<SilkFlo.Data.Core.Domain.Business.Version> versions) => this.GetApplicationForAsync(versions).RunSynchronously();

    public async Task GetApplicationForAsync(IEnumerable<SilkFlo.Data.Core.Domain.Business.Version> versions)
    {
      if (versions == null)
        return;
      foreach (SilkFlo.Data.Core.Domain.Business.Version version in versions)
        await this.GetApplicationForAsync(version);
    }

    public void GetApplicationFor(SilkFlo.Data.Core.Domain.Business.Version version) => this.GetApplicationForAsync(version).RunSynchronously();

    public async Task GetApplicationForAsync(SilkFlo.Data.Core.Domain.Business.Version version)
    {
      if (version == null)
        ;
      else
      {
        SilkFlo.Data.Core.Domain.Business.Version version1 = version;
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        version1.Application = dataSet.BusinessApplications.SingleOrDefault<Core.Domain.Business.Application>((Func<Core.Domain.Business.Application, bool>) (x => x.Id == version.ApplicationId));
        version1 = (SilkFlo.Data.Core.Domain.Business.Version) null;
        dataSet = (DataSet) null;
      }
    }

    public Core.Domain.Business.Application GetByName(string name) => this.GetByNameAsync(name).Result;

    public async Task<Core.Domain.Business.Application> GetByNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (Core.Domain.Business.Application) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessApplications.SingleOrDefault<Core.Domain.Business.Application>((Func<Core.Domain.Business.Application, bool>) (x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
            Core.Domain.Business.Application entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(Core.Domain.Business.Application entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(Core.Domain.Business.Application entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<Core.Domain.Business.Application> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Core.Domain.Business.Application> entities)
    {
      if (entities == null)
        throw new DuplicateException("The applications are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
