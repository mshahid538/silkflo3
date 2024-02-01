// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.Business.ILocationRepository
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using SilkFlo.Data.Core.Domain;
using SilkFlo.Data.Core.Domain.Business;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Business
{
  public interface ILocationRepository
  {
    bool IncludeDeleted { get; set; }

    Task<Location> GetAsync(string id);

    Task<Location> SingleOrDefaultAsync(Func<Location, bool> predicate);

    Task<bool> AddAsync(Location entity);

    Task<bool> AddRangeAsync(IEnumerable<Location> entities);

    Task<IEnumerable<Location>> GetAllAsync();

    Task<IEnumerable<Location>> FindAsync(Func<Location, bool> predicate);

    Task<Location> GetUsingNameAsync(string name);

    Task GetForClientAsync(Client client);

    Task GetForClientAsync(IEnumerable<Client> clients);

    Task GetLocationForAsync(User user);

    Task GetLocationForAsync(IEnumerable<User> users);

    Task<Location> GetByNameAsync(string name);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(Location entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<Location> entities);
  }
}
