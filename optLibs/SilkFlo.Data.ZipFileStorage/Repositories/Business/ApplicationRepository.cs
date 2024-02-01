// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.Repositories.Business.ApplicationRepository
// Assembly: SilkFlo.Data.ZipFileStorage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BC78CDC2-416F-4981-AF6D-F81E52FD30E4
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.ZipFileStorage.dll

using SilkFlo.Data.Core;
using SilkFlo.Data.Core.Domain.Business;
using SilkFlo.Data.Core.Repositories.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


#nullable enable
namespace SilkFlo.Data.Persistence.Repositories.Business
{
  public class ApplicationRepository : IApplicationRepository
  {
    private readonly 
    #nullable disable
    UnitOfWork _unitOfWork;

    public ApplicationRepository(UnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

    public bool IncludeDeleted { get; set; }

    public Application Get(string id) => this.GetAsync(id).Result;

    public async Task<Application> GetAsync(string id)
    {
      if (id == null)
        return (Application) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessApplications.SingleOrDefault<Application>((Func<Application, bool>) (x => x.Id == id));
    }

    public Application SingleOrDefault(Func<Application, bool> predicate) => this.SingleOrDefaultAsync(predicate).Result;

    public async Task<Application> SingleOrDefaultAsync(Func<Application, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessApplications.Where<Application>(predicate).FirstOrDefault<Application>();
    }

    public bool Add(Application entity) => this.AddAsync(entity).Result;

    public async Task<bool> AddAsync(Application entity)
    {
      if (entity == null)
        return false;
      await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public bool AddRange(IEnumerable<Application> entities) => this.AddRangeAsync(entities).Result;

    public async Task<bool> AddRangeAsync(IEnumerable<Application> entities)
    {
      if (entities == null)
        return false;
      foreach (Application entity in entities)
        await this._unitOfWork.AddAsync(entity);
      return true;
    }

    public IEnumerable<Application> GetAll() => this.GetAllAsync().Result;

    public async Task<IEnumerable<Application>> GetAllAsync()
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<Application>) dataSetAsync.BusinessApplications.OrderBy<Application, string>((Func<Application, string>) (m => m.Name));
    }

    public IEnumerable<Application> Find(Func<Application, bool> predicate) => this.FindAsync(predicate).Result;

    public async Task<IEnumerable<Application>> FindAsync(Func<Application, bool> predicate)
    {
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return (IEnumerable<Application>) dataSetAsync.BusinessApplications.Where<Application>(predicate).OrderBy<Application, string>((Func<Application, string>) (m => m.Name));
    }

    public Application GetUsingName(string name) => this.GetUsingNameAsync(name).Result;

    public async Task<Application> GetUsingNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (Application) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessApplications.SingleOrDefault<Application>((Func<Application, bool>) (x => x.Name == name));
    }

    public void GetForClient(Client client) => this.GetForClientAsync(client).RunSynchronously();

    public async Task GetForClientAsync(Client client)
    {
      List<Application> lst;
      if (client == null)
        lst = (List<Application>) null;
      else if (string.IsNullOrWhiteSpace(client.Id))
      {
        lst = (List<Application>) null;
      }
      else
      {
        DataSet dataSet = await UnitOfWork.GetDataSetAsync();
        lst = dataSet.BusinessApplications.Where<Application>((Func<Application, bool>) (x => x.ClientId == client.Id)).OrderBy<Application, string>((Func<Application, string>) (x => x.Name)).ToList<Application>();
        dataSet = (DataSet) null;
        foreach (Application item in lst)
        {
          item.ClientId = client.Id;
          item.Client = client;
        }
        client.Applications = lst;
        lst = (List<Application>) null;
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
        version1.Application = dataSet.BusinessApplications.SingleOrDefault<Application>((Func<Application, bool>) (x => x.Id == version.ApplicationId));
        version1 = (SilkFlo.Data.Core.Domain.Business.Version) null;
        dataSet = (DataSet) null;
      }
    }

    public Application GetByName(string name) => this.GetByNameAsync(name).Result;

    public async Task<Application> GetByNameAsync(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (Application) null;
      DataSet dataSetAsync = await UnitOfWork.GetDataSetAsync();
      return dataSetAsync.BusinessApplications.SingleOrDefault<Application>((Func<Application, bool>) (x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)));
    }

    public DataStoreResult Remove(string id) => this.RemoveAsync(id).Result;

    public async Task<DataStoreResult> RemoveAsync(string id)
    {
      if (id == null)
        return DataStoreResult.Success;
      Application entity = await this.GetAsync(id);
      if (entity == null)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this.RemoveAsync(entity);
      return dataStoreResult;
    }

    public DataStoreResult Remove(Application entity) => this.RemoveAsync(entity).Result;

    public async Task<DataStoreResult> RemoveAsync(Application entity)
    {
      if (entity == null || entity.IsNew)
        return DataStoreResult.Success;
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entity, true);
      return dataStoreResult;
    }

    public DataStoreResult RemoveRange(IEnumerable<Application> entities) => this.RemoveRangeAsync(entities).Result;

    public async Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Application> entities)
    {
      if (entities == null)
        throw new DuplicateException("The applications are present");
      DataStoreResult dataStoreResult = await this._unitOfWork.DeleteAsync(entities, true);
      return dataStoreResult;
    }
  }
}
