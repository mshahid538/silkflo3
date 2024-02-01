// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Core.Repositories.Business.IRoleCostRepository
// Assembly: SilkFlo.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36C13AAB-6F0A-4973-BB89-665E3C9E4420
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.Core.dll

using SilkFlo.Data.Core.Domain.Business;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Data.Core.Repositories.Business
{
  public interface IRoleCostRepository
  {
    bool IncludeDeleted { get; set; }

    Task<RoleCost> GetAsync(string id);

    Task<RoleCost> SingleOrDefaultAsync(Func<RoleCost, bool> predicate);

    Task<bool> AddAsync(RoleCost entity);

    Task<bool> AddRangeAsync(IEnumerable<RoleCost> entities);

    Task<IEnumerable<RoleCost>> GetAllAsync();

    Task<IEnumerable<RoleCost>> FindAsync(Func<RoleCost, bool> predicate);

    Task GetForClientAsync(Client client);

    Task GetForClientAsync(IEnumerable<Client> clients);

    Task GetForRoleAsync(Role role);

    Task GetForRoleAsync(IEnumerable<Role> roles);

    Task<DataStoreResult> RemoveAsync(string id);

    Task<DataStoreResult> RemoveAsync(RoleCost entity);

    Task<DataStoreResult> RemoveRangeAsync(IEnumerable<RoleCost> entities);
  }
}
