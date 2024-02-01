// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.Business.IApplicationRepository
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using SilkFlo.Data.Core.Domain.Business;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Business
{
  public interface IApplicationRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Application> GetAsync(string id);

    Task<Application> SingleOrDefaultAsync(Func<Application, bool> predicate);

    Task<bool> AddAsync(Application entity);

    Task<bool> AddRangeAsync(IEnumerable<Application> entities);

    Task<IEnumerable<Application>> GetAllAsync();

    Task<IEnumerable<Application>> FindAsync(Func<Application, bool> predicate);

    Task<Application> GetUsingNameAsync(string name);

    Task GetForClientAsync(Client client);

    Task GetForClientAsync(IEnumerable<Client> clients);

    Task GetApplicationForAsync(SilkFlo.Data.Core.Domain.Business.Version version);

    Task GetApplicationForAsync(IEnumerable<SilkFlo.Data.Core.Domain.Business.Version> versions);

    Task<Application> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Application entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Application> entities);
  }
}
